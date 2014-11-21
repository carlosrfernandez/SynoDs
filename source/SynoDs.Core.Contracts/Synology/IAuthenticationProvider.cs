using System.Threading.Tasks;
using SynoDs.Core.Dal.BaseApi;

namespace SynoDs.Core.Contracts.Synology
{
    /// <summary>
    /// Defines the methods needed in order to authenticate
    /// </summary>
    public interface IAuthenticationProvider
    {
        LoginCredentials Credentials { get; set;}

        /// <summary>
        /// Will tell us if we're logged in.
        /// </summary>
        bool IsLoggedIn { get; set; }
        
        /// <summary>
        /// Flago to indicate that an Authentication request is in progres.
        /// </summary>
        bool IsLoggingIn { get; set; }

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