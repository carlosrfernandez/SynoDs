using System.Runtime.Serialization;

namespace SynoDs.Core.Dal.FileStation.List
{
    [DataContract]
    public class SharesAdditional
    {
        [DataMember(Name="real_path")]
        public string RealPath { get; set; }

        [DataMember(Name = "owner")]
        public Owner Owner { get; set; }

        [DataMember(Name="time")]
        public Time Time { get; set; }

        [DataMember(Name="perm")]
        public Permission Permission { get; set; }

        [DataMember(Name="mount_point_type")]
        public string MountPointType { get; set; }

        [DataMember(Name = "volume_status")]
        public VolumeStatus VolumeStatus { get; set; }
    }
}
