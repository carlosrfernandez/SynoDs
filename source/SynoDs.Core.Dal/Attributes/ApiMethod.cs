// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApiMethod.cs" company="">
//   
// </copyright>
// <summary>
//   The api method.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynoDs.Core.Dal.Attributes
{
    using System;

    /// <summary>
    /// The api method.
    /// </summary>
    public class ApiMethod : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiMethod"/> class.
        /// </summary>
        /// <param name="methodName">
        /// The method name.
        /// </param>
        public ApiMethod(string methodName)
        {
            this.MethodName = methodName;
        }

        /// <summary>
        /// Gets the method name.
        /// </summary>
        private string MethodName { get; }

        /// <summary>
        /// The get method name.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetMethodName()
        {
            return this.MethodName;
        }
    }
}