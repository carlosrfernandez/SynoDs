using SynoDs.Core.Contracts.IoC;

namespace SynoDs.Core.CrossCutting
{
    public abstract class BootstrapperBase : IBootstrapper
    {
        public abstract void Startup();

        public abstract void Shutdown();
    }
}
