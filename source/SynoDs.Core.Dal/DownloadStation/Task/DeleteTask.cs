namespace SynoDs.Core.Dal.DownloadStation.Task
{
    using System.Runtime.Serialization;
    using Attributes;

    /// <summary>
    /// Placeholder for the delete task operation.
    /// </summary>
    [DataContract]
    [ApiMethod("delete")]
    public class DeleteTask : TaskOperationBase
    {
    }
}
