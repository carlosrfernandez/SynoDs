using System;
using System.Threading.Tasks;
using SynoDs.Core.Contracts.Synology;
using SynoDs.Core.CrossCutting;
using SynoDs.Core.Contracts;

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
