using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SynoDs.Core.Contracts.Modularity;
using SynoDs.Core.CrossCutting;

namespace SynoDs.Universal
{
    public class WindowsPhoneBootstrapper : BootstrapperBase
    {
        public WindowsPhoneBootstrapper(AppModulesCatalog modules) : base(modules)
        {
        }
        public override void Shutdown()
        {
            
        }

        public override void Run()
        {
            // Todo: Implement Windows Phone Runner
        }
    }
}
