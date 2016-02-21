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
using GalaSoft.MvvmLight.Command;

namespace SynoDs.UWP.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        /// <summary>
        /// The authentication provider
        /// </summary>
        private readonly IAuthenticationProvider authenticationProvider;

        private string host { get; set; }

        private string userName { get; set; }

        private string password { get; set; }

        private int port => 5000;

        private bool useSsl { get; set; }

        private bool storeCredentials { get; set; }

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

        public async Task<bool> LoginAsync()
        {
#if DEBUG
            
#endif
            var fullUrl = new Uri($"{host}:{port}");
            var loginResult = await this.authenticationProvider.LoginAsync(fullUrl, this.userName, this.password);

            //TODO: control error messages here.
            if (!loginResult.IsLoggedIn()) return false;

            return true;
        }

        private void NavigateToMainView()
        {
            NavigationService.Navigate(typeof (Views.MainPage));
        }

        public override Task OnNavigatedFromAsync(IDictionary<string, object> state, bool suspending)
        {
            return base.OnNavigatedFromAsync(state, suspending);
        }

        public override Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
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
