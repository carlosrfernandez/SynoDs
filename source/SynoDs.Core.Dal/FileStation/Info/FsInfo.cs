namespace SynoDs.Core.Dal.FileStation.Info
{
    using System.Runtime.Serialization;
    using Attributes;

    [DataContract]
    [ApiMethod("getinfo")]
    public class FsInfo
    {
        [DataMember(Name = "hostname")]
        public string Hostname { get; set; }

        [DataMember(Name = "is_manager")]
        public bool IsManager { get; set; }

        [DataMember(Name = "support_virtual")]
        public string SupportVirtual { get; set; }

        [DataMember(Name = "support_sharing")]
        public bool SupportSharing { get; set; }
    }
}
