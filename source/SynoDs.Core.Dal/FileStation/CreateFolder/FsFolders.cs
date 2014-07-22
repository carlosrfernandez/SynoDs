namespace SynoDs.Core.Dal.FileStation.CreateFolder
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Attributes;
    [DataContract]
    [ApiMethod("create")]
    public class FsFolders
    {
        [DataMember(Name = "folders")]
        public IList<File> Folders { get; set; }
    }
}
