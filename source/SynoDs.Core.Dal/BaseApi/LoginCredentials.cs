namespace SynoDs.Core.Dal.BaseApi
{
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
    }
}
