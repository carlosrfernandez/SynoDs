using System.Collections.Generic;

namespace SynoDs.Core.Contracts.Modularity
{
    public interface IModuleManager
    {
        IEnumerable<IApiModule> GetApplicationModules();
    }
}
