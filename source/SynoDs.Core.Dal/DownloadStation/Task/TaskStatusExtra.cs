namespace SynoDs.Core.Dal.DownloadStation.Task
{
    using System.Runtime.Serialization;

    [DataContract]
    public class TaskStatusExtra
    {
        [DataMember(Name = "error_detail")]
        public string ErrorDetail { get; set; }

        [DataMember(Name = "unzip_progress")]
        public int UnzipProgress { get; set; }
    }
}
