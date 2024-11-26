using OPP.ViewModels.Guides;
using ToleranceOfParts.Views.Pages.Guides.Edits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OPP.Navigation;

namespace ToleranceOfParts.Views.Pages.Guides
{
    /// <summary>
    /// Логика взаимодействия для Parts.xaml
    /// </summary>
    public partial class Parts : Page
    {
        PartsViewModel viewModel;
        public Parts()
        {
            InitializeComponent();

            viewModel = new PartsViewModel();
            DataContext = viewModel;
        }

        private void AddPart_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Add();
        }

        private void EditPart_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedPart == null) return;

            NavigationManager.Navigate(new EditPart(viewModel.SelectedPart));
        }

        private void DeletePart_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Delete();
        }
    }
}
