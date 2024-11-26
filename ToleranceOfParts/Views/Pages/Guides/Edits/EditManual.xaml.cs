using System.Windows;
using OPP.AppData.Guides;
using OPP.ViewClasses;
using OPP.ViewModels.Guides.Edits;

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
