using SynoDs.Core.DownloadStation.ErrorHandling;

namespace SynoDs.Core.DownloadStation
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using Api;
    using CrossCutting.Common;
    using Dal.DownloadStation.Task;
    using Dal.Enums;
    using Dal.HttpBase;
    using Interfaces;

    /// <summary>
    /// DownloadStation client class.
    /// </summary>
    public sealed class DownloadManager : Base
    {
        // TODO: Add constructor overload to inject error providers.

        public const string DlSessionName = "DownloadStation";

        private readonly IErrorProvider _dlErrorProvider;

        protected override IErrorProvider ErrorProvider
        {
            get { return this._dlErrorProvider; }
        }

        protected override string GetSessionName()
        {
            return DlSessionName;
        }

        /// <summary>
        /// Basic Constructor
        /// </summary>
        public DownloadManager()
        {
            _dlErrorProvider = new DownloadErrorProvider();
        }

        /// <summary>
        /// Injection of IErrorProvider for the DownloadStation api errors.
        /// </summary>
        /// <param name="errorProvider">The source of error descriptions.</param>
        public DownloadManager(IErrorProvider errorProvider)
        {
            _dlErrorProvider = errorProvider;
        }

        /// <summary>
        /// Retrieves a list of tasks that are available on the DownloadStation
        /// </summary>
        /// <param name="offset">For pagination purposes.</param>
        /// <param name="limit">Limit the number of tasks to retrieve.</param>
        /// <param name="additionalInfo">Additional information <see cref="TaskAdditionalInfoValues"/></param>
        /// <returns>A list of Tasks, <see cref="TaskListResponse"/></returns>
        public async Task<TaskListResponse> ListTasksAsync(int offset = 0, int limit = -1,
            TaskAdditionalInfoValues[] additionalInfo = null)
        {
            var requestParams = new RequestParameters();

            if (offset != 0)
                requestParams.Add("offset", offset.ToString());

            if (limit != -1)
                requestParams.Add("limit", limit.ToString());

            if (additionalInfo != null && additionalInfo.Length != 0)
            {
                requestParams.Add("additional", string.Join(",", additionalInfo).ToLower());
            }

            if (requestParams.Count > 0)
            {
                return await PerformOperationAsync<TaskListResponse>(requestParams);
            }
            return await PerformOperationAsync<TaskListResponse>();
        }

        /// <summary>
        /// Gets the information on the Task(s) id's supplied. 
        /// </summary>
        /// <param name="taskList">The ID's of the tasks to get information on.</param>
        /// <param name="additionalInfo">Additional info to request. <see cref="TaskAdditionalInfoValues"/></param>
        /// <returns>A task information result. <see cref="TaskGetInfoResponse"/></returns>
        public async Task<TaskGetInfoResponse> GetTaskInfoAsync(IList<string> taskList,
            TaskAdditionalInfoValues[] additionalInfo = null)
        {
            Validate.ArgumentIsNotNullOrEmpty(taskList);

            var requestParams = new RequestParameters
            {
                {"id", string.Join(",", taskList)}
            };

            if (additionalInfo != null)
                requestParams.Add("additional", string.Join(",", additionalInfo).ToLower());

            return await PerformOperationAsync<TaskGetInfoResponse>(requestParams);
        }

        /// <summary>
        /// Creates a Download task on the DownloadStation using the supplied URL and additional optional parameters.
        /// </summary>
        /// <param name="taskUrl">The URI for the download to create. Can be any valid URI.</param>
        /// <param name="userName">Optional username if the download requires authentication.</param>
        /// <param name="password">Optional password if the download requires authentication.</param>
        /// <param name="unzipPass">Unzip password if it is a zip file download and it has a password.</param>
        /// <param name="fileStream">FileStream for uploading a torrent file directly from your client.</param>
        /// <returns>A creation response, <see cref="CreateTaskResponse"/></returns>
        public async Task<CreateTaskResponse> CreateTaskAsync(string taskUrl, string userName = "", 
            string password = "", string unzipPass = "", Stream fileStream = null)
        {

            Validate.ArgumentIsNotNullOrEmpty(taskUrl);
            
            // Todo: refactor parameter parsing
            var requestParams = new RequestParameters
            {
                { "uri", taskUrl }
            };
            if (userName != string.Empty)
                requestParams.Add("username", userName);

            if (password != string.Empty)
                requestParams.Add("password", password);

            if (unzipPass != string.Empty)
                requestParams.Add("unzip_password", unzipPass);

            if (fileStream != null && fileStream.Length >0)
                return await PerformOperationWithFileAsync<CreateTaskResponse>(requestParams, fileStream);

            return await PerformOperationAsync<CreateTaskResponse>(requestParams);
        }

        /// <summary>
        /// Deletes a Task(s) from the DownloadStation
        /// </summary>
        /// <param name="taskList">List of Task ID's to delete.</param>
        /// <param name="forceComplete">Delete tasks and force to move uncompleted download files to the destination.</param>
        /// <returns>A delete response, with a list of ID's and a boolean value indicating if it was deleted correctly.
        ///  <see cref="DeleteTaskResponse"/></returns>
        public async Task<DeleteTaskResponse> DeleteTaskAsync(IList<string> taskList, bool forceComplete)
        {
            Validate.ArgumentIsNotNullOrEmpty(taskList);

            var requestParams = new RequestParameters
            {
                {"id", string.Join(",", taskList)},
                {"force_complete", forceComplete ? "true" : "false"}
            };

            return await PerformOperationAsync<DeleteTaskResponse>(requestParams);
        }
        
        /// <summary>
        /// Pause Tasks.
        /// </summary>
        /// <param name="taskList">The list of tasks to pause.</param>
        /// <returns>A list of Task Id's with the boolean value indicating if it was paused correctly.</returns>
        public async Task<PauseTaskResponse> PauseTaskAsync(IList<string> taskList)
        {
            Validate.ArgumentIsNotNullOrEmpty(taskList);

            var requestParams = new RequestParameters
            {
                {"id", string.Join(",", taskList)},
            };

            return await PerformOperationAsync<PauseTaskResponse>(requestParams);
        }

        /// <summary>
        /// Resume Tasks
        /// </summary>
        /// <param name="taskList">The list of tasks to resume.</param>
        /// <returns>A list of Id's with the boolean value indicating if it was properly resumed.</returns>
        public async Task<ResumeTaskResponse> ResumeTaskAsync(IList<string> taskList)
        {
            Validate.ArgumentIsNotNullOrEmpty(taskList);

            var requestParams = new RequestParameters
            {
                {"id", string.Join(",", taskList)},
            };

            return await PerformOperationAsync<ResumeTaskResponse>(requestParams);
        }
    }
}
