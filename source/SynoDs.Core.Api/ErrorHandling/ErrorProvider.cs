using SynoDs.Core.Interfaces;

namespace SynoDs.Core.Api.ErrorHandling
{
    /// <summary>
    /// Provides the errors for all documented api errors
    /// </summary>
    public class ErrorProvider : IErrorProvider
    {
        //Todo add error info to resource file.
        public string GetErrorDescriptionForCode(string apiName, int errorCode)
        {
            // get that error info----
            return ReadErrorCodeFromResource(string.Format("{0}{1}", apiName, errorCode));
        }

        private string ReadErrorCodeFromResource(string index)
        {
            try
            {
                return Resources.ErrorRepository.ResourceManager.GetString(index);
            }
            catch
            {
                return "Unknown error!";
            }
        }
    }
}
