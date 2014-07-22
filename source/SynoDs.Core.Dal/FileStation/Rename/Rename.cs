namespace SynoDs.Core.Dal.FileStation.Rename
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using CreateFolder;
    using Attributes;

    [DataContract]
    [ApiMethod("rename")]
    public class Rename
    {
        [DataMember(Name= "files")]
        public IEnumerable<File> Files { get; set; } 
    }
}
