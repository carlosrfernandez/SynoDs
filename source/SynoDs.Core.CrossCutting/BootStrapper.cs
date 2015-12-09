// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Bootstrapper.cs" company="">
//   
// </copyright>
// <summary>
//   The bootstrapper base.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynoDs.Core.CrossCutting
{
    using System;

    using SynoDs.Core.Contracts.IoC;

    /// <summary>
    /// The bootstrapper base.
    /// </summary>
    public abstract class BootstrapperBase : IBootstrapper
    {
        /// <summary>
        /// The _app module initialize.
        /// </summary>
        private readonly AppModulesCatalog _appModuleInitialize;

        /// <summary>
        /// Initializes a new instance of the <see cref="BootstrapperBase"/> class.
        /// </summary>
        /// <param name="modules">
        /// The modules.
        /// </param>
        protected BootstrapperBase(AppModulesCatalog modules)
        {
            this._appModuleInitialize = modules;
        }

        /// <summary>
        /// The startup.
        /// </summary>
        /// <exception cref="NullReferenceException">
        /// </exception>
        public virtual void Startup()
        {
            if (this._appModuleInitialize != null)
            {
                this._appModuleInitialize.InitCatalog();
            }
            else
            {
                throw new NullReferenceException("Error, you need to implement the ApiModulesCatalog class!");
            }
        }

        /// <summary>
        /// The shutdown.
        /// </summary>
        public abstract void Shutdown();

        /// <summary>
        /// The run.
        /// </summary>
        public abstract void Run();
    }
}