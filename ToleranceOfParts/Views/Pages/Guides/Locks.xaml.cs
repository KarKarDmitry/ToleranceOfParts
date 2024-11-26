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
    /// Логика взаимодействия для Locks.xaml
    /// </summary>
    public partial class Locks : Page
    {
        LocksViewModel viewModel;
        public Locks()
        {
            InitializeComponent();

            viewModel = new LocksViewModel();
            DataContext = viewModel;
        }

        private void AddLock_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Add();
        }

        private void EditLock_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedLock == null) return;

            NavigationManager.Navigate(new EditLock(viewModel.SelectedLock));
        }

        private void DeleteLock_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Delete();
        }
    }
}
