using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynoDs.Core.Interfaces.Synology
{
    public abstract class ApiBase : ApiCore
    {
        private readonly IAuthenticationProvider _authenticationProvider;
        private readonly IInformationProvider _informationProvider;

        protected ApiBase(IOperationProvider operationProvider, IErrorProvider errorProvider,
            IInformationProvider informationProvider, IAuthenticationProvider authenticationProvider) 
            : base(operationProvider, errorProvider)
        {
            _authenticationProvider = authenticationProvider;
            _informationProvider = informationProvider;
        }
    }
}
