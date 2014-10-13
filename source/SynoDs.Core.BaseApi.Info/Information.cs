using SynoDs.Core.Api.Http;
using SynoDs.Core.Interfaces;

namespace SynoDs.Core.BaseApi.Info
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Api;
    using Dal.BaseApi;
    using Dal.HttpBase;
    using Interfaces.Synology;

    public class Information : Base, IApiInformation
    {
        public bool IsCacheEmtpy { get; set; }

        public bool FullyLoadApiInformationCache { get; set; }

        private const string InfoSessionName = "InfoSession";

        private ApiInfoWrapper ApiInformationCache { get; set; }

        private readonly IErrorProvider _errorProvider;

        public Information()
        {
            IsCacheEmtpy = true;
            FullyLoadApiInformationCache = false;
            _errorProvider = new InfoErrorProvider();
        }

        public Information(IErrorProvider infoErrorProvider)
        {
            this._errorProvider = infoErrorProvider;
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

            var result = await PerformOperationAsync<InfoResponse>(requestParams);
            if (result.Success)
                ApiInformationCache.Add(apiName, result.ResponseData[apiName]);

            return ApiInformationCache.FirstOrDefault(n=>n.Key == apiName).Value;
        }

        /// <summary>
        /// Fills the internal cache of information
        /// </summary>
        public async Task GetApiInformationCacheAsync()
        {
            var infoResult = await PerformOperationAsync<InfoResponse>(new RequestParameters
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
                throw new Exception("Error while getting API Information. ");
            }
        }

        protected override IErrorProvider ErrorProvider
        {
            get { return this._errorProvider; }
        }
    }
}
