using System.Windows;
using OPP.AppData.Guides;
using OPP.Navigation;
using OPP.ViewClasses;
using OPP.ViewModels.Guides.Edits;
using Label = OPP.AppData.Guides.Label;

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
