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
        ///     The disk station session handler
        /// </summary>
        private readonly DiskStationDto diskStationDto;

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
        /// <param name="diskStationDto">
        /// The session handler
        /// </param>
        public DownloadManager(IRequestService requestService, DiskStationDto diskStationDto)
        {
            if (requestService == null)
            {
                throw new ArgumentNullException(nameof(requestService));
            }

            if (diskStationDto == null)
            {
                throw new ArgumentNullException(nameof(diskStationDto));
            }

            this.requestService = requestService;
            this.diskStationDto = diskStationDto;
        }

        /// <summary>
        /// Retrieves a list of tasks that are available on the DownloadStation
        /// </summary>
        /// <param name="offset">
        /// For pagination purposes.
        /// </param>
        /// <param name="limit">
        /// Limit the number of tasks to retrieve.
        /// </param>
        /// <param name="additionalInfo">
        /// Additional information <see cref="TaskAdditionalInfoValues"/>
        /// </param>
        /// <returns>
        /// A list of Tasks, <see cref="TaskListResponse"/>
        /// </returns>
        public async Task<TaskListResponse> ListTasksAsync(
            int offset = 0, 
            int limit = -1, 
            TaskAdditionalInfoValues[] additionalInfo = null)
        {
            var requestParams = new RequestParameters { { "_sid", this.diskStationDto.SessionId } };

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
                        this.diskStationDto.DiskStation.HostName.ToString(), 
                        requestParams);
            }

            return await this.requestService.PerformOperationAsync<TaskListResponse>(null);
        }

        /// <summary>
        /// Gets the information on the Task(s) id's supplied.
        /// </summary>
        /// <param name="taskList">
        /// The ID's of the tasks to get information on.
        /// </param>
        /// <param name="additionalInfo">
        /// Additional info to request. <see cref="TaskAdditionalInfoValues"/>
        /// </param>
        /// <returns>
        /// A task information result. <see cref="TaskGetInfoResponse"/>
        /// </returns>
        public async Task<TaskGetInfoResponse> GetTaskInfoAsync(
            IList<string> taskList, 
            TaskAdditionalInfoValues[] additionalInfo = null)
        {
            var requestParams = new RequestParameters
                                    {
                                        { "id", string.Join(",", taskList) }, 
                                        { "_sid", this.diskStationDto.SessionId }
                                    };

            if (additionalInfo != null)
            {
                requestParams.Add("additional", string.Join(",", additionalInfo).ToLower());
            }

            return
                await
                this.requestService.PerformOperationAsync<TaskGetInfoResponse>(
                    this.diskStationDto.DiskStation.HostName.ToString(), 
                    requestParams);
        }

        /// <summary>
        /// Creates a Download task on the DownloadStation using the supplied URL and additional optional parameters.
        /// </summary>
        /// <param name="taskUrl">
        /// The URI for the download to create. Can be any valid URI.
        /// </param>
        /// <param name="userName">
        /// Optional username if the download requires authentication.
        /// </param>
        /// <param name="password">
        /// Optional password if the download requires authentication.
        /// </param>
        /// <param name="unzipPass">
        /// Unzip password if it is a zip file download and it has a password.
        /// </param>
        /// <param name="fileStream">
        /// FileStream for uploading a torrent file directly from your client.
        /// </param>
        /// <returns>
        /// A creation response, <see cref="CreateTaskResponse"/>
        /// </returns>
        public async Task<CreateTaskResponse> CreateTaskAsync(
            string taskUrl, 
            string userName = "", 
            string password = "", 
            string unzipPass = "", 
            Stream fileStream = null)
        {
            // Todo: refactor parameter parsing
            var requestParams = new RequestParameters
                                    {
                                        { "uri", taskUrl }, 
                                        { "_sid", this.diskStationDto.SessionId }
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
                        this.diskStationDto.DiskStation.HostName.ToString(), 
                        requestParams, 
                        fileStream);
            }

            return
                await
                this.requestService.PerformOperationAsync<CreateTaskResponse>(
                    this.diskStationDto.DiskStation.HostName.ToString(), 
                    requestParams);
        }

        /// <summary>
        /// Deletes a Task(s) from the DownloadStation
        /// </summary>
        /// <param name="taskList">
        /// List of Task ID's to delete.
        /// </param>
        /// <param name="forceComplete">
        /// Delete tasks and force to move uncompleted download files to the destination.
        /// </param>
        /// <returns>
        /// A delete response, with a list of ID's and a boolean value indicating if it was deleted correctly.
        ///     <see cref="DeleteTaskResponse"/>
        /// </returns>
        public async Task<DeleteTaskResponse> DeleteTaskAsync(IList<string> taskList, bool forceComplete)
        {
            var requestParams = new RequestParameters
                                    {
                                        { "id", string.Join(",", taskList) }, 
                                        { "force_complete", forceComplete ? "true" : "false" }, 
                                        { "_sid", this.diskStationDto.SessionId }
                                    };

            return
                await
                this.requestService.PerformOperationAsync<DeleteTaskResponse>(
                    this.diskStationDto.DiskStation.HostName.ToString(), 
                    requestParams);
        }

        /// <summary>
        /// Pause Tasks.
        /// </summary>
        /// <param name="taskList">
        /// The list of tasks to pause.
        /// </param>
        /// <returns>
        /// A list of Task Id's with the boolean value indicating if it was paused correctly.
        /// </returns>
        public async Task<PauseTaskResponse> PauseTaskAsync(IList<string> taskList)
        {
            var requestParams = new RequestParameters
                                    {
                                        { "id", string.Join(",", taskList) }, 
                                        { "_sid", this.diskStationDto.SessionId }
                                    };

            return
                await
                this.requestService.PerformOperationAsync<PauseTaskResponse>(
                    this.diskStationDto.DiskStation.HostName.ToString(), 
                    requestParams);
        }

        /// <summary>
        /// Resume Tasks
        /// </summary>
        /// <param name="taskList">
        /// The list of tasks to resume.
        /// </param>
        /// <returns>
        /// A list of Id's with the boolean value indicating if it was properly resumed.
        /// </returns>
        public async Task<ResumeTaskResponse> ResumeTaskAsync(IList<string> taskList)
        {
            var requestParams = new RequestParameters
                                    {
                                        { "id", string.Join(",", taskList) }, 
                                        { "_sid", this.diskStationDto.SessionId }
                                    };

            return
                await
                this.requestService.PerformOperationAsync<ResumeTaskResponse>(
                    this.diskStationDto.DiskStation.HostName.ToString(), 
                    requestParams);
        }
    }
}