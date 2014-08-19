using System.Reflection;

namespace SynoDs.Core.ErrorProvider
{
    using Interfaces;

    /// <summary>
    /// This class handles basic error resource file and gets the 
    /// known error message for the supplied code.
    /// TODO: This class needs to be refactored in order to support multiple UI Cultures if possible.
    /// </summary>
    public class ResourceErrorProvider : IErrorProvider
    {
        private const string ErrorKeyPrefixSymbol = "_";
        private const string UnknownErrorCodeMessage = "Error code not found";

        /// <summary>
        /// TODO: Complete this...
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        public string GetErrorDescriptionForType<T>(int errorCode)
        {
            //var errorForCode =
            //    BaseErrors.ResourceManager.GetString(string.Format("{0}{1}", ErrorKeyPrefixSymbol, errorCode));
            
            //return string.IsNullOrEmpty(errorForCode) ? UnknownErrorCodeMessage : errorForCode;
            return string.Empty;
        }

        public IErrorRepository ErrorRepository { get; private set; }
        
        public string GetErrorDescriptionForType(int errorCode)
        {
            throw new System.NotImplementedException();
        }
    }
}
