using OPP.AppData.Guides;
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
    /// Логика взаимодействия для Manuals.xaml
    /// </summary>
    public partial class Manuals : Page
    {
        ManualsViewModel viewModel;
        public Manuals()
        {
            InitializeComponent();

            viewModel = new ManualsViewModel();
            DataContext = viewModel;
        }

        private void AddManual_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Add();
        }

        private void EditManual_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedManual == null) return;

            NavigationManager.Navigate(new EditManual(viewModel.SelectedManual));
        }

        private void DeleteManual_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Delete();
        }
    }
}
