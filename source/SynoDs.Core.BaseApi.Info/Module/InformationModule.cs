using SynoDs.Core.CrossCutting;
using SynoDs.Core.Contracts.Modularity;
using SynoDs.Core.Contracts.Synology;

namespace SynoDs.Core.BaseApi.Info.Module
{
    public class InformationModule : IApiModule
    {
        public bool RequiresAuthenticatedRequests { get; private set; }

        public void Configure()
        {
            this.RequiresAuthenticatedRequests = false;
            IoCFactory.Container.Register<IInformationProvider, InformationProvider>();
        }
    }
}
