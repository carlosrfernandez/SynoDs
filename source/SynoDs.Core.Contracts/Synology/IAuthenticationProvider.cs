using System.Threading.Tasks;
using SynoDs.Core.Dal.BaseApi;

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
        /// Asynchronous call to Login
        /// </summary>
        Task<string> LoginAsync(LoginCredentials credentials);

        /// <summary>
        /// Asynchronous call to logout.
        /// </summary>
        Task<bool> LogoutAsync();
    }
}