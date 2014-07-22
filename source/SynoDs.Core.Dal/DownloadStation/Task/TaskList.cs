using System.Collections.Generic;
using System.Runtime.Serialization;
using SynoDs.Core.Dal.Attributes;

namespace SynoDs.Core.Dal.DownloadStation.Task
{
    [DataContract]
    [ApiMethod("list")]
    public class TaskList
    {
        [DataMember(Name = "offset")]
        public int Offset { get; set; }
        
        [DataMember(Name = "total")]
        public int Total { get; set; }

        [DataMember(Name = "tasks")]
        public IList<DsTask> Tasks { get; set; }
    }
}
