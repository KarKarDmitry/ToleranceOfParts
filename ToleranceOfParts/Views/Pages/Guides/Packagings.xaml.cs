using OPP.ViewModels.Guides.Edits;
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
    /// Логика взаимодействия для Packagings.xaml
    /// </summary>
    public partial class Packagings : Page
    {
        PackagingsViewModel viewModel;

        public Packagings()
        {
            InitializeComponent();

            viewModel = new PackagingsViewModel();
            DataContext = viewModel;
        }

        private void AddPackaging_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Add();
        }

        private void EditPackaging_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedPackaging == null) return;

            NavigationManager.Navigate(new EditPackaging(viewModel.SelectedPackaging));
        }

        private void DeletePackaging_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Delete();
        }
    }
}
