// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RequestProvider.cs" company="">
//   
// </copyright>
// <summary>
//   The request provider.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynoDs.Core.Api
{
    using System.Net;
    using System.Threading.Tasks;

    using SynoDs.Core.Contracts;
    using SynoDs.Core.Contracts.Synology;
    using SynoDs.Core.CrossCutting.Common;
    using SynoDs.Core.Dal.HttpBase;
    using SynoDs.Core.Exceptions;

    /// <summary>
    /// The request provider.
    /// </summary>
    public class RequestProvider : IRequestProvider
    {
        /// <summary>
        /// The _attribute reader.
        /// </summary>
        private readonly IAttributeReader attributeReader;

        /// <summary>
        /// The _information provider.
        /// </summary>
        private readonly IInformationProvider informationProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestProvider"/> class.
        /// </summary>
        /// <param name="sessionHandler">
        /// The session handler.
        /// </param>
        /// <param name="attributeReader">
        /// The attribute reader.
        /// </param>
        /// <param name="informationProvider">
        /// The information provider.
        /// </param>
        public RequestProvider(
            IAttributeReader attributeReader, 
            IInformationProvider informationProvider)
        {
            Validate.ArgumentIsNotNullOrEmpty(attributeReader);
            Validate.ArgumentIsNotNullOrEmpty(informationProvider);
            
            this.attributeReader = attributeReader;
            this.informationProvider = informationProvider;
        }

        /// <summary>
        /// Todo: Implement the missing params.
        /// </summary>
        /// <typeparam name="TResult">
        /// </typeparam>
        /// <param name="requestParameters">
        /// The Request params.
        /// </param>
        /// <param name="diskStationHostName">
        /// </param>
        /// <param name="sessionId">
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<string> PrepareRequestAsync<TResult>(string diskStationHostName, RequestParameters requestParameters, string sessionId = "")
        {
            var api = this.attributeReader.ReadApiNameFromT<TResult>();
            var method = this.attributeReader.ReadMethodAttributeFromT<TResult>();
            var requiresLogin = this.attributeReader.ReadAuthenticationFlagFromT<TResult>();

            if (requiresLogin && string.IsNullOrEmpty(sessionId))
            {
                throw new SynologyException("Unauthorized operation, login before making this request.");
            }

            var infoResponse = await this.informationProvider.GetApiInformationAsync(api);

            var requestBase = new RequestBase
                                  {
                                      ApiName = api, 
                                      Method = method, 
                                      Path = infoResponse.Path, 
                                      Version = infoResponse.MaxVersion.ToString()
                                  };

            if (requestParameters != null)
            {
                requestBase.RequestParameters = this.CleanRequestParams(requestParameters);
            }

            if (requiresLogin)
            {
                // we already checked that SessionId is not empty. So we add it.
                requestBase.Sid = sessionId;
            }

            var requestSuffix = requestBase.ToString();

            return string.Format("{0}{1}", diskStationHostName, requestSuffix);
        }

        /// <summary>
        /// The clean request params.
        /// </summary>
        /// <param name="dirtyRequestParameters">
        /// The dirty request parameters.
        /// </param>
        /// <returns>
        /// The <see cref="RequestParameters"/>.
        /// </returns>
        public RequestParameters CleanRequestParams(RequestParameters dirtyRequestParameters)
        {
            if (dirtyRequestParameters == null)
            {
                return null;
            }

            var cleanParams = new RequestParameters();

            foreach (var kvp in dirtyRequestParameters)
            {
                cleanParams.Add(kvp.Key, WebUtility.UrlEncode(kvp.Value));
            }

            return cleanParams;
        }
    }
}