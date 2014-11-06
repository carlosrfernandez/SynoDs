using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynoDs.Core.Interfaces.Synology
{
    public abstract class ApiCore
    {
        private readonly IOperationProvider _apiOperationProvider;
        private readonly IErrorProvider _apiErrorProvider;

        protected ApiCore(IOperationProvider operationProvider, IErrorProvider errorProvider)
        {
            _apiOperationProvider = operationProvider;
            _apiErrorProvider = errorProvider;
        }
    }
}
