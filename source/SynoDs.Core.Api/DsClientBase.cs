using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SynoDs.Core.Api.Http;
using SynoDs.Core.Api.StringUtils;
using SynoDs.Core.Dal.BaseApi;
using SynoDs.Core.Dal.HttpBase;
using SynoDs.Core.Interfaces;

namespace SynoDs.Core.Api
{
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

        public DsClientBase()
        {
            JsonParser = new JsonHandler();
            SessionName = "DsBase";
        }

        public DsClientBase(string username, string password, Uri host) : this()
        {
            DsUsername = username;
            DsPassword = password;
            DsAddress = host;
            SessionId = string.Empty;
        }

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
        
        public async Task<InfoResponse> GetApiInformation(string apiName)
        {
            var requestParams = new RequestParameters
            {
                {"query", apiName}
            };

            var result = await PerformOperationAsync<InfoResponse>(requestParams);
            return result;
        }

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

        protected async Task<T> PerformOperationWithFileAsync<T>(RequestParameters optionalParameters, Stream fileStream)
        {
            throw new NotImplementedException("This method is yet to be implemented.");
        }

        protected virtual RequestBase PrepareRequest<T>(RequestParameters optionalParameters)
        {
            var apiName = AttributeMapper.ReadApiNameFromInstance<T>();
            var apiMethod = AttributeMapper.ReadMethodFromInstance<T>();
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
