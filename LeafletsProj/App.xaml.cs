#if !DEBUG

using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

#endif

using LeafletsProj.Services;
using LeafletsProj.View;
using LeafletsProj.ViewModel;
using IoC;
using System;
using System.Threading.Tasks;
using Utils.Navigation;
using Utils.Services;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace LeafletsProj
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            RequestedTheme = ApplicationTheme.Light;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
            if (e.PreviousExecutionState != ApplicationExecutionState.Running)
            {
                bool loadState = (e.PreviousExecutionState == ApplicationExecutionState.Terminated);
                ExtendedSplash extendedSplash = new ExtendedSplash(e.SplashScreen, loadState);
                Window.Current.Content = extendedSplash;
            }

            Window.Current.Activate();

#if !DEBUG
            AppCenter.Start("38aac728-671e-4661-9562-d469396fde16",
            typeof(Analytics), typeof(Crashes));
#endif

            await LaunchApp(e);
        }

        /// <summary>
        /// Инициализация приложения
        /// </summary>
        /// <param name="e"></param>
        private async Task LaunchApp(LaunchActivatedEventArgs e)
        {
            ConfigurateServices();
            await AssetsServiceInitializeAsync();

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        /// <summary>
        /// Initialize assets service
        /// </summary>
        /// <returns></returns>
        private async Task AssetsServiceInitializeAsync()
        {
            AssetsService _assetsService = ServicesContainer.GetService<AssetsService>();
            await _assetsService.InitializeAsync();
        }

        /// <summary>
        /// Configurate service container
        /// </summary>
        private void ConfigurateServices()
        {
            // Services for working
            ServicesContainer.AddSingletonService<NavigationService>();
            ServicesContainer.AddSingletonService<AssetsService>();
            ServicesContainer.AddSingletonService<FontsService>();
            ServicesContainer.AddSingletonService<RenderUIElementService>();
            ServicesContainer.AddSingletonService<SaveDataService>();

            // View models
            ServicesContainer.AddSingletonService<StartPageViewModel>();
            ServicesContainer.AddSingletonService<EditPageViewModel>();
            ServicesContainer.AddSingletonService<SavePageViewModel>();

            ServicesContainer.BuildServices();
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}