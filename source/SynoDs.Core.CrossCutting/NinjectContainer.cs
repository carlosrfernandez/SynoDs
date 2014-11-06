using SynoDs.Core.Interfaces.IoC;
using Ninject;

namespace SynoDs.Core.CrossCutting
{
    public class NinjectContainer : IContainer
    {
        private readonly IKernel _container;

        public NinjectContainer()
        {
            this._container = new StandardKernel();
        }
        
        public T Resolve<T>()
        {
            return _container.Get<T>();
        }

        public void RegisterWithInstance<TAbs, TImpl>(TImpl instance) where TImpl : TAbs
        {
            if (!this._container.CanResolve<TAbs>())
            {
                this._container.Bind<TAbs>().ToConstant(instance);
            }
        }

        public void Register<T, TR>() where TR : T
        {
            if (!this._container.CanResolve<TR>())
            {
                this._container.Bind<T>().To<TR>();
            }
        }
    }
}
