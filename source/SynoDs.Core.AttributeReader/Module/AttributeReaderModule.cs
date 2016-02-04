// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AttributeReaderModule.cs" company="">
//   
// </copyright>
// <summary>
//   The attribute reader module.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynoDs.Core.AttributeReader.Module
{
    using Contracts;
    using Contracts.Modularity;
    using CrossCutting;
    using Microsoft.Practices.Unity;

    /// <summary>
    /// The attribute reader module.
    /// </summary>
    public class AttributeReaderModule : IApiModule
    {
        /// <summary>
        /// The configure.
        /// </summary>
        public void Configure()
        {
            IoCFactory.Container.RegisterType<IAttributeReader, AttributeReader>();
        }
    }
}
