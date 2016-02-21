// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDiskStationSession.cs" company="">
//   
// </copyright>
// <summary>
//   Will store the DiskStation information in order to provide it to the rest of the
//   modules if they require it.
//   This should be the entry point. Once this variables are set, the rest of the modules will use
//   them to make requests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynoDs.Core.Contracts.Synology
{
    using System;

    using SynoDs.Core.Dal.BaseApi;

    /// <summary>
    /// Will store the DiskStation information in order to provide it to the rest of the
    /// modules if they require it.
    /// This should be the entry point. Once this variables are set, the rest of the modules will use
    /// them to make requests.
    /// </summary>
    public class DiskStation : IDiskStationSession
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DiskStation"/> class.
        /// </summary>
        /// <param name="credentials">
        /// The credentials.
        /// </param>
        /// <param name="host">
        /// The Hostname or IP Address with the port.
        /// </param>
        public DiskStation(LoginCredentials credentials, string host)
        {
            this.Credentials = credentials;
            this.Host = new Uri(host);
        }

        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        public Uri Host { get; set; }

        /// <summary>
        /// Gets or sets the credentials.
        /// </summary>
        /// <value>
        /// The credentials.
        /// </value>
        public LoginCredentials Credentials { get; set;  }

        /// <summary>
        /// Gets or sets the session identifier.
        /// </summary>
        /// <value>
        /// The session identifier.
        /// </value>
        public string SessionId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [use SSL].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [use SSL]; otherwise, <c>false</c>.
        /// </value>
        public bool UseSsl { get; set; }

        /// <summary>
        /// The is logged in.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsLoggedIn()
        {
            return !string.IsNullOrEmpty(this.SessionId);
        }
    }
}