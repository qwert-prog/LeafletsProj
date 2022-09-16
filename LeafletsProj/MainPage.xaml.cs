using LeafletsProj.Services;
using LeafletsProj.View;
using LeafletsProj.ViewModel;
using IoC;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utils.Navigation;
using Utils.Navigation.Interfaces;
using Windows.Globalization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace LeafletsProj
{
    public sealed partial class MainPage : Page
    {
        /// <summary>
        /// Represents navigation service.
        /// </summary>
        private static INavigationService _navigator;

        /// <summary>
        /// Represents router for navigation.
        /// </summary>
        private static IRouter _router;

        /// <summary>
        /// Initializes an instance of a class <see cref="MainPage"/>
        /// </summary>
        public MainPage()

        {
            this.InitializeComponent();

#if DEBUG
            ChangeLanguage();
#endif

            _router = new Router();
            RegisterRoutes(_router);

            ConfigureNavigation();
        }

        /// <summary>
        /// Setting up navigation and switching to <see cref="ProjectsPage"/>
        /// </summary>
        private void ConfigureNavigation()
        {
            _navigator = ServicesContainer.GetService<NavigationService>();
            _navigator.Configure(_router, MainFrame);

            StartPageViewModel startPageVM = ServiceLocator.StartPageViewModel;
            _navigator.NavigateTo(startPageVM, default);
        }

        /// <summary>
        /// Executed registration of services collection.
        /// </summary>
        /// <param name="router">Router.</param>
        private void RegisterRoutes(IRouter router)
        {
            StartPageViewModel startPageVM = ServicesContainer.GetService<StartPageViewModel>();
            router.RegisterRoute<StartPage>(startPageVM);
            EditPageViewModel editPageVM = ServicesContainer.GetService<EditPageViewModel>();
            router.RegisterRoute<EditPage>(editPageVM);
            SavePageViewModel savePageVM = ServicesContainer.GetService<SavePageViewModel>();
            router.RegisterRoute<SavePage>(savePageVM);
        }

        /// <summary>
        /// Add ComboBox to change languages ​​while debugging
        /// </summary>
        private void ChangeLanguage()
        {
            ComboBox combobox = new ComboBox();
            combobox.HorizontalAlignment = HorizontalAlignment.Right;
            combobox.Margin = new Thickness(0, 0, 100, 0);
            Canvas.SetZIndex(combobox, 100);
            combobox.Items.Clear();
            combobox.IsTabStop = false;
            foreach (string item in new List<string>() { "en-Us" })
            {
                combobox.Items.Add(item);
            }
            combobox.SelectionChanged += Combobox_SelectionChanged;
            Grid gr = Content as Grid;

            gr.Children.Add(combobox);
        }

        /// <summary>
        /// Handles the language selection event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = (sender as ComboBox).SelectedItem as string;

            if (text != null)
            {
                ApplicationLanguages.PrimaryLanguageOverride = text;
                await Task.Delay(1000);
                Frame.Navigate(GetType());
            }
        }
    }
}