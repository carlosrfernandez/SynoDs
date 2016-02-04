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
    using Microsoft.Practices.Unity;

    /// <summary>
    /// The IOC factory.
    /// </summary>
    public class IoCFactory
    {
        /// <summary>
        /// The _container.
        /// </summary>
        private static IUnityContainer container;

        public IoCFactory(IUnityContainer container)
        {
            IoCFactory.container = container;
        }

        /// <summary>
        /// Gets the container.
        /// </summary>
        public static IUnityContainer Container => container ?? (container = new UnityContainer());
    }
}