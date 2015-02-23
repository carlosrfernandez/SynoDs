using SynoDs.Core.Contracts;
using SynoDs.Core.Contracts.IoC;
using SynoDs.Core.Contracts.Modularity;
using SynoDs.Core.CrossCutting;

namespace SynoDs.Core.AttributeReader.Module
{
    public class AttributeReaderModule : IApiModule
    {
        public void Configure()
        {
            IoCFactory.Container.Register<IAttributeReader, AttributeReader>();
        }
    }
}
