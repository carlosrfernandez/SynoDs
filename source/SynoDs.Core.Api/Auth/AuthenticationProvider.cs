// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthenticationProvider.cs" company="">
//   
// </copyright>
// <summary>
//   The authentication provider.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynoDs.Core.Api.Auth
{
    using System;
    using System.Threading.Tasks;

    using SynoDs.Core.Contracts.Synology;
    using SynoDs.Core.Dal.BaseApi;
    using SynoDs.Core.Dal.HttpBase;

    /// <summary>
    /// The authentication provider.
    /// </summary>
    public class AuthenticationProvider : IAuthenticationProvider
    {
        /// <summary>
        /// The session name.
        /// </summary>
        private const string SessionName = "DiskStation";

        /// <summary>
        /// The _operation provider.
        /// </summary>
        private readonly IRequestService requestService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationProvider"/> class.
        /// </summary>
        /// <param name="diskStation">The diskstation</param>
        /// <param name="requestService">
        /// The operation provider.
        /// </param>
        public AuthenticationProvider(IRequestService requestService)
        {
            this.IsLoggedIn = false;
            this.requestService = requestService;
        }

        /// <summary>
        /// Gets or sets a value indicating whether is logged in.
        /// </summary>
        public bool IsLoggedIn { get; set; }

        /// <summary>
        /// The login async.
        /// </summary>
        /// <param name="host">
        /// The host.
        /// </param>
        /// <param name="username">
        /// The username.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<IDiskStationSession> LoginAsync(Uri host, string username, string password)
        {
            var diskStationSession = new DiskStation(new LoginCredentials { UserName = username, Password = password }, host.ToString());
            
            // prepare request
            var parameters = new RequestParameters
                                 {
                                     { "account", username }, 
                                     { "passwd", password }, 
                                     { "session", SessionName }, 
                                     { "format", "sid" }
                                 };

            var loginResult = await this.requestService.PerformOperationAsync<LoginResponse>(host.ToString(), parameters);

            this.IsLoggedIn = loginResult.Success;

            diskStationSession.SessionId = IsLoggedIn == true ? loginResult.ResponseData.Sid : null;

            return diskStationSession;
        }

        /// <summary>
        ///     Logs out of the DiskStation.
        /// </summary>
        /// <returns>True if logged in, false in case of errors.</returns>
        public async Task<bool> LogoutAsync(IDiskStationSession diskStation)
        {
            var logoutParams = new RequestParameters { { "session", SessionName } };
            var logoutRequestResult = await this.requestService.PerformOperationAsync<LogoutResponse>(diskStation.Host.ToString(), logoutParams);
            this.IsLoggedIn = false;
            return logoutRequestResult.Success;
        }
    }
}