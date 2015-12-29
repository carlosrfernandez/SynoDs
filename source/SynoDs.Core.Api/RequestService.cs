// --------------------------------------------------------------------------------------------------------------------
// <copyright file="requestService.cs" company="">
//   
// </copyright>
// <summary>
//   The request provider.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynoDs.Core.Api
{
    using System;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;

    using SynoDs.Core.Contracts;
    using SynoDs.Core.Contracts.Synology;
    using SynoDs.Core.Dal.HttpBase;
    using SynoDs.Core.Exceptions;

    /// <summary>
    /// The request provider.
    /// </summary>
    public class RequestService : IRequestService
    {
        /// <summary>
        /// The http client.
        /// </summary>
        private readonly IHttpClient httpClient;

        /// <summary>
        /// The json parser.
        /// </summary>
        private readonly IJsonParser jsonParser;

        /// <summary>
        /// The _attribute reader.
        /// </summary>
        private readonly IAttributeReader attributeReader;

        /// <summary>
        /// The _information provider.
        /// </summary>
        private readonly IInformationProvider informationProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestService"/> class.
        /// </summary>
        /// <param name="httpClient">
        /// The HTTP client
        /// </param>
        /// <param name="jsonParser">
        /// The json parser
        /// </param>
        /// <param name="attributeReader">
        /// The attribute reader.
        /// </param>
        /// <param name="informationProvider">
        /// The information provider.
        /// </param>
        public RequestService(
            IHttpClient httpClient,
            IJsonParser jsonParser,
            IAttributeReader attributeReader, 
            IInformationProvider informationProvider)
        {
            if (httpClient == null)
            {
                throw new ArgumentNullException(nameof(httpClient));
            }

            if (jsonParser == null)
            {
                throw new ArgumentNullException(nameof(jsonParser));
            }

            if (attributeReader == null)
            {
                throw new ArgumentNullException(nameof(attributeReader));
            }

            if (informationProvider == null)
            {
                throw new ArgumentNullException(nameof(informationProvider));
            }

            this.httpClient = httpClient;
            this.jsonParser = jsonParser;
            this.attributeReader = attributeReader;
            this.informationProvider = informationProvider;
        }

        /// <summary>
        /// The prepare request async.
        /// </summary>
        /// <param name="diskStationHostName">
        /// The disk station host name.
        /// </param>
        /// <param name="requestParameters">
        /// The request parameters.
        /// </param>
        /// <param name="sessionId">
        /// The session id.
        /// </param>
        /// <typeparam name="TResult">
        /// The result object type
        /// </typeparam>
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

            var infoResponse = await this.informationProvider.GetApiInformationAsync(api, diskStationHostName);

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

        /// <summary>
        /// The perform operation async.
        /// </summary>
        /// <param name="diskStationEndpoint">
        /// The disk station endpoint.
        /// </param>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        /// <param name="sessionId">
        /// The session id.
        /// </param>
        /// <typeparam name="T">
        /// The response object type.
        /// </typeparam>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<T> PerformOperationAsync<T>(string diskStationEndpoint, RequestParameters parameters = null, string sessionId = "")
        {
            var queryString = await this.PrepareRequestAsync<T>(diskStationEndpoint, parameters);
            this.httpClient.CreateRequestSession(queryString);
            var jsonResult = await this.httpClient.SendRequestAsync();
            var resultObject = this.jsonParser.FromJson<T>(jsonResult);
            return resultObject;
        }

        /// <summary>
        /// Performs the operation with file asynchronous.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="diskStationEndpoint">
        /// The disk station endpoint.
        /// </param>
        /// <param name="requestParams">
        /// The request parameters.
        /// </param>
        /// <param name="fileStream">
        /// The file stream.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<T> PerformOperationWithFileAsync<T>(string diskStationEndpoint, RequestParameters requestParams, Stream fileStream)
        {
            throw new NotImplementedException();
        }
    }
}