// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DownloadManager.cs" company="">
//   
// </copyright>
// <summary>
//   DownloadStation client class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynoDs.Core.DownloadStation
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    using SynoDs.Core.Contracts.Synology;
    using SynoDs.Core.Dal.DownloadStation.Task;
    using SynoDs.Core.Dal.Enums;
    using SynoDs.Core.Dal.HttpBase;

    /// <summary>
    ///     DownloadStation client class.
    /// </summary>
    public class DownloadManager : IDownloadStation
    {
        /// <summary>
        ///     The dl session name
        /// </summary>
        public const string DlSessionName = "DownloadStation";

        /// <summary>
        ///     The request service
        /// </summary>
        private readonly IRequestService requestService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadManager"/> class.
        ///     Starts an instance of the DL manager.
        /// </summary>
        /// <param name="requestService">
        /// The request Service.
        /// </param>
        /// <param name="diskStation">
        /// The session handler
        /// </param>
        public DownloadManager(IRequestService requestService)
        {
            if (requestService == null)
            {
                throw new ArgumentNullException(nameof(requestService));
            }

            this.requestService = requestService;
        }

        /// <summary>
        /// Retrieves a list of tasks that are available on the DownloadStation
        /// </summary>
        /// <param name="diskStation"></param>
        /// <param name="offset">
        ///     For pagination purposes.
        /// </param>
        /// <param name="limit">
        ///     Limit the number of tasks to retrieve.
        /// </param>
        /// <param name="additionalInfo">
        ///     Additional information <see cref="TaskAdditionalInfoValues"/>
        /// </param>
        /// <returns>
        /// A list of Tasks, <see cref="TaskListResponse"/>
        /// </returns>
        public async Task<TaskListResponse> ListTasksAsync(IDiskStationSession diskStation, int offset = 0, int limit = -1, TaskAdditionalInfoValues[] additionalInfo = null)
        {
            var requestParams = new RequestParameters { { "_sid", diskStation.SessionId } };

            if (offset != 0)
            {
                requestParams.Add("offset", offset.ToString());
            }

            if (limit != -1)
            {
                requestParams.Add("limit", limit.ToString());
            }

            if (additionalInfo != null && additionalInfo.Length != 0)
            {
                requestParams.Add("additional", string.Join(",", additionalInfo).ToLower());
            }

            if (requestParams.Count > 0)
            {
                return
                    await
                    this.requestService.PerformOperationAsync<TaskListResponse>(
                        diskStation.Host.ToString(), 
                        requestParams);
            }

            return await this.requestService.PerformOperationAsync<TaskListResponse>(null);
        }

        /// <summary>
        /// Gets the information on the Task(s) id's supplied.
        /// </summary>
        /// <param name="diskStation"></param>
        /// <param name="taskList">
        ///     The ID's of the tasks to get information on.
        /// </param>
        /// <param name="additionalInfo">
        ///     Additional info to request. <see cref="TaskAdditionalInfoValues"/>
        /// </param>
        /// <returns>
        /// A task information result. <see cref="TaskGetInfoResponse"/>
        /// </returns>
        public async Task<TaskGetInfoResponse> GetTaskInfoAsync(
            IDiskStationSession  diskStation,
            IList<string> taskList, 
            TaskAdditionalInfoValues[] additionalInfo = null)
        {
            var requestParams = new RequestParameters
                                    {
                                        { "id", string.Join(",", taskList) }, 
                                        { "_sid", diskStation.SessionId }
                                    };

            if (additionalInfo != null)
            {
                requestParams.Add("additional", string.Join(",", additionalInfo).ToLower());
            }

            return
                await
                this.requestService.PerformOperationAsync<TaskGetInfoResponse>(
                    diskStation.Host.ToString(), 
                    requestParams);
        }

        /// <summary>
        /// Creates a Download task on the DownloadStation using the supplied URL and additional optional parameters.
        /// </summary>
        /// <param name="diskStation"></param>
        /// <param name="taskUrl">
        ///     The URI for the download to create. Can be any valid URI.
        /// </param>
        /// <param name="userName">
        ///     Optional username if the download requires authentication.
        /// </param>
        /// <param name="password">
        ///     Optional password if the download requires authentication.
        /// </param>
        /// <param name="unzipPass">
        ///     Unzip password if it is a zip file download and it has a password.
        /// </param>
        /// <param name="fileStream">
        ///     FileStream for uploading a torrent file directly from your client.
        /// </param>
        /// <returns>
        /// A creation response, <see cref="CreateTaskResponse"/>
        /// </returns>
        public async Task<CreateTaskResponse> CreateTaskAsync(IDiskStationSession diskStation, string taskUrl, string userName = "", string password = "", string unzipPass = "", Stream fileStream = null)
        {
            // Todo: refactor parameter parsing
            var requestParams = new RequestParameters
                                    {
                                        { "uri", taskUrl }, 
                                        { "_sid", diskStation.SessionId }
                                    };

            if (userName != string.Empty)
            {
                requestParams.Add("username", userName);
            }

            if (password != string.Empty)
            {
                requestParams.Add("password", password);
            }

            if (unzipPass != string.Empty)
            {
                requestParams.Add("unzip_password", unzipPass);
            }

            if (fileStream != null && fileStream.Length > 0)
            {
                return
                    await
                    this.requestService.PerformOperationWithFileAsync<CreateTaskResponse>(
                        diskStation.Host.ToString(), 
                        requestParams, 
                        fileStream);
            }

            return
                await
                this.requestService.PerformOperationAsync<CreateTaskResponse>(
                    diskStation.Host.ToString(), 
                    requestParams);
        }

        /// <summary>
        /// Deletes a Task(s) from the DownloadStation
        /// </summary>
        /// <param name="diskStation">
        /// </param>
        /// <param name="taskList">
        ///     List of Task ID's to delete.
        /// </param>
        /// <param name="forceComplete">
        ///     Delete tasks and force to move uncompleted download files to the destination.
        /// </param>
        /// <returns>
        /// A delete response, with a list of ID's and a boolean value indicating if it was deleted correctly.
        ///     <see cref="DeleteTaskResponse"/>
        /// </returns>
        public async Task<DeleteTaskResponse> DeleteTaskAsync(IDiskStationSession diskStation, IList<string> taskList, bool forceComplete)
        {
            var requestParams = new RequestParameters
                                    {
                                        { "id", string.Join(",", taskList) }, 
                                        { "force_complete", forceComplete ? "true" : "false" }, 
                                        { "_sid", diskStation.SessionId }
                                    };

            return
                await
                this.requestService.PerformOperationAsync<DeleteTaskResponse>(
                    diskStation.Host.ToString(), 
                    requestParams);
        }

        /// <summary>
        /// Pause Tasks.
        /// </summary>
        /// <param name="diskStation">
        /// </param>
        /// <param name="taskList">
        ///     The list of tasks to pause.
        /// </param>
        /// <returns>
        /// A list of Task Id's with the boolean value indicating if it was paused correctly.
        /// </returns>
        public async Task<PauseTaskResponse> PauseTaskAsync(IDiskStationSession diskStation, IList<string> taskList)
        {
            var requestParams = new RequestParameters
                                    {
                                        { "id", string.Join(",", taskList) }, 
                                        { "_sid", diskStation.SessionId }
                                    };

            return
                await
                this.requestService.PerformOperationAsync<PauseTaskResponse>(
                    diskStation.Host.ToString(), 
                    requestParams);
        }

        /// <summary>
        /// Resume Tasks
        /// </summary>
        /// <param name="diskStation"></param>
        /// <param name="taskList">
        ///     The list of tasks to resume.
        /// </param>
        /// <returns>
        /// A list of Id's with the boolean value indicating if it was properly resumed.
        /// </returns>
        public async Task<ResumeTaskResponse> ResumeTaskAsync(IDiskStationSession diskStation, List<string> taskList)
        {
            var requestParams = new RequestParameters
                                    {
                                        { "id", string.Join(",", taskList) }, 
                                        { "_sid", diskStation.SessionId }
                                    };

            return
                await
                this.requestService.PerformOperationAsync<ResumeTaskResponse>(
                    diskStation.Host.ToString(), 
                    requestParams);
        }
    }
}