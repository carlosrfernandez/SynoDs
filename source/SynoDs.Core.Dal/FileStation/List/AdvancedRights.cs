using System.Runtime.Serialization;

namespace SynoDs.Core.Dal.FileStation.List
{
    [DataContract]
    public class AdvancedRights
    {
        [DataMember(Name = "disable_download")]
        public bool DisabledDownloads { get; set; }

        [DataMember(Name = "disable_list")]
        public bool DisabledList { get; set; }

        [DataMember(Name = "disable_modify")]
        public bool DisabledModify { get; set; }
    }
}
