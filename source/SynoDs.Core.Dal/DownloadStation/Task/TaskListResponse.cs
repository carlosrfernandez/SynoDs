namespace SynoDs.Core.Dal.DownloadStation.Task
{
    using System.Runtime.Serialization;
    using HttpBase;
    using Attributes;
    using Enums;

    [DataContract]
    [Api(RootApi.DownloadStation, ChildApi.Task)]
    public class TaskListResponse : ResponseWrapper<TaskList>
    {

    }
}
