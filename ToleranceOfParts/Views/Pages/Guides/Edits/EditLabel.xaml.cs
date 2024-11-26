using System.Windows;
using OPP.AppData.Guides;
using OPP.ViewClasses;
using OPP.ViewModels.Guides.Edits;

namespace ToleranceOfParts.Views.Pages.Guides.Edits
{
    /// <summary>
    /// Логика взаимодействия для EditLabel.xaml
    /// </summary>
    public partial class EditLabel : DisposablePage
    {
        EditLabelViewModel viewModel;
        public EditLabel(Label label)
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
