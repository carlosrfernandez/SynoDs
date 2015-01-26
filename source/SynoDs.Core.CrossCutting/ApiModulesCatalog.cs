using SynoDs.Core.Contracts.IoC;

namespace SynoDs.Core.CrossCutting
{
    // TODO: this has to be implemented by 
    public abstract class ApiModulesCatalog : IApiModuleCatalog
    {
        // This will be implemented on a per platform project
        public abstract void InitCatalog();
    }
}
