namespace SynoDs.Core.Interfaces
{
    public interface IHttpClient
    {
        System.Threading.Tasks.Task<string> SendRequestAsync();
    }
}
