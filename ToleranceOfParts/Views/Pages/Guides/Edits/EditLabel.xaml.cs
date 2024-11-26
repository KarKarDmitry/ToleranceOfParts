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

namespace ToleranceOfParts.Views.Pages.Guides.Edits
{
    /// <summary>
    /// Логика взаимодействия для EditLabel.xaml
    /// </summary>
    public partial class EditLabel : DisposablePage
    {
        EditLabelViewModel viewModel;
        public EditLabel(OPP.AppData.Guides.Label label)
        {
            InitializeComponent();

            viewModel = new EditLabelViewModel(label);
            DataContext = viewModel;
        }

        public override void DisposeResources()
        {
            viewModel = null;
        }

        private void SaveLabel_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SaveLabel();
        }
    }
}
