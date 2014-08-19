using SynoDs.Core.Interfaces.IoC;

namespace SynoDs.Core.CrossCutting.Modularity
{
    using Interfaces;

    public abstract class ModuleBase : IModule
    {
        protected ModuleBase(IContainer container)
        {
            if (container != null)
            {
                Container = container;
            }
        }
        public abstract void Configure();

        protected IContainer Container { get; private set; }
    }
}
