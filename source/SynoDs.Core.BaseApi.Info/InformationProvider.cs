namespace SynoDs.Core.BaseApi.Info
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Exceptions;
    using Dal.BaseApi;
    using Dal.HttpBase;
    using Interfaces;
    using Interfaces.Synology;
    using SynoDs.Core.Exception;

    public class InformationProvider : Base, IInformationProvider
    {
        public bool IsCacheEmtpy { get; set; }

        public bool FullyLoadApiInformationCache { get; set; }

        private const string InfoSessionName = "InfoSession";

        private ApiInfoWrapper ApiInformationCache { get; set; }

        private const string ErrorProviderName = "InfoErrorProvider";

        private readonly IOperationProvider _operationProvider;
        private readonly IJsonParser _jsonParser;

        public InformationProvider(IOperationProvider operationProvider, IJsonParser jsonParser)
        {
            IsCacheEmtpy = true;
            FullyLoadApiInformationCache = true; //add config and read this from it.
            this._operationProvider = operationProvider;
            this._jsonParser = jsonParser;
        }

        protected override string GetSessionName()
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
            if (string.IsNullOrEmpty(result))
                throw new NullReferenceException("Error while getting information.");

            var response = _jsonParser.FromJson<InfoResponse>(result);
            //ApiInformationCache.Add(apiName,]);

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

            var infoResult = _jsonParser.FromJson<InfoResponse>(infoResponse);

            if (infoResult.Success)
            {
                ApiInformationCache = infoResult.ResponseData;
                IsCacheEmtpy = false;
                FullyLoadApiInformationCache = false; // reset for now. test this.
            }
            else
            {
                throw new SynologyException("Error while getting API InformationProvider.");
            }
        }

        IErrorProvider IApi.ErrorProvider { get; set; }
    }
}
