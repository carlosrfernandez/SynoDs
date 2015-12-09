// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Validate.cs" company="">
//   
// </copyright>
// <summary>
//   Used to validate parameters.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynoDs.Core.CrossCutting.Common
{
    using System;

    /// <summary>
    ///     Used to validate parameters.
    /// </summary>
    public static class Validate
    {
        /// <summary>
        /// The argument is not null or empty.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <exception cref="ArgumentException">
        /// </exception>
        public static void ArgumentIsNotNullOrEmpty(object obj)
        {
            ArgumentIsNull(obj);

            var isEmtpy = string.IsNullOrEmpty(obj.ToString());

            if (!isEmtpy)
            {
                return;
            }

            throw new ArgumentException(obj.GetType() + "is empty!");
        }

        /// <summary>
        /// The argument is null.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <exception cref="NullReferenceException">
        /// </exception>
        public static void ArgumentIsNull(object obj)
        {
            if (obj == null)
            {
                throw new NullReferenceException("Object is null");
            }
        }
    }
}