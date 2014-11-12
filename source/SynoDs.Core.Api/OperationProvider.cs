using System.Net;
using System.Threading.Tasks;
using SynoDs.Core.Exceptions.Exceptions;
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

        private readonly LoginCredentials _userCredentials;
        private readonly DsStationInfo _dsStationHostInfo;

        public OperationProvider(DsStationInfo dsStationHostInfo, LoginCredentials userCredentials, IHttpClient httpClient, IRequestProvider requestProvider)
        {
            _httpClient = httpClient;
            _requestProvider = requestProvider;
            _dsStationHostInfo = dsStationHostInfo;
            _userCredentials = userCredentials;
        }
        
        public async Task<string> PerformOperationAsync<TResult>(RequestParameters requestParameters = null)
        {
            var request = _requestProvider.PrepareRequest<TResult>(requestParameters);
            _httpClient.CreateRequestSession(request);
            var jsonResult = await _httpClient.SendRequestAsync();
            //var result = _jsonParser.FromJson<TResult>(jsonResult);
            return jsonResult;
        }

        public async Task<string> PerformOperationWithFileAsync<TResult>(RequestParameters requestParameters, Stream fileStream)
        {
            throw new System.NotImplementedException();
        }
    }
}
