namespace SynoDs.Core.DownloadStation.ErrorHandling
{
    using Interfaces;

    public class DownloadErrorProvider : IErrorProvider
    {
        public IErrorRepository ErrorRepository { get; set; }

        public string GetErrorDescriptionForCode(int errorCode)
        {
            return "This is a Download Error";
        }
    }
}
