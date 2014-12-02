using SynoDs.Core.Contracts.Modularity;

namespace SynoDs.Core.CrossCutting.Modularity
{
    public abstract class ModuleBase : IApiModule
    {
        public abstract void Configure();
    }
}
