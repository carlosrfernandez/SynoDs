// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InformationProvider.cs" company="">
//   
// </copyright>
// <summary>
//   The information provider.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynoDs.Core.Api.Info
{
    using System.Linq;
    using System.Threading.Tasks;

    using SynoDs.Core.Contracts.Synology;
    using SynoDs.Core.Dal.BaseApi;
    using SynoDs.Core.Exceptions;

    /// <summary>
    /// The information provider.
    /// </summary>
    public class InformationProvider : IInformationProvider
    {
        /// <summary>
        /// The info session name.
        /// </summary>
        private const string InfoSessionName = "InfoSession";

        /// <summary>
        /// The _info repository.
        /// </summary>
        private readonly IInformationRepository infoRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="InformationProvider"/> class.
        /// </summary>
        /// <param name="infoRepository">
        /// The info repository.
        /// </param>
        public InformationProvider(IInformationRepository infoRepository)
        {
            this.infoRepository = infoRepository;
        }

        /// <summary>
        /// Gets or sets a value indicating whether is loading api information cache.
        /// </summary>
        public bool IsLoadingApiInformationCache { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether fully load api information cache.
        /// </summary>
        public bool FullyLoadApiInformationCache { get; set; }

        /// <summary>
        /// Gets the Api information for the requested API Name
        /// </summary>
        /// <param name="apiName">
        ///     Api name to get information on. 
        /// </param>
        /// <param name="endpointDiskStation"></param>
        /// <returns>
        /// The InformationResponse for the supplied API.
        /// </returns>
        public async Task<ApiInfo> GetApiInformationAsync(string apiName, string endpointDiskStation)
        {
            if (this.infoRepository.IsCacheEmtpy)
            {
                await this.infoRepository.LoadInformationCacheAsync(endpointDiskStation);
            }

            var apiInfo = this.infoRepository.InformationCache.FirstOrDefault(n => n.Key == apiName).Value;

            if (apiInfo != null)
            {
                return apiInfo;
            }

            throw new SynologyException("Error while getting the API information.");
        }

        /// <summary>
        /// The get session name.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        protected string GetSessionName()
        {
            return InfoSessionName;
        }
    }
}