// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OperationProvider.cs" company="">
//   
// </copyright>
// <summary>
//   The operation provider.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynoDs.Core.Api.Operation
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using SynoDs.Core.Contracts;
    using SynoDs.Core.Contracts.Synology;
    using SynoDs.Core.Dal.BaseApi;
    using SynoDs.Core.Dal.HttpBase;

    /// <summary>
    /// The operation provider.
    /// </summary>
    public class OperationProvider : IOperationProvider
    {
        /// <summary>
        /// The _http client.
        /// </summary>
        private readonly IHttpClient _httpClient;

        /// <summary>
        /// The _json parser.
        /// </summary>
        private readonly IJsonParser _jsonParser;

        /// <summary>
        /// The _request provider.
        /// </summary>
        private readonly IRequestProvider _requestProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationProvider"/> class.
        /// </summary>
        /// <param name="httpClient">
        /// The http client.
        /// </param>
        /// <param name="requestProvider">
        /// The request provider.
        /// </param>
        /// <param name="jsonParser">
        /// The json parser.
        /// </param>
        public OperationProvider(IHttpClient httpClient, IRequestProvider requestProvider, IJsonParser jsonParser)
        {
            this._httpClient = httpClient;
            this._requestProvider = requestProvider;
            this._jsonParser = jsonParser;
        }

        /// <summary>
        /// Gets or sets the disk station.
        /// </summary>
        public DiskStation DiskStation { get; set; }

        /// <summary>
        /// Performs and http call with the Request parameters and returns the serialized Json object
        /// </summary>
        /// <typeparam name="TResult">
        /// The Result of type ResponseWrapper
        /// </typeparam>
        /// <param name="requestParameters">
        /// The request parameters.
        /// </param>
        /// <returns>
        /// The serialized Response Object
        /// </returns>
        public async Task<TResult> PerformOperationAsync<TResult>(RequestParameters requestParameters = null)
        {
            var request = await this._requestProvider.PrepareRequestAsync<TResult>(this.DiskStation.HostName.ToString(), requestParameters);
            this._httpClient.CreateRequestSession(request);
            var jsonResult = await this._httpClient.SendRequestAsync();
            var resultObject = this._jsonParser.FromJson<TResult>(jsonResult);
            return resultObject;
        }

        // ReSharper disable once CSharpWarnings::CS1998
        /// <summary>
        /// The perform operation with file async.
        /// </summary>
        /// <param name="requestParameters">
        /// The request parameters.
        /// </param>
        /// <param name="fileStream">
        /// The file stream.
        /// </param>
        /// <typeparam name="TResult">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public async Task<TResult> PerformOperationWithFileAsync<TResult>(
            RequestParameters requestParameters, 
            Stream fileStream)
        {
            throw new NotImplementedException();
        }
    }
}