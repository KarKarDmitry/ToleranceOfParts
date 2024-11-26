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
using OPP.Navigation;

namespace ToleranceOfParts.Views.Pages.Guides.Edits
{
    /// <summary>
    /// Логика взаимодействия для EditLock.xaml
    /// </summary>
    public partial class EditLock : DisposablePage
    {
        EditLockViewModel viewModel;
        public EditLock(Lock @lock)
        {
            InitializeComponent();

            viewModel = new EditLockViewModel(@lock);
            DataContext = viewModel;
        }

        public override void DisposeResources()
        {
            viewModel = null;
        }

        private void EditCoatingType_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.Lock.Coating == null) return;

            NavigationManager.Navigate(new EditCoating(Coating.GetByID(viewModel.Lock.Coating.Value)));
        }

        private void AddCoatingType_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(new EditCoating(new Coating()));
        }

        private void EditManual_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.Lock.Coating == null) return;

            NavigationManager.Navigate(new EditManual(Manual.GetByID(viewModel.Lock.Coating.Value)));
        }

        private void AddManual_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(new EditManual(new Manual()));
        }

        private void EditKit_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.Lock.Kit == null) return;

            NavigationManager.Navigate(new EditKit(Kit.GetByID(viewModel.Lock.Kit.Value)));
        }

        private void AddKit_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(new EditKit(new Kit()));
        }

        private void EditPackage_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.Lock.Package == null) return;

            NavigationManager.Navigate(new EditPackage(Package.GetByID(viewModel.Lock.Package.Value)));
        }

        private void AddPackage_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(new EditPackage(new Package()));
        }

        private void SaveLock_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SaveLock();
        }

        private void AddAssemblyCompositionRow_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AddAssemblyComposition();
        }

        private void DeleteAssemblyCompositionRow_Click(object sender, RoutedEventArgs e)
        {
            viewModel.DeleteAssemblyComposition();
        }

        private void EditAssembly_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedAssemblyComposition.Assembly == null) return;

            NavigationManager.Navigate(new EditAssembly(Assembly.GetByID(viewModel.SelectedAssemblyComposition.Assembly.Value)));
        }

        private void AddAssembly_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(new EditAssembly(new Assembly()));
        }

        private void AddPartCompositionRow_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AddPartComposition();
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
