using System.Windows;
using System.Windows.Controls;
using OPP.Navigation;
using OPP.ViewModels.Guides.Edits;
using ToleranceOfParts.Views.Pages.Guides.Edits;

namespace ToleranceOfParts.Views.Pages.Guides
{
    /// <summary>
    /// Логика взаимодействия для Packagings.xaml
    /// </summary>
    public partial class Packagings : Page
    {
        PackagingsViewModel viewModel;

        public Packagings()
        {
            InitializeComponent();

            viewModel = new PackagingsViewModel();
            DataContext = viewModel;
        }

        private void AddPackaging_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Add();
        }

        private void EditPackaging_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedPackaging == null) return;

            NavigationManager.Navigate(new EditPackaging(viewModel.SelectedPackaging));
        }

        private void DeletePackaging_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Delete();
        }
    }
}
