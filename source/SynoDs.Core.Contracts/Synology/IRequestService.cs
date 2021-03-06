﻿using System.Threading.Tasks;
using SynoDs.Core.Dal.HttpBase;

namespace SynoDs.Core.Contracts.Synology
{
    using System.IO;

    /// <summary>
    /// Provides the contracts to be used by Operations.
    /// </summary>
    public interface IRequestService
    {
        /// <summary>
        /// Should return a string with the final request URL
        /// </summary>
        /// <param name="diskStationHostAddress">
        /// The disk Station Host Address.
        /// </param>
        /// <param name="requestParameters">
        /// RequestParameters to build the request string.
        /// </param>
        /// <param name="sessionId">
        /// The session Id.
        /// </param>
        /// <returns>
        /// The final request string after having been cleaned.
        /// </returns>
        Task<string> PrepareRequestAsync<TResult>(string diskStationHostAddress, RequestParameters requestParameters, string sessionId = "");

        /// <summary>
        /// This method will call the URL encode to make sure no weird chars get sent in the request. 
        /// </summary>
        /// <param name="dirtyRequestParameters">The request params to clean.</param>
        /// <returns>The encoded request params.</returns>
        RequestParameters CleanRequestParams(RequestParameters dirtyRequestParameters);

        /// <summary>
        /// The perform operation async.
        /// </summary>
        /// <param name="diskStationEndpoint">
        /// The disk station endpoint.
        /// </param>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        /// <param name="sessionId">
        /// The session id.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<T> PerformOperationAsync<T>(string diskStationEndpoint, RequestParameters parameters = null, string sessionId = "");

        /// <summary>
        /// The perform operation with file async.
        /// </summary>
        /// <param name="diskStationEndpoint">
        /// The disk station endpoint.
        /// </param>
        /// <param name="requestParams">
        /// The request params.
        /// </param>
        /// <param name="fileStream">
        /// The file stream.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<T> PerformOperationWithFileAsync<T>(string diskStationEndpoint, RequestParameters requestParams, Stream fileStream);
    }
}
