using System;
using SynoDs.Core.Contracts.IoC;

namespace SynoDs.Core.CrossCutting
{
    public abstract class BootstrapperBase : IBootstrapper
    {
        private readonly AppModulesCatalog _appModuleInitialize;

        protected BootstrapperBase(AppModulesCatalog modules)
        {
            _appModuleInitialize = modules;
        }

        public virtual void Startup()
        {
            if (_appModuleInitialize != null)
                _appModuleInitialize.InitCatalog();
            else
                throw new NullReferenceException("Error, you need to implement the ApiModulesCatalog class!");
        }

        public abstract void Shutdown();

        public abstract void Run();
    }
}