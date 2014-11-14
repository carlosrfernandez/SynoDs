using System;
using System.IO;
using System.Threading.Tasks;
using SynoDs.Core.Dal.HttpBase;
using SynoDs.Core.Dal.BaseApi;

namespace SynoDs.Core.Interfaces.Synology
{
    public interface IOperationProvider
    {
        /// <summary>
        /// This method will be used to upload a file to the DS.
        /// </summary>
        /// <typeparam name="TResponse">Response object</typeparam>
        /// <param name="requestParameters">The request parameters.</param>
        /// <param name="isAuthenticatedRequest">True: the request requires a logged in session (SID)</param>
        /// <returns>The response object with the result of the request</returns>
        Task<TResponse> PerformOperationAsync<TResponse>(RequestParameters requestParameters, bool isAuthenticatedRequest = false);

        /// <summary>
        /// This method will be used to upload a file to the DS.
        /// </summary>
        /// <typeparam name="TResponse">Response object</typeparam>
        /// <param name="requestParameters">The request parameters.</param>
        /// <param name="fileStream">The filestream to upload.</param>
        /// <param name="isAuthenticatedRequest">True: the request requires a logged in session. (SSID)</param>
        /// <returns>The response object with the result of the request</returns>
        Task<TResponse> PerformOperationWithFileAsync<TResponse>(RequestParameters requestParameters,Stream fileStream, bool isAuthenticatedRequest = true);
    }
}