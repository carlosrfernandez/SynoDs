namespace SynoDs.Core.Interfaces
{
    using System;

    public interface IHttpClient : IDisposable
    {
        System.Threading.Tasks.Task<string> SendRequestAsync();
    }
}
