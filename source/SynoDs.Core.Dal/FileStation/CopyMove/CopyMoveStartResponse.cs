using SynoDs.Core.Dal.Attributes;
using SynoDs.Core.Dal.Enums;

namespace SynoDs.Core.Dal.FileStation.CopyMove
{
    using System.Runtime.Serialization;
    using HttpBase;

    [DataContract]
    [Api(RootApi.FileStation,ChildApi.CopyMove)]
    public class CopyMoveStartResponse : ResponseWrapper<CopyMoveStart>
    {
    }
}
