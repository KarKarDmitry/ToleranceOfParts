using System.Windows;
using OPP.AppData.Guides;
using OPP.Navigation;
using OPP.ViewClasses;
using OPP.ViewModels.Guides.Edits;
using Label = OPP.AppData.Guides.Label;

namespace ToleranceOfParts.Views.Pages.Guides.Edits
{
    /// <summary>
    /// Логика взаимодействия для EditPackage.xaml
    /// </summary>
    public partial class EditPackage : DisposablePage
    {
        EditPackageViewModel viewModel;
        public EditPackage(Package package)
        {
            InitializeComponent();

            viewModel = new EditPackageViewModel(package);
            DataContext = viewModel;
        }

        public override void DisposeResources()
        {
            viewModel = null;
        }

        private void EditLabel_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.Package.Label == null) return;

            NavigationManager.Navigate(new EditLabel(Label.GetByID(viewModel.Package.Label.Value)));
        }

        private void AddLabel_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(new EditLabel(new Label()));
        }

        private void SavePackage_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SavePackage();
        }
    }
}
