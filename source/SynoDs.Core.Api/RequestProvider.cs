using System.Net;
using System.Threading.Tasks;
using SynoDs.Core.CrossCutting.Common;
using SynoDs.Core.Dal.HttpBase;
using SynoDs.Core.Contracts;
using SynoDs.Core.Contracts.Synology;

namespace SynoDs.Core.Api
{
    public class RequestProvider : IRequestProvider
    {
        private readonly IAttributeReader _attributeReader;
        private readonly IInformationProvider _informationProvider;
        private readonly IDiskStationSessionHandler _sessionHandler;

        public RequestProvider(IDiskStationSessionHandler sessionHandler, IAttributeReader attributeReader, IInformationProvider informationProvider)
        {
            Validate.ArgumentIsNotNullOrEmpty(attributeReader);
            Validate.ArgumentIsNotNullOrEmpty(informationProvider);
            Validate.ArgumentIsNotNullOrEmpty(sessionHandler);

            _attributeReader = attributeReader;
            _informationProvider = informationProvider;
            _sessionHandler = sessionHandler;
        }

        /// <summary>
        /// Todo: Implement the missing params.
        /// </summary>
        /// <param name="requestParameters">The Request params.</param>
        /// <param name="authenticationToken"></param>
        /// <returns></returns>
        public async Task<string> PrepareRequestAsync<TResult>(RequestParameters requestParameters, string authenticationToken = "")
        {
            var api = _attributeReader.ReadApiNameFromT<TResult>();
            var method = _attributeReader.ReadMethodAttributeFromT<TResult>();

            var infoResponse = await _informationProvider.GetApiInformationAsync(api);
            
            var requestBase = new RequestBase
            {
                ApiName = api,
                Method = method,
                Path = infoResponse.Path,
                Version = infoResponse.MaxVersion.ToString()
            };

            if (requestParameters != null)
                requestBase.RequestParameters = CleanRequestParams(requestParameters);

            if (authenticationToken != string.Empty)
                requestBase.Sid = authenticationToken;

            var requestSuffix = requestBase.ToString();

            return string.Format("{0}{1}", _sessionHandler.DiskStation.HostName, requestSuffix);
        }

        public RequestParameters CleanRequestParams(RequestParameters dirtyRequestParameters)
        {
            if (dirtyRequestParameters == null) return null;

            var cleanParams = new RequestParameters();
            
            foreach (var kvp in dirtyRequestParameters)
            {
                cleanParams.Add(kvp.Key, WebUtility.UrlEncode(kvp.Value));
            }

            return cleanParams;
        }
    }
}
