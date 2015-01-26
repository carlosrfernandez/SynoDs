using System;

namespace SynoDs.Core.Contracts
{
    public interface IHttpClient : IDisposable
    {
        System.Threading.Tasks.Task<string> SendRequestAsync();
        void CreateRequestSession(string requestUrl);

    }
}
