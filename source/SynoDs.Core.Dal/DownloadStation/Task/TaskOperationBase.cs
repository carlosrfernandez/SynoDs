using System.Runtime.Serialization;

namespace SynoDs.Core.Dal.DownloadStation.Task
{
    [DataContract]
    public class TaskOperationBase
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "error")]
        public int Error { get; set; }
    }
}
