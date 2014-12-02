using SynoDs.Core.Contracts.IoC;

namespace SynoDs.Core.CrossCutting
{
    public abstract class BootstrapperBase : IBootstrapper
    {
        public ApiModulesCatalog ApiCatalog { get; set; }

        protected BootstrapperBase()
        {
            
        }

        public abstract void Startup();

        public abstract void Shutdown();

        protected void RegisterApiModules()
        {
            // this method will read all the assemblies and load them up with MEF
            if (ApiCatalog!= null)
                this.ApiCatalog.InitCatalog();
        }
    }
}
