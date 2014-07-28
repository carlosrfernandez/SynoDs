namespace SynoDs.Core.Dal.DownloadStation.Task
{
    using System.Runtime.Serialization;
    using Attributes;

    [DataContract]
    [ApiMethod("resume")]
    public class ResumeTask : TaskOperationBase
    {

    }
}
