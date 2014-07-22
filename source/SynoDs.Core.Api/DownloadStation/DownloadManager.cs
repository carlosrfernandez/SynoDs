using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SynoDs.Core.Dal.Enums;
using SynoDs.Core.Dal.HttpBase;

namespace SynoDs.Core.Api.DownloadStation
{
    using Dal.DownloadStation.Task;
    
    public class DownloadManager : DsClientBase
    {
        public DownloadManager()
        {
            SessionName = "DownloadManager";
        }

        public DownloadManager(string userName, string password, Uri hostUri) : base(userName, password, hostUri)
        {
            
        }

        public async Task<TaskListResponse> ListTasksAsync(int offset = 0, int limit = -1,
            TaskAdditionalInfoValues[] additionalInfo = null)
        {
            var requestParams = new RequestParameters()
                ;
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

        public async Task<TaskGetInfoResponse> GetTaskInfoAsync(IList<string> taskList,
            TaskAdditionalInfoValues[] additionalInfo = null)
        {
            var requestParams = new RequestParameters
            {
                {"id", string.Join(",", taskList)}
            };

            if (additionalInfo != null)
                requestParams.Add("additional", string.Join(",", additionalInfo).ToLower());

            return await PerformOperationAsync<TaskGetInfoResponse>(requestParams);
        }

        public async Task<CreateTaskResponse> CreateTaskAsync(Uri taskUri, string userName = "", 
            string password = "", string unzipPass = "", Stream fileStream = null)
        {
            // Todo: refactor parameter parsin
            var requestParams = new RequestParameters
            {
                { "uri", taskUri.ToString() }
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

        public async Task<DeleteTaskResponse> DeleteTaskAsync(IList<string> taskList, bool forceComplete)
        {
            var requestParams = new RequestParameters
            {
                {"id", string.Join(",", taskList)},
                {"force_complete", forceComplete ? "true" : "false"}
            };

            return await PerformOperationAsync<DeleteTaskResponse>(requestParams);
        }
        
        public async Task<PauseTaskResponse> PauseTaskAsync(IList<string> taskList)
        {
            var requestParams = new RequestParameters
            {
                {"id", string.Join(",", taskList)},
            };

            return await PerformOperationAsync<PauseTaskResponse>(requestParams);
        }

        public async Task<ResumeTaskResponse> ResumeTaskAsync(IList<string> taskList)
        {
            var requestParams = new RequestParameters
            {
                {"id", string.Join(",", taskList)},
            };

            return await PerformOperationAsync<ResumeTaskResponse>(requestParams);
        }


    }
}
