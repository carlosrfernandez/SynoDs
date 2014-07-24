namespace SynoDs.Core.Api
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Http;
    using StringUtils;
    using Dal.BaseApi;
    using Dal.HttpBase;
    using Interfaces;
    
    /// <summary>
    /// This is the API base class. It contains a Generic PerformOperationAsync method that 
    /// can be used by the rest of the API's in order to communicate with the Diskstation.
    /// TODO: Add the File Upload method for uploading torrents from the client application.
    /// TODO: Add known error handling of the API
    /// </summary>
    public class DsClientBase
    {
        protected string DsUsername { get; set; }
        protected string DsPassword { get; set; }
        protected Uri DsAddress { get; set; }
        protected string SessionId { get; set; }
        protected IParameterMapper ParameterMapper { get; set; }
        protected IHttpClient RequestClient { get; set; }
        protected IJsonParser JsonParser { get; set; }
        protected string SessionName { get; set; }


        /// <summary>
        /// Checks the SessionId to see if it's set (means we're logged in).
        /// </summary>
        public bool IsLoggedIn
        {
            get { return SessionId != string.Empty; }
        }

        /// <summary>
        /// Checks if the dictionary with the API Information is loaded.
        /// </summary>
        private bool IsApiInfoCacheEmtpy
        {
            get { return ApiInformationCache == null || ApiInformationCache.Count == 0; }
        }

        /// <summary>
        /// Stores the API information retrieved from the NAS.
        /// </summary>
        protected ApiInfoWrapper ApiInformationCache { get; set; }

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
        public DsClientBase()
        {
            JsonParser = new JsonHandler();
            SessionName = "DsBase";
        }

        /// <summary>
        /// Constructor that checks the input parameters for any errors and stores the information
        /// </summary>
        /// <param name="username">Username that has access to the DiskStation</param>
        /// <param name="password">Password that goes with the username. </param>
        /// <param name="host">DiskStation IP address or hostname.</param>
        public DsClientBase(string username, string password, Uri host) : this()
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException("username", @"Username cannot be emtpy.");

            if(string.IsNullOrEmpty(password))
                throw new ArgumentNullException("password", @"Password cannot be empty.");

            if (!host.IsWellFormedOriginalString())
                throw new ArgumentException("Invalid host.");

            DsUsername = username;
            DsPassword = password;
            DsAddress = host;
            SessionId = string.Empty;
        }

        /// <summary>
        /// Logs into the DiskStation with the supplied credentials.
        /// </summary>
        /// <returns>True if logged in and false if any errors occur.</returns>
        public async Task<bool> LoginAsync()
        {
            if (IsApiInfoCacheEmtpy)
            {
                await GetApiInformationCache();
            }
            var parameters = new RequestParameters
            {
                {"account", DsUsername},
                {"passwd", DsPassword},
                {"session", SessionName},
                {"format", "sid" }
            };
            var loginResult = await PerformOperationAsync<LoginResponse>(parameters);
            SessionId = loginResult.ResponseData.Sid;
            return loginResult.Success;
        }

        /// <summary>
        /// Gets all of the DiskStation's API information. This method will store
        /// the API's in an internal dictionary until the client is destroyed.
        /// </summary>
        /// <returns>An emtpy task.</returns>
        private async Task GetApiInformationCache()
        {
            var infoResult = await PerformOperationAsync<InfoResponse>(new RequestParameters
            {
                {"query", "ALL"}
            });

            if (infoResult.Success)
            {
                ApiInformationCache = infoResult.ResponseData;
            }
            else
            {
                throw new Exception("Error while getting API Information. ");
            }
        }
        
        /// <summary>
        /// Gets the Api information for the requested API Name
        /// </summary>
        /// <param name="apiName">Api name to get information on. </param>
        /// <returns>The InformationResponse for the supplied API.</returns>
        public async Task<InfoResponse> GetApiInformation(string apiName)
        {
            var requestParams = new RequestParameters
            {
                {"query", apiName}
            };

            var result = await PerformOperationAsync<InfoResponse>(requestParams);
            return result;
        }

        /// <summary>
        /// Logs out of the DiskStation. 
        /// </summary>
        /// <returns></returns>
        public async Task<bool> LogoutAsync()
        {
            var logoutParams = new RequestParameters
            {
                {"session", SessionName}
            };

            var logoutRequestResult = await PerformOperationAsync<LogoutResponse>(logoutParams);
            SessionId = string.Empty; // erase the sid.
            return logoutRequestResult.Success;
        }
        
        /// <summary>
        /// Performs a Request to the DiskStation with the supplied parameters. 
        /// </summary>
        /// <typeparam name="T">Response type object, will tell us what method and which API to call by use of attributes.</typeparam>
        /// <param name="optionalParameters">Additional optional parameters to send the request with.</param>
        /// <returns>Task of type T which represents the response object.</returns>
        protected async Task<T> PerformOperationAsync<T>(RequestParameters optionalParameters = null)
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
        protected virtual RequestBase PrepareRequest<T>(RequestParameters optionalParameters)
        {
            var apiName = AttributeMapper.ReadApiNameFromInstance<T>();
            var apiMethod = AttributeMapper.ReadMethodAttributeFromT<T>();
            var request = new RequestBase {ApiName = apiName, Method = apiMethod};

            var t = typeof (T);
            if (t == typeof(InfoResponse))
            {
                // this is an information request.
                request.Path = "query.cgi";
                request.Version = "1";
                request.Method = "query";
                request.RequestParameters = optionalParameters;
                // todo: possibly move the information api info to config file since it's the entry point for getting information on the other apis. 
            }
            else // this is a normal request
            {
                var apiInfo = ApiInformationCache.FirstOrDefault(n => n.Key == apiName).Value;
                request.Path = apiInfo.Path;
                request.Version = apiInfo.MaxVersion.ToString(); // use max version always. 
                request.Sid = SessionId;
                request.RequestParameters = optionalParameters;
            }
            return request;
        }
    }
}
