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
        bool IsManager { get; set; }

        [DataMember(Name = "support_virtual")]
        string SupportVirtual { get; set; }

        [DataMember(Name = "support_sharing")]
        bool SupportSharing { get; set; }
    }
}
