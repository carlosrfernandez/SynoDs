using System.Runtime.Serialization;
using SynoDs.Core.Dal.Attributes;

namespace SynoDs.Core.Dal.DownloadStation.Info
{
    [DataContract]
    [ApiMethod("setserverconfig")]
    public class SetServerConfig
    {
        [DataMember(Name = "bt_max_download")]
        public int BtMaxDownload { get; set; }

        [DataMember(Name = "bt_max_upload")]
        public int BtMaxUpload { get; set; }

        [DataMember(Name = "emule_enabled")]
        public bool EmuleEnabled { get; set; }

        [DataMember(Name = "emule_max_download")]
        public int EmuleMaxDownload { get; set; }

        [DataMember(Name = "emule_max_upload")]
        public int EmuleMaxUpload { get; set; }

        [DataMember(Name = "ftp_max_download")]
        public int FtpMaxDownload { get; set; }

        [DataMember(Name = "http_max_download")]
        public int HttpMaxDownload { get; set; }

        [DataMember(Name = "nzb_max_download")]
        public int NzbMaxDownload { get; set; }

        [DataMember(Name = "unzip_service_enabled")]
        public bool UnzipServiceEnabled { get; set; }
    }
}
