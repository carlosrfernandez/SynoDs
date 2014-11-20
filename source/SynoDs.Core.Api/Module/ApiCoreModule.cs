using SynoDs.Core.Api.Http;
using SynoDs.Core.CrossCutting;
using SynoDs.Core.Contracts;
using SynoDs.Core.Contracts.Modularity;
using SynoDs.Core.Contracts.Synology;

namespace SynoDs.Core.Api.Module
{
    public class ApiCoreModule : IApiModule
    {
        public bool RequiresAuthenticatedRequests { get; private set; }

        public void Configure()
        {
            // todo: verify usage of this variable across modules.
            RequiresAuthenticatedRequests = false; // initially this doesn't require authentication.
            IoCFactory.Container.Register<IOperationProvider, OperationProvider>();
            IoCFactory.Container.Register<IHttpClient, HttpGetRequestClient>();
        }
    }
}
