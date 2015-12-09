// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppModulesCatalog.cs" company="">
//   
// </copyright>
// <summary>
//   The app modules catalog.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynoDs.Core.CrossCutting
{
    using System.Collections.Generic;

    using SynoDs.Core.Contracts.IoC;
    using SynoDs.Core.Contracts.Modularity;

    // TODO: this has to be implemented by 
    /// <summary>
    /// The app modules catalog.
    /// </summary>
    public abstract class AppModulesCatalog : IApiModuleCatalog
    {
        /// <summary>
        /// The _application modules.
        /// </summary>
        private readonly IEnumerable<IApiModule> _applicationModules;

        // This will be implemented on a per platform project
        /// <summary>
        /// Initializes a new instance of the <see cref="AppModulesCatalog"/> class.
        /// </summary>
        /// <param name="appModules">
        /// The app modules.
        /// </param>
        public AppModulesCatalog(IEnumerable<IApiModule> appModules)
        {
            this._applicationModules = appModules;
        }

        /// <summary>
        /// The init catalog.
        /// </summary>
        public void InitCatalog()
        {
            foreach (var module in this._applicationModules)
            {
                module.Configure();
            }
        }
    }
}