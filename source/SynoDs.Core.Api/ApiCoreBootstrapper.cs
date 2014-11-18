using SynoDs.Core.CrossCutting;

namespace SynoDs.Core.Api
{
    public class ApiCoreBootstrapper : BootstrapperBase
    {
        // todo: make sure all modules are loaded into the container and all dependencies resolved.
        // todo: inject all constructor dependencies here.
        public ApiCoreBootstrapper(IoCFactory factory) : base(factory)
        {

        }

        public override void Startup()
        {
            
        }

        public override void Shutdown()
        {
            
        }
    }
}
