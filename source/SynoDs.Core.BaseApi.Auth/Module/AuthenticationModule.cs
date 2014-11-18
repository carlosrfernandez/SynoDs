using SynoDs.Core.CrossCutting;
using SynoDs.Core.Contracts.Modularity;
using SynoDs.Core.Contracts.Synology;

namespace SynoDs.Core.BaseApi.Auth.Module
{
    public class AuthenticationModule : IApiModule
    {
        public bool RequiresAuthenticatedRequests { get; private set; }

        public void Configure()
        {
            IoCFactory.Container.Register<IAuthenticationProvider, Authentication>();
            RequiresAuthenticatedRequests = false;
        }
    }
}
