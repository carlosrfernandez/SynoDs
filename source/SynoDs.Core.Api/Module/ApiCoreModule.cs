using SynoDs.Core.Api.Http;
using SynoDs.Core.Api.Info;
using SynoDs.Core.Api.Auth;
using SynoDs.Core.CrossCutting;
using SynoDs.Core.Contracts;
using SynoDs.Core.Contracts.Modularity;
using SynoDs.Core.Contracts.Synology;
using SynoDs.Core.Error;

namespace SynoDs.Core.Api.Module
{
    public class ApiCoreModule : IApiModule
    {
        public bool RequiresAuthenticatedRequests { get; private set; }

        public void Configure()
        {
            // todo: verify usage of this variable across modules.
            RequiresAuthenticatedRequests = false; // initially this doesn't require authentication.

            IoCFactory.Container.RegisterWithInstance<IAttributeReader, AttributeReader.AttributeReader>(
                new AttributeReader.AttributeReader());
            IoCFactory.Container.RegisterWithInstance<IErrorProvider, ErrorProvider>(
                new ErrorProvider(IoCFactory.Container.Resolve<IAttributeReader>()));
            IoCFactory.Container.RegisterWithInstance<IJsonParser, JsonParser.JsonParser>(
                new JsonParser.JsonParser(IoCFactory.Container.Resolve<IErrorProvider>()));
            
            IoCFactory.Container.RegisterWithInstance<IHttpClient, HttpGetRequestClient>(
                new HttpGetRequestClient());

            // these need to be initialized
            IoCFactory.Container.Register<IOperationProvider, OperationProvider>(); 
            
            IoCFactory.Container.Register<IInformationProvider, InformationProvider>();
            var op = IoCFactory.Container.Resolve<IOperationProvider>();
            IoCFactory.Container.RegisterWithInstance<IAuthenticationProvider, AuthenticationProvider>(
                new AuthenticationProvider(IoCFactory.Container.Resolve<IOperationProvider>()));


            IoCFactory.Container.Register<IRequestProvider, RequestProvider>();
        }
    }
}
