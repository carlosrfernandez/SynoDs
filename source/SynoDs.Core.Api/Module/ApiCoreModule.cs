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
    using Auth;
    using Http;
    using Info;
    using AttributeReader;
    using Contracts;
    using Contracts.Modularity;
    using Contracts.Synology;
    using CrossCutting;
    using Error;
    using JsonParser;
    using Microsoft.Practices.Unity;
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
            // Register Attribute reader.
            IoCFactory.Container.RegisterType<IAttributeReader, AttributeReader>();

            // Register Error data source.
            IoCFactory.Container.RegisterType<IErrorProvider, ErrorProvider>();

            // Register JSON parser.
            IoCFactory.Container.RegisterType<IJsonParser, JsonParser>();

            // Register HttpClient
            IoCFactory.Container.RegisterType<IHttpClient, HttpGetRequestClient>();

            // Register Information repo. 
            // This one needs a DiskStation Session to go to. 
            IoCFactory.Container.RegisterType<IInformationRepository, InformationRepository>();

            // Register Information provider.
            IoCFactory.Container.RegisterType<IInformationProvider, InformationProvider>();

            // Register the requestService 
            IoCFactory.Container.RegisterType<IRequestService, RequestService>();

            // Create
            IoCFactory.Container.RegisterType<IAuthenticationProvider, AuthenticationProvider>();
        }
    }
}