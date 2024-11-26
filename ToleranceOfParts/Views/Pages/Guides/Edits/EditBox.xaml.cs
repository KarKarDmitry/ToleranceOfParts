using OPP.AppData.Guides;
using OPP.ViewModels.Guides.Edits;
using OPP.ViewClasses;
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
using Label = OPP.AppData.Guides.Label;
using OPP.Navigation;

namespace ToleranceOfParts.Views.Pages.Guides.Edits
{
    /// <summary>
    /// Логика взаимодействия для EditBox.xaml
    /// </summary>
    public partial class EditBox : DisposablePage
    {
        EditBoxViewModel viewModel;
        public EditBox(Box box)
        {
            InitializeComponent();
            viewModel = new EditBoxViewModel(box);
            DataContext = viewModel;
        }

        public override void DisposeResources()
        {
            viewModel = null;
        }

        private void EditLabel_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.Box.Label != null)
            {
                NavigationManager.Navigate(new EditLabel(Label.GetByID(viewModel.Box.Label.Value)));
            }
        }

        private void AddLabel_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(new EditLabel(new Label()));
        }

        private void SaveBox_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SaveBox();
        }
    }
}
