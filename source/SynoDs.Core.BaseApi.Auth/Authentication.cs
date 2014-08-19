using System.Threading.Tasks;
using SynoDs.Core.Api;
using SynoDs.Core.Interfaces.Synology;

namespace SynoDs.Core.BaseApi.Auth
{
    public class Authentication : Base, IAuthenticationProvider
    {
        public bool IsLoggedIn { get; set; }
        
        public string Sid { get; set; }

        public Authentication()
        {
            IsLoggedIn = false;
        }

        public async Task<bool> LoginAsync()
        {
            IsLoggedIn = true;
            return true;
        }

        public async Task<bool> LogoutAsync()
        {
            IsLoggedIn = false;
            return true;
        }
    }
}
