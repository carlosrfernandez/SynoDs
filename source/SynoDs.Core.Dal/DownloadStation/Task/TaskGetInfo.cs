using System.Collections.Generic;
using System.Runtime.Serialization;
using SynoDs.Core.Dal.Attributes;

namespace SynoDs.Core.Dal.DownloadStation.Task
{
    [DataContract]
    [ApiMethod("getinfo")]
    public class TaskGetInfo
    {
        [DataMember(Name = "tasks")]
        public IList<DsTask> Tasks { get; set; }
    }
}
