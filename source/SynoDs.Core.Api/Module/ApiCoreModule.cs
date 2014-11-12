using SynoDs.Core.Exceptions.ErrorHandling;
using SynoDs.Core.Exceptions.Http;
using SynoDs.Core.CrossCutting;
using SynoDs.Core.CrossCutting.Modularity;
using SynoDs.Core.Interfaces;
using SynoDs.Core.Interfaces.IoC;
using SynoDs.Core.Interfaces.Modularity;
using SynoDs.Core.Interfaces.Synology;
using SynoDs.Core.JsonParser;

namespace SynoDs.Core.Exceptions.Module
{
    public class ApiCoreModule : IApiModule
    {
        public bool RequiresAuthenticatedRequests { get; private set; }

        public void Configure()
        {
            // todo: verify usage of this variable across modules.
            RequiresAuthenticatedRequests = false; // initially this doesn't require authentication.
            IoCFactory.Container.Register<IErrorProvider, ErrorProviderBase>();
            IoCFactory.Container.Register<IOperationProvider, OperationProvider>();
            IoCFactory.Container.Register<IHttpClient, HttpGetRequestClient>();
            IoCFactory.Container.Register<IJsonParser, JsonParser.JsonParser>();
            IoCFactory.Container.Register<IAttributeReader, AttributeReader>();
        }
    }
}
