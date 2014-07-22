using System.Runtime.Serialization;

namespace SynoDs.Core.Dal.FileStation.List
{
    [DataContract]
    public class VolumeStatus
    {
        [DataMember(Name="freespace")]
        public int FreeSpace { get; set; }

        [DataMember(Name = "totalspace")]
        public int TotalSpace { get; set; }

        [DataMember(Name = "readonly")]
        public bool ReadOnly { get; set; }
    }
}
