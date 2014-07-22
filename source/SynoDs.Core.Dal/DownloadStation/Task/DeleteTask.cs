using System.Runtime.Serialization;
using SynoDs.Core.Dal.Attributes;

namespace SynoDs.Core.Dal.DownloadStation.Task
{
    /// <summary>
    /// Placeholder for the delete task operation.
    /// </summary>
    [DataContract]
    [ApiMethod("delete")]
    public class DeleteTask : TaskOperationBase
    {
    }
}
