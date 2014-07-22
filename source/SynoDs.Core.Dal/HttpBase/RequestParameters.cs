namespace SynoDs.Core.Dal.HttpBase
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    /// <summary>
    /// Represents a list of key-value pairs that will be sent in the GET request as 
    /// parameters.
    /// </summary>
    [DataContract]
    public class RequestParameters : Dictionary<string, string>
    {

    }
}