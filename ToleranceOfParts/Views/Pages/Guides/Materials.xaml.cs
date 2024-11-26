using System.Windows;
using System.Windows.Controls;
using OPP.ViewModels.Guides;

namespace ToleranceOfParts.Views.Pages.Guides
{
    /// <summary>
    /// Логика взаимодействия для Materials.xaml
    /// </summary>
    public partial class Materials : Page
    {
        MaterialsViewModel viewModel;
        public Materials()
        {
            InitializeComponent();

            viewModel = new MaterialsViewModel();
            DataContext = viewModel;
        }

        private void AddMaterial_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Add();
        }

        private void EditMaterial_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteMaterial_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Delete();
        }
    }
}
