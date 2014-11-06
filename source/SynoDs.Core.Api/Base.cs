namespace SynoDs.Core.Api
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using System.Net;
    using Http;
    using JsonParser;
    using Dal.BaseApi;
    using Dal.HttpBase;
    using Interfaces;
    using Interfaces.Synology;
    
    /// <summary>
    /// This is the API base class. It contains a Generic PerformOperationAsync method that 
    /// can be used by the rest of the API's in order to communicate with the Diskstation.
    /// DONE: Convert to abstract.
    /// Done: Remove Login and InformationProvider Methods into separate projects.
    /// Done: Refactor the Info cache so that it is properly used in the base class (use information interface to access the Api Cache)
    /// TODO: Add the File Upload method for uploading torrents from the client application.
    /// TODO: Add known error handling of the API
    /// </summary>
    public abstract class Base
    {
        // Api properties
        protected string DsUsername { get; set; }
        protected string DsPassword { get; set; }
        protected Uri DsAddress { get; set; }
        protected string SessionId { get; set; }
        protected const string SessionName = "DsBase";

        // Dependencies
        private IHttpClient HttpClient { get; set; }
        private IJsonParser JsonParser { get; set; }
        private IInformationProvider InformationProvider { get; set; }

        // Virtual members:
        protected abstract IErrorProvider ErrorProvider { get; }

        
        /// <summary>
        /// Overridable method to get the session name used to log out.
        /// </summary>
        /// <returns>The current session's name</returns>
        protected virtual string GetSessionName()
        {
            return SessionName;
        }

        /// <summary>
        /// Default parameterless constructor
        /// </summary>
        protected Base()
        {
            this.HttpClient = new HttpGetRequestClient();
            this.JsonParser = new JsonParser();
        }

        protected Base(IInformationProvider informationProvider)
        {
            this.InformationProvider = informationProvider;
        }

        protected Base(IJsonParser jsonParser) 
        {
            this.JsonParser = jsonParser;
        }

        protected Base(IHttpClient httpClient)
        {
            this.HttpClient = httpClient;
        }

        protected Base(IHttpClient httpClient, IJsonParser jsonParser, IInformationProvider informationProvider)
        {
            this.InformationProvider = informationProvider;
            this.JsonParser = jsonParser;
            this.HttpClient = httpClient;
        }

        /// <summary>
        /// Performs a Request to the DiskStation with the supplied parameters. 
        /// </summary>
        /// <typeparam name="T">Response type object, will tell us what method and which API to call by use of attributes.</typeparam>
        /// <param name="optionalParameters">Additional optional parameters to send the request with.</param>
        /// <returns>Task of type T which represents the response object.</returns>
        public async Task<T> PerformOperationAsync<T>(RequestParameters optionalParameters = null)
        {
            var request = PrepareRequest<T>(optionalParameters);

            try
            {
                using (var requestClient = new HttpGetRequestClient(string.Format("{0}{1}", DsAddress, request)))
                {
                    var jsonResult = await requestClient.SendRequestAsync();
                    var result = JsonParser.FromJson<T>(jsonResult);
                    return result;
                }
            }
            catch
            {
                return default(T);
            }
            // TODO: Add the verification for errors in here.
        }

        /// <summary>
        /// Not implemented yet: will upload a torrent file to the DiskStation from the client.
        /// </summary>
        /// <typeparam name="T">The response object.</typeparam>
        /// <param name="optionalParameters">The optional parameters</param>
        /// <param name="fileStream">The FileStream to upload</param>
        /// <returns>A task with the Response object data.</returns>
// ReSharper disable once CSharpWarnings::CS1998
        protected async Task<T> PerformOperationWithFileAsync<T>(RequestParameters optionalParameters, Stream fileStream)
        {
            throw new NotImplementedException("This method is yet to be implemented.");
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

            var t = typeof (T);
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
