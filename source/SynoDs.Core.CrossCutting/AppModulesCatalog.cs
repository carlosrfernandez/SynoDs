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

    /// <summary>
    /// The app modules catalog.
    /// </summary>
    public abstract class AppModulesCatalog : IApiModuleCatalog
    {
        /// <summary>
        /// The _application modules.
        /// </summary>
        private readonly IEnumerable<IApiModule> applicationModules;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="AppModulesCatalog"/> class.
        /// This will be implemented on a per platform project
        /// </summary>
        /// <param name="appModules">
        /// The app modules.
        /// </param>
        public AppModulesCatalog(IEnumerable<IApiModule> appModules)
        {
            this.applicationModules = appModules;
        }

        /// <summary>
        /// The init catalog.
        /// </summary>
        public void InitCatalog()
        {
            foreach (var module in this.applicationModules)
            {
                module.Configure();
            }
        }
    }
}