namespace SynoDs.Core.Dal.FileStation.CopyMove
{
    using System.Runtime.Serialization;
    using Attributes;

    [DataContract]
    [ApiMethod("status")]
    public class CopyMoveStatus
    {
        [DataMember(Name = "processed_size")]
        public int ProcessedSize { get; set; }

        [DataMember(Name = "total")]
        public int Total { get; set; }

        [DataMember(Name = "path")]
        public string Path { get; set; }

        [DataMember(Name = "finished")]
        public bool Finished { get; set; }

        [DataMember(Name = "progress")]
        public double Progress { get; set; }

        [DataMember(Name = "dest_folder_path")]
        public string DestinationFolderPath { get; set; }
    }
}
