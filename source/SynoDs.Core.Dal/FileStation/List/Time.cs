using System.Runtime.Serialization;

namespace SynoDs.Core.Dal.FileStation.List
{
    [DataContract]
    public class Time
    {
        [DataMember(Name = "atime")]
        public long AccessTime { get; set; }

        [DataMember(Name = "mtime")]
        public long ModifiedTime { get; set; }

        [DataMember(Name = "ctime")]
        public long LastChangeTime { get; set; }

        [DataMember(Name = "crtime")]
        public long CreationTime { get; set; }
    }
}
