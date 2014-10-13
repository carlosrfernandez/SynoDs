namespace SynoDs.Core.CrossCutting.Modularity
{
    using Interfaces.IoC;
    using Interfaces.Modularity;

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
