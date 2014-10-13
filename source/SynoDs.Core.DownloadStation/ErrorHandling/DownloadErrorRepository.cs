namespace SynoDs.Core.DownloadStation.ErrorHandling
{
    using Interfaces;

    class DownloadErrorRepository : IErrorRepository
    {
        // Todo: add error access.
        public DownloadErrorRepository()
        {
            
        }

        public string GetErrorDescription(int errorCode)
        {
            return "Error while performing DS Operation.";
        }
    }
}
