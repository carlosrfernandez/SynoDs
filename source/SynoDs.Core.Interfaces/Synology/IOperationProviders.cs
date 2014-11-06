using System.IO;
using System.Threading.Tasks;
using SynoDs.Core.Dal.HttpBase;
using SynoDs.Core.Dal.BaseApi;

namespace SynoDs.Core.Interfaces.Synology
{
    public interface IOperationProvider
    {
        Task<TResult> PerformOperationAsync<TResult>(LoginCredentials credentials, RequestParameters requestParameters);

        Task<TResult> PerformOperationWithFileAsync<TResult>(LoginCredentials credentials, RequestParameters requestParameters,
            Stream fileStream);
    }
}