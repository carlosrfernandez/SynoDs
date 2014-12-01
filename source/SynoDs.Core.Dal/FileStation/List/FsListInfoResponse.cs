namespace SynoDs.Core.Dal.FileStation.List
{
    using System.Runtime.Serialization;
    using Attributes;
    using Enums;
    using HttpBase;

    [DataContract]
    [Api(RootApi.FileStation, ChildApi.List)]
    [AuthenticationRequired(true)]
    public class FsListInfoResponse : ResponseWrapper<FsListInfo>
    {
    }
}
