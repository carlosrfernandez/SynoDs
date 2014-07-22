using System.Runtime.Serialization;

namespace SynoDs.Core.Dal.FileStation.List
{
    [DataContract]
    public class Shares
    {
        [DataMember(Name = "path")]
        public string Path { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
        
        [DataMember(Name = "additional")]
        public SharesAdditional Additional { get; set; }
    }
}
