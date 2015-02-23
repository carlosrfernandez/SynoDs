using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SynoDs.Core.Contracts.Modularity;
using SynoDs.Core.CrossCutting;

namespace SynoDs.Universal.Infrastructure
{
    public class WPhoneAppModuleCatalog : AppModulesCatalog
    {
        public WPhoneAppModuleCatalog(IEnumerable<IApiModule> appModules) : base(appModules)
        {

        }
    }
}
