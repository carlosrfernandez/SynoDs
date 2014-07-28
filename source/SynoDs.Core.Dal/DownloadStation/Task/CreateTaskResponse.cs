namespace SynoDs.Core.Dal.DownloadStation.Task
{
    using Attributes;
    using Enums;
    using HttpBase;

    [Api(RootApi.DownloadStation, ChildApi.Task)]
    public class CreateTaskResponse : ResponseWrapper<CreateTask>
    {
        
    }
}