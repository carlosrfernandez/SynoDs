using System;

namespace SynoDs.Core.Dal.Attributes
{
    public class ApiMethod : Attribute
    {
        private string MethodName { get; set; }

        public ApiMethod(string methodName)
        {
            MethodName = methodName;
        }

        public string GetMethodName()
        {
            return MethodName;
        }
    }
}
