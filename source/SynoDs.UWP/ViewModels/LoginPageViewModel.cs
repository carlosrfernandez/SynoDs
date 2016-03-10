using SynoDs.Core.Contracts.Synology;
using SynoDs.Core.CrossCutting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using Microsoft.Practices.Unity;
using System.Windows.Input;
using Windows.Security.Credentials;
using Windows.Storage;
using Windows.UI.ViewManagement;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using SynoDs.UWP.Models;

namespace SynoDs.UWP.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        /// <summary>
        /// The authentication provider
        /// </summary>
        private readonly IAuthenticationProvider authenticationProvider;

        // todo: implement connection list
        public List<Connection> ConnectionsList { get; set; }

        public string Host { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        private int Port => 5001;

        public bool UseSsl { get; set; }

        public bool StoreCredentials { get; set; }

        public ICommand LoginCommand { get; private set; }

        private const string VaultResource = @"SynoDsCredStore";

        private const string ConnectionContainerName = @"ConnectionsContainer";

        private bool CredentialsRecovered
        {
            get; set;
        }


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
            this.CredentialsRecovered = false;
            this.RecoverCredentials();
        }

        public async Task LoginAsync()
        {
#if DEBUG
           // for debugging
#endif
            Views.Shell.SetBusy(true, "Loading...");
                
            var fullUrl = new Uri($"{Host}:{Port}");
            var loginResult = await this.authenticationProvider.LoginAsync(fullUrl, this.UserName, this.Password);
            
            Views.Shell.SetBusy(false);
            //TODO: control error messages here.
            if (!loginResult.IsLoggedIn())
            {
                // do something with this
                return;
            }

            if (StoreCredentials && !this.CredentialsRecovered)
            {
                this.StoreCredentialsInVault();
            }

            Views.Shell.SetBusy(false);

            NavigationService.Navigate(typeof (Views.MainPage));
        }

        private void StoreCredentialsInVault()
        {
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

            var container = roamingSettings.CreateContainer(ConnectionContainerName, Windows.Storage.ApplicationDataCreateDisposition.Always);

            var connection = new Connection
            {
                Host = this.Host,
                RememberMe = this.StoreCredentials,
                UseSsl = this.UseSsl,
                Username = this.UserName
            };

            // var dictionary = new Dictionary<string, Connection>();
            // dictionary.Add(this.Host, new Connection { Host = this.Host, RememberMe = this.StoreCredentials, UseSsl = this.UseSsl });
            var jsonStringConnection = JsonConvert.SerializeObject(connection);

            container.Values.Add(this.Host, jsonStringConnection); 
            var vault = new Windows.Security.Credentials.PasswordVault();
            vault.Add(new PasswordCredential(this.Host, UserName, Password));
        }

        private void RecoverCredentials()
        {
            var roamingSettings = ApplicationData.Current.RoamingSettings;
            var container = roamingSettings.CreateContainer(ConnectionContainerName,
                ApplicationDataCreateDisposition.Always);

            foreach (var kvp in container.Values)
            {
                this.Host = kvp.Key;
                var connectionJson = kvp.Value as string;
                if (connectionJson == null)
                {
                    continue;
                }

                var connection = JsonConvert.DeserializeObject<Connection>(connectionJson);

                if (connection == null)
                {
                    continue;
                }

                if (!connection.RememberMe)
                {
                    continue;
                }

                var vault = new PasswordVault();
                var password = vault.Retrieve(connection.Host, connection.Username);
                this.Password = password.Password;
                this.UserName = connection.Username;
                this.StoreCredentials = connection.RememberMe;
                this.UseSsl = connection.UseSsl;
                this.CredentialsRecovered = true;
                return;
            }
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
