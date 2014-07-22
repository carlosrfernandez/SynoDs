namespace SynoDs.Core.Dal.FileStation.Info
{
    using System.Runtime.Serialization;
    using HttpBase;
    using Attributes;
    using Enums;

    [DataContract]
    [Api(RootApi.FileStation, ChildApi.Info)]
    public class FsInfoResponse : ResponseWrapper<FsInfo>
    {
    }
}
