using System.Net;
using SynoDs.Core.Dal.HttpBase;
using SynoDs.Core.Interfaces.Synology;

namespace SynoDs.Core.Exceptions
{
    public class RequestProvider : IRequestProvider
    {
        /// <summary>
        /// Todo: Implement
        /// </summary>
        /// <param name="requestParameters"></param>
        /// <returns></returns>
        public string PrepareRequest<TResult>(RequestParameters requestParameters)
        {
            return string.Empty;
        }

        public RequestParameters CleanRequestParams(RequestParameters dirtyRequestParameters)
        {
            var cleanParams = new RequestParameters();
            
            foreach (var kvp in dirtyRequestParameters)
            {
                cleanParams.Add(kvp.Key, WebUtility.UrlEncode(kvp.Value));
            }

            return cleanParams;
        }
    }
}
