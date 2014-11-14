using System.Threading.Tasks;
using SynoDs.Core.CrossCutting.Common;
using SynoDs.Core.Dal.BaseApi;
using SynoDs.Core.Dal.HttpBase;
using SynoDs.Core.Interfaces.Synology;

namespace SynoDs.Core.BaseApi.Auth
{
    public class Authentication : IAuthenticationProvider
    {
        public bool IsLoggedIn { get; set; }
        
        public string Sid { get; set; }

        private readonly IOperationProvider _operationProvider;

        private readonly LoginCredentials _credentials;

        private const string SessionName = "DiskStation";

        public Authentication(IOperationProvider operationProvider, LoginCredentials loginCredentials)
        {
            IsLoggedIn = false;
            _operationProvider = operationProvider;
            _credentials = loginCredentials;
        }

        /// <summary>
        /// Logs into the DiskStation with the supplied credentials.
        /// </summary>
        /// <returns>True if logged in and false if any errors occur.</returns>
        public async Task<bool> LoginAsync()
        {
            Validate.ArgumentIsNotNullOrEmpty(_credentials.UserName);
            Validate.ArgumentIsNotNullOrEmpty(_credentials.Password);

            // prepare request
            var parameters = new RequestParameters
            {
                {"account", _credentials.UserName},
                {"passwd", _credentials.Password},
                {"session", SessionName },
                {"format", "sid" }
            };
            var loginResult = await _operationProvider.PerformOperationAsync<LoginResponse>(parameters);
            Sid = loginResult.ResponseData.Sid;
            IsLoggedIn = true;
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