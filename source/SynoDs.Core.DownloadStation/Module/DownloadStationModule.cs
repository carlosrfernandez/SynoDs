using SynoDs.Core.Contracts.Modularity;
using SynoDs.Core.Contracts.Synology;
using SynoDs.Core.CrossCutting;
using Microsoft.Practices.Unity;

namespace SynoDs.Core.DownloadStation.Module
{
    public class DownloadStationModule : IApiModule
    {
        public void Configure()
        {
            IoCFactory.Container.RegisterType<IDownloadStation, DownloadManager>();
        }
    }
}
