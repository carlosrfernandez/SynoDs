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
    using SynoDs.Core.Contracts;
    using SynoDs.Core.Contracts.Modularity;
    using SynoDs.Core.CrossCutting;

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
            IoCFactory.Container.Register<IJsonParser, JsonParser>();
        }
    }
}