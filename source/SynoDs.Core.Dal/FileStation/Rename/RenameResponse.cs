namespace SynoDs.Core.Dal.FileStation.Rename
{
    using System.Runtime.Serialization;
    using HttpBase;
    using Enums;
    using Attributes;

    [DataContract]
    [Api(RootApi.FileStation, ChildApi.Rename)]
    public class RenameResponse : ResponseWrapper<Rename>
    {
    }
}
