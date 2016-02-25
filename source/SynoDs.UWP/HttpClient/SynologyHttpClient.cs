using System;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography.Certificates;
using Windows.Web.Http;
using Windows.Web.Http.Filters;
using SynoDs.Core.Contracts;

namespace SynoDs.UWP.HttpClient
{
    public class SynologyHttpClient : IHttpClient
    {
        public SynologyHttpClient()
        {

        }

        public async Task<string> SendGetRequestAsync(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException(nameof(url));
            }
            var uri = new Uri(url);
            var filter = new HttpBaseProtocolFilter();

            filter.IgnorableServerCertificateErrors.Add(ChainValidationResult.Untrusted);
            filter.IgnorableServerCertificateErrors.Add(ChainValidationResult.InvalidName);

            using (var httpClient = new Windows.Web.Http.HttpClient(filter))
            {
                try
                {
                    var responseString = string.Empty;
                    //var request = new HttpRequestMessage(HttpMethod.Get, uri);
                    var response = await httpClient.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        responseString = await response.Content.ReadAsStringAsync();
                    }

                    return responseString;
                }
                catch (Exception exception) //for debugging
                {
                    throw;
                }
            }
        }

        public void CreateRequestSession(string requestUrl)
        {
            throw new NotImplementedException();
        }
    }
}
