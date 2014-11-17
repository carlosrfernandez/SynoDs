using System.IO;
using System.Threading.Tasks;
using SynoDs.Core.Dal.HttpBase;
using SynoDs.Core.Exception;
using SynoDs.Core.Interfaces;
using SynoDs.Core.Interfaces.Synology;

namespace SynoDs.Core.Api
{
    public class OperationProvider : IOperationProvider
    {
        private readonly IHttpClient _httpClient;
        private readonly IRequestProvider _requestProvider;
        private readonly IJsonParser _jsonParser;
        private readonly IAuthenticationProvider _authenticationProvider;

        public OperationProvider(IAuthenticationProvider authenticationProvider,
            IHttpClient httpClient, IRequestProvider requestProvider, IJsonParser jsonParser)
        {
            _httpClient = httpClient;
            _requestProvider = requestProvider;
            _jsonParser = jsonParser;
            _authenticationProvider = authenticationProvider;
        }
        
        public async Task<TResult> PerformOperationAsync<TResult>(RequestParameters requestParameters = null, bool isAuthenticatedRequest = false)
        {
            if (isAuthenticatedRequest && !_authenticationProvider.IsLoggedIn)
            {
               var loginResult =  await _authenticationProvider.LoginAsync();
                if (!loginResult)
                {
                    throw new SynologyException("Error loggging in.");
                }
            }

            var request = _requestProvider.PrepareRequest<TResult>(requestParameters, _authenticationProvider.Sid);
            _httpClient.CreateRequestSession(request);
            var jsonResult = await _httpClient.SendRequestAsync();
            var resultObject = _jsonParser.FromJson<TResult>(jsonResult);
            return resultObject;
        }

// ReSharper disable once CSharpWarnings::CS1998
        public async Task<TResult> PerformOperationWithFileAsync<TResult>(RequestParameters requestParameters, Stream fileStream, bool isAuthenticatedRequest = true)
        {
            throw new System.NotImplementedException();
        }
    }
}
