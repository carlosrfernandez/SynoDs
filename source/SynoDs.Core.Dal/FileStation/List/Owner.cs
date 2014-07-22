using System.Runtime.Serialization;

namespace SynoDs.Core.Dal.FileStation.List
{
    [DataContract]
    public class Owner
    {
        [DataMember(Name = "user")]
        public string User { get; set; }

        [DataMember(Name = "group")]
        public string Group { get; set; }

        [DataMember(Name = "uid")]
        public int Uid { get; set; }

        [DataMember(Name = "gid")]
        public int Gid { get; set; }
    }
}
