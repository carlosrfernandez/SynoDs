// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDownloadStation.cs" company="">
//   
// </copyright>
// <summary>
//   The DownloadStation interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynoDs.Core.Contracts.Synology
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    using SynoDs.Core.Dal.DownloadStation.Task;
    using SynoDs.Core.Dal.Enums;

    /// <summary>
    /// The DownloadStation interface.
    /// </summary>
    public interface IDownloadStation
    {
        /// <summary>
        /// The list tasks async.
        /// </summary>
        /// <param name="diskStation">
        /// The disk station.
        /// </param>
        /// <param name="offset">
        /// The offset.
        /// </param>
        /// <param name="limit">
        /// The limit.
        /// </param>
        /// <param name="additionalInfo">
        /// The additional info.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<TaskListResponse> ListTasksAsync(
            IDiskStationSession diskStation, 
            int offset = 0, 
            int limit = -1, 
            TaskAdditionalInfoValues[] additionalInfo = null);

        /// <summary>
        /// The get task info async.
        /// </summary>
        /// <param name="diskStation">
        /// The disk station.
        /// </param>
        /// <param name="taskList">
        /// The task list.
        /// </param>
        /// <param name="additionalInfo">
        /// The additional info.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<TaskGetInfoResponse> GetTaskInfoAsync(
            IDiskStationSession diskStation, 
            IList<string> taskList, 
            TaskAdditionalInfoValues[] additionalInfo = null);

        /// <summary>
        /// The create task async.
        /// </summary>
        /// <param name="diskStation">
        /// The disk station.
        /// </param>
        /// <param name="taskUrl">
        /// The task url.
        /// </param>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <param name="unzipPass">
        /// The unzip pass.
        /// </param>
        /// <param name="fileStream">
        /// The file stream.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<CreateTaskResponse> CreateTaskAsync(
            IDiskStationSession diskStation, 
            string taskUrl, 
            string userName = "", 
            string password = "", 
            string unzipPass = "", 
            Stream fileStream = null);

        /// <summary>
        /// The delete task async.
        /// </summary>
        /// <param name="diskStation">
        /// The disk station.
        /// </param>
        /// <param name="taskList">
        /// The task list.
        /// </param>
        /// <param name="forceComplete">
        /// The force complete.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<DeleteTaskResponse> DeleteTaskAsync(
            IDiskStationSession diskStation, 
            IList<string> taskList, 
            bool forceComplete);

        /// <summary>
        /// The pause task async.
        /// </summary>
        /// <param name="diskStation">
        /// The disk station.
        /// </param>
        /// <param name="taskList">
        /// The task list.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PauseTaskResponse> PauseTaskAsync(IDiskStationSession diskStation, IList<string> taskList);

        /// <summary>
        /// The resume task async.
        /// </summary>
        /// <param name="diskStation">
        /// The disk station.
        /// </param>
        /// <param name="taskList">
        /// The task list.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<ResumeTaskResponse> ResumeTaskAsync(IDiskStationSession diskStation, List<string> taskList);
    }
}