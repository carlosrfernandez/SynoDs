using SynoDs.Core.Contracts.IoC;

namespace SynoDs.Core.CrossCutting
{
    public class IoCFactory
    {
        private static IContainer _container;

        public static IContainer Container
        {
            get { return _container ?? (_container = new NinjectContainer()); }
        }
    }
}
