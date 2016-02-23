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
            this.container.RegisterType<IAttributeReader, AttributeReader>(new ContainerControlledLifetimeManager());

            // Register Error data source.
            this.container.RegisterType<IErrorProvider, ErrorProvider>(new ContainerControlledLifetimeManager());

            // Register JSON parser.
            this.container.RegisterType<IJsonParser, JsonParser>(new ContainerControlledLifetimeManager());

            // Register HttpClient
            this.container.RegisterType<IHttpClient, HttpGetRequestClient>(new ContainerControlledLifetimeManager());

            // Register Information repo. 
            // This one needs a DiskStation Session to go to. 
            this.container.RegisterType<IInformationRepository, InformationRepository>(new ContainerControlledLifetimeManager());

            // Register Information provider.
            this.container.RegisterType<IInformationProvider, InformationProvider>(new ContainerControlledLifetimeManager());

            // Register the requestService 
            this.container.RegisterType<IRequestService, RequestService>(new ContainerControlledLifetimeManager());

            // Create
            this.container.RegisterType<IAuthenticationProvider, AuthenticationProvider>(new ContainerControlledLifetimeManager());
        }
    }
}