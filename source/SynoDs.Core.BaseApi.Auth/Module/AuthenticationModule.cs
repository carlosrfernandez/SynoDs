using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SynoDs.Core.BaseApi.Auth.ErrorHandling;
using SynoDs.Core.CrossCutting;
using SynoDs.Core.CrossCutting.Modularity;
using SynoDs.Core.Interfaces;
using SynoDs.Core.Interfaces.Modularity;
using SynoDs.Core.Interfaces.Synology;

namespace SynoDs.Core.BaseApi.Auth.Module
{
    public class AuthenticationModule : IApiModule
    {
        public void Configure()
        {
            IoCFactory.Container.Register<IAuthenticationProvider, Authentication>();
            IoCFactory.Container.Register<IErrorProvider, AuthenticationErrorProvider>();
        }
    }
}
