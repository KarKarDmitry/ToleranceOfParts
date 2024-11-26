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
