using System.Windows;
using System.Windows.Controls;
using OPP.Navigation;
using OPP.ViewModels.Guides;
using ToleranceOfParts.Views.Pages.Guides.Edits;

namespace ToleranceOfParts.Views.Pages.Guides
{
    /// <summary>
    /// Логика взаимодействия для Coatings.xaml
    /// </summary>
    public partial class Coatings : Page
    {
        CoatingsViewModel viewModel;
        public Coatings()
        {
            InitializeComponent();

            viewModel = new CoatingsViewModel();
            DataContext = viewModel;
        }

        private void AddCoating_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Add();
        }

        private void EditCoating_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedCoating == null) return;

            NavigationManager.Navigate(new EditCoating(viewModel.SelectedCoating));

        }

        private void DeleteCoating_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Delete();
        }
    }
}
