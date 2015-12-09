// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApiCoreModule.cs" company="">
//   
// </copyright>
// <summary>
//   The api core module.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynoDs.Core.Api.Module
{
    using SynoDs.Core.Api.Auth;
    using SynoDs.Core.Api.Http;
    using SynoDs.Core.Api.Info;
    using SynoDs.Core.Api.Operation;
    using SynoDs.Core.AttributeReader;
    using SynoDs.Core.Contracts;
    using SynoDs.Core.Contracts.Modularity;
    using SynoDs.Core.Contracts.Synology;
    using SynoDs.Core.CrossCutting;
    using SynoDs.Core.Error;
    using SynoDs.Core.JsonParser;

    /// <summary>
    ///     The API core module.
    /// </summary>
    public class ApiCoreModule : IApiModule
    {
        /// <summary>
        /// The configure.
        /// </summary>
        public void Configure()
        {
            // Register session handler. 
            IoCFactory.Container.Register<IDiskStationSessionHandler, DsSessionHandler>();

            // Register Attribute reader.
            IoCFactory.Container.Register<IAttributeReader, AttributeReader>();

            // Register Error data source.
            IoCFactory.Container.Register<IErrorProvider, ErrorProvider>();

            // Register JSON parser.
            IoCFactory.Container.Register<IJsonParser, JsonParser>();

            // Register HttpClient
            IoCFactory.Container.Register<IHttpClient, HttpGetRequestClient>();

            // Register Information repo. 
            // This one needs a DiskStation Session to go to. 
            IoCFactory.Container.Register<IInformationRepository, InformationRepository>();

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