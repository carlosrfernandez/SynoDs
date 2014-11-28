using System.Threading.Tasks;
using SynoDs.Core.Dal.BaseApi;

namespace SynoDs.Core.Contracts.Synology
{
    public interface IInformationRepository
    {
        /// <summary>
        /// Stores all of the API information. 
        /// </summary>
        ApiInfoWrapper InformationCache { get; }

        /// <summary>
        /// True by default. 
        /// Set to False when the cache has been fully loaded.
        /// </summary>
        bool IsCacheEmtpy { get; }

        /// <summary>
        /// Fills the cache, by making a request to the DS.
        /// </summary>
        /// <returns>Void</returns>
        Task LoadInformationCacheAsync();
    }
}
