// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ErrorProvider.cs" company="">
//   
// </copyright>
// <summary>
//   Provides the errors for all documented api errors
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynoDs.Core.Error
{
    using System;

    using SynoDs.Core.Contracts;
    using SynoDs.Core.Error.Resources;
    using SynoDs.Core.Exceptions;

    /// <summary>
    ///     Provides the errors for all documented api errors
    /// </summary>
    public class ErrorProvider : IErrorProvider
    {
        // TODO: Split API name to get the proper error description.
        // TODO: Add error information from all api's. 

        /// <summary>
        /// The attribute reader.
        /// </summary>
        private readonly IAttributeReader attributeReader;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorProvider"/> class.
        /// </summary>
        /// <param name="attributeReader">
        /// The attribute reader.
        /// </param>
        public ErrorProvider(IAttributeReader attributeReader)
        {
            this.attributeReader = attributeReader;
        }

        /// <summary>
        /// The get error description for type.
        /// </summary>
        /// <param name="errorCode">
        /// The error code.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="NotSupportedException">
        /// </exception>
        public string GetErrorDescriptionForType<T>(int errorCode)
        {
            // Get API info. 
            var apiName = this.attributeReader.ReadApiNameFromT<T>();
            var rootApi = apiName.Split('.')[1];
            string indexPrefix;
            switch (rootApi)
            {
                case "DownloadStation":
                    indexPrefix = "DS";
                    break;
                case "FileStation":
                    indexPrefix = "FS";
                    break;
                case "API":
                    indexPrefix = "Base";
                    break;
                default:
                    return "Unknown error.";
            }

            // get that error info.
            return ReadErrorCodeFromResource(string.Format("{0}{1}", indexPrefix, errorCode));
        }

        /// <summary>
        /// The read error code from resource.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="SynologyException">
        /// </exception>
        private static string ReadErrorCodeFromResource(string index)
        {
            try
            {
                var message = ErrorRepository.ResourceManager.GetString(index);
                if (!string.IsNullOrEmpty(message))
                {
                    return message;
                }

                message = "Unknown error has occurred!";
                return message;
            }
            catch (Exception exception)
            {
                throw new SynologyException(
                    "Could not get error description, unknown error occurred. See inner exception for details", 
                    exception);
            }
        }
    }
}