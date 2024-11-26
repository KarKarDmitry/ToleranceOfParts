using System.Windows;
using OPP.AppData.Guides;
using OPP.ViewClasses;
using OPP.ViewModels.Guides.Edits;

namespace ToleranceOfParts.Views.Pages.Guides.Edits
{
    /// <summary>
    /// Логика взаимодействия для EditCoating.xaml
    /// </summary>
    public partial class EditCoating : DisposablePage
    {
        EditCoatingViewModel viewModel;
        public EditCoating(Coating coating)
        {
            InitializeComponent();
            viewModel = new EditCoatingViewModel(coating);
            DataContext = viewModel;
        }

        public override void DisposeResources()
        {
            viewModel = null;
        }

        private void SaveCoating_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SaveCoating();
        }
    }
}
