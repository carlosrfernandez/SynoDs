using System.Collections.Generic;
using SynoDs.Core.Dal.Attributes;

namespace SynoDs.Core.Dal.BaseApi
{
    [ApiMethod("query")]
    public class ApiInfoWrapper : Dictionary<string, ApiInfo> { }
}