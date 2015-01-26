using SynoDs.Core.Contracts.Modularity;
using SynoDs.Core.Contracts.Synology;
using SynoDs.Core.CrossCutting;

namespace SynoDs.Core.FileStation.Module
{
    public class FileStationModule : IApiModule
    {
        public void Configure()
        {
            IoCFactory.Container.Register<IFileStation, FileStation>();
        }
    }
}
