﻿using System.Threading.Tasks;

namespace SynoDs.Core.Contracts.Synology
{
    /// <summary>
    /// Defines the methods needed in order to authenticate
    /// </summary>
    public interface IAuthenticationProvider
    {
        /// <summary>
        /// Will tell us if we're logged in.
        /// </summary>
        bool IsLoggedIn { get; set; }

        /// <summary>
        /// Stores the Session Id for the current session.
        /// </summary>
        string Sid { get; set; }

        /// <summary>
        /// Asynchronous call to Login
        /// </summary>
        Task<bool> LoginAsync();

        /// <summary>
        /// Asynchronous call to logout.
        /// </summary>
        Task<bool> LogoutAsync();
    }
}