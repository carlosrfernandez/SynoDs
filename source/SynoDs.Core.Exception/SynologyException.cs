// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SynologyException.cs" company="">
//   
// </copyright>
// <summary>
//   The synology exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynoDs.Core.Exceptions
{
    using System;

    /// <summary>
    /// The synology exception.
    /// </summary>
    public class SynologyException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SynologyException"/> class.
        /// </summary>
        public SynologyException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SynologyException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public SynologyException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SynologyException"/> class.
        /// </summary>
        /// <param name="errorCode">
        /// The error code.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        public SynologyException(int errorCode, string message)
            : base(string.Format("Error code: {0}: {1}", errorCode, message))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SynologyException"/> class.
        /// </summary>
        /// <param name="error">
        /// The error.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        public SynologyException(string error, Exception innerException)
            : base(error, innerException)
        {
        }
    }
}