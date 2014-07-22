using System.Runtime.Serialization;

namespace SynoDs.Core.Dal.FileStation.List
{
    [DataContract]
    public class FileExtended
    {
        [DataMember(Name = "path")]
        public string Path { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "isdir")]
        public bool IsDir { get; set; }

        [DataMember(Name = "children")]
        public Children Children { get; set; }

        [DataMember(Name = "additional")]
        public FileAdditional Additional { get; set; }
    }
}
