using System;
using SynoDs.Core.CrossCutting;

namespace SynoDs.WPF.Client.Bootstrapper
{
    public class WpfBootstrapper : BootstrapperBase
    {
        public WpfBootstrapper(AppModulesCatalog modules) : base(modules)
        {

        }

        public override void Startup()
        {
            
        }

        public override void Shutdown()
        {
        }

        public override void Run()
        {
            throw new NotImplementedException();
        }
    }
}
