using System.Threading.Tasks;
using SynoDs.Core.Dal.BaseApi;

namespace SynoDs.Core.Contracts.Synology
{
    /// <summary>
    /// Defines the way that the Info Api will be implemented.
    /// </summary>
    public interface IInformationProvider
    {
        /// <summary>
        /// Gets the Information on the given Api name.
        /// </summary>
        /// <param name="apiName">API to get information on</param>
        /// <param name="endpointDiskStation"></param>
        /// <returns>An object storing all of the Info</returns>
        Task<ApiInfo> GetApiInformationAsync(string apiName, string endpointDiskStation);
    }
}