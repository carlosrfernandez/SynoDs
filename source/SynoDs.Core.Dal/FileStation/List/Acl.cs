using System.Runtime.Serialization;

namespace SynoDs.Core.Dal.FileStation.List
{
    [DataContract]
    public class Acl
    {
        [DataMember(Name = "append")]
        public bool Append { get; set; }

        [DataMember(Name = "del")]
        public bool Delete { get; set; }

        [DataMember(Name = "exec")]
        public bool Execute { get; set; }

        [DataMember(Name = "read")]
        public bool Read { get; set; }

        [DataMember(Name = "write")]
        public bool Write { get; set; }       
    }
}
