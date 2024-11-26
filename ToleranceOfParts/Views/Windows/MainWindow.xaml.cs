using System.Windows;
using OPP.AppData.Guides;
using OPP.Navigation;
using OPP.ViewModels.Guides;
using OPP.ViewModels.Guides.Edits;
using ToleranceOfParts.Views.Pages.Guides;
using ToleranceOfParts.Views.Windows;
using P_Accounting = ToleranceOfParts.Views.Pages.Accounting;
using P_PDB = ToleranceOfParts.Views.Pages.Guides;

namespace ToleranceOfParts
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            // Открытие окна аутентификации
            AutentificationWindow authWindow = new AutentificationWindow();
            bool? auth = authWindow.ShowDialog();

            if (auth.HasValue && !auth.Value)
            {
                // Если аутентификация не удалась, закрываем главное окно
                this.Close();
            }
            else
            {
                InitializeComponent();

                // Загружаем данные и ждем завершения перед отображением окна
                LoadDataAsync();

                new _GeneralGuidesViewModel().SubscribeEvents();

                ContentFrame.NavigationService.Navigate(App.GetNavigationPage<P_PDB.SelectFolder>("GuidesSelectFolderPage"));
            }

        }

        private async Task LoadDataAsync()
        {
            // Создаем список задач для параллельной загрузки данных
            var tasks = new List<Task>();

            tasks.AddRange(new List<Task>
            {
                Task.Run(() => Coating.GetAll()),
                Task.Run(() => OPP.AppData.Guides.Label.GetAll()),
                Task.Run(() => Manual.GetAll()),
                Task.Run(() => Blank.GetAll()),
                Task.Run(() => Box.GetAll()),
                Task.Run(() => Package.GetAll()),
                Task.Run(() => Part.GetAll()),
                Task.Run(() => Assembly.GetAll()),
                Task.Run(() => Kit.GetAll()),
                Task.Run(() => Lock.GetAll()),
                Task.Run(() => Packaging.GetAll()),
                Task.Run(() => AssemblyAssemblyComposition.GetAll()),
                Task.Run(() => AssemblyPartComposition.GetAll()),
                Task.Run(() => KitAssemblyComposition.GetAll()),
                Task.Run(() => KitPartComposition.GetAll()),
                Task.Run(() => LockAssemblyComposition.GetAll()),
                Task.Run(() => LockPartComposition.GetAll()),
                Task.Run(() => PackagingAssemblyComposition.GetAll()),
                Task.Run(() => PackagingPartComposition.GetAll()),
                Task.Run(() => PackagingLockComposition.GetAll()),
                Task.Run(() => PartComposition.GetAll())
            });

            // Await all tasks to complete
            await Task.WhenAll(tasks);

        }


        private void PDP_Button_Checked(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(App.GetNavigationPage<P_PDB.SelectFolder>("GuidesSelectFolderPage"));
            NavigationManager.SetGuidesContext();
        }

        private void Accounting_Button_Checked(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(App.GetNavigationPage<P_Accounting.SelectFolder>("AccountingSelectFolderPage"));
            NavigationManager.SetAccountingContext();
        }

        private void TechnicalBureau_Button_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
