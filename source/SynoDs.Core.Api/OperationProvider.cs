using System.IO;
using System.Threading.Tasks;
using SynoDs.Core.Dal.BaseApi;
using SynoDs.Core.Dal.HttpBase;
using SynoDs.Core.Exceptions;
using SynoDs.Core.Contracts;
using SynoDs.Core.Contracts.Synology;

namespace SynoDs.Core.Api
{
    public class OperationProvider : IOperationProvider
    {
        private readonly IHttpClient _httpClient;
        private readonly IRequestProvider _requestProvider;
        private readonly IJsonParser _jsonParser;
        private readonly IAuthenticationProvider _authenticationProvider;

        public DsStationInfo StationEndpoint { get; set; }

        public OperationProvider(IAuthenticationProvider authenticationProvider,
            IHttpClient httpClient, IRequestProvider requestProvider, IJsonParser jsonParser)
        {
            _httpClient = httpClient;
            _requestProvider = requestProvider;
            _jsonParser = jsonParser;
            _authenticationProvider = authenticationProvider;
        }

        /// <summary>
        /// Performs and http call with the Request parameters and returns the serialized Json object 
        /// </summary>
        /// <typeparam name="TResult">The Result of type ResponseWrapper</typeparam>
        /// <param name="requestParameters">The request parameters.</param>
        /// <param name="isAuthenticatedRequest">If the request requires the session ID.</param>
        /// <returns>The serialized Response Object</returns>
        public async Task<TResult> PerformOperationAsync<TResult>(RequestParameters requestParameters = null, bool isAuthenticatedRequest = false)
        {
            if (isAuthenticatedRequest && !_authenticationProvider.IsLoggedIn)
            {
                // If this is the first call to Login. then go in.
                if (!_authenticationProvider.IsLoggingIn)
                {
                    var loginResult = await _authenticationProvider.LoginAsync();
                    if (!loginResult)
                    {
                        throw new SynologyException("Error loggging in.");
                    }
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
