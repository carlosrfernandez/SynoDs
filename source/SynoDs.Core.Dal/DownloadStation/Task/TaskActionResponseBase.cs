namespace SynoDs.Core.Dal.DownloadStation.Task
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using HttpBase;
    using Attributes;
    using Enums;

    [DataContract]
    [Api(RootApi.DownloadStation, ChildApi.Task)]
    public class TaskActionResponseBase : ResponseWrapper<IEnumerable<TaskOperationBase>>
    {

    }
}
