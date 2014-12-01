using SynoDs.Core.Dal.Attributes;
using SynoDs.Core.Dal.HttpBase;
namespace SynoDs.Core.Dal.DownloadStation.Info
{
    using System.Runtime.Serialization;
    
    [DataContract]
    [AuthenticationRequired(true)]
    public class GetConfigResponse : ResponseWrapper<Config>
    {
    }
}
