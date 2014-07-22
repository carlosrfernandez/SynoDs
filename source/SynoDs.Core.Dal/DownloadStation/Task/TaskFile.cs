using System.Runtime.Serialization;

namespace SynoDs.Core.Dal.DownloadStation.Task
{
    [DataContract]
    public class TaskFile
    {
        [DataMember(Name = "filename")]
        public string FileName { get; set; }

        [DataMember(Name = "size")]
        public string Size { get; set; }

        [DataMember(Name = "size_downloaded")]
        public string SizeDownloaded { get; set; }

        [DataMember(Name = "priority")]
        public string Priority { get; set; }
    }
}
