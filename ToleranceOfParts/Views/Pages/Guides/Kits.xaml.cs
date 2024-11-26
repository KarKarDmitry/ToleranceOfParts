using System.Windows;
using System.Windows.Controls;
using OPP.Navigation;
using OPP.ViewModels.Guides;
using ToleranceOfParts.Views.Pages.Guides.Edits;

namespace ToleranceOfParts.Views.Pages.Guides
{
    /// <summary>
    /// Логика взаимодействия для Kits.xaml
    /// </summary>
    public partial class Kits : Page
    {
        KitsViewModel viewModel;
        public Kits()
        {
            InitializeComponent();

            viewModel = new KitsViewModel();
            DataContext = viewModel;
        }

        private void AddKit_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Add();
        }

        private void EditKit_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedKit == null) return;

            NavigationManager.Navigate(new EditKit(viewModel.SelectedKit));
        }

        private void DeleteKit_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Delete();
        }
    }
}
