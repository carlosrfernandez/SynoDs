using SynoDs.Core.BaseApi.Info.ErrorHandling;

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

    public class InformationProvider : Base, IInformationProvider
    {
        public bool IsCacheEmtpy { get; set; }

        public bool FullyLoadApiInformationCache { get; set; }

        private const string InfoSessionName = "InfoSession";

        private ApiInfoWrapper ApiInformationCache { get; set; }

        private static readonly IErrorProvider _errorProvider = new InfoErrorProvider();

        private const string ErrorProviderName = "InfoErrorProvider";

        private readonly IOperationProvider _operationProvider;

        public InformationProvider(IOperationProvider operationProvider)
        {
            IsCacheEmtpy = true;
            FullyLoadApiInformationCache = false;
            this._operationProvider = operationProvider;
        }

        protected override IErrorProvider ErrorProvider
        {
            get { return _errorProvider; }
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
            if (result.Success)
                ApiInformationCache.Add(apiName, result.ResponseData[apiName]);

            return ApiInformationCache.FirstOrDefault(n=>n.Key == apiName).Value;
        }

        /// <summary>
        /// Fills the internal cache of information
        /// </summary>
        public async Task GetApiInformationCacheAsync()
        {
            var infoResult = await _operationProvider.PerformOperationAsync<InfoResponse>(new RequestParameters
            {
                {"query", "ALL"}
            });

            if (infoResult.Success)
            {
                ApiInformationCache = infoResult.ResponseData;
                IsCacheEmtpy = false;
                FullyLoadApiInformationCache = false; // reset for now. test this.
            }
            else
            {
                throw new Exception("Error while getting API InformationProvider. ");
            }
        }

        IErrorProvider IApi.ErrorProvider { get; set; }
    }
}
