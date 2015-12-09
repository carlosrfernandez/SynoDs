﻿using SynoDs.Core.Dal.BaseApi;

namespace SynoDs.Core.Contracts.Synology
{
    using System.Threading.Tasks;

    /// <summary>
    /// Will store the DiskStation information in order to provide it to the rest of the 
    /// modules if they require it. 
    /// This should be the entry point. Once this variables are set, the rest of the modules will use
    /// them to make requests. 
    /// </summary>
    public interface IDiskStationSessionHandler
    {
        /// <summary>
        /// URL of the DiskStation
        /// </summary>
        DiskStation DiskStation { get; }

        /// <summary>
        /// Credentials for the DiskStation.
        /// </summary>
        LoginCredentials Credentials { get; }

        /// <summary>
        /// Creates a session. 
        /// </summary>
        /// <param name="diskStation">
        /// The target IP or hostname of our DiskStation
        /// </param>
        /// <param name="credentials">
        /// The credentials to be used.
        /// </param>
        /// <param name="useSsl">
        /// Flag to determine if the requests will go through ssl
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task CreateSessionAsync(DiskStation diskStation, LoginCredentials credentials, bool useSsl = false);

        /// <summary>
        /// SessionId of the current session.
        /// </summary>
        string SessionId { get; }

        /// <summary>
        /// Use SSL
        /// </summary>
        bool UseSsl { get; set; }
    }
}
