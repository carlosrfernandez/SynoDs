using System;
using SynoDs.Core.Contracts.Synology;
using SynoDs.Core.CrossCutting;
using SynoDs.Core.Dal.BaseApi;

namespace SynoDs.Core.Api
{
    public class DsSessionHandler : IDiskStationSessionHandler
    {
        public DsStationInfo DiskStation { get; private set; }
        public LoginCredentials Credentials { get; private set; }
        private IAuthenticationProvider _authenticationProvider;

        public string SessionId { get; set; }
        public bool UseSsl { get; set; }

        /// <summary>
        /// Will load all required dependencies. 
        /// </summary>
        /// <param name="dsStation"></param>
        /// <param name="credentials"></param>
        /// <param name="useSsl">Flag to determine if the requests will go through ssl</param>
        public void CreateSession(DsStationInfo dsStation, LoginCredentials credentials, bool useSsl = false)
        {
            this.DiskStation = dsStation;
            this.Credentials = credentials;
            this.SessionId = string.Empty;
            this.UseSsl = useSsl;

            _authenticationProvider = IoCFactory.Container.Resolve<IAuthenticationProvider>();
            if (!_authenticationProvider.LoginAsync(Credentials).Result)
            {
                throw new Exception("Error while establishing connection to the DS");
            }
        }
    }
}
