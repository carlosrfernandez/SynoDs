// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAuthenticationProvider.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the methods needed in order to authenticate
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynoDs.Core.Contracts.Synology
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the methods needed in order to authenticate
    /// </summary>
    public interface IAuthenticationProvider
    {
        /// <summary>
        /// Gets or sets a value indicating whether is logged in.
        /// </summary>
        bool IsLoggedIn { get; set; }

        /// <summary>
        /// Asynchronous call to Login
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
        Task<IDiskStationSession> LoginAsync(Uri host, string username, string password);

        /// <summary>
        /// Asynchronous call to logout.
        /// </summary>
        /// <param name="diskStation">
        /// The disk Station.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<bool> LogoutAsync(IDiskStationSession diskStation);
    }
}