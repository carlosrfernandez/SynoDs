using System;
using SynoDs.Core.Interfaces;

namespace SynoDs.Core.Exceptions
{
    public abstract class ErrorProviderBase : IErrorProvider
    {
        protected ErrorProviderBase(IErrorRepository errorRepository)
        {
            _errorRepository = errorRepository;
        }

        private readonly IErrorRepository _errorRepository;

        public string GetErrorDescriptionForCode(int errorCode)
        {
            try
            {
                var error = _errorRepository.GetErrorDescription(errorCode);
                if (string.IsNullOrEmpty(error))
                {
                    throw new Exception("Error while getting the error description.");
                }
                return error;
            }
            catch (Exception exception)
            {
                return string.Format("Unknown error occurred while getting error description: '{0}'", exception.Message);
            }
        }
    }
}
