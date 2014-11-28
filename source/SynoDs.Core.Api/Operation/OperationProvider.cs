using System.IO;
using System.Threading.Tasks;
using SynoDs.Core.Contracts;
using SynoDs.Core.Contracts.Synology;
using SynoDs.Core.CrossCutting.Common;
using SynoDs.Core.Dal.BaseApi;
using SynoDs.Core.Dal.HttpBase;

namespace SynoDs.Core.Api.Operation
{
    public class OperationProvider : IOperationProvider
    {
        private readonly IHttpClient _httpClient;
        private readonly IRequestProvider _requestProvider;
        private readonly IJsonParser _jsonParser;
        
        public DsStationInfo StationEndpoint { get; set; }

        public OperationProvider(IHttpClient httpClient, IRequestProvider requestProvider, IJsonParser jsonParser)
        {
            _httpClient = httpClient;
            _requestProvider = requestProvider;
            _jsonParser = jsonParser;
        }

        /// <summary>
        /// Performs and http call with the Request parameters and returns the serialized Json object 
        /// </summary>
        /// <typeparam name="TResult">The Result of type ResponseWrapper</typeparam>
        /// <param name="requestParameters">The request parameters.</param>
        /// <returns>The serialized Response Object</returns>
        public async Task<TResult> PerformOperationAsync<TResult>(RequestParameters requestParameters = null)
        {
            var request = await _requestProvider.PrepareRequestAsync<TResult>(requestParameters);
            _httpClient.CreateRequestSession(request);
            var jsonResult = await _httpClient.SendRequestAsync();
            var resultObject = _jsonParser.FromJson<TResult>(jsonResult);
            return resultObject;
        }

// ReSharper disable once CSharpWarnings::CS1998
        public async Task<TResult> PerformOperationWithFileAsync<TResult>(RequestParameters requestParameters, Stream fileStream)
        {
            throw new System.NotImplementedException();
        }
    }
}
