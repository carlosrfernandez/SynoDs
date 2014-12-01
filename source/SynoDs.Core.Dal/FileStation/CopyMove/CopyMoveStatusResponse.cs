namespace SynoDs.Core.Dal.FileStation.CopyMove
{
    using System.Runtime.Serialization;
    using HttpBase;
    using Attributes;
    using Enums;

    [DataContract]
    [Api(RootApi.FileStation, ChildApi.CopyMove)]
    [AuthenticationRequired(true)]
    public class CopyMoveStatusResponse : ResponseWrapper<CopyMoveStatus>
    {
    }
}
