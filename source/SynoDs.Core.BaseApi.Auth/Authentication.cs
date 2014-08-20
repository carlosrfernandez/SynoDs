namespace SynoDs.Core.BaseApi.Auth
{
    using System.Threading.Tasks;
    using SynoDs.Core.Api;
    using SynoDs.Core.CrossCutting.Common;
    using SynoDs.Core.CrossCutting.Model;
    using SynoDs.Core.Dal.HttpBase;
    using SynoDs.Core.Interfaces.Synology;
    using SynoDs.Core.Dal.BaseApi;

    public class Authentication : Base, IAuthenticationProvider
    {
        public bool IsLoggedIn { get; set; }
        
        public string Sid { get; set; }

        public Authentication()
        {
            IsLoggedIn = false;
        }

        /// <summary>
        /// Logs into the DiskStation with the supplied credentials.
        /// </summary>
        /// <returns>True if logged in and false if any errors occur.</returns>
        public async Task<bool> LoginAsync(LoginCredentials loginCredentials)
        {
            Validate.ArgumentIsNotNullOrEmpty(loginCredentials.UserName);
            Validate.ArgumentIsNotNullOrEmpty(loginCredentials.Password);
            Validate.ArgumentIsNotNullOrEmpty(loginCredentials.Uri);

            // store the data.
            DsAddress = loginCredentials.Uri;
            DsUsername = loginCredentials.UserName;
            DsPassword = loginCredentials.Password;

            var parameters = new RequestParameters
            {
                {"account", DsUsername},
                {"passwd", DsPassword},
                {"session", SessionName},
                {"format", "sid" }
            };
            var loginResult = await PerformOperationAsync<LoginResponse>(parameters);
            SessionId = loginResult.ResponseData.Sid;
            return loginResult.Success;
        }

        public async Task<bool> LogoutAsync()
        {
            IsLoggedIn = false;
            return true;
        }
    }
}
