﻿namespace SynoDs.Core.BaseApi.Auth
{
    using System.Threading.Tasks;
    using Exceptions;
    using CrossCutting.Common;
    using Dal.HttpBase;
    using Interfaces.Synology;
    using Dal.BaseApi;
    using ErrorHandling;
    using Interfaces;

    public class Authentication : Base, IAuthenticationProvider
    {
        #region singleton stuff - might not use
        private static Authentication _instance;

        private static readonly object SyncRoot = new object();

        public static IAuthenticationProvider Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new Authentication();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion

        public bool IsLoggedIn { get; set; }
        
        public string Sid { get; set; }

        private readonly IErrorProvider _authErrorProvider;

        public Authentication()
        {
            _authErrorProvider = new AuthenticationErrorProvider();
            IsLoggedIn = false;
        }

        public Authentication(IErrorProvider authErrorProvider)
        {
            IsLoggedIn = false;
            this._authErrorProvider = authErrorProvider;
        }

        /// <summary>
        /// Logs into the DiskStation with the supplied credentials.
        /// </summary>
        /// <returns>True if logged in and false if any errors occur.</returns>
        public async Task<bool> LoginAsync(LoginCredentials credentials)
        {
            Validate.ArgumentIsNotNullOrEmpty(credentials.UserName);
            Validate.ArgumentIsNotNullOrEmpty(credentials.Password);
            Validate.ArgumentIsNotNullOrEmpty(credentials.Uri);

            // store the data.
            DsAddress = credentials.Uri;
            DsUsername = credentials.UserName;
            DsPassword = credentials.Password;

            var parameters = new RequestParameters
            {
                {"account", DsUsername},
                {"passwd", DsPassword},
                {"session", SessionName},
                {"format", "sid" }
            };
            var loginResult = await PerformOperationAsync<LoginResponse>(parameters);
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

            var logoutRequestResult = await PerformOperationAsync<LogoutResponse>(logoutParams);
            SessionId = string.Empty; // erase the sid.
            IsLoggedIn = false;
            return logoutRequestResult.Success;
        }

        protected override IErrorProvider ErrorProvider
        {
            get { return this._authErrorProvider; }
        }
    }
}