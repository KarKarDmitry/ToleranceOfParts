using System.Windows;
using System.Windows.Controls;
using OPP.Navigation;
using OPP.ViewModels.Guides;
using ToleranceOfParts.Views.Pages.Guides.Edits;

namespace ToleranceOfParts.Views.Pages.Guides
{
    /// <summary>
    /// Логика взаимодействия для Labels.xaml
    /// </summary>
    public partial class Labels : Page
    {
        LabelsViewModel viewModel;
        public Labels()
        {
            InitializeComponent();

            viewModel = new LabelsViewModel();
            DataContext = viewModel;
        }

        private void AddLabel_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Add();
        }

        private void EditLabel_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedLabel == null) return;

            NavigationManager.Navigate(new EditLabel(viewModel.SelectedLabel));
        }

        private void DeleteLabel_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Delete();
        }
    }
}
