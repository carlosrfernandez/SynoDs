using System.Collections.Generic;
using System.Runtime.Serialization;
using SynoDs.Core.Dal.FileStation.CreateFolder;

namespace SynoDs.Core.Dal.FileStation.List
{
    public class Children
    {
        [DataMember(Name = "total")]
        public int Total { get; set; }

        [DataMember(Name = "offset")]
        public int Offset { get; set; }

        [DataMember(Name = "files")]
        public IEnumerable<FileExtended> Shares { get; set; }
    }
}
