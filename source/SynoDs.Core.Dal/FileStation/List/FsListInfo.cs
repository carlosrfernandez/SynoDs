namespace SynoDs.Core.Dal.FileStation.List
{
    using System.Runtime.Serialization;
    using System.Collections.Generic;
    using CreateFolder;
    using Attributes;
    
    [DataContract]
    [ApiMethod("getinfo")]
    public class FsListInfo
    {
        //Todo: Test this with the GetInfo method. Might not work with this FileExtended 
        [DataMember(Name= "files")]
        public IEnumerable<File> Files { get; set; }
    }
}
