using System.Windows;
using System.Windows.Controls;
using OPP.Navigation;
using OPP.ViewModels.Guides;
using ToleranceOfParts.Views.Pages.Guides.Edits;

namespace ToleranceOfParts.Views.Pages.Guides
{
    /// <summary>
    /// Логика взаимодействия для Assemblies.xaml
    /// </summary>
    public partial class Assemblies : Page
    {
        private AssembliesViewModel viewModel;
        public Assemblies()
        {
            InitializeComponent();
            viewModel = new AssembliesViewModel();
            DataContext = viewModel;
        }

        private void AddAssembly_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Add();
        }

        private void EditAssembly_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(new EditAssembly(viewModel.SelectedAssembly));
        }

        private void DeleteAssembly_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Delete();
        }
    }
}
