using SynoDs.Core.Api.Http;
using SynoDs.Core.Api.Info;
using SynoDs.Core.Api.Auth;
using SynoDs.Core.Api.Operation;
using SynoDs.Core.CrossCutting;
using SynoDs.Core.Contracts;
using SynoDs.Core.Contracts.Modularity;
using SynoDs.Core.Contracts.Synology;
using SynoDs.Core.Error;

namespace SynoDs.Core.Api.Module
{
    public class ApiCoreModule : IApiModule
    {
        public void Configure()
        {
            // Register session handler. 
            IoCFactory.Container.RegisterWithInstance<IDiskStationSessionHandler, DsSessionHandler>(
                new DsSessionHandler());

            // Register Attribute reader.
            IoCFactory.Container.RegisterWithInstance<IAttributeReader, AttributeReader.AttributeReader>(
                new AttributeReader.AttributeReader());

            // Register Error data source.
            IoCFactory.Container.RegisterWithInstance<IErrorProvider, ErrorProvider>(
                new ErrorProvider(IoCFactory.Container.Resolve<IAttributeReader>()));

            // Register JSON parser.
            IoCFactory.Container.RegisterWithInstance<IJsonParser, JsonParser.JsonParser>(
                new JsonParser.JsonParser(IoCFactory.Container.Resolve<IErrorProvider>()));
            
            // Register HttpClient
            IoCFactory.Container.RegisterWithInstance<IHttpClient, HttpGetRequestClient>(
                new HttpGetRequestClient());

            // Register Information repo. 
            // This one needs a DiskStation Session to go to. 
            IoCFactory.Container.RegisterWithInstance<IInformationRepository, InformationRepository>(
                new InformationRepository(IoCFactory.Container.Resolve<IDiskStationSessionHandler>(), 
                    IoCFactory.Container.Resolve<IHttpClient>(), 
                    IoCFactory.Container.Resolve<IJsonParser>()));

            // Register Operations. 
            IoCFactory.Container.Register<IOperationProvider, OperationProvider>(); 
            
            // Register Information provider.
            IoCFactory.Container.Register<IInformationProvider, InformationProvider>();

            // Register the RequestProvider 
            IoCFactory.Container.Register<IRequestProvider, RequestProvider>();

            // Create
            IoCFactory.Container.Register<IAuthenticationProvider, AuthenticationProvider>();
        }
    }
}
