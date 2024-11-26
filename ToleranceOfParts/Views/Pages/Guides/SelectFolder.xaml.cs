using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using OPP.Navigation;
using OPP.Navigation.Guides;
using OPP.ViewModels.Guides;

namespace ToleranceOfParts.Views.Pages.Guides
{
    /// <summary>
    /// Логика взаимодействия для SelectFolder.xaml
    /// </summary>
    public partial class SelectFolder : Page
    {
        public SelectFolder()
        {
            InitializeComponent();

            GuidesNavigation.NavigateTo += OnNavigateToPage;
            GuidesNavigation.NavigateGoBack += OnNavigateGoBack;

        }

        private void OnNavigateToPage(Page page)
        {
            if (ContentFrame.NavigationService.Content != null)
            {
                Storyboard fadeOutStoryboard = (Storyboard)this.Resources["FadeOutStoryboard"];

                EventHandler fadeOutCompletedHandler = null;
                fadeOutCompletedHandler = (s, e) =>
                {
                    fadeOutStoryboard.Completed -= fadeOutCompletedHandler;

                    ContentFrame.NavigationService.Navigate(page);

                    Storyboard fadeInStoryboard = (Storyboard)this.Resources["FadeInStoryboard"];
                    fadeInStoryboard.Begin();
                };

                fadeOutStoryboard.Completed += fadeOutCompletedHandler;

                fadeOutStoryboard.Begin();
            }
            else
            {
                ContentFrame.NavigationService.Navigate(page);
            }
        }

        private void OnNavigateGoBack()
        {
            if (!ContentFrame.NavigationService.CanGoBack) return;

            Storyboard fadeOutStoryboard = (Storyboard)this.Resources["FadeOutStoryboard"];

            EventHandler fadeOutCompletedHandler = null;
            fadeOutCompletedHandler = (s, e) =>
            {
                fadeOutStoryboard.Completed -= fadeOutCompletedHandler;
                ContentFrame.NavigationService.GoBack();

                Storyboard fadeInStoryboard = (Storyboard)this.Resources["FadeInStoryboard"];
                fadeInStoryboard.Begin();
            };

            fadeOutStoryboard.Completed += fadeOutCompletedHandler;

            fadeOutStoryboard.Begin();
        }

        private void GlobalGuides_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(App.GetPage("GlobalGuideViewPage"));
        }

        private void Assemblies_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(App.GetPage("GuidesAssembliesPage"));
        }

        private void Blanks_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(App.GetPage("GuidesBlanksPage"));
        }

        private void Boxes_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(App.GetPage("GuidesBoxesPage"));
        }

        private void Coatings_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(App.GetPage("GuidesCoatingsPage"));
        }

        private void Kits_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(App.GetPage("GuidesKitsPage"));
        }

        private void Labels_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(App.GetPage("GuidesLabelsPage"));
        }

        private void Locks_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(App.GetPage("GuidesLocksPage"));
        }

        private void Manuals_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(App.GetPage("GuidesManualsPage"));
        }

        private void Packages_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(App.GetPage("GuidesPackagesPage"));
        }

        private void Parts_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(App.GetPage("GuidesPartsPage"));
        }

        private void Packagings_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(App.GetPage("GuidesPackagingsPage"));
        }

        private void Materials_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(App.GetPage("GuidesMaterialsPage"));
        }

        private void Sectors_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(App.GetPage("GuidesSectorsPage"));
        }

        private void Remainings_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(App.GetPage("GuidesRemainingsPage"));
        }

        private void Movements_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(App.GetPage("GuidesMovementsPage"));
        }
    }
}
