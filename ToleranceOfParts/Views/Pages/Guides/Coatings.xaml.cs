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
    /// Логика взаимодействия для Coatings.xaml
    /// </summary>
    public partial class Coatings : Page
    {
        CoatingsViewModel viewModel;
        public Coatings()
        {
            InitializeComponent();

            viewModel = new CoatingsViewModel();
            DataContext = viewModel;
        }

        private void AddCoating_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Add();
        }

        private void EditCoating_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedCoating == null) return;

            NavigationManager.Navigate(new EditCoating(viewModel.SelectedCoating));

        }

        private void DeleteCoating_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Delete();
        }
    }
}
