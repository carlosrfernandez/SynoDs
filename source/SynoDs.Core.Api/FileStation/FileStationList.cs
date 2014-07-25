using System.Net;

namespace SynoDs.Core.Api.FileStation
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dal.HttpBase;
    using Dal.FileStation.List;
    using Dal.Enums;

    /// <summary>
    /// Contains the Listing of remote files and folders
    /// (list_shares, getinfo, list)
    /// </summary>
    public partial class FileStation
    {
        /// <summary>
        /// Enumerates the files in a given folder.
        /// </summary>
        /// <param name="folderPath">A listed folder path started with a shared folder.</param>
        /// <param name="offset">Optional. Specify how many files are skipped before beginning to return listed files.</param>
        /// <param name="limit">Optional. Number of files requested. 0 indicates to list all files with a given folder.</param>
        /// <param name="sortBy">Optional. Specify which file information to sort on. Default: name.</param>
        /// <param name="sortDirection">Optional. Specify to sort ascending or descending</param>
        /// <param name="globPattern">Optional. Given glob pattern(s) to find files whose names and extensions 
        /// match a case-insensitive glob pattern.Note:
        /// 1. If the pattern doesn’t contain any glob syntax (? and *), * 
        /// of glob syntax will be added at begin and end of the string automatically 
        /// for partially matching the pattern.
        /// 2. You can use ”,” to separate multiple glob patterns.</param>
        /// <param name="fileType">Optional. “file”: only enumerate regular files; “dir”: only enumerate folders; “all” enumerate regular files and folders</param>
        /// <param name="gotoPath">Optional. Folder path started with a shared folder. Return all files 
        /// and sub-folders within folder_path path until goto_path path recursively.</param>
        /// <param name="additionalInfo">Optional. Additional requested file information.</param>
        /// <returns>The list of files in the given Folder Path.</returns>
        public async Task<FsListResponse> ListFilesInFolderAsync(string folderPath, int offset = 0, int limit = 0,
            FileInformationSortValues sortBy = FileInformationSortValues.Name, SortDirection sortDirection = SortDirection.Asc, 
            string globPattern = "", FileType fileType = FileType.All, string gotoPath = "", IEnumerable<FileStationAdditionalInfoValues> additionalInfo = null)
        {
            var requestParameters = new RequestParameters
            {
                {"folder_path", WebUtility.UrlEncode(folderPath)},
                {"offset", offset.ToString()},
                {"limit", limit.ToString()},
                {"sort_by", sortBy.ToString().ToLower()},
                {"pattern", globPattern}, //test possible errors if this parameter is sent empty
                {"filetype", fileType.ToString().ToLower()},
            };

            if (!string.IsNullOrEmpty(globPattern))
                requestParameters.Add("sort_direction", sortDirection.ToString().ToLower());
            
            if (!string.IsNullOrEmpty(gotoPath))
                requestParameters.Add("goto_path", WebUtility.UrlEncode(gotoPath));
            
            if (additionalInfo!= null)
                requestParameters.Add("additional", string.Join(",",additionalInfo).ToLower());
            
            return await PerformOperationAsync<FsListResponse>(requestParameters);
        }

        /// <summary>
        /// Retrieves information on File(s).
        /// </summary>
        /// <param name="pathList">One or more folder/file path(s) started with a shared folder</param>
        /// <param name="additionalInfo">Optional. Additional requested file info.</param>
        /// <returns>The information of the requested files.</returns>
        public async Task<FsListInfoResponse> GetFileInfoAsync(IList<string> pathList, FileStationAdditionalInfoValues[] additionalInfo = null)
        {
            var requestParameters = new RequestParameters
            {
                {"path", string.Join(",", pathList)}
            };

            if (additionalInfo != null && additionalInfo.Length > 0)
                requestParameters.Add("additional", string.Join(",", additionalInfo).ToLower());

            return await PerformOperationAsync<FsListInfoResponse>(requestParameters);
        }

        /// <summary>
        /// List all shared folders.
        /// </summary>
        /// <param name="offset">Optional. Specify how many shared folders are skipped before beginning to return listed shared folders.</param>
        /// <param name="limit">Optional. Number of shared folders requested. 0 lists all shared folders.</param>
        /// <param name="sortBy">Optional. Specify which file information to sort on.</param>
        /// <param name="sortDirection">Optional. Specify to sort ascending or to sort descending.</param>
        /// <param name="onlyWritable">Optional. “true”: List writable shared folders; “false”: List writable and read-only shared folders.</param>
        /// <param name="additionalInfo">Optional. Additional requested file information</param>
        /// <returns>The list of all the shared folders.</returns>
        public async Task<FsListShareResponse> ListSharedFoldersAsync(int offset = 0, int limit = 0,
            FileInformationSortValues sortBy = FileInformationSortValues.Name,
            SortDirection sortDirection = SortDirection.Asc, bool onlyWritable = false,
            SharesAdditionalInfo[] additionalInfo = null)
        {
            var requestParams = new RequestParameters
            {
                {"offset", offset.ToString()},
                {"limit", limit.ToString()},
                {"sort_by", sortBy.ToString().ToLower()},
                {"sort_direction", sortDirection.ToString().ToLower()},
                {"onlywritable", onlyWritable ? "true" : "false"},
            };

            if (additionalInfo != null && additionalInfo.Length > 0)
            {
                requestParams.Add("additional", string.Join(",", additionalInfo).ToLower());
            }

            return await PerformOperationAsync<FsListShareResponse>(requestParams);
        }
    }
}
