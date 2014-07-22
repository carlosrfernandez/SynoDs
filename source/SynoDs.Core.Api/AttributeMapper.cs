using System.Linq;

namespace SynoDs.Core.Api
{
    using System.Reflection;
    using Dal.Attributes;

    public static class AttributeMapper
    {
        public static string ReadMethodFromInstance<T>()
        {
            var info = typeof(T).GetTypeInfo();
            var genericParams = info.BaseType.GenericTypeArguments;
            return genericParams.Select(type => type.GetTypeInfo().GetCustomAttribute<ApiMethod>())
                                .Where(methodName => methodName != null)
                                .Select(methodName => methodName.GetMethodName())
                                .FirstOrDefault();
        }

        public static string ReadApiNameFromInstance<T>()
        {
            var info = typeof (T).GetTypeInfo();
            var attribute = info.GetCustomAttribute<Api>();
            return attribute.GetApi();
        }
    }
}
