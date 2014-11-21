using System.Threading.Tasks;
using SynoDs.Core.Contracts.Synology;
using SynoDs.Core.CrossCutting;
using SynoDs.Core.CrossCutting.Common;
using SynoDs.Core.Dal.BaseApi;

namespace SynoDs.Core.Api
{
    /// <summary>
    /// This is the API base class. It contains a Generic PerformOperationAsync method that 
    /// can be used by the rest of the API's in order to communicate with the Diskstation.
    /// DONE: Convert to abstract.
    /// Done: Remove Login and InformationProvider Methods into separate projects.
    /// Done: Refactor the Info cache so that it is properly used in the base class (use information interface to access the Api Cache)
    /// TODO: Add the File Upload method for uploading torrents from the client application.
    /// TODO: Add known error handling of the API
    /// </summary>
    public class DsClient
    {
        // Api properties
        private readonly DsStationInfo _stationInfo;
        protected string SessionId { get; set; }
        protected const string SessionName = "DsBase";

        private IAuthenticationProvider AuthenticationProvider { get; set; }

        /// <summary>
        /// Overridable method to get the session name used to log out.
        /// </summary>
        /// <returns>The current session's name</returns>
        protected virtual string GetSessionName()
        {
            return SessionName;
        }

        /// <summary>
        /// Default parameterless constructor
        /// </summary>
        protected DsClient(DsStationInfo dsInfo, LoginCredentials credentials)
        {
            Validate.ArgumentIsNotNullOrEmpty(dsInfo);
            Validate.ArgumentIsNotNullOrEmpty(credentials);

            _stationInfo = dsInfo;

            if (AuthenticationProvider == null)
            {
                AuthenticationProvider = IoCFactory.Container.Resolve<IAuthenticationProvider>();
            }
            AuthenticationProvider.Credentials = credentials;
        }

        public async Task<bool> LoginAsync()
        {
            if (AuthenticationProvider == null)
                AuthenticationProvider = IoCFactory.Container.Resolve<IAuthenticationProvider>();
            return  await AuthenticationProvider.LoginAsync();
        }

        public async Task<bool> LogoutAsync()
        {
            if (AuthenticationProvider == null)
                AuthenticationProvider = IoCFactory.Container.Resolve<IAuthenticationProvider>();
            return await AuthenticationProvider.LogoutAsync();
        }
    }
}
