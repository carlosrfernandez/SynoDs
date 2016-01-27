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
    using SynoDs.Core.Contracts;
    using SynoDs.Core.Contracts.Modularity;
    using SynoDs.Core.CrossCutting;

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
            IoCFactory.Container.Register<IAttributeReader, AttributeReader>();
        }
    }
}
