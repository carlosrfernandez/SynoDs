using SynoDs.Core.Contracts.Modularity;

namespace SynoDs.Core.CrossCutting.Modularity
{
    public abstract class ModuleBase : IApiModule
    {
        public bool RequiresAuthenticatedRequests { get; private set; }
        public abstract void Configure();
    }
}
