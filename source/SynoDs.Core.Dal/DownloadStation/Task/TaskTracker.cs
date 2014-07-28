namespace SynoDs.Core.Dal.DownloadStation.Task
{
    using System.Runtime.Serialization;

    [DataContract]
    public class TaskTracker
    {
        [DataMember(Name = "url")]
        public string Url { get; set; }
        
        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "update_timer")]
        public int UpdateTimer { get; set; }
        
        [DataMember(Name = "seeds")]
        public int Seeds { get; set; }
        
        [DataMember(Name = "peers")]
        public int Peers { get; set; }
    }
}
