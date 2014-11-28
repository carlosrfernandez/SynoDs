using System.Runtime.InteropServices;

namespace SynoDs.Core.Dal.HttpBase
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Serves as a generic base class for the response data. 
    /// Cannot be deserialized with JSON, will cause errors when response data includes json arrays.
    /// </summary>
    /// <typeparam name="T">Generic object that will come in the "data" node of the json string.</typeparam>
    [DataContract]
    [ComVisible(true)]
    public class ResponseWrapper<T>
    {
        [DataMember(Name = "data")]
        public T ResponseData { get; set; }

        [DataMember(Name = "success")]
        public bool Success { get; set; }

        [DataMember(Name = "error")]
        public ErrorObject ErrorCode { get; set; }
    }
}
