using System.Collections.Generic;
using SynoDs.Core.Contracts.IoC;
using SynoDs.Core.Contracts.Modularity;

namespace SynoDs.Core.CrossCutting
{
    // TODO: this has to be implemented by 
    public abstract class AppModulesCatalog : IApiModuleCatalog
    {
        private readonly IEnumerable<IApiModule> _applicationModules; 
        // This will be implemented on a per platform project
        public AppModulesCatalog(IEnumerable<IApiModule> appModules)
        {
            _applicationModules = appModules;
        }

        public void InitCatalog()
        {
            foreach (var module in _applicationModules)
            {
                module.Configure();
            }
        }
    }
}
