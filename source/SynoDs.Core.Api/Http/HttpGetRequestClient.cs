using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SynoDs.Core.Interfaces;

namespace SynoDs.Core.Api.Http
{
    // TODO: Refactor this class 
    // Bug: This class implementation can't handle SSL.
    // TODO: Remove from main API project and let the client handle the HTTP communication. 
    // Until PCL's support SSL .
    // Change all HTTP objects to use "using" statements.
    // Add exception handling. 
    public class HttpGetRequestClient : IHttpClient, IDisposable
    {
        private readonly string _url;
/*
        private byte[] FileStream { get; set; }
*/
        private string FileName { get; set; }
        private string FileParam { get; set; }
        private HttpClientHandler Handler { get; set; }
        private HttpClient Client { get; set; }
        private StreamContent FileStreamContent { get; set; }
        
        public HttpGetRequestClient(string url)
        {
            _url = url;
            Handler = new HttpClientHandler();
            Client = new HttpClient(Handler);
        }

        public HttpGetRequestClient(string url, Stream file, string fileName, string fileParam) : this(url)
        {
            FileStreamContent = new StreamContent(file);
            FileName = fileName;
            FileParam = fileParam;
        }

        public async Task<string> SendRequestAsync()
        {
            if (FileStreamContent != null)
            {
                // Perform file upload request
                return await UploadFileAndGetResponseAsync();
            }
            // Perform normal request. 
            return await PerformRequestAsync();
        }

        private async Task<string> PerformRequestAsync()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, _url);
                if (Handler.SupportsTransferEncodingChunked())
                {
                    request.Headers.TransferEncodingChunked = true;
                }

                var response = await Client.SendAsync(request);
                var responseAsByteArray = await response.Content.ReadAsByteArrayAsync();
                var responseString = Encoding.UTF8.GetString(responseAsByteArray, 0, responseAsByteArray.Length);
                return responseString;
            }
            catch (Exception ex)
            {
                throw new Exception("Error sending request", ex);
            }
        }

        private async Task<string> UploadFileAndGetResponseAsync()
        {
            try
            {
                var message = new HttpRequestMessage(HttpMethod.Post, new Uri(_url));
                if (Handler.SupportsTransferEncodingChunked())
                {
                    message.Headers.TransferEncodingChunked = true;
                }

                using (var formData = new MultipartFormDataContent())
                {
                    formData.Add(FileStreamContent, FileParam, FileName);
                    message.Content = formData;
                    var result = await Client.SendAsync(message);
                    if (!result.IsSuccessStatusCode)
                        throw new WebException(
                            string.Format("Error while receiving the response from the server, got status code: {0}",
                                result.StatusCode));
                    var resultContent = await result.Content.ReadAsByteArrayAsync();
                    var resultString = Encoding.UTF8.GetString(resultContent, 0, resultContent.Length);
                    return resultString;
                }
            }
            catch (WebException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error performing web request", ex);
            }
        }

        /// <summary>
        ///  For now.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (Client != null)
                Client.Dispose();

            if (Handler != null)
                Handler.Dispose();
        }
    }
}
