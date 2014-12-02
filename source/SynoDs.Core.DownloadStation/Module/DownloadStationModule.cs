using System.Composition;
using SynoDs.Core.Contracts.Modularity;
using SynoDs.Core.Contracts.Synology;
using SynoDs.Core.CrossCutting;
using SynoDs.Core.CrossCutting.Modularity;

namespace SynoDs.Core.DownloadStation.Module
{
    [ApiModule("DownloadStation")]
    public class DownloadStationModule : IApiModule
    {
        public void Configure()
        {
            IoCFactory.Container.Register<IDownloadStation, DownloadManager>();
        }
    }
}
