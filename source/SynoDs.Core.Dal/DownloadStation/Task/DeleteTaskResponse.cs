namespace SynoDs.Core.Dal.DownloadStation.Task
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using HttpBase;
    using Enums;
    using Attributes;

    [DataContract]
    [Api(RootApi.DownloadStation, ChildApi.Task)]
    [AuthenticationRequired(true)]
    public class DeleteTaskResponse : ResponseWrapper<IEnumerable<DeleteTask>>
    {
        
    }
}