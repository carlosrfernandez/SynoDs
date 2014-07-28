namespace SynoDs.Core.Dal.DownloadStation.Task
{
    using System.Runtime.Serialization;

    [DataContract]
    public class TaskTransfer
    {
        [DataMember(Name = "size_downloaded")]
        public string SizeDownloaded { get; set; }

        [DataMember(Name = "size_uploaded")]
        public string SizeUploaded { get; set; }

        [DataMember(Name = "speed_download")]
        public int SpeedDownload { get; set; }

        [DataMember(Name = "speed_upload")]
        public int SpeedUpload { get; set; }
    }
}
