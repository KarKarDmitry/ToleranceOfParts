using OPP.ViewModels.Guides;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OPP.AppData.Guides;
using System.Windows.Controls;
using ToleranceOfParts.Views.Pages.Guides.Edits;
using Label = OPP.AppData.Guides.Label;
using OPP.Navigation;

namespace ToleranceOfParts.Views.Pages.Guides
{
    /// <summary>
    /// Логика взаимодействия для Labels.xaml
    /// </summary>
    public partial class Labels : Page
    {
        LabelsViewModel viewModel;
        public Labels()
        {
            InitializeComponent();

            viewModel = new LabelsViewModel();
            DataContext = viewModel;
        }

        private void AddLabel_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Add();
        }

        private void EditLabel_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedLabel == null) return;

            NavigationManager.Navigate(new EditLabel(viewModel.SelectedLabel));
        }

        private void DeleteLabel_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Delete();
        }
    }
}
