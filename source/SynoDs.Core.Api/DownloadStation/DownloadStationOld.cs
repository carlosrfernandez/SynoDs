//using System;
//using System.IO;
//using System.Threading.Tasks;
//using SynoDs.Core.Dal.DownloadStation.Info;
//using SynoDs.Core.Dal.DownloadStation.Task;
//using SynoDs.Core.Dal.HttpBase;

//namespace SynoDs.Core.Api.DownloadStation
//{
//    public sealed class DownloadStation : DiskStationBase
//    {
//        protected override string[] ImplementedApi
//        {
//            get
//            {
//                return new[] { "SYNO.API.Auth", "SYNO.DownloadStation.Task", "SYNO.DownloadStation.Info" };
//            }
//        }

//        public DownloadStation()
//        {
//            InternalSession = "DownloadStation";
//        }

//        public DownloadStation(Uri url, string username, string password)
//            : base(url, username, password)
//        { }

//        public async Task<DsInfoResponse> GetDownloadStationApiInfoAsync()
//        {
//            return await CallMethodAsync<DsInfoResponse>("SYNO.DownloadStation.Info", "getinfo");
//        }

//        public async Task<TaskListResponse> GetTaskListAsync()
//        {
//            return await CallMethodAsync<TaskListResponse>("SYNO.DownloadStation.Task", "list");
//        }


//        public async Task<TaskListResponse> GetTasksAsync(string[] taskIds, string[] additional)
//        {
//            return await GetTasksAsync(String.Join(",", taskIds), String.Join(",", additional));
//        }

//        public async Task<TaskListResponse> GetTasksAsync(string taskIds, string additional = "detail")
//        {
//            return await CallMethodAsync<TaskListResponse>("SYNO.DownloadStation.Task",
//                "getinfo", new RequestParameters
//                {
//                        {"id", taskIds},
//                        {"additional", additional}
//                    }
//            );
//        }

//        public async Task<TaskListResponse> GetTaskListAsync(string[] additional, int offset = 0, int limit = -1)
//        {
//            return await GetTaskListAsync(String.Join(",", additional), offset, limit);
//        }

//        public async Task<TaskListResponse> GetTaskListAsync(string additional, int offset = 0, int limit = -1)
//        {
//            return await CallMethodAsync<TaskListResponse>("SYNO.DownloadStation.Task",
//                "list", new RequestParameters
//                    {
//                        {"additional", additional},
//                        {"offset", offset.ToString()},
//                        {"limit", limit.ToString()}
//                    }
//           );
//        }

//        public async Task<TaskActionResponseBase> PauseTasksAsync(string[] taskIds)
//        {
//            return await PauseTasksAsync(String.Join(",", taskIds));
//        }

//        public async Task<TaskActionResponseBase> PauseTasksAsync(string taskIds)
//        {
//            return await CallMethodAsync<TaskActionResponseBase>("SYNO.DownloadStation.Task",
//                "pause", new RequestParameters
//                {
//                        {"id", taskIds},
//                    }
//            );
//        }

//        public async Task<TaskActionResponseBase> ResumeTasksAsync(string[] taskIds)
//        {
//            return await ResumeTasksAsync(String.Join(",", taskIds));
//        }

//        public async Task<TaskActionResponseBase> ResumeTasksAsync(string taskIds)
//        {
//            return await CallMethodAsync<TaskActionResponseBase>("SYNO.DownloadStation.Task",
//                "resume", new RequestParameters
//                {
//                        {"id", taskIds},
//                    }
//            );
//        }

//        public async Task<TaskActionResponseBase> DeleteTasksAsync(string[] taskIds, bool forceComplete = false)
//        {
//            return await DeleteTasksAsync(String.Join(",", taskIds), forceComplete);
//        }

//        public async Task<TaskActionResponseBase> DeleteTasksAsync(string taskIds, bool forceComplete = false)
//        {
//            var falseCompleteString = forceComplete ? "true" : "false"; 
//            return await CallMethodAsync<TaskActionResponseBase>("SYNO.DownloadStation.Task",
//                "delete", new RequestParameters
//                {
//                        {"id", taskIds},
//                        {"force_complete", falseCompleteString } 
//                    }
//            );
//        }
//        /// <summary>
//        /// Creates a task that requires no authentication (public ftp files, torrents, magnet links etc)
//        /// </summary>
//        /// <param name="url">Url for the task.</param>
//        /// <returns></returns>
//        public async Task<ResponseWrapper<Object>> CreateAnonymousTaskAsync(string url)
//        {
//            return await CallMethodAsync<ResponseWrapper<Object>>("SYNO.DownloadStation.Task",
//                "create", new RequestParameters
//                {
//                    {"uri", url}
//                }
//            );
//        }

//        /// <summary>
//        /// Creates a list of tasks that require no authentication (public ftp files, torrents, magnet links etc)
//        /// </summary>
//        /// <param name="urls">Url for the task.</param>
//        /// <returns>an emtpy response object</returns>
//        public async Task<ResponseWrapper<Object>> CreateAnonymousTaskAsync(string[] urls)
//        {
//            return await CallMethodAsync<ResponseWrapper<Object>>("SYNO.DownloadStation.Task",
//                "create", new RequestParameters
//                {
//                    {"uri", String.Join(",", urls)}
//                }
//            );
//        }

//        /// <summary>
//        /// Creates a singe download task with authentication.
//        /// </summary>
//        /// <param name="url">The url.</param>
//        /// <param name="username">Username for download</param>
//        /// <param name="password">Password for download</param>
//        /// <returns>Success / failure data.</returns>
//        public async Task<ResponseWrapper<Object>> CreateAuthenticatedTaskAsync(string url, string username, string password)
//        {
//            return await CallMethodAsync<ResponseWrapper<Object>>("SYNO.DownloadStation.Task", "create",
//                new RequestParameters
//                    {
//                        {"uri", url},
//                        {"username", username},
//                        {"password", password}
//                    }
//                );
//        }

//        /// <summary>
//        /// Creates a task with a list of URLs that require username and password.
//        /// </summary>
//        /// <param name="urls">Urls to create download tasks</param>
//        /// <param name="username">Username for download</param>
//        /// <param name="password">Password for download</param>
//        /// <returns>A response with the success / failure details.</returns>
//        public async Task<ResponseWrapper<Object>> CreateAuthenticatedTaskAsync(string[] urls, string username, string password)
//        {
//            return await CallMethodAsync<ResponseWrapper<Object>>("SYNO.DownloadStation.Task", "create",
//                new RequestParameters
//                {
//                    {"uri", String.Join(",", urls)},
//                    {"username", username},
//                    {"password", password}
//                });
//        }


//        /// <summary>
//        /// Creates a task by uploading a torrent file to the DS.
//        /// </summary>
//        /// <param name="fileName">Filename</param>
//        /// <param name="fileStream">Stream with file contents.</param>
//        /// <returns>No particular response. Only success data and error data.</returns>
//        public async Task<ResponseWrapper<Object>> CreateTaskAsync(string fileName, Stream fileStream)
//        {
//            return await PostFileAsync<ResponseWrapper<Object>>("SYNO.DownloadStation.Task", "create", fileName, fileStream);
//        }
//    }
//}
