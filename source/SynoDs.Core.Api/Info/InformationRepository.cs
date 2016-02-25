// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InformationRepository.cs" company="">
//   
// </copyright>
// <summary>
//   The information repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynoDs.Core.Api.Info
{
    using System;
    using System.Threading.Tasks;

    using SynoDs.Core.Contracts;
    using SynoDs.Core.Contracts.Synology;
    using SynoDs.Core.Dal.BaseApi;
    using SynoDs.Core.Exceptions;

    /// <summary>
    /// The information repository.
    /// </summary>
    public class InformationRepository : IInformationRepository
    {
        /// <summary>
        /// The _http client.
        /// </summary>
        private readonly IHttpClient httpClient;

        /// <summary>
        /// The _json parser.
        /// </summary>
        private readonly IJsonParser jsonParser;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="InformationRepository"/> class.
        /// </summary>
        /// <param name="httpClient">
        /// The http client.
        /// </param>
        /// <param name="jsonParser">
        /// The json parser.
        /// </param>
        public InformationRepository(
            IHttpClient httpClient,
            IJsonParser jsonParser)
        {
            if (httpClient == null)
            {
                throw new ArgumentNullException(nameof(httpClient));
            }

            if (jsonParser == null)
            {
                throw new ArgumentNullException(nameof(jsonParser));
            }

            this.IsCacheEmtpy = true;
            this.IsLoadingApiInformationCache = false;

            this.httpClient = httpClient;
            this.jsonParser = jsonParser;
        }

        /// <summary>
        /// Gets or sets a value indicating whether is loading api information cache.
        /// </summary>
        public bool IsLoadingApiInformationCache { get; set; } 

        /// <summary>
        /// Gets or sets the information cache.
        /// </summary>
        public ApiInfoWrapper InformationCache { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is cache emtpy.
        /// </summary>
        public bool IsCacheEmtpy { get; set; }

        /// <summary>
        /// Performs a RAW request using the standard API information url's and paramters to load all of the API information
        ///     data into a local cache. This will make subsequent calls for information faster.
        /// </summary>
        /// <param name="endpointDiskStation">The diskstation</param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task LoadInformationCacheAsync(string endpointDiskStation)
        {
            if (endpointDiskStation == null)
            {
                throw new ArgumentNullException(nameof(endpointDiskStation));
            }

            var getRequestUrl = string.Format(
                "{0}webapi/query.cgi?api=SYNO.API.Info&version=1&method=query&query=ALL", 
                endpointDiskStation);

            // this.httpClient.CreateRequestSession(getRequestUrl);

            var requestResult = await this.httpClient.SendGetRequestAsync(getRequestUrl);

            if (string.IsNullOrEmpty(requestResult))
            {
                throw new SynologyException("Error loading API information cache!");
            }

            var infoResult = this.jsonParser.FromJson<InfoResponse>(requestResult);

            if (infoResult.Success)
            {
                this.InformationCache = infoResult.ResponseData;
            }
            else
            {
                throw new SynologyException(infoResult.ErrorCode.Code, "Error loading API information cache!.");
            }
        }
    }
}