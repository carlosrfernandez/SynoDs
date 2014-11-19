using System;
using System.Net;
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

        public RequestProvider(IAttributeReader attributeReader, IInformationProvider informationProvider)
        {
            Validate.ArgumentIsNotNullOrEmpty(attributeReader);
            Validate.ArgumentIsNotNullOrEmpty(informationProvider);

            _attributeReader = attributeReader;
            _informationProvider = informationProvider;
        }

        /// <summary>
        /// Todo: Implement the missing params.
        /// </summary>
        /// <param name="requestParameters">The Request params.</param>
        /// <param name="authenticationToken"></param>
        /// <returns></returns>
        public string PrepareRequest<TResult>(RequestParameters requestParameters, string authenticationToken = "")
        {
            var api = _attributeReader.ReadApiNameFromT<TResult>();
            var method = _attributeReader.ReadMethodAttributeFromT<TResult>();

            var requestBase = new RequestBase
            {
                ApiName = api,
                Method = method,
            };

            if (requestParameters != null)
                requestBase.RequestParameters = CleanRequestParams(requestParameters);

            if (authenticationToken != string.Empty)
                requestBase.Sid = authenticationToken;

            return requestBase.ToString();
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
