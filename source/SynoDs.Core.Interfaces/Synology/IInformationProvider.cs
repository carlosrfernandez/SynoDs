namespace SynoDs.Core.Interfaces.Synology
{
    using Dal.BaseApi;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the way that the Info Api will be implemented.
    /// </summary>
    public interface IInformationProvider
    {
        /// <summary>
        /// True if the cache still needs filling up.
        /// </summary>
        bool IsCacheEmtpy { get; set; }

        /// <summary>
        /// When set to true, once GetApiInformationAsync is called, the full API information 
        /// data should be stored in the internal dictionary.
        /// </summary>
        bool FullyLoadApiInformationCache { get; set; }

        /// <summary>
        /// Gets the Information on the given Api name.
        /// </summary>
        /// <param name="apiName">API to get information on</param>
        /// <returns>An object storing all of the Info</returns>
        Task<ApiInfo> GetApiInformationAsync(string apiName);

        /// <summary>
        /// Should fill an internal cache with all of the known apis' information.
        /// </summary>
        Task GetApiInformationCacheAsync();

    }
}