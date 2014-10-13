namespace SynoDs.Core.BaseApi.Auth.ErrorHandling
{
    using Interfaces;

    public class AuthenticationErrorRepository: IErrorRepository
    {

        public string GetErrorDescription(int errorCode)
        {
            return "Unknown error while authenticating";
        }
    }
}
