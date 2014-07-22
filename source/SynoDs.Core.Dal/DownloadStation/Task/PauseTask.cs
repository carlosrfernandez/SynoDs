using System.Collections;
using System.Runtime.Serialization;
using SynoDs.Core.Dal.Attributes;

namespace SynoDs.Core.Dal.DownloadStation.Task
{
    [DataContract]
    [ApiMethod("pause")]
    public class PauseTask : TaskOperationBase
    {
    }
}
