using SynoDs.Core.Interfaces.IoC;

namespace SynoDs.Core.CrossCutting
{
// ReSharper disable once InconsistentNaming
    public class IoCFactory
    {
        private static IContainer _container;

        public static IContainer Container
        {
            get { return _container ?? (_container = new NinjectContainer()); }
        }
    }
}
