//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;
//using SynoDs.Core.Dal.BaseApi;
//using SynoDs.Core.Dal.HttpBase;

//namespace SynoDs.Core.Api
//{
//    // TODO: Remove code in region sections
//    // TODO: change the RequestParameter class to an interface
//    [Obsolete("This class is to be removed / changed for a new one.", false)]
//    public class DiskStationBase
//    {
//        protected Uri BaseUrl;
//        protected string Username;
//        protected string Password;
//        protected string Sid;
//        protected string InternalSession = string.Empty;
//        protected InfoResponse Info;
//        //protected RequestEvents RequestEventHanlder { get; set; }

//        //private static IContainer Container { get; set; }

//        //IHttpRequestProvider HttpClient { get; set; }

//        // ILoggingProvider Logger { get; set; }

//        //IJsonHandler JsonHandler{ get; set; }
        
//        protected virtual string[] ImplementedApi
//        {
//            get
//            {
//                return new[] { "SYNO.API.Auth" };
//            }
//        }

//        public DiskStationBase()
//        {
//            // TODO: 
//            // Fix the SSL certificate handling.
//            // Investigate if it's possible to handle from the api / backend. If not
//            // let the UI handle it.
//            #region commented ssl cert code.
//            /*
//            ServicePointManager.ServerCertificateValidationCallback +=
//                delegate(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
//                                        System.Security.Cryptography.X509Certificates.X509Chain chain,
//                                        System.Net.Security.SslPolicyErrors sslPolicyErrors)
//                {
//                    return true; // **** Ignore any ssl errors
//                };
//             */
//            //ServicePointManager.ServerCertificateValidationCallback +=
//            //    (sender, certificate, chain, sslPolicyErrors) => true;
//            #endregion

//            //var builder = new ContainerBuilder();
//            //builder.RegisterType<JsonHandler>().As<IJsonHandler>();
//            //builder.RegisterType<LoggingProvider>().As<ILoggingProvider>();
//            //Container = builder.Build();
//            //RequestEventHanlder = new RequestEvents();
//            //ResponseEvents responseEvents = new ResponseEvents();
//            //responseEvents.ResponseHttpError += ResponseEventsOnResponseHttpError;
            
//            JsonHandler  = new JsonHandler();
//        }
       
//        public DiskStationBase(Uri url, string username, string password)
//            : this()
//        {
//            BaseUrl = url;
//            Username = username;
//            Password = password;
//            InternalSession = "DownloadStation";
//        }

//        /// <summary>
//        /// Creates an Http call with the supplied Parameters. 
//        /// </summary>
//        /// <param name="parameters">The list of parameters.</param>
//        /// <returns></returns>
//        protected async Task<string> _runAsync(RequestBuilder parameters)
//        {
//            #region Old api code.
//            //var request = WebRequest.Create(BaseUrl.ToString() + requestBuilder);
//            //WebResponse response = request.GetResponse();
            
//            //var res = client.GetResultAsync().Result;
//            //return res.ToString();
//            #endregion

//            var client = new HttpGetRequestClient(BaseUrl + parameters.ToString());
//            var res = await client.SendRequestAsync();
//            return res;
//        }

//        /// <summary>
//        /// Creates a request using request builder with the supplied api specification. 
//        /// </summary>
//        /// <param name="apiSpec">Api specification</param>
//        /// <returns>The request builder object for the supplied API.</returns>
//        public RequestBuilder CreateRequest(KeyValuePair<string, ApiInfo> apiSpec)
//        {
//            return (new RequestBuilder()).
//                        Api(apiSpec.Key).
//                        CgiPath(apiSpec.Value.Path).
//                        Version(apiSpec.Value.MaxVersion.ToString()).
//                        Method("");
//        }

//        /// <summary>
//        /// Overwrites the supplied requestBuilder parameters witht he supplied API Specifications
//        /// </summary>
//        /// <param name="requestBuilder">The request builder object to update.</param>
//        /// <param name="apiSpec">The API information to use in the request builder object.</param>
//        /// <returns>The updated request parameters. </returns>
//        public RequestBuilder CreateRequest(RequestBuilder requestBuilder, KeyValuePair<string, ApiInfo> apiSpec)
//        {
//            return requestBuilder.
//                        Api(apiSpec.Key).
//                        CgiPath(apiSpec.Value.Path).
//                        Version(apiSpec.Value.MaxVersion.ToString());
//        }

//        /// <summary>
//        /// Creates a request with the supplied information and sends the file stream to the server. 
//        /// </summary>
//        /// <typeparam name="T">The type of object we're expecting from the incoming JSON response.</typeparam>
//        /// <param name="apiName">API name.</param>
//        /// <param name="method">Method to call on the server</param>
//        /// <param name="fileName">Name of the file we're uploading.</param>
//        /// <param name="fileBytes">The stream with the file contents.</param>
//        /// <param name="fileParam">The file parameter for the API call.</param>
//        /// <returns>A deserialized object with the expected result.</returns>
//        public async Task<T> PostFileAsync<T>(string apiName, string method, string fileName, Stream fileBytes, string fileParam = "file")
//        {
//            var stationApiResult = await GetApi(apiName);
//            var stationApi = stationApiResult.FirstOrDefault();
//            if(stationApi.Key == null) return default(T);
//            var reqBuild = CreateRequest(new RequestBuilder(Sid).Session(Sid).Method(method), stationApi);
//            var postFileResult = await _postFileAsync(reqBuild, fileName, fileBytes, fileParam);
//            return JsonHandler.FromJson<T>(postFileResult);

//            #region Commented old-api code
//            /*JsonHelper.FromJson<T>(
//                        _postFile(
//                            CreateRequest((new RequestBuilder(Sid)).Session(Sid).Method(method), stationApi),
//                            fileName,
//                            fileStream,
//                            fileParam
//                       )
//            );*/
//            #endregion
//        }

//        /// <summary>
//        /// Runs an HTTP call to the API with the supplied parameters and awaits for the response. 
//        /// </summary>
//        /// <typeparam name="T">The expected resulting object type.</typeparam>
//        /// <param name="apiName">The name of the API we're calling.</param>
//        /// <param name="requestBuilder">The request parameters</param>
//        /// <returns>The response object deserialized from JSON.</returns>
//        public async Task<T> CallAsync<T>(string apiName, RequestBuilder requestBuilder)
//        {
//            //var stationApi = GetApi(apiName).FirstOrDefault();
//            var stationApiRequest = await GetApi(apiName);
//            var stationApi = stationApiRequest.FirstOrDefault();
            
//            if (stationApi.Key == null) return default(T);
//            var requestedObject = await _runAsync(CreateRequest(requestBuilder, stationApi));
//            return JsonHandler.FromJson<T>(requestedObject);

//            //return stationApi.Key == null ? default(T) : JsonHelper.FromJson<T>(_run(CreateRequest(requestBuilder, stationApi)));
//        }

//        /// <summary>
//        /// Calls a specific method on the API.
//        /// </summary>
//        /// <typeparam name="T">The expected object type.</typeparam>
//        /// <param name="apiName">Name of the API we're calling.</param>
//        /// <param name="method">The method to call on the server.</param>
//        /// <returns>The object with the result.</returns>
//        protected async Task<T> CallMethodAsync<T>(string apiName, string method)
//        {
//            return await CallAsync<T>(apiName, (new RequestBuilder(Sid)).Session(Sid).Method(method));
//        }

//        /// <summary>
//        /// Calls a specific method on the API with the supplied params.
//        /// </summary>
//        /// <typeparam name="T">The expected object type.</typeparam>
//        /// <param name="apiName">API Name</param>
//        /// <param name="method">Method to call on the server.</param>
//        /// <param name="param">Parameters</param>
//        /// <returns>The object of type T with the result.</returns>
//        public async Task<T> CallMethodAsync<T>(string apiName, string method, RequestParameters param)
//        {
//            return await CallAsync<T>(apiName, (new RequestBuilder(Sid)).Method(method, param));
//        }

//        /// <summary>
//        /// Queries the INFO api to get information on the currently implemented API.
//        /// </summary>
//        /// <param name="apiName">API name to get information on.</param>
//        /// <returns>A dictionary with the API information.</returns>
//        public async Task<Dictionary<string, ApiInfo>> GetApi(string apiName)
//        {
//            if (Info == null)
//            {
//                var apiRequest = await _runAsync(new RequestBuilder().Api("SYNO.API.Info").AddParam("query", String.Join(",", ImplementedApi)));
//                Info = JsonHandler.FromJson<InfoResponse>(apiRequest);
//            }
//            return Info.ResponseData.Where(p => p.Key.StartsWith(apiName)).ToDictionary(t => t.Key, t => t.Value);
//        }

//        /// <summary>
//        /// Login to the Server. 
//        /// </summary>
//        /// <returns>True if login was successful. False if login failed.</returns>
//        public async Task<bool> LoginAsync()
//        {
//            var loginResult = await CallMethodAsync<LoginResponse>("SYNO.API.Auth",
//                "login", new RequestParameters
//                        {
//                            {"account", Username},
//                            {"passwd", Password},
//                            {"session", InternalSession},
//                            {"format", "sid"}
//                        }
//            );
//            if (loginResult.Success)
//            {
//                Sid = loginResult.ResponseData.Sid;
//            }
//            return loginResult.Success;
//        }

//        /// <summary>
//        /// Logout from the server.
//        /// </summary>
//        /// <returns></returns>
//        public async Task<bool> Logout()
//        {
//            // This might be changed to string.empty anyways. 
//            var logoutResult = await CallMethodAsync<ResponseWrapper<Object>>("SYNO.API.Auth", "logout", new RequestParameters { { "session", InternalSession } });
//            if (logoutResult.Success)
//                Sid = string.Empty;
//            return logoutResult.Success;
//        }

//        /// <summary>
//        /// Uploads a file stream to the server. 
//        /// </summary>
//        /// <param name="requestBuilder">The parameters for the request.</param>
//        /// <param name="fileName">The name of the file.</param>
//        /// <param name="fileBytes">The file contents</param>
//        /// <param name="fileParam">The API file parameter name</param>
//        /// <returns>The resulting JSON string from the HTTP call.</returns>
//        protected async Task<string> _postFileAsync(RequestBuilder requestBuilder, string fileName, Stream fileBytes, string fileParam = "file")
//        {
//            // TODO: find async handler for creating this request with a payload.
//            // Normally torrent files.
//            // requestBuilder.Params.Add(fileParam, fileName);
//            var requestHandler = new HttpGetRequestClient(BaseUrl + requestBuilder.ToString(), fileBytes, fileName, fileParam);
//            var result = await requestHandler.SendRequestAsync();
//            return result;

//            #region Old API code.
//            //ar requestHandler = new HttpWebRequest();
//            //requestHandler."
//            //HttpContent fileStreamContent = new StreamContent(fileStream);
//            //var requestHandler = new HttpClientHandler();
//            //throw new NotImplementedException();
//            //string requestUri = BaseUrl.ToString() + requestBuilder;
//            //Stream result = null;
//            //string resJson = String.Empty;
//            //using (var client = new HttpClient(requestHandler))
//            //{
//            //    using (var formData = new MultipartFormDataContent())
//            //    {
//            //        formData.Add(fileStreamContent, fileParam, fileName);
//            //        var response = client.PostAsync(requestUri, formData).Result;
//            //        if (response.IsSuccessStatusCode)
//            //        {
//            //            result = response.Content.ReadAsStreamAsync().Result;
//            //        }
//            //    }
//            //}
//            //using (var reader = new StreamReader(stream: result))
//            //{
//            //    resJson = reader.ReadToEnd();
//            //}
//            //return resJson;
//            #endregion
//        }

//    }
//}
