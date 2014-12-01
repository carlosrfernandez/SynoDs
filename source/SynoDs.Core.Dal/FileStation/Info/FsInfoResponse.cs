namespace SynoDs.Core.Dal.FileStation.Info
{
    using System.Runtime.Serialization;
    using HttpBase;
    using Attributes;
    using Enums;

    [DataContract]
    [Api(RootApi.FileStation, ChildApi.Info)]
    [AuthenticationRequired(true)]
    public class FsInfoResponse : ResponseWrapper<FsInfo>
    {
    }
}
