using System.Threading.Tasks;

namespace SynoDs.Core.Interfaces.Synology
{
    /// <summary>
    /// Defines the way that the Info Api will be implemented.
    /// </summary>
    public interface IApiInformationProvider<T>
    {
        /// <summary>
        /// True if the cache still needs filling up.
        /// </summary>
        bool IsCacheEmtpy { get; set; }
        
        /// <summary>
        /// Gets the Information on the given Api name.
        /// </summary>
        /// <param name="apiName">API to get information on</param>
        /// <returns>An object storing all of the Info</returns>
        Task<T> GetApiInformationAsync(string apiName);

        /// <summary>
        /// Should fill an internal cache with all of the known apis' information.
        /// </summary>
        void GetApiInformationCacheAsync();

    }
}