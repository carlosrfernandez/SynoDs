using System.Threading.Tasks;
using SynoDs.Core.Contracts;
using SynoDs.Core.Contracts.Synology;
using SynoDs.Core.CrossCutting.Common;
using SynoDs.Core.Dal.BaseApi;
using SynoDs.Core.Exceptions;

namespace SynoDs.Core.Api.Info
{
    public class InformationRepository : IInformationRepository
    {
        public ApiInfoWrapper InformationCache { get; set; }
        public bool IsLoadingApiInformationCache { get; set; }
        public bool IsCacheEmtpy { get; set; }

        private readonly IJsonParser _jsonParser;
        private readonly IHttpClient _httpClient;
        private readonly IDiskStationSessionHandler _sessionHandler;

        public InformationRepository(IDiskStationSessionHandler sessionHandler, IHttpClient httpClient, IJsonParser jsonParser)
        {
            IsCacheEmtpy = true;
            IsLoadingApiInformationCache = false;

            this._httpClient = httpClient;
            this._jsonParser = jsonParser;
            this._sessionHandler = sessionHandler;
        }

        /// <summary>
        /// Performs a RAW request using the standard API information url's and paramters to load all of the API information
        /// data into a local cache. This will make subsequent calls for information faster.
        /// </summary>
        /// <returns></returns>
        public async Task LoadInformationCacheAsync()
        {
            Validate.ArgumentIsNotNullOrEmpty(_sessionHandler.DiskStation);

            var getRequestUrl = string.Format(
                "{0}webapi/query.cgi?api=SYNO.API.Info&version=1&method=query&query=ALL", _sessionHandler.DiskStation.HostName);

            _httpClient.CreateRequestSession(getRequestUrl);

            var requestResult = await _httpClient.SendRequestAsync();

            if (string.IsNullOrEmpty(requestResult))
            {
                throw new SynologyException("Error loading API information cache!");
            }

            var infoResult = _jsonParser.FromJson<InfoResponse>(requestResult);

            if (infoResult.Success)
            {
                InformationCache = infoResult.ResponseData;
            }
            else
            {
                throw new SynologyException(infoResult.ErrorCode.Code, "Error loading API information cache!.");
            }
        }
    }
}
