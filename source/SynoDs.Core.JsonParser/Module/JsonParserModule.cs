// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JsonParserModule.cs" company="">
//   
// </copyright>
// <summary>
//   The json parser module.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynoDs.Core.JsonParser.Module
{
    using Contracts;
    using Contracts.Modularity;
    using CrossCutting;
    using Microsoft.Practices.Unity;
    /// <summary>
    /// The json parser module.
    /// </summary>
    public class JsonParserModule : IApiModule
    {
        /// <summary>
        /// Gets a value indicating whether requires authenticated requests.
        /// </summary>
        public bool RequiresAuthenticatedRequests => false;

        /// <summary>
        /// The configure.
        /// </summary>
        public void Configure()
        {
            IoCFactory.Container.RegisterType<IJsonParser, JsonParser>();
        }
    }
}