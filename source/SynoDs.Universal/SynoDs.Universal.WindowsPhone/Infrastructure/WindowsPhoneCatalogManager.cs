using System.Collections;
using System.Collections.Generic;
using SynoDs.Core.Api.Module;
using SynoDs.Core.AttributeReader;
using SynoDs.Core.AttributeReader.Module;
using SynoDs.Core.Contracts.Modularity;
using SynoDs.Core.DownloadStation.Module;

namespace SynoDs.Universal.Infrastructure
{
    public class WindowsPhoneCatalogManager : IModuleManager
    {
        public IEnumerable<IApiModule> GetApplicationModules()
        {
            return new List<IApiModule>
            {
                new ApiCoreModule(),
                new AttributeReaderModule(),
                new DownloadStationModule()
            };
        }
    }
}
