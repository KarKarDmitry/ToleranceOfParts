using System.Windows;
using System.Windows.Controls;
using OPP.Navigation;
using OPP.ViewModels.Guides;
using ToleranceOfParts.Views.Pages.Guides.Edits;

namespace ToleranceOfParts.Views.Pages.Guides
{
    /// <summary>
    /// Логика взаимодействия для Manuals.xaml
    /// </summary>
    public partial class Manuals : Page
    {
        ManualsViewModel viewModel;
        public Manuals()
        {
            InitializeComponent();

            viewModel = new ManualsViewModel();
            DataContext = viewModel;
        }

        private void AddManual_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Add();
        }

        private void EditManual_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedManual == null) return;

            NavigationManager.Navigate(new EditManual(viewModel.SelectedManual));
        }

        private void DeleteManual_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Delete();
        }
    }
}
