using System.Runtime.Serialization;
using SynoDs.Core.Dal.FileStation.List;

namespace SynoDs.Core.Dal.FileStation.CreateFolder
{
    public class File
    {
        [DataMember(Name = "path")]
        public string Path { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "isdir")]
        public bool IsDir { get; set; }

        [DataMember(Name = "additional")]
        public FileAdditional Additional { get; set; }
    }
}
