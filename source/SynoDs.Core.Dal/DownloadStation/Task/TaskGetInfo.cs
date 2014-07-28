namespace SynoDs.Core.Dal.DownloadStation.Task
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Attributes;

    [DataContract]
    [ApiMethod("getinfo")]
    public class TaskGetInfo
    {
        [DataMember(Name = "tasks")]
        public IList<DsTask> Tasks { get; set; }
    }
}
