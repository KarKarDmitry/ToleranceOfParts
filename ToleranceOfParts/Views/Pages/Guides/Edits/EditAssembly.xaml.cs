using OPP.AppData.Guides;
using OPP.ViewModels.Guides.Edits;
using OPP.ViewClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;
using Assembly = OPP.AppData.Guides.Assembly;
using OPP.Enviroment;
using OPP.Navigation;
using System.Windows.Controls.Primitives;

namespace ToleranceOfParts.Views.Pages.Guides.Edits
{
    /// <summary>
    /// Логика взаимодействия для EditAssembly.xaml
    /// </summary>
    public partial class EditAssembly : DisposablePage
    {
        private EditAssemblyViewModel viewModel;
        public EditAssembly(Assembly assembly)
        {
            InitializeComponent();
            viewModel = new EditAssemblyViewModel(assembly);
            DataContext = viewModel;

            viewModel.RequestActivateEditing += ActivateEditingForCurrentItem;

            AssemblyPlane.Source = Assembly.GetImageAsBitmap(viewModel.Assembly);
        }

        public override void DisposeResources()
        {
            viewModel = null;
        }

        public void ActivateEditingForCurrentItem(bool isPlanSize)
        {
            // Получить текущую редактируемую строку
            var dataGridRow = FindRowByItem(viewModel._currentEditingItem);
            if (dataGridRow == null) return;

            // Найти нужную ячейку
            var columnIndex = isPlanSize ? 0 : 1; // Индекс колонки: PlanSize (0) или AllowedSize (1)
            var cell = GetCell(dataGridRow, columnIndex);
            if (cell != null)
            {
                // Перевести ячейку в режим редактирования
                var dataGrid = FindParent<DataGrid>(cell);
                if (dataGrid != null)
                {
                    dataGrid.CurrentCell = new DataGridCellInfo(cell);
                    dataGrid.BeginEdit();

                    // Найти TextBox и установить на него фокус
                    var textBox = FindVisualChild<TextBox>(cell);
                    if (textBox != null)
                    {
                        textBox.Focus();
                        textBox.CaretIndex = textBox.Text.Length;
                    }

                }
            }
        }

        private DataGridRow FindRowByItem(object item)
        {
            if (item == null) return null;

            var dataGrid = this.FindName("AssemblyTolerances") as DataGrid;
            if (dataGrid == null) return null;

            return dataGrid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
        }

        private DataGridCell GetCell(DataGridRow row, int columnIndex)
        {
            if (row == null) return null;

            var presenter = FindVisualChild<DataGridCellsPresenter>(row);
            if (presenter == null) return null;

            return presenter.ItemContainerGenerator.ContainerFromIndex(columnIndex) as DataGridCell;
        }

        private void CellGrid_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            // Найти ячейку, на которую нажали
            var dataGridCell = FindParent<DataGridCell>(e.OriginalSource as DependencyObject);
            if (dataGridCell != null && !dataGridCell.IsEditing)
            {
                // Перевод ячейки в режим редактирования
                dataGridCell.Focus();
                var dataGrid = FindParent<DataGrid>(dataGridCell);
                if (dataGrid != null)
                {
                    dataGrid.BeginEdit(); // Переводим ячейку в режим редактирования
                }

                // Найти TextBox внутри ячейки
                var textBox = FindVisualChild<TextBox>(dataGridCell);
                if (textBox != null)
                {
                    textBox.Focus(); // Установить фокус на TextBox
                    textBox.SelectAll(); // Выделить весь текст для удобного редактирования
                }
            }
        }

        // Рекурсивный поиск родителя определенного типа
        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            if (child == null) return null;

            var parent = VisualTreeHelper.GetParent(child);
            if (parent is T parentAsT)
            {
                return parentAsT;
            }
            return FindParent<T>(parent);
        }

        // Рекурсивный поиск дочернего элемента определенного типа
        private T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            if (parent == null) return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T childAsT)
                {
                    return childAsT;
                }

                var childOfChild = FindVisualChild<T>(child);
                if (childOfChild != null)
                {
                    return childOfChild;
                }
            }
            return null;
        }

        private void EditCoatingType_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.Assembly.Coating != null)
            {
                NavigationManager.Navigate(new EditCoating(Coating.GetByID(viewModel.Assembly.Coating.Value)));
            }
        }

        private void AddCoatingType_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(new EditCoating(new Coating()));
        }

        private void DeleteCompositionRow_Click(object sender, RoutedEventArgs e)
        {
            viewModel.DeleteAssemblyComposition();
        }

        private void AddCompositionRow_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AddAssemblyComposition();
        }

        private void SaveCompositions_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SaveCompositions();
        }

        private void SaveAssembly_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SaveAssembly();
        }

        private void AssemblyComposition_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            if (dataGrid != null && dataGrid.SelectedCells.Count > 0)
            {
                // Получаем первую выделенную ячейку
                var selectedCell = dataGrid.SelectedCells[0];

                // Получаем объект строки, содержащий выбранную ячейку
                DataGridRow row = dataGrid.ItemContainerGenerator.ContainerFromItem(selectedCell.Item) as DataGridRow;

                if (row != null)
                {
                    // Получаем колонку с ячейкой
                    var column = selectedCell.Column;

                    // Программно включаем редактирование ячейки
                    dataGrid.CurrentCell = new DataGridCellInfo(selectedCell.Item, column);
                    dataGrid.BeginEdit();
                }
            }
        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox != null)
            {
                comboBox.IsDropDownOpen = true;
            }
        }

        private void AssemblyDetailCompositions_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            if (dataGrid != null && dataGrid.SelectedCells.Count > 0)
            {
                var selectedCell = dataGrid.SelectedCells[0];

                DataGridRow row = dataGrid.ItemContainerGenerator.ContainerFromItem(selectedCell.Item) as DataGridRow;

                if (row != null)
                {
                    var column = selectedCell.Column;

                    dataGrid.CurrentCell = new DataGridCellInfo(selectedCell.Item, column);
                    dataGrid.BeginEdit();
                }
            }
        }

        private void EditDetail_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedPartComposition != null)
            {
                NavigationManager.Navigate(new EditPart(Part.GetByID(viewModel.SelectedPartComposition.Part.Value)));
            }
        }

        private void AddDetail_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(new EditPart(new Part()));
        }

        private void AddAssemblies_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(new EditAssembly(new Assembly()));
        }

        private void EditAssemblies_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedAssemblyComposition.Subassembly != null)
            {
                NavigationManager.Navigate(new EditAssembly(Assembly.GetByID(viewModel.SelectedAssemblyComposition.Subassembly.Value)));
            }
        }

        private void AddPartCompositionRow_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AddPartCompositions();
        }

        private void DeletePartCompositionRow_Click(object sender, RoutedEventArgs e)
        {
            viewModel.DeletePartComposition();
        }

        private void AddToleranceRow_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AddTolerance();
        }

        private void DeleteToleranceRow_Click(object sender, RoutedEventArgs e)
        {
            viewModel.DeleteTolerance();
        }

        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
            var byteImage = ImageWorker.TakeImageOrCompasAsBytes();
            var byteMap = Assembly.SetImage(viewModel.Assembly, byteImage);
            if (byteMap != null) AssemblyPlane.Source = byteMap;
        }
    }
}
