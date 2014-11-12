using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynoDs.Core.Interfaces
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
    }
}
