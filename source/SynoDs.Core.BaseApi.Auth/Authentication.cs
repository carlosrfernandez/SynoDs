namespace SynoDs.Core.BaseApi.Auth
{
    using System.Threading.Tasks;
    using Exceptions;
    using CrossCutting.Common;
    using Dal.HttpBase;
    using Interfaces.Synology;
    using Dal.BaseApi;
    using Interfaces;

    public class Authentication : Base, IAuthenticationProvider
    {
        public bool IsLoggedIn { get; set; }
        
        public string Sid { get; set; }

        private readonly IOperationProvider _operationProvider;

        private readonly LoginCredentials _credentials;

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

            // store the data.
            DsUsername = _credentials.UserName;
            DsPassword = _credentials.Password;

            var parameters = new RequestParameters
            {
                {"account", DsUsername},
                {"passwd", DsPassword},
                {"session", SessionName},
                {"format", "sid" }
            };
            var loginResult = await _operationProvider.PerformOperationAsync<LoginResponse>(parameters);
            SessionId = loginResult.ResponseData.Sid;
            IsLoggedIn = true;
            return loginResult.Success;
        }

        /// <summary>
        /// Logs out of the DiskStation. 
        /// </summary>
        /// <returns></returns>
        public async Task<bool> LogoutAsync()
        {
            var logoutParams = new RequestParameters
            {
                {"session", SessionName}
            };

            var logoutRequestResult = await _operationProvider.PerformOperationAsync<LogoutResponse>(logoutParams);
            SessionId = string.Empty; // erase the sid.
            IsLoggedIn = false;
            return logoutRequestResult.Success;
        }
    }
}