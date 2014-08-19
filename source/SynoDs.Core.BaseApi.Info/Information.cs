using System;
using System.Linq;
using System.Threading.Tasks;
using SynoDs.Core.Api;
using SynoDs.Core.Dal.BaseApi;
using SynoDs.Core.Dal.HttpBase;
using SynoDs.Core.Interfaces.Synology;

namespace SynoDs.Core.BaseApi.Info
{
    public class Information : Base, IApiInformationProvider<InfoResponse>
    {
        public bool IsCacheEmtpy { get; set; }
        
        private ApiInfoWrapper ApiInfoCache { get; set; }

        public Information()
        {
            IsCacheEmtpy = true;
            ApiInfoCache = new ApiInfoWrapper();
        }

        /// <summary>
        /// Gets the Api information for the requested API Name
        /// </summary>
        /// <param name="apiName">Api name to get information on. </param>
        /// <returns>The InformationResponse for the supplied API.</returns>
        public async Task<InfoResponse> GetApiInformationAsync(string apiName)
        {
            var apiInfo = ApiInformationCache.FirstOrDefault(n => n.Key == apiName).Value;
            if (apiInfo != null)
            {
                return new InfoResponse
                    {
                        ErrorCode = 0,
                        Success = true,
                        ResponseData = new ApiInfoWrapper
                        {
                            {apiName, apiInfo}
                        }
                    };
            }

            var requestParams = new RequestParameters
            {
                {"query", apiName}
            };

            var result = await PerformOperationAsync<InfoResponse>(requestParams);
            if (result.Success)
                ApiInfoCache.Add(apiName, result.ResponseData[apiName]);
            return result;
        }

        /// <summary>
        /// Fills the internal cache of information
        /// </summary>
        public async void GetApiInformationCacheAsync()
        {
            var infoResult = await PerformOperationAsync<InfoResponse>(new RequestParameters
            {
                {"query", "ALL"}
            });

            if (infoResult.Success)
            {
                ApiInformationCache = infoResult.ResponseData;
                IsCacheEmtpy = false;
            }
            else
            {
                throw new Exception("Error while getting API Information. ");
            }
        }
    }
}
