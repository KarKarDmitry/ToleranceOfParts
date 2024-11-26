using System.Windows;
using System.Windows.Controls;
using OPP.Navigation;
using OPP.ViewModels.Guides;
using ToleranceOfParts.Views.Pages.Guides.Edits;

namespace ToleranceOfParts.Views.Pages.Guides
{
    /// <summary>
    /// Логика взаимодействия для Parts.xaml
    /// </summary>
    public partial class Parts : Page
    {
        PartsViewModel viewModel;
        public Parts()
        {
            InitializeComponent();

            viewModel = new PartsViewModel();
            DataContext = viewModel;
        }

        private void AddPart_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Add();
        }

        private void EditPart_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedPart == null) return;

            NavigationManager.Navigate(new EditPart(viewModel.SelectedPart));
        }

        private void DeletePart_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Delete();
        }
    }
}
