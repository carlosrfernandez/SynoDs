using SynoDs.Core.Dal.Attributes;

namespace SynoDs.Core.Dal.BaseApi
{
    using System.Runtime.Serialization;

    [DataContract]
    [ApiMethod("login")]
    public class Login
    {
        [DataMember(Name = "sid")]
        public string Sid { get; set; }
    }
}
