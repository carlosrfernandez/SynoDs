// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HttpGetRequestClient.cs" company="">
//   
// </copyright>
// <summary>
//   The http get request client.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynoDs.Core.Api.Http
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    using SynoDs.Core.Contracts;
    using SynoDs.Core.Exceptions;

    // TODO: Refactor this class 
    // Bug: This class implementation can't handle SSL.
    // TODO: Remove from main API project and let the client handle the HTTP communication. 
    // Until PCL's support SSL .
    // Change all HTTP objects to use "using" statements.
    // Add exception handling. 
    /// <summary>
    /// The http get request client.
    /// </summary>
    public class HttpGetRequestClient : IHttpClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpGetRequestClient"/> class.
        /// </summary>
        public HttpGetRequestClient()
        {
        }

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        private string Url { get; set; }

        /*
        private byte[] FileStream { get; set; }
*/

        /// <summary>
        /// Gets the file name.
        /// </summary>
        private string FileName { get; }

        /// <summary>
        /// Gets the file param.
        /// </summary>
        private string FileParam { get; }

        /// <summary>
        /// Gets or sets the handler.
        /// </summary>
        private HttpClientHandler Handler { get; set; }

        /// <summary>
        /// Gets or sets the client.
        /// </summary>
        private HttpClient Client { get; set; }

        /// <summary>
        /// Gets the file stream content.
        /// </summary>
        private StreamContent FileStreamContent { get; }

        /// <summary>
        /// The send request async.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<string> SendRequestAsync()
        {
            if (this.FileStreamContent != null)
            {
                // Perform file upload request
                return await this.UploadFileAndGetResponseAsync();
            }

            // Perform normal request. 
            return await this.PerformRequestAsync();
        }

        /// <summary>
        /// The create request session.
        /// </summary>
        /// <param name="requestUrl">
        /// The request url.
        /// </param>
        public void CreateRequestSession(string requestUrl)
        {
            this.Url = requestUrl;
            this.Handler = new HttpClientHandler();
            this.Client = new HttpClient(this.Handler);
        }

        /// <summary>
        ///     For now.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// The perform request async.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        /// <exception cref="SynologyException">
        /// </exception>
        private async Task<string> PerformRequestAsync()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, this.Url);
                var response = await this.Client.SendAsync(request);
                var responseAsByteArray = await response.Content.ReadAsByteArrayAsync();
                var responseString = Encoding.UTF8.GetString(responseAsByteArray, 0, responseAsByteArray.Length);
                return responseString;
            }
            catch (Exception ex)
            {
                throw new SynologyException("Error sending request", ex);
            }
        }

        /// <summary>
        /// The upload file and get response async.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        /// <exception cref="WebException">
        /// </exception>
        /// <exception cref="SynologyException">
        /// </exception>
        private async Task<string> UploadFileAndGetResponseAsync()
        {
            try
            {
                var message = new HttpRequestMessage(HttpMethod.Post, new Uri(this.Url));
                if (this.Handler.SupportsTransferEncodingChunked())
                {
                    message.Headers.TransferEncodingChunked = true;
                }

                using (var formData = new MultipartFormDataContent())
                {
                    formData.Add(this.FileStreamContent, this.FileParam, this.FileName);
                    message.Content = formData;
                    var result = await this.Client.SendAsync(message);
                    if (!result.IsSuccessStatusCode)
                    {
                        throw new WebException(
                            string.Format(
                                "Error while receiving the response from the server, got status code: {0}", 
                                result.StatusCode));
                    }

                    var resultContent = await result.Content.ReadAsByteArrayAsync();
                    var resultString = Encoding.UTF8.GetString(resultContent, 0, resultContent.Length);
                    return resultString;
                }
            }
            catch (WebException webException)
            {
                throw new SynologyException("Http error.", webException);
            }
            catch (Exception ex)
            {
                throw new SynologyException("Error performing web request", ex);
            }
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        /// <param name="disposing">
        /// The disposing.
        /// </param>
        private void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            if (this.Client != null)
            {
                this.Client.Dispose();
            }

            if (this.Handler != null)
            {
                this.Handler.Dispose();
            }
        }
    }
}