namespace SynoDs.Core.Dal.FileStation.List
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Attributes;
    
    [DataContract]
    [ApiMethod("list")]
    public class FsList
    {
        [DataMember(Name = "total")]
        public int Total { get; set; }

        [DataMember(Name = "offset")]
        public int Offset { get; set; }

        [DataMember(Name = "files")]
        public IEnumerable<FileExtended> Files { get; set; }

    }
}
