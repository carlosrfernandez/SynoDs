using System;
using System.IO;
using System.Threading.Tasks;
using SynoDs.Core.Dal.HttpBase;
using SynoDs.Core.Dal.BaseApi;

namespace SynoDs.Core.Interfaces.Synology
{
    public interface IOperationProvider
    {
        Task<TResponse> PerformOperationAsync<TResponse>(RequestParameters requestParameters, string authenticationToken = "");

        Task<TResponse> PerformOperationWithFileAsync<TResponse>(RequestParameters requestParameters,Stream fileStream, string authenticationToken = "");
    }
}