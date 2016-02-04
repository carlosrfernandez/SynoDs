// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DependencyInitTest.cs" company="">
//   
// </copyright>
// <summary>
//   The dependency init test.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynologyTests
{
    using System;
    using NUnit.Framework;

    using SynoDs.Core.Api;
    using SynoDs.Core.Api.Auth;
    using SynoDs.Core.Api.Http;
    using SynoDs.Core.Api.Info;
    using SynoDs.Core.Api.Module;
    using SynoDs.Core.AttributeReader;
    using SynoDs.Core.Contracts;
    using SynoDs.Core.Contracts.IoC;
    using SynoDs.Core.Contracts.Synology;
    using SynoDs.Core.CrossCutting;
    using SynoDs.Core.Error;
    using SynoDs.Core.JsonParser;

    /// <summary>
    /// The dependency init test.
    /// </summary>
    [TestFixture]
    public class DependencyInitTest
    {
        /// <summary>
        /// Gets or sets the container.
        /// </summary>
        private IContainer Container { get; set; }

        /// <summary>
        /// The resolve test.
        /// </summary>
        /// <param name="interfaceType">
        /// The interface type.
        /// </param>
        /// <param name="expectedType">
        /// The expected type.
        /// </param>
        [TestCase(typeof(IAttributeReader), typeof(AttributeReader))]
        [TestCase(typeof(IErrorProvider), typeof(ErrorProvider))]
        [TestCase(typeof(IJsonParser), typeof(JsonParser))]
        [TestCase(typeof(IHttpClient), typeof(HttpGetRequestClient))]
        [TestCase(typeof(IInformationRepository), typeof(InformationRepository))]
        [TestCase(typeof(IInformationProvider), typeof(InformationProvider))]
        [TestCase(typeof(IRequestService), typeof(RequestService))]
        [TestCase(typeof(IAuthenticationProvider), typeof(AuthenticationProvider))]
        public void ResolveTest(Type interfaceType, Type expectedType)
        {
            var resolvedtype = this.Container.Resolve(interfaceType);

            Assert.IsInstanceOf(expectedType, resolvedtype);
        }

        /// <summary>
        /// The test api base init.
        /// </summary>
        [OneTimeSetUp]
        public void TestSetup()
        {
            this.Container = new NinjectContainer();

            var factory = new IoCFactory(this.Container);

            var apiCoreModule = new ApiCoreModule();

            apiCoreModule.Configure();
        }
    }
}