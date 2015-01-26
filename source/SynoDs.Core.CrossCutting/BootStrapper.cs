using System;
using SynoDs.Core.Contracts.IoC;

namespace SynoDs.Core.CrossCutting
{
    public abstract class BootstrapperBase : IBootstrapper
    {
        public ApiModulesCatalog ApiCatalog { get; set; }

        public abstract void Startup();

        public abstract void Shutdown();

        protected void RegisterApiModules()
        {
            // this method will read all the assemblies and load them up with MEF
            if (ApiCatalog != null)
                this.ApiCatalog.InitCatalog();
            else
                throw new NullReferenceException("Error, you need to implement the ApiModulesCatalog class!");

        }
    }
}
