using SynoDs.Core.Contracts.Modularity;
using SynoDs.Core.Contracts.Synology;
using SynoDs.Core.CrossCutting;

namespace SynoDs.Core.DownloadStation.Module
{
    public class DownloadStationModule : IApiModule
    {
        public void Configure()
        {
            IoCFactory.Container.Register<IDownloadStation, DownloadManager>();
        }
    }
}
