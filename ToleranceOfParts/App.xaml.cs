using ToleranceOfParts.Views.Pages.Guides;
using Accounting = ToleranceOfParts.Views.Pages.Accounting;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using ToleranceOfParts.Views.Pages.Accounting;
using OPP.AppData.Registers.Movements;
using OPP.AppData.Registers;

namespace ToleranceOfParts
{
    public partial class App : Application
    {
        static App()
        {
            RegisterPage("GuidesPackagesPage", () => new Packages());
            RegisterPage("GuidesAssembliesPage", () => new Assemblies());
            RegisterPage("GuidesBlanksPage", () => new Blanks());
            RegisterPage("GuidesBoxesPage", () => new Boxes());
            RegisterPage("GuidesCoatingsPage", () => new Coatings());
            RegisterPage("GlobalGuideViewPage", () => new GlobalGuidesView());
            RegisterPage("GuidesKitsPage", () => new Kits());
            RegisterPage("GuidesLabelsPage", () => new Labels());
            RegisterPage("GuidesLocksPage", () => new Locks());
            RegisterPage("GuidesManualsPage", () => new Manuals());
            RegisterPage("GuidesMaterialsPage", () => new Materials());
            RegisterPage("GuidesPackagingsPage", () => new Packagings());
            RegisterPage("GuidesPartsPage", () => new Parts());
        }

        private static Dictionary<string, Func<Page>> PageConstructors = new Dictionary<string, Func<Page>>();
        private static Dictionary<string, Page> NavigationPages = new Dictionary<string, Page>();

        public static Page GetNavigationPage<NavigationPageType>(string name) where NavigationPageType : Page, new()
        {
            // Если страница с таким именем уже существует, возвращаем её
            if (NavigationPages.ContainsKey(name))
            {
                return NavigationPages[name];
            }
            else
            {
                // Если страницы нет, создаём новую и добавляем в словарь
                var newPage = new NavigationPageType();
                NavigationPages[name] = newPage;
                return newPage;
            }
        }

        public static void RegisterPage(string name, Func<Page> constructor)
        {
            PageConstructors[name] = constructor;
        }

        public static Page GetPage(string name)
        {
            if (PageConstructors.ContainsKey(name))
            {
                return PageConstructors[name]();
            }
            throw new InvalidOperationException($"Page '{name}' not found in PageFactory.");
        }

        public static void DeletePage(string name)
        {
            if (PageConstructors.ContainsKey(name))
            {
                PageConstructors.Remove(name);
            }
        }
    }
}
