using System.Windows;
using System.Windows.Controls;
using OPP.Navigation;
using OPP.ViewModels.Guides;
using ToleranceOfParts.Views.Pages.Guides.Edits;

namespace ToleranceOfParts.Views.Pages.Guides
{
    /// <summary>
    /// Логика взаимодействия для Blanks.xaml
    /// </summary>
    public partial class Blanks : Page
    {
        BlanksViewModel viewModel;
        public Blanks()
        {
            InitializeComponent();

            viewModel = new BlanksViewModel();
            DataContext = viewModel;
        }

        private void AddBlank_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Add();
        }

        private void EditBlank_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedBlank != null)
            {
                NavigationManager.Navigate(new EditBlank(viewModel.SelectedBlank));
            }
        }

        private void DeleteBlank_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Delete();
        }
    }
}
