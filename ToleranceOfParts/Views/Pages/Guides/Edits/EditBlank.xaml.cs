using System.Windows;
using OPP.AppData.Guides;
using OPP.ViewClasses;
using OPP.ViewModels.Guides.Edits;

namespace ToleranceOfParts.Views.Pages.Guides.Edits
{
    /// <summary>
    /// Логика взаимодействия для EditBlank.xaml
    /// </summary>
    public partial class EditBlank : DisposablePage
    {
        EditBlankViewModel viewModel;
        public EditBlank(Blank blank)
        {
            InitializeComponent();
            viewModel = new EditBlankViewModel(blank);
            DataContext = viewModel;
        }

        public override void DisposeResources()
        {
            viewModel = null;
        }

        private void SaveBlank_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SaveBlank();
        }
    }
}
