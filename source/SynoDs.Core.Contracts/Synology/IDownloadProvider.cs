using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SynoDs.Core.Dal.DownloadStation.Task;
using SynoDs.Core.Dal.Enums;

namespace SynoDs.Core.Contracts.Synology
{
    public interface IDownloadProvider
    {
        Task<TaskListResponse> ListTasksAsync(int offset = 0, int limit = -1,
            TaskAdditionalInfoValues[] additionalInfo = null);

        Task<TaskGetInfoResponse> GetTaskInfoAsync(IList<string> taskList,
            TaskAdditionalInfoValues[] additionalInfo = null);

        Task<CreateTaskResponse> CreateTaskAsync(string taskUrl, string userName = "",
            string password = "", string unzipPass = "", Stream fileStream = null);

        Task<DeleteTaskResponse> DeleteTaskAsync(IList<string> taskList, bool forceComplete);

        Task<PauseTaskResponse> PauseTaskAsync(IList<string> taskList);

        Task<ResumeTaskResponse> ResumeTaskAsync(IList<string> taskList);
    }
}
