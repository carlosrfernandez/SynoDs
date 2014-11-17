using System;
using SynoDs.Core.Exceptions;
using SynoDs.Core.Interfaces;

namespace SynoDs.Core.Api.ErrorHandling
{
    /// <summary>
    /// Provides the errors for all documented api errors
    /// </summary>
    public class ErrorProvider : IErrorProvider
    {
        private readonly IAttributeReader _attributeReader;

        public ErrorProvider(IAttributeReader attributeReader)
        {
            _attributeReader = attributeReader;
        }

        //Todo add error info to resource file.
        public string GetErrorDescriptionForType<T>(int errorCode)
        {
            // Get API info. 
            var apiName = _attributeReader.ReadApiNameFromT<T>();
            
            // get that error info.
            return ReadErrorCodeFromResource(string.Format("{0}{1}", apiName, errorCode));
        }

        private string ReadErrorCodeFromResource(string index)
        {
            try
            {
                return Resources.ErrorRepository.ResourceManager.GetString(index);
            }
            catch(Exception exception)
            {
                throw new SynologyException("Could not get error description, unknown error occurred. See inner exception for details", exception);
            }
        }
    }
}
