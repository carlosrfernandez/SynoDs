using System;
using Windows.UI.Xaml;
using System.Threading.Tasks;
using SynoDs.UWP.Services.SettingsServices;
using Windows.ApplicationModel.Activation;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using SynoDs.Core.Contracts.IoC;
using Template10.Utils;
using SynoDs.Core.CrossCutting;

namespace SynoDs.UWP
{
    /// Documentation on APIs used in this page:
    /// https://github.com/Windows-XAML/Template10/wiki

    sealed partial class App : Template10.Common.BootStrapper
    {
        /// <summary>
        /// The container
        /// </summary>
        private IUnityContainer container = new UnityContainer();

        /// <summary>
        /// The bootstrapper.
        /// </summary>
        private IBootstrapper bootstrapper;

        public App()
        {
            Microsoft.ApplicationInsights.WindowsAppInitializer.InitializeAsync(
                Microsoft.ApplicationInsights.WindowsCollectors.Metadata |
                Microsoft.ApplicationInsights.WindowsCollectors.Session);
            InitializeComponent();
            SplashFactory = (e) => new Views.Splash(e);

            var locator = new UnityServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => locator);

            #region App settings

            var _settings = SettingsService.Instance;
            RequestedTheme = _settings.AppTheme;
            CacheMaxDuration = _settings.CacheMaxDuration;
            ShowShellBackButton = _settings.UseShellBackButton;
            #endregion
        }

        // runs even if restored from state
        public override Task OnInitializeAsync(IActivatedEventArgs args)
        {
            // load app modules.
            this.bootstrapper = new Bootstrapper(container);
            bootstrapper.Startup();


            // content may already be shell when resuming
            if ((Window.Current.Content as Views.Shell) == null)
            {
                // setup hamburger shell
                var nav = NavigationServiceFactory(BackButton.Attach, ExistingContent.Include);
                Window.Current.Content = new Views.Shell(nav);
            }

            return Task.CompletedTask;
        }

        // runs only when not restored from state
        public override Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            NavigationService.Navigate(typeof(Views.LoginPage));
            return Task.CompletedTask;
        }
    }
}

