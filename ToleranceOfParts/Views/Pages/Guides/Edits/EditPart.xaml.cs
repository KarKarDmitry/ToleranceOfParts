using System.Windows;
using System.Windows.Controls;
using OPP.AppData.Guides;
using OPP.Navigation;
using OPP.ViewClasses;
using OPP.ViewModels.Guides.Edits;
using ToleranceOfParts.Classes.Environment;

namespace ToleranceOfParts.Views.Pages.Guides.Edits
{
    /// <summary>
    /// Логика взаимодействия для EditPart.xaml
    /// </summary>
    public partial class EditPart : DisposablePage
    {
        EditPartViewModel viewModel;
        public EditPart(Part part)
        {
            InitializeComponent();

            viewModel = new EditPartViewModel(part);
            DataContext = viewModel;

            PartPlane.Source = Part.GetImageAsBitmap(viewModel.Part);
        }

        public override void DisposeResources()
        {
            viewModel = null;
        }

        private void EditCoatingType_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.Part.Coating == null) return;

            NavigationManager.Navigate(new EditCoating(Coating.GetByID(viewModel.Part.Coating.Value)));
        }

        private void AddCoatingType_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(new EditCoating(new Coating()));
        }

        private void AddPartCompositionRow_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AddBlankCompositions();
        }

        private void DeletePartCompositionRow_Click(object sender, RoutedEventArgs e)
        {
            viewModel.DeleteBlankComposition();
        }

        private void EditDetail_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedBlankComposition?.Blank == null) return;

            NavigationManager.Navigate(new EditBlank(Blank.GetByID(viewModel.SelectedBlankComposition.Blank.Value)));
        }

        private void AddDetail_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(new EditBlank(new Blank()));
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

        private void SavePart_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SavePart();
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
            var byteMap = Part.SetImage(viewModel.Part, byteImage);
            if (byteMap != null) PartPlane.Source = byteMap;
        }
    }
}
