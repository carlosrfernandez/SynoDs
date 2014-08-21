namespace SynoDs.Core.CrossCutting.Common
{
    using System;

    /// <summary>
    /// Used to validate parameters.
    /// </summary>
    public static class Validate
    {
        public static void ArgumentIsNotNullOrEmpty(object obj)
        {
            ArgumentIsNull(obj);

            var isEmtpy = string.IsNullOrEmpty(obj.ToString());

            if (!isEmtpy) return;
            throw new ArgumentException(obj.GetType() + "is empty!");
        }

        public static void ArgumentIsNull(object obj)
        {
            if (obj == null)
            {
                throw new NullReferenceException("Object is null");
            }
        }
    }
}
