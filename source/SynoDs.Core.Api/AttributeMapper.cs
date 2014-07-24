namespace SynoDs.Core.Api
{
    using System.Linq;
    using System.Reflection;
    using Dal.Attributes;

    /// <summary>
    /// Read the attributes of the Objects defined in the DAL so that we can retrieve the API to which they belong 
    /// and the methods that we need to use. 
    /// </summary>
    public static class AttributeMapper
    {
        /// <summary>
        /// Reads the method attribute from the Generic member of the supplied generic Object.>
        /// </summary>
        /// <typeparam name="T">The object to read the Method attribute from</typeparam>
        /// <returns>The method name.</returns>
        public static string ReadMethodAttributeFromT<T>()
        {
            var info = typeof(T).GetTypeInfo();
            var genericParams = info.BaseType.GenericTypeArguments;
            return genericParams.Select(type => type.GetTypeInfo().GetCustomAttribute<ApiMethod>())
                                .Where(methodName => methodName != null)
                                .Select(methodName => methodName.GetMethodName())
                                .FirstOrDefault();
        }

        /// <summary>
        /// Reads the API Name from the supplied Object.
        /// </summary>
        /// <typeparam name="T">The object to read the API attribute from</typeparam>
        /// <returns>A tring with the API name</returns>
        public static string ReadApiNameFromT<T>()
        {
            var info = typeof (T).GetTypeInfo();
            var attribute = info.GetCustomAttribute<Api>();
            return attribute.GetApi();
        }
    }
}
