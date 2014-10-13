namespace SynoDs.Core.DownloadStation
{
    using Interfaces;

    public class DownloadErrorProvider : IErrorProvider
    {
        public IErrorRepository ErrorRepository { get; private set; }
        public string GetErrorDescriptionForType(int errorCode)
        {
            return "This is a Download Error";
        }
    }
}
