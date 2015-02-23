using System.Collections.Generic;
using SynoDs.Core.Contracts.Modularity;

namespace SynoDs.Universal.Infrastructure
{
    public abstract class WinRtAppModuleCatalogInitializer
    {
        public abstract IEnumerable<IApiModule> GetApplicationModules();
    }
}
