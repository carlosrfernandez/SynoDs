// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoginViewModel.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the LoginViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynoDs.WPF.Client.ViewModel
{
    using System;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    using SynoDs.Core.Contracts.Synology;
    using SynoDs.Core.Dal.BaseApi;
    using SynoDs.WPF.Client.Views;

    /// <summary>
    /// The login view model.
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// The authentication provider
        /// </summary>
        private readonly IAuthenticationProvider authenticationProvider;

        private IDiskStationSession DiskStationSession { get; set; }

        /// <summary>
        /// The use SSL
        /// </summary>
        private bool useSsl => false;

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        private string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        private string Password { get; set; }

        private string HostName { get; set; }

        private bool RememberMe { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginViewModel"/> class.
        /// </summary>
        /// <param name="authenticationProvider">
        /// The authentication provider.
        /// </param>
        public LoginViewModel(IAuthenticationProvider authenticationProvider)
        {
            if (authenticationProvider == null)
            {
                throw new ArgumentNullException(nameof(authenticationProvider));
            }

            this.authenticationProvider = authenticationProvider;
        }

        /// <summary>
        /// The login async.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task LoginAsync()
        {
            var loginResult = await this.authenticationProvider.LoginAsync(new Uri(this.HostName), this.UserName, this.Password) as IDiskStationSession;
            if (loginResult == null)
            {
                throw new Exception("Login response error.");
            }

            if (!loginResult.IsLoggedIn())
            {
                // show invalid credentials message.
                MessageBox.Show("Invalid login credentials supplied.", "Login error.", MessageBoxButton.OK);
            }
            else
            {
                
            }
        }
    }
}
