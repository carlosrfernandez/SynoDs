using System.Runtime.Serialization;
using SynoDs.Core.Dal.Attributes;
using SynoDs.Core.Dal.Enums;
using SynoDs.Core.Dal.HttpBase;

namespace SynoDs.Core.Dal.BaseApi
{
    [DataContract]
    [Api(RootApi.API, ChildApi.Auth)]
    public class LogoutResponse : ResponseWrapper<Logout>
    {
    }
}
