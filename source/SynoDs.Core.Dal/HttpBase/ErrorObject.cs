using System.Runtime.Serialization;

namespace SynoDs.Core.Dal.HttpBase
{
    [DataContract]
    public class ErrorObject
    {
        [DataMember(Name = "code")]
        public int Code { get; set; }
    }
}