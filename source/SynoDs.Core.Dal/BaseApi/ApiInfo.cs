using SynoDs.Core.Dal.Attributes;

namespace SynoDs.Core.Dal.BaseApi
{
    using System.Runtime.Serialization;
    /// <summary>
    /// According to PDF specifications.
    /// This class contains the data members for the result from Syno.Api.Info and Syno.Api.Auth
    /// </summary>
    [DataContract]
    [ApiMethod("query")]
    public class ApiInfo
    {
        [DataMember(Name = "path")]
        public string Path { get; set; }

        [DataMember(Name = "minVersion")]
        public int MinVersion { get; set; }

        [DataMember(Name = "maxVersion")]
        public int MaxVersion { get; set; }
    }
}
