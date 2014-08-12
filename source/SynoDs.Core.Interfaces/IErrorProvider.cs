namespace SynoDs.Core.Interfaces
{
    /// <summary>
    /// Interface for getting known error types. 
    /// </summary>
    public interface IErrorProvider
    {
        /// <summary>
        /// It's our source of information on known error types.
        /// </summary>
        IErrorRepository ErrorRepository { get; }

        /// <summary>
        /// Given the error code, this method will return the error description. 
        /// </summary>
        /// <param name="errorCode">Synology API error code.</param>
        /// <returns>String with error description.</returns>
        string GetErrorDescriptionForType(int errorCode);
    }
}
