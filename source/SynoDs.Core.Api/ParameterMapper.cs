using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SynoDs.Core.Dal.BaseApi;
using SynoDs.Core.Interfaces;

namespace SynoDs.Core.Api
{
    public class ParameterMapper : IParameterMapper
    {

        public Dal.HttpBase.RequestParameters CreateRequestParameters<T1>()
        {
            throw new NotImplementedException();
        }
    }
}
