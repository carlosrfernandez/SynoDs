using System;
using System.Collections.Generic;
using System.Text;
using SynoDs.Core.Contracts.Modularity;

namespace SynoDs.Universal.Infrastructure
{
    public abstract class UniversalAppModuleCatalogInitializer
    {
        public abstract IEnumerable<IApiModule> GetApplicationModules();
    }
}
