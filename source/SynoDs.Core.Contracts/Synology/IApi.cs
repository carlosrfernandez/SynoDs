namespace SynoDs.Core.Contracts.Synology
{
    /// <summary>
    /// Defines the basic contracts to be implemented by all APIs
    /// </summary>
    public interface IApi
    {
        IErrorProvider ErrorProvider { get; set; }
    }
}
