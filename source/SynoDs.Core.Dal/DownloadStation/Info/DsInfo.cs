using SynoDs.Core.Dal.Attributes;

namespace SynoDs.Core.Dal.DownloadStation.Info
{
    using System.Runtime.Serialization;
    
    [DataContract]
    [ApiMethod("getinfo")]
    public class DsInfo
    {
        [DataMember(Name = "version")]
        public int Version { get; set; }

        [DataMember(Name = "version_string")]
        public string VersionString { get; set; }

        [DataMember(Name = "is_manager")]
        public bool IsManager { get; set; }
    }
}
