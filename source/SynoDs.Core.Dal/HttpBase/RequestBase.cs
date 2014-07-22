using System.Text;

namespace SynoDs.Core.Dal.HttpBase
{
    using System.Runtime.Serialization;
    [DataContract]
    public class RequestBase
    {
        [DataMember(Name = "api")]
        public string ApiName { get; set; }

        [DataMember(Name = "version")]
        public string Version { get; set; }

        [DataMember(Name = "path")]
        public string Path { get; set; }

        [DataMember(Name = "method")]
        public string Method { get; set; }

        [DataMember(Name = "_sid")]
        public string Sid { get; set; }

        [DataMember(Name = "params")]
        public RequestParameters RequestParameters { get; set; }

        /// <summary>
        /// Creates the request string according to the parameters stored in the RequestBase.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var requestParametersString = ParseRequestParameters();
            if (Sid != string.Empty)
                requestParametersString = string.Format("{0}&_sid={1}", requestParametersString, Sid);
            
            return string.Format(@"webapi/{0}?api={1}&version={2}&method={3}{4}", Path, ApiName, Version,
                Method, requestParametersString);
        }

        private string ParseRequestParameters()
        {
            if (RequestParameters == null || RequestParameters.Count == 0)
            {
                return string.Empty;
            }

            var builder = new StringBuilder();
            
            builder.Append("&");

            foreach (var k in RequestParameters)
            {
                builder.Append(string.Format("{0}={1}&", k.Key, k.Value));
            }

            builder.Remove(builder.Length - 1, 1); // remove trailing &
            
            return builder.ToString();
        }
    }
}
