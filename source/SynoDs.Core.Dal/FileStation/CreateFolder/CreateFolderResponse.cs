using SynoDs.Core.Dal.Enums;

namespace SynoDs.Core.Dal.FileStation.CreateFolder
{
    using HttpBase;
    using Attributes;
    using Enums;
    using System.Runtime.Serialization;

    [DataContract]
    [Api(RootApi.FileStation, ChildApi.CreateFolder)]
    public class CreateFolderResponse : ResponseWrapper<FsFolders>
    {

    }
}
