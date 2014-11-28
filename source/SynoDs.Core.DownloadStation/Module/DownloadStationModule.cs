using SynoDs.Core.Contracts.Modularity;
using SynoDs.Core.Contracts.Synology;
using SynoDs.Core.CrossCutting;

namespace SynoDs.Core.DownloadStation.Module
{
    public class DownloadStationModule : IApiModule
    {
        public bool RequiresAuthenticatedRequests { get; private set; }
        public void Configure()
        {
            RequiresAuthenticatedRequests = true;
            IoCFactory.Container.Register<IDownloadProvider, DownloadManager>();
        }
    }
}
