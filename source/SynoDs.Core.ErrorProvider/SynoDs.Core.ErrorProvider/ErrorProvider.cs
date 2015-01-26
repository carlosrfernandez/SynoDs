using System;
using SynoDs.Core.Contracts;
using SynoDs.Core.Exceptions;

namespace SynoDs.Core.Error
{
    /// <summary>
    /// Provides the errors for all documented api errors
    /// </summary>
    public class ErrorProvider : IErrorProvider
    {
        // TODO: Split API name to get the proper error description.
        // TODO: Add error information from all api's. 
        
        private readonly IAttributeReader _attributeReader;

        public ErrorProvider(IAttributeReader attributeReader)
        {
            _attributeReader = attributeReader;
        }

        public string GetErrorDescriptionForType<T>(int errorCode)
        {
            // Get API info. 
            var apiName = _attributeReader.ReadApiNameFromT<T>();
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
                    throw new NotSupportedException("Unknown error.");
            }
            
            // get that error info.
            return ReadErrorCodeFromResource(string.Format("{0}{1}", indexPrefix, errorCode));
        }

        private static string ReadErrorCodeFromResource(string index)
        {
            try
            {
                var message = Resources.ErrorRepository.ResourceManager.GetString(index);
                if (!string.IsNullOrEmpty(message)) return message;

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
