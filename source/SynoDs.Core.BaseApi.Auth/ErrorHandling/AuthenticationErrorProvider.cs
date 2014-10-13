using SynoDs.Core.Api;
using SynoDs.Core.Interfaces;

namespace SynoDs.Core.BaseApi.Auth.ErrorHandling
{
    public class AuthenticationErrorProvider : ErrorProviderBase
    {
        public AuthenticationErrorProvider(IErrorRepository errorRepository) : base(errorRepository)
        {
        }

        public AuthenticationErrorProvider() : base (new AuthenticationErrorRepository())
        {
        }
    }
}
