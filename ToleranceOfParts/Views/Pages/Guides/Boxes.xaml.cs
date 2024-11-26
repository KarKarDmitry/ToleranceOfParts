using System.Windows;
using System.Windows.Controls;
using OPP.Navigation;
using OPP.ViewModels.Guides;
using ToleranceOfParts.Views.Pages.Guides.Edits;

namespace ToleranceOfParts.Views.Pages.Guides
{
    /// <summary>
    /// Логика взаимодействия для Boxes.xaml
    /// </summary>
    public partial class Boxes : Page
    {
        BoxesViewModel viewModel;
        public Boxes()
        {
            InitializeComponent();

            viewModel = new BoxesViewModel();
            DataContext = viewModel;
        }

        private void AddBox_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Add();
        }

        private void EditBox_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(new EditBox(viewModel.SelectedBox));
        }

        private void DeleteBox_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Delete();
        }
    }
}
