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
        /// The disk station.
        /// </summary>
        private readonly DiskStationDto diskStation;

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
        public AuthenticationProvider(DiskStationDto diskStation, IRequestService requestService)
        {
            if (diskStation == null)
            {
                throw new ArgumentNullException(nameof(diskStation));
            }

            this.IsLoggedIn = false;
            this.diskStation = diskStation;
            this.requestService = requestService;
        }

        /// <summary>
        /// Gets or sets a value indicating whether is logged in.
        /// </summary>
        public bool IsLoggedIn { get; set; }

        // to control logging in process.

        /// <summary>
        /// Logs into the DiskStation with the supplied credentials.
        /// </summary>
        /// <param name="credentials">
        /// The credentials.
        /// </param>
        /// <returns>
        /// True if logged in and false if any errors occur.
        /// </returns>
        public async Task<string> LoginAsync(LoginCredentials credentials)
        {
            // prepare request
            var parameters = new RequestParameters
                                 {
                                     { "account", credentials.UserName }, 
                                     { "passwd", credentials.Password }, 
                                     { "session", SessionName }, 
                                     { "format", "sid" }
                                 };

            var loginResult = await this.requestService.PerformOperationAsync<LoginResponse>(this.diskStation.DiskStation.HostName.ToString(), parameters);

            this.IsLoggedIn = loginResult.Success;

            return loginResult.ResponseData.Sid;
        }

        /// <summary>
        ///     Logs out of the DiskStation.
        /// </summary>
        /// <returns>True if logged in, false in case of errors.</returns>
        public async Task<bool> LogoutAsync()
        {
            var logoutParams = new RequestParameters { { "session", SessionName } };
            var logoutRequestResult = await this.requestService.PerformOperationAsync<LogoutResponse>(this.diskStation.DiskStation.HostName.ToString(), logoutParams);
            this.IsLoggedIn = false;
            return logoutRequestResult.Success;
        }
    }
}