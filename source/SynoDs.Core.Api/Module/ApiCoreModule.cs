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
        private readonly IUnityContainer container;

        public ApiCoreModule(IUnityContainer container)
        {
            this.container = container;
        }

        /// <summary>
        /// The configure.
        /// </summary>
        public void Configure()
        {
            // Register Attribute reader.
            this.container.RegisterType<IAttributeReader, AttributeReader>();

            // Register Error data source.
            this.container.RegisterType<IErrorProvider, ErrorProvider>();

            // Register JSON parser.
            this.container.RegisterType<IJsonParser, JsonParser>();

            // Register HttpClient
            this.container.RegisterType<IHttpClient, HttpGetRequestClient>();

            // Register Information repo. 
            // This one needs a DiskStation Session to go to. 
            this.container.RegisterType<IInformationRepository, InformationRepository>();

            // Register Information provider.
            this.container.RegisterType<IInformationProvider, InformationProvider>();

            // Register the requestService 
            this.container.RegisterType<IRequestService, RequestService>();

            // Create
            this.container.RegisterType<IAuthenticationProvider, AuthenticationProvider>();
        }
    }
}