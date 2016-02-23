using SynoDs.Core.Contracts.Synology;
using SynoDs.Core.CrossCutting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using Microsoft.Practices.Unity;
using System.Windows.Input;
using Windows.UI.ViewManagement;
using GalaSoft.MvvmLight.Command;

namespace SynoDs.UWP.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        /// <summary>
        /// The authentication provider
        /// </summary>
        private readonly IAuthenticationProvider authenticationProvider;

        public string Host { get; set; }

        public string UserName { get; set; }

        private string password { get; set; }

        private int port => 5000;

        private bool useSsl { get; set; }

        public bool StoreCredentials { get; set; }

        public ICommand LoginCommand { get; private set; }

        /// <summary>
        /// The empty constructor for mocking data.
        /// </summary>
        public LoginPageViewModel()
        {

        }

        public LoginPageViewModel(IAuthenticationProvider authenticationProvider)
        {
            this.LoginCommand = new RelayCommand(async () => await LoginAsync(), () => true);
            this.authenticationProvider = authenticationProvider;
        }

        public async Task LoginAsync()
        {
#if DEBUG
           
#endif
            Views.Shell.SetBusy(true, "Loading...");

            if (!Host.StartsWith("http://") || !Host.StartsWith("https://"))
            {
                Host = $"http://{Host}";
            }
                
            var fullUrl = new Uri($"{Host}:{port}");
            var loginResult = await this.authenticationProvider.LoginAsync(fullUrl, this.UserName, this.password);
            
            Views.Shell.SetBusy(false);
            //TODO: control error messages here.
            if (!loginResult.IsLoggedIn())
            {
                // do something with this
                return;
            }

            NavigationService.Navigate(typeof (Views.MainPage));
        }

        private void NavigateToMainView()
        {
            NavigationService.Navigate(typeof (Views.MainPage));
        }

        public override Task OnNavigatedFromAsync(IDictionary<string, object> state, bool suspending)
        {
            if (this.authenticationProvider.IsLoggedIn)
            {
                // do summin', we left.
            }

            return base.OnNavigatedFromAsync(state, suspending);
        }

        public override Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            if (this.authenticationProvider.IsLoggedIn)
            {
                // do summin' we're leaving
            }
            return base.OnNavigatingFromAsync(args);
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            // navigation mode = new on boot?
            if (mode == NavigationMode.New)
            {
                // load existing connections or create new one
            }
            return base.OnNavigatedToAsync(parameter, mode, state);
        }
    }
}
