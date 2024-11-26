using System.Windows;
using System.Windows.Controls;
using OPP.AppData.Guides;
using OPP.Navigation;
using OPP.ViewClasses;
using OPP.ViewModels.Guides.Edits;

namespace ToleranceOfParts.Views.Pages.Guides.Edits
{
    /// <summary>
    /// Логика взаимодействия для EditPackaging.xaml
    /// </summary>
    public partial class EditPackaging : DisposablePage
    {
        EditPackagingViewModel viewModel;
        public EditPackaging(Packaging packaging)
        {
            InitializeComponent();

            viewModel = new EditPackagingViewModel(packaging);
            DataContext = viewModel;
        }

        public override void DisposeResources()
        {
            viewModel = null;
        }

        private void EditBox_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.Packaging.Box == null) return;

            NavigationManager.Navigate(new EditBox(Box.GetByID(viewModel.Packaging.Box.Value)));
        }

        private void AddBox_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(new EditBox(new Box()));
        }

        private void SavePackaging_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SavePackaging();
        }

        private void AddLockCompositionRow_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AddLockCompositions();
        }

        private void DeleteLockCompositionRow_Click(object sender, RoutedEventArgs e)
        {
            viewModel.DeleteLockComposition();
        }

        private void EditLock_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedLockComposition.Lock == null) return;

            NavigationManager.Navigate(new EditLock(Lock.GetByID(viewModel.SelectedLockComposition.Lock.Value)));
        }

        private void AddLock_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(new EditLock(new Lock()));
        }

        private void AddAssemblyCompositionRow_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AddAssemblyComposition();
        }

        private void DeleteAssemblyCompositionRow_Click(object sender, RoutedEventArgs e)
        {
            viewModel.DeleteAssemblyComposition();
        }

        private void EditAssemblies_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedAssemblyComposition.Assembly == null) return;

            NavigationManager.Navigate(new EditAssembly(Assembly.GetByID(viewModel.SelectedAssemblyComposition.Assembly.Value)));
        }

        private void AddAssemblies_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(new EditAssembly(new Assembly()));
        }

        private void AddPartCompositionRow_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AddPartCompositions();
        }

        private void DeletePartCompositionRow_Click(object sender, RoutedEventArgs e)
        {
            viewModel.DeletePartComposition();
        }

        private void EditPart_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedPartComposition.Part == null) return;

            NavigationManager.Navigate(new EditPart(Part.GetByID(viewModel.SelectedPartComposition.Part.Value)));
        }

        private void AddPart_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(new EditPart(new Part()));
        }

        private void SaveComposition_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SaveCompositions();
        }

        private void SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
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
    }
}
