using System.Runtime.Serialization;

namespace SynoDs.Core.Dal.DownloadStation.Task
{
    [DataContract]
    public class DsTask
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "username")]
        public string Username { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "size")]
        public string Size { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "status_extra")]
        public TaskStatusExtra StatusExtra { get; set; }

        [DataMember(Name = "additional")]
        public TaskAdditional Additional { get; set; }
        
    }
}
