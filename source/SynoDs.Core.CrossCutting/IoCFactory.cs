// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IoCFactory.cs" company="">
//   
// </copyright>
// <summary>
//   The io c factory.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynoDs.Core.CrossCutting
{
    using SynoDs.Core.Contracts.IoC;

    /// <summary>
    /// The IOC factory.
    /// </summary>
    public class IoCFactory
    {
        /// <summary>
        /// The _container.
        /// </summary>
        private static IContainer container;

        public IoCFactory(IContainer container)
        {
            IoCFactory.container = container;
        }

        /// <summary>
        /// Gets the container.
        /// </summary>
        public static IContainer Container => container ?? (container = new NinjectContainer());
    }
}