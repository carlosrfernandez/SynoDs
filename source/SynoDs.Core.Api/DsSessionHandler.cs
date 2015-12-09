// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DsSessionHandler.cs" company="">
//   
// </copyright>
// <summary>
//   The ds session handler.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynoDs.Core.Api
{
    using System;
    using System.Threading.Tasks;

    using SynoDs.Core.Contracts.Synology;
    using SynoDs.Core.CrossCutting;
    using SynoDs.Core.Dal.BaseApi;

    /// <summary>
    /// The ds session handler.
    /// </summary>
    public class DsSessionHandler : IDiskStationSessionHandler
    {
        /// <summary>
        /// The _authentication provider.
        /// </summary>
        private readonly IAuthenticationProvider authenticationProvider;

        /// <summary>
        /// Gets the disk station.
        /// </summary>
        public DiskStation DiskStation { get; private set; }

        /// <summary>
        /// Gets the credentials.
        /// </summary>
        public LoginCredentials Credentials { get; private set; }

        /// <summary>
        /// Gets the session id.
        /// </summary>
        public string SessionId { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether use SSL.
        /// </summary>
        public bool UseSsl { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DsSessionHandler"/> class.
        /// </summary>
        /// <param name="authenticationProvider">
        /// The authentication provider.
        /// </param>
        public DsSessionHandler(IAuthenticationProvider authenticationProvider)
        {
            if (authenticationProvider == null)
            {
                throw new ArgumentNullException(nameof(authenticationProvider));
            }

            this.authenticationProvider = authenticationProvider;
        }

        /// <summary>
        /// Will load all required dependencies.
        /// </summary>
        /// <param name="diskStation">
        /// </param>
        /// <param name="credentials">
        /// </param>
        /// <param name="useSsl">
        /// Flag to determine if the requests will go through ssl
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task CreateSessionAsync(DiskStation diskStation, LoginCredentials credentials, bool useSsl = false)
        {
            this.DiskStation = diskStation;
            this.Credentials = credentials;
            this.SessionId = string.Empty;
            this.UseSsl = useSsl;
            this.SessionId = await this.authenticationProvider.LoginAsync(this.Credentials);
        }
    }
}