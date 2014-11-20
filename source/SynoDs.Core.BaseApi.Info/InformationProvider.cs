using SynoDs.Core.Contracts;
using SynoDs.Core.Contracts.Synology;

namespace SynoDs.Core.BaseApi.Info
{
    using System.Linq;
    using System.Threading.Tasks;
    using Exceptions;
    using Dal.BaseApi;
    using Dal.HttpBase;

    public class InformationProvider :  IInformationProvider
    {
        public bool IsCacheEmtpy { get; set; }

        public bool FullyLoadApiInformationCache { get; set; }

        private const string InfoSessionName = "InfoSession";

        private ApiInfoWrapper ApiInformationCache { get; set; }

        private readonly IHttpClient _httpClient;

        private readonly IJsonParser _jsonParser;

        private readonly IOperationProvider _operationProvider;

        public InformationProvider(IOperationProvider operationProvider, IHttpClient httpClient, IJsonParser jsonParser)
        {
            IsCacheEmtpy = true;
            FullyLoadApiInformationCache = true; //add config and read this from it.
            this._operationProvider = operationProvider;
            _httpClient = httpClient;
            _jsonParser = jsonParser;
        }

        protected string GetSessionName()
        {
            return InfoSessionName;
        }

        /// <summary>
        /// Gets the Api information for the requested API Name
        /// </summary>
        /// <param name="apiName">Api name to get information on. </param>
        /// <returns>The InformationResponse for the supplied API.</returns>
        public async Task<ApiInfo> GetApiInformationAsync(string apiName)
        {
            if (IsCacheEmtpy && FullyLoadApiInformationCache)
                await GetApiInformationCacheAsync();

            var apiInfo = ApiInformationCache.FirstOrDefault(n => n.Key == apiName).Value;
            if (apiInfo != null)
            {
                return apiInfo;
            }

            var requestParams = new RequestParameters
            {
                {"query", apiName}
            };

            var result = await _operationProvider.PerformOperationAsync<InfoResponse>(requestParams);

            if (result.Success)
                ApiInformationCache.Add(result.ResponseData.Keys.First(), result.ResponseData.Values.First());//revise this.

            return ApiInformationCache.FirstOrDefault(n=>n.Key == apiName).Value;
        }

        /// <summary>
        /// Fills the internal cache of information
        /// </summary>
        public async Task GetApiInformationCacheAsync()
        {
            var infoResponse = await _operationProvider.PerformOperationAsync<InfoResponse>(new RequestParameters
            {
                {"query", "ALL"}
            });

            if (infoResponse.Success)
            {
                ApiInformationCache = infoResponse.ResponseData;
                IsCacheEmtpy = false;
                FullyLoadApiInformationCache = false; // reset for now. test this.
            }
            else
            {
                throw new SynologyException("Error while getting API InformationProvider.");
            }
        }
    }
}
