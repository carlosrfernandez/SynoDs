using System.Collections.Generic;

namespace SynoDs.Core.Interfaces
{
    /// <summary>
    /// Interface for accessing the Error data.
    /// </summary>
    public interface IErrorRepository
    {
        IDictionary<int, string> ErrorDictionary { get; set; }
        string GetErrorDescription(int errorCode);
    }
}
