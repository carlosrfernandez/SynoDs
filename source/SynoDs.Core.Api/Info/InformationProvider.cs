using System.Linq;
using System.Threading.Tasks;
using SynoDs.Core.Contracts.Synology;
using SynoDs.Core.Dal.BaseApi;
using SynoDs.Core.Exceptions;

namespace SynoDs.Core.Api.Info
{
    public class InformationProvider : IInformationProvider
    {
        public bool IsLoadingApiInformationCache { get; set; }

        public bool FullyLoadApiInformationCache { get; set; }

        private const string InfoSessionName = "InfoSession";

        private readonly IInformationRepository _infoRepository;

        public InformationProvider(IInformationRepository infoRepository)
        {
            this._infoRepository = infoRepository;
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
            if (_infoRepository.IsCacheEmtpy)
            {
                await _infoRepository.LoadInformationCacheAsync();
            }

            var apiInfo = _infoRepository.InformationCache.FirstOrDefault(n => n.Key == apiName).Value;
            
            if (apiInfo != null)
            {
                return apiInfo;
            }

            throw new SynologyException("Error while getting the API information.");
        }
    }
}