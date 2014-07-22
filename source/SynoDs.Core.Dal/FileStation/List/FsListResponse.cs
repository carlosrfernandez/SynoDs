namespace SynoDs.Core.Dal.FileStation.List
{
    using Attributes;
    using Enums;
    using HttpBase;

    [Api(RootApi.FileStation, ChildApi.List)]
    public class FsListResponse : ResponseWrapper<FsList>
    {
    }
}
