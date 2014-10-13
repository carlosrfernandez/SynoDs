namespace SynoDs.Core.FileStation.ErrorHandling
{
    using Api;
    using Interfaces;

    public class FileStationErrorProvider : ErrorProviderBase
    {
        public FileStationErrorProvider(IErrorRepository errorRepository) : base(errorRepository)
        {
        }

        public FileStationErrorProvider() : base(new FileStationErrorRepository())
        {
        }
    }
}
