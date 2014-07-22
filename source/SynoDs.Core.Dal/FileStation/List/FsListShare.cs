namespace SynoDs.Core.Dal.FileStation.List
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Attributes;

    /// <summary>
    /// Lists all shared folders
    /// </summary>
    [DataContract]
    [ApiMethod("list_shares")]
    public class FsListShare
    {
        [DataMember(Name = "total")]
        public int Total { get; set; }

        [DataMember(Name ="offset")]
        public int Offset { get; set; }

        [DataMember(Name= "shares")]
        public IEnumerable<Shares> Shares { get; set; }
    }
}
