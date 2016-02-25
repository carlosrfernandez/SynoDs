// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHttpClient.cs" company="">
//   
// </copyright>
// <summary>
//   The HttpClient interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynoDs.Core.Contracts
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// The HttpClient interface.
    /// </summary>
    public interface IHttpClient
    {
        /// <summary>
        /// The send request async.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<string> SendGetRequestAsync(string url);
    }
}