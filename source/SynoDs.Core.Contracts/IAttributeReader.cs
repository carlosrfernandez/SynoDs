namespace SynoDs.Core.Contracts
{
    /// <summary>
    /// Reads attributes from the DAL classes in order to get some information required for requests.
    /// </summary>
    public interface IAttributeReader
    {
        /// <summary>
        /// Reads the Method Attribute from the Type T.
        /// </summary>
        /// <typeparam name="T">The type to read the attributes from.</typeparam>
        /// <returns>The method name to call.</returns>
        string ReadMethodAttributeFromT<T>();
        
        /// <summary>
        /// Reads the API name to which the Type belongs.
        /// </summary>
        /// <typeparam name="T">The type to read the API info from.</typeparam>
        /// <returns>The API name to call.</returns>
        string ReadApiNameFromT<T>();

        /// <summary>
        /// Reads the RequiresAuthentication attribute from the response object to ensure we're logged in. 
        /// </summary>
        /// <typeparam name="T">The type to read the attribute from </typeparam>
        /// <returns>True if it requires authentication, false by default.</returns>
        bool ReadAuthenticationFlagFromT<T>();
    }
}
