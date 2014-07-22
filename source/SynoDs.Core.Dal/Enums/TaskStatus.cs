namespace SynoDs.Core.Dal.Enums
{
    public enum TaskStatus
    {
        Waiting,
        Downloading,
        Paused,
        Finishing,
        Finished,
        Hash_Checking,
        Seeding,
        Filehosting_Waiting,
        Extracting,
        Error
    }
}
