using SynoDs.Core.CrossCutting;
using SynoDs.Core.Interfaces.Modularity;
using SynoDs.Core.Interfaces.Synology;

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
