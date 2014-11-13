using System.Threading.Tasks;
using SynoDs.Core.Dal.BaseApi;
using SynoDs.Core.Dal.HttpBase;
using SynoDs.Core.Interfaces;
using SynoDs.Core.Interfaces.Synology;
using System.IO;

namespace SynoDs.Core.Exceptions
{
    public class OperationProvider : IOperationProvider
    {
        //private readonly IJsonParser _jsonParser;
        private readonly IHttpClient _httpClient;
        private readonly IRequestProvider _requestProvider;
        private readonly IJsonParser _jsonParser;

        private readonly LoginCredentials _userCredentials;
        private readonly DsStationInfo _dsStationHostInfo;

        public OperationProvider(DsStationInfo dsStationHostInfo, LoginCredentials userCredentials,
            IHttpClient httpClient, IRequestProvider requestProvider, IJsonParser jsonParser)
        {
            _httpClient = httpClient;
            _requestProvider = requestProvider;
            _dsStationHostInfo = dsStationHostInfo;
            _userCredentials = userCredentials;
            _jsonParser = jsonParser;
        }
        
        public async Task<TResult> PerformOperationAsync<TResult>(RequestParameters requestParameters = null, string authenticationToken = "")
        {
            var request = _requestProvider.PrepareRequest<TResult>(requestParameters, authenticationToken);
            _httpClient.CreateRequestSession(request);
            var jsonResult = await _httpClient.SendRequestAsync();
            var resultObject = _jsonParser.FromJson<TResult>(jsonResult);
            return resultObject;
        }

        public async Task<TResult> PerformOperationWithFileAsync<TResult>(RequestParameters requestParameters, Stream fileStream, string authenticationToken = "")
        {
            throw new System.NotImplementedException();
        }
    }
}
