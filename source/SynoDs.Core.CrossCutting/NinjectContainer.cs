// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NinjectContainer.cs" company="">
//   
// </copyright>
// <summary>
//   The ninject container.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynoDs.Core.CrossCutting
{
    using System;

    using Ninject;

    using SynoDs.Core.Contracts.IoC;

    /// <summary>
    /// The ninject container.
    /// </summary>
    public class NinjectContainer : IContainer
    {
        /// <summary>
        /// The _container.
        /// </summary>
        private readonly IKernel _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectContainer"/> class.
        /// </summary>
        public NinjectContainer()
        {
            this._container = new StandardKernel();
        }

        /// <summary>
        /// The resolve.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public object Resolve<T>()
        {
            return this._container.Get<T>();
        }

        /// <summary>
        /// The resolve.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public object Resolve(Type type)
        {
            return this._container.Get(type);
        }

        /// <summary>
        /// The register with instance.
        /// </summary>
        /// <param name="instance">
        /// The instance.
        /// </param>
        /// <typeparam name="TAbs">
        /// </typeparam>
        /// <typeparam name="TImpl">
        /// </typeparam>
        public void RegisterWithInstance<TAbs, TImpl>(TImpl instance) where TImpl : TAbs
        {
            if (!this._container.CanResolve<TAbs>())
            {
                this._container.Bind<TAbs>().ToConstant(instance);
            }
        }

        /// <summary>
        /// The register.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <typeparam name="TR">
        /// </typeparam>
        public void Register<T, TR>() where TR : T
        {
            if (!this._container.CanResolve<TR>())
            {
                this._container.Bind<T>().To<TR>();
            }
        }
    }
}