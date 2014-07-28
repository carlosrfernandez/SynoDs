namespace SynoDs.Core.Dal.DownloadStation.Task
{
    using System.Runtime.Serialization;
    
    [DataContract]
    public class TaskDetail
    {
        [DataMember(Name = "destination")]
        public string Destination { get; set; }

        [DataMember(Name = "uri")]
        public string Uri { get; set; }

        [DataMember(Name = "create_time")]
        public string CreateTime { get; set; }

        [DataMember(Name = "priority")]
        public string Priority { get; set; }

        [DataMember(Name = "total_peers")]
        public int TotalPeers { get; set; }
        
        [DataMember(Name = "connected_seeders")]
        public int ConnectedSeeders { get; set; }

        [DataMember(Name = "connected_leechers")]
        public int ConnectedLeechers { get; set; }
    }
}
