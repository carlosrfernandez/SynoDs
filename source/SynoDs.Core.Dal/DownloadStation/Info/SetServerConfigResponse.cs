using System.Runtime.Serialization;
using SynoDs.Core.Dal.Attributes;
using SynoDs.Core.Dal.Enums;
using SynoDs.Core.Dal.HttpBase;

namespace SynoDs.Core.Dal.DownloadStation.Info
{
    [DataContract]
    [Api(RootApi.DownloadStation, ChildApi.Info)]
    public class SetServerConfigResponse : ResponseWrapper<object>
    {
    }
}
