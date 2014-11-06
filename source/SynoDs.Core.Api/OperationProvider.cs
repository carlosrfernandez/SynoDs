using System.Net;
using System.Threading.Tasks;
using SynoDs.Core.Dal.BaseApi;
using SynoDs.Core.Dal.HttpBase;
using SynoDs.Core.Interfaces;
using SynoDs.Core.Interfaces.Synology;

namespace SynoDs.Core.Api
{
    public class OperationProvider : IOperationProvider
    {
        private readonly IJsonParser _jsonParser;
        private readonly IHttpClient _httpClient;

        public OperationProvider(IJsonParser jsonParser, IHttpClient httpClient)
        {
            _jsonParser = jsonParser;
            _httpClient = httpClient;
        }
        
        public async Task<TResult> PerformOperationAsync<TResult>(LoginCredentials credentials, RequestParameters requestParameters = null)
        {
            var request = PrepareRequest<TResult>(requestParameters);

            try
            {
                _httpClient.CreateRequestSession(string.Format("{0}{1}", credentials.Uri, request));
                var jsonResult = await _httpClient.SendRequestAsync();
                var result = _jsonParser.FromJson<TResult>(jsonResult);
                return result;
                //// Todo control SSL if possible.
                //using (var requestClient = new HttpGetRequestClient(string.Format("{0}{1}", credentials.Uri, request)))
                //{
                //    var jsonResult = await requestClient.SendRequestAsync();
                //    var result = _jsonParser.FromJson<TResult>(jsonResult);
                //    return result;
                //}
            }
            catch
            {
                return default(TResult);
            }
        }

        public async Task<TResult> PerformOperationWithFileAsync<TResult>(LoginCredentials credentials, RequestParameters requestParameters, System.IO.Stream fileStream)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Prepares a request to the API using the optional parameters. 
        /// It will read the Object's attribute and determine which API and which method to call. 
        /// </summary>
        /// <typeparam name="T">ResponseWrapper object that will tell us through attributes, which API and method to call</typeparam>
        /// <param name="optionalParameters">The optional parameters to add to the tail of the Request.</param>
        /// <returns>A Request object with the resulting string to use in the GET Request.</returns>
        protected virtual async Task<RequestBase> PrepareRequest<T>(RequestParameters optionalParameters)
        {
            var apiName = AttributeReader.ReadApiNameFromT<T>();
            var apiMethod = AttributeReader.ReadMethodAttributeFromT<T>();
            var request = new RequestBase { ApiName = apiName, Method = apiMethod };

            if (optionalParameters != null)
                request.RequestParameters = CleanRequestParameters(optionalParameters);

            var t = typeof(T);
            if (t == typeof(InfoResponse))
            {
                // this is an information request.
                request.Path = "query.cgi";
                request.Version = "1";
                request.Method = "query";
                // todo: possibly move the information api info to config file since it's the entry point for getting information on the other apis. 
            }
            else // this is a normal request
            {
                //ApiInformationCache.FirstOrDefault(n => n.Key == apiName).Value;
                var apiInfo = await InformationProvider.GetApiInformationAsync(apiName);
                request.Path = apiInfo.Path;
                request.Version = apiInfo.MaxVersion.ToString(); // use max version always. 
                request.Sid = SessionId;
            }
            return request;

        }

        /// <summary>
        /// Encodes the parameters in the query so that no illegal symbols are sent through the get request..
        /// </summary>
        /// <param name="parameters">RequestParameters with possibly dirty chars.</param>
        /// <returns>Clean parameter dictionary.</returns>
        protected virtual RequestParameters CleanRequestParameters(RequestParameters parameters)
        {
            var cleanParams = new RequestParameters();
            foreach (var kvp in parameters)
            {
                cleanParams.Add(kvp.Key, WebUtility.UrlEncode(kvp.Value));
            }
            return cleanParams;
        }       
    }
}
