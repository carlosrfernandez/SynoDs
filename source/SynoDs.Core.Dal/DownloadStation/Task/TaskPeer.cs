using System.Runtime.Serialization;

namespace SynoDs.Core.Dal.DownloadStation.Task
{
    [DataContract] 
    public class TaskPeer
    {
        [DataMember(Name = "address")]
        public string Address { get; set; }

        [DataMember(Name = "agent")]
        public string Agent { get; set; }

        [DataMember(Name = "progress")]
        public float Progress { get; set; }

        [DataMember(Name = "speed_download")]
        public int SpeedDownload { get; set; }

        [DataMember(Name = "speed_upload")]
        public int SpeedUpload { get; set; }
    }
}
