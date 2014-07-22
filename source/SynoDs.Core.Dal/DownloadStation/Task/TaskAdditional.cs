using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SynoDs.Core.Dal.DownloadStation.Task
{
    [DataContract]
    public class TaskAdditional
    {
        [DataMember(Name = "detail")]
        public TaskDetail Detail { get; set; }

        [DataMember(Name = "transfer")]
        public TaskTransfer Transfer { get; set; }

        [DataMember(Name = "file")]
        public IList<TaskFile> File { get; set; }

        [DataMember(Name = "tracker")]
        public IList<TaskTracker> Tracker { get; set; }

        [DataMember(Name = "peer")]
        public IList<TaskPeer> Peer { get; set; }
    }
}
