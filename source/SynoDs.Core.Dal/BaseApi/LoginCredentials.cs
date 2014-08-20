namespace SynoDs.Core.Dal.BaseApi
{
    using System;

    /// <summary>
    /// Model object to store login credentials.
    /// </summary>
    public class LoginCredentials
    {
        /// <summary>
        /// Username string.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
        
        /// <summary>
        /// Url of the DiskStation
        /// </summary>
        public Uri Uri { get; set; }

        /// <summary>
        /// UseSSL
        /// </summary>
        public bool UseSsl { get; set; }
    }
}
