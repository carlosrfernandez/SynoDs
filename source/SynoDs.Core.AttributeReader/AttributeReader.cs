// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AttributeReader.cs" company="">
//   
// </copyright>
// <summary>
//   Read the attributes of the Objects defined in the DAL so that we can retrieve the API to which they belong
//   and the methods that we need to use.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynoDs.Core.AttributeReader
{
    using System;
    using System.Linq;
    using System.Reflection;

    using SynoDs.Core.Contracts;
    using SynoDs.Core.Dal.Attributes;

    /// <summary>
    /// Read the attributes of the Objects defined in the DAL so that we can retrieve the API to which they belong 
    /// and the methods that we need to use. 
    /// </summary>
    public class AttributeReader : IAttributeReader
    {
        /// <summary>
        /// Reads the method attribute from the Generic member of the supplied generic Object.>
        /// </summary>
        /// <typeparam name="T">The object to read the Method attribute from</typeparam>
        /// <returns>The method name.</returns>
        public string ReadMethodAttributeFromT<T>()
        {
            var info = typeof(T).GetTypeInfo();
            var genericParams = info.BaseType.GenericTypeArguments;

            // First level generic type argument check.
            var result = genericParams.Select(type => type.GetTypeInfo().GetCustomAttribute<ApiMethod>())
                                .Where(methodName => methodName != null)
                                .Select(methodName => methodName.GetMethodName())
                                .FirstOrDefault();

            if (result != null)
            {
                return result;
            }

            // If we have a second level generic entity (like IEnumerable<MyType>) we need to check IEnumerable's generic type arguments.
            foreach (var secondLevelResult in genericParams.Select(item => item.GetTypeInfo().GenericTypeArguments))
            {
                result = secondLevelResult.Select(type => type.GetTypeInfo().GetCustomAttribute<ApiMethod>())
                    .Where(methodName => methodName != null)
                    .Select(methodName => methodName.GetMethodName())
                    .FirstOrDefault();

                if (result != null)
                {
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Reads the API Name from the supplied Object.
        /// </summary>
        /// <typeparam name="T">The object to read the API attribute from</typeparam>
        /// <returns>A tring with the API name</returns>
        public string ReadApiNameFromT<T>()
        {
            var info = typeof(T).GetTypeInfo();
            var attribute = info.GetCustomAttribute<Api>();
            return attribute.GetApi();
        }

        /// <summary>
        /// TODO: Test this method. 
        /// Checks if the requesting Type has the Authentication attribute set to true.
        /// </summary>
        /// <typeparam name="T">The type to check the attribute of.</typeparam>
        /// <returns>True if it set to true</returns>
        public bool ReadAuthenticationFlagFromT<T>()
        {
            try
            {
                var info = typeof(T).GetTypeInfo();
                var authAttribute = info.GetCustomAttribute<AuthenticationRequired>();
                return authAttribute.GetAuthenticationRequirements();
            }
            catch (NullReferenceException)
            {
                return false;
            }
        }
    }
}
