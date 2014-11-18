using SynoDs.Core.Contracts.IoC;

namespace SynoDs.Core.CrossCutting
{
    public abstract class BootstrapperBase : IBootstrapper
    {
        public IoCFactory Factory { get; set; }

        protected BootstrapperBase(IoCFactory factory)
        {
            Factory = factory;
        }

        public abstract void Startup();

        public abstract void Shutdown();
    }
}
