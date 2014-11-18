﻿using SynoDs.Core.Dal.HttpBase;

namespace SynoDs.Core.Contracts.Synology
{
    /// <summary>
    /// Provides the contracts to be used by Operations.
    /// </summary>
    public interface IRequestProvider
    {
        /// <summary>
        /// Should return a string with the final request URL
        /// </summary>
        /// <param name="requestParameters">RequestParameters to build the request string.</param>
        /// <param name="authenticationToken">The SID for authenticated requests</param>
        /// <returns>The final request string after having been cleaned.</returns>
        string PrepareRequest<TResult>(RequestParameters requestParameters, string authenticationToken = "");

        /// <summary>
        /// This method will call the URL encode to make sure no weird chars get sent in the request. 
        /// </summary>
        /// <param name="dirtyRequestParameters">The request params to clean.</param>
        /// <returns>The encoded request params.</returns>
        RequestParameters CleanRequestParams(RequestParameters dirtyRequestParameters);        
    }
}