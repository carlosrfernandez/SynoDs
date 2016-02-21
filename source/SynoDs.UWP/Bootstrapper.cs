using System;
using Microsoft.Practices.Unity;
using SynoDs.Core.Contracts.IoC;
using SynoDs.Core.Api.Module;

namespace SynoDs.UWP
{
    internal class Bootstrapper : IBootstrapper
    {
        private IUnityContainer container;

        public Bootstrapper(IUnityContainer container)
        {
            this.container = container;
        }

        public void Shutdown()
        {
            this.container.Dispose();
        }

        public void Startup()
        {
            var coreModule = new ApiCoreModule(container);
            coreModule.Configure();
        }
    }
}