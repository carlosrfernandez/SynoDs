using SynoDs.Core.Dal.Attributes;
using SynoDs.Core.Dal.Enums;
using SynoDs.Core.Dal.HttpBase;

namespace SynoDs.Core.Dal.DownloadStation.Info
{
    using System.Runtime.Serialization;

    [Api(RootApi.DownloadStation, ChildApi.Info)]
    [DataContract]
    public class DsInfoResponse : ResponseWrapper<DsInfo>
    {
    }
}
