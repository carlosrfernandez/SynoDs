namespace SynoDs.Core.FileStation.ErrorHandling
{
    using Interfaces;

    public class FileStationErrorRepository : IErrorRepository
    {
        public FileStationErrorRepository()
        {
        }

        public string GetErrorDescription(int errorCode)
        {
            return "Error in the FileStation operation.";
        }
    }
}
