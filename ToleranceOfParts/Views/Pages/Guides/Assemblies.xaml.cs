using OPP.ViewModels;
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
    /// Логика взаимодействия для Assemblies.xaml
    /// </summary>
    public partial class Assemblies : Page
    {
        private AssembliesViewModel viewModel;
        public Assemblies()
        {
            InitializeComponent();
            viewModel = new AssembliesViewModel();
            DataContext = viewModel;
        }

        private void AddAssembly_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Add();
        }

        private void EditAssembly_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedAssembly == null) return;
            NavigationManager.Navigate(new EditAssembly(viewModel.SelectedAssembly));
        }

        private void DeleteAssembly_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Delete();
        }
    }
}
