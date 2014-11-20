using SynoDs.Core.Contracts;
using SynoDs.Core.Contracts.Modularity;
using SynoDs.Core.CrossCutting;

namespace SynoDs.Core.AttributeReader.Module
{
    public class AttributeReaderModule : IApiModule
    {
        public bool RequiresAuthenticatedRequests { get; private set; }
        public void Configure()
        {
            RequiresAuthenticatedRequests = false;
            IoCFactory.Container.Register<IAttributeReader, AttributeReader>();
        }
    }
}
