using Microsoft.Practices.Unity;

namespace SynoDs.UWP.DependencyInitializer
{
    public class SynoDsDependencyInitializer
    {
        // static? or not?
        private IUnityContainer container;

        public SynoDsDependencyInitializer(IUnityContainer container)
        {
            this.container = container;
        }
    }
}
