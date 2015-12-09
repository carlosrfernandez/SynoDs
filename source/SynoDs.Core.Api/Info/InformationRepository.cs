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
    using System.Threading.Tasks;

    using SynoDs.Core.Contracts;
    using SynoDs.Core.Contracts.Synology;
    using SynoDs.Core.CrossCutting.Common;
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
        private readonly IHttpClient _httpClient;

        /// <summary>
        /// The _json parser.
        /// </summary>
        private readonly IJsonParser _jsonParser;

        /// <summary>
        /// The _session handler.
        /// </summary>
        private readonly IDiskStationSessionHandler _sessionHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="InformationRepository"/> class.
        /// </summary>
        /// <param name="sessionHandler">
        /// The session handler.
        /// </param>
        /// <param name="httpClient">
        /// The http client.
        /// </param>
        /// <param name="jsonParser">
        /// The json parser.
        /// </param>
        public InformationRepository(
            IDiskStationSessionHandler sessionHandler, 
            IHttpClient httpClient, 
            IJsonParser jsonParser)
        {
            this.IsCacheEmtpy = true;
            this.IsLoadingApiInformationCache = false;

            this._httpClient = httpClient;
            this._jsonParser = jsonParser;
            this._sessionHandler = sessionHandler;
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
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task LoadInformationCacheAsync()
        {
            Validate.ArgumentIsNotNullOrEmpty(this._sessionHandler.DiskStation);

            var getRequestUrl = string.Format(
                "{0}webapi/query.cgi?api=SYNO.API.Info&version=1&method=query&query=ALL", 
                this._sessionHandler.DiskStation.HostName);

            this._httpClient.CreateRequestSession(getRequestUrl);

            var requestResult = await this._httpClient.SendRequestAsync();

            if (string.IsNullOrEmpty(requestResult))
            {
                throw new SynologyException("Error loading API information cache!");
            }

            var infoResult = this._jsonParser.FromJson<InfoResponse>(requestResult);

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