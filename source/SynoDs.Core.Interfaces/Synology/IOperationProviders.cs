using System;
using System.IO;
using System.Threading.Tasks;
using SynoDs.Core.Dal.HttpBase;
using SynoDs.Core.Dal.BaseApi;

namespace SynoDs.Core.Interfaces.Synology
{
    public interface IOperationProvider
    {
        Task<string> PerformOperationAsync<TResponse>(RequestParameters requestParameters);

        Task<string> PerformOperationWithFileAsync<TResponse>(RequestParameters requestParameters,Stream fileStream);
    }
}