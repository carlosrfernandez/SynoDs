using System.Threading.Tasks;
using SynoDs.Core.Contracts.Synology;
using SynoDs.Core.CrossCutting.Common;
using SynoDs.Core.Dal.BaseApi;
using SynoDs.Core.Dal.HttpBase;

namespace SynoDs.Core.Api.Auth
{
    public class AuthenticationProvider : IAuthenticationProvider
    {
        public bool IsLoggedIn { get; set; }

        public bool IsLoggingIn { get; set; } // to control logging in process.

        public string Sid { get; set; }

        private readonly IOperationProvider _operationProvider;

        private const string SessionName = "DiskStation";

        public AuthenticationProvider(IOperationProvider operationProvider)
        {
            IsLoggedIn = false;
            _operationProvider = operationProvider;
        }

        /// <summary>
        /// Logs into the DiskStation with the supplied credentials.
        /// </summary>
        /// <returns>True if logged in and false if any errors occur.</returns>
        public async Task<bool> LoginAsync(LoginCredentials credentials)
        {
            Validate.ArgumentIsNotNullOrEmpty(credentials.UserName);
            Validate.ArgumentIsNotNullOrEmpty(credentials.Password);

            // prepare request
            var parameters = new RequestParameters
            {
                {"account", credentials.UserName},
                {"passwd", credentials.Password},
                {"session", SessionName },
                {"format", "sid" }
            };
            this.IsLoggingIn = true;
            var loginResult = await _operationProvider.PerformOperationAsync<LoginResponse>(parameters);
            Sid = loginResult.ResponseData.Sid;
            IsLoggedIn = true;
            IsLoggingIn = false;
            return loginResult.Success;
        }

        /// <summary>
        /// Logs out of the DiskStation. 
        /// </summary>
        /// <returns>True if logged in, false in case of errors.</returns>
        public async Task<bool> LogoutAsync()
        {
            var logoutParams = new RequestParameters
            {
                {"session", SessionName}
            };

            var logoutRequestResult = await _operationProvider.PerformOperationAsync<LogoutResponse>(logoutParams);
            Sid = string.Empty; // erase the sid.
            IsLoggedIn = false;
            return logoutRequestResult.Success;
        }
    }
}
