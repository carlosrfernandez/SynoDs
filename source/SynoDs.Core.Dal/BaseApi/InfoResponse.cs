using SynoDs.Core.Dal.Attributes;
using SynoDs.Core.Dal.Enums;
using SynoDs.Core.Dal.HttpBase;

namespace SynoDs.Core.Dal.BaseApi
{
    using System.Runtime.Serialization;
    using System.Collections.Generic;
    
    /// <summary>
    /// According to PDF specifications.
    /// This class contains the data members for the result from Syno.Api.Info and Syno.Api.Auth
    /// The string represents the API Name. 
    /// </summary>
    [DataContract]
    [Api(RootApi.API, ChildApi.Info)]
    public class InfoResponse : ResponseWrapper<ApiInfoWrapper>
    {

    }
}
