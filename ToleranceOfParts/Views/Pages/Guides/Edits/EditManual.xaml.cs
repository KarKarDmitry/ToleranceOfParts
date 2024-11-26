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
    /// Логика взаимодействия для EditManual.xaml
    /// </summary>
    public partial class EditManual : DisposablePage
    {
        EditManualViewModel viewModel;
        public EditManual(Manual manual)
        {
            InitializeComponent();

            DataContext = new EditManualViewModel(manual);
        }

        public override void DisposeResources()
        {
            viewModel = null;
        }

        private void SaveManual_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SaveManual();
        }
    }
}
