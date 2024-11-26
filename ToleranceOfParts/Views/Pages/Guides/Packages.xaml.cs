using System.Windows;
using System.Windows.Controls;
using OPP.Navigation;
using OPP.ViewModels.Guides;
using ToleranceOfParts.Views.Pages.Guides.Edits;

namespace ToleranceOfParts.Views.Pages.Guides
{
    /// <summary>
    /// Логика взаимодействия для Packages.xaml
    /// </summary>
    public partial class Packages : Page
    {
        PackagesViewModel viewModel;
        public Packages()
        {
            InitializeComponent();

            viewModel = new PackagesViewModel();
            DataContext = viewModel;
        }

        private void AddPackage_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Add();
        }

        private void EditPackage_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(new EditPackage(viewModel.SelectedPackage));
        }

        private void DeletePackage_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Delete();
        }
    }
}
