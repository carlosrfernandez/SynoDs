using System.Runtime.Serialization;

namespace SynoDs.Core.Dal.FileStation.List
{
    [DataContract]
    public class FileAdditional
    {
        [DataMember(Name = "real_path")]
        public string RealPath { get; set; }

        [DataMember(Name = "size")]
        public int Size { get; set; }

        [DataMember(Name = "owner")]
        public Owner Owner { get; set; }

        [DataMember(Name = "time")]
        public Time Time { get; set; }

        [DataMember(Name = "perm")]
        public Permission Permission { get; set; }

        [DataMember(Name = "mount_point_type")]
        public string MountPointType { get; set; }

        [DataMember(Name = "type")]
        public string Extension { get; set; }
    }
}
