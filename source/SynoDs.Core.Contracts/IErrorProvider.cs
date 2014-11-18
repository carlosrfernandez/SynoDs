namespace SynoDs.Core.Contracts
{
    /// <summary>
    /// Interface for getting known error types. 
    /// </summary>
    public interface IErrorProvider
    {
        /// <summary>
        /// Given the error code, this method will return the error description. 
        /// </summary>
        /// <param name="errorCode">Synology API error code.</param>
        /// <returns>String with error description.</returns>
        string GetErrorDescriptionForType<T>(int errorCode);
    }
}
