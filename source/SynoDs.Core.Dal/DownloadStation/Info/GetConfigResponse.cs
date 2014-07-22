using SynoDs.Core.Dal.HttpBase;
namespace SynoDs.Core.Dal.DownloadStation.Info
{
    using System.Runtime.Serialization;
    
    [DataContract]
    public class GetConfigResponse : ResponseWrapper<Config>
    {
    }
}
