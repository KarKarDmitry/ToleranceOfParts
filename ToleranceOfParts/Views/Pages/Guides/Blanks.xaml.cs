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
