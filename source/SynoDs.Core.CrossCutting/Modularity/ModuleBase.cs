namespace SynoDs.Core.CrossCutting.Modularity
{
    using Interfaces.IoC;
    using Interfaces.Modularity;

    public abstract class ModuleBase : IApiModule
    {
        public bool RequiresAuthenticatedRequests { get; private set; }
        public abstract void Configure();
    }
}
