using System.Collections.Generic;
using System.Threading.Tasks;
using SynoDs.Core.Dal.Enums;
using SynoDs.Core.Dal.FileStation.CreateFolder;
using SynoDs.Core.Dal.FileStation.List;
using SynoDs.Core.Dal.FileStation.Rename;

namespace SynoDs.Core.Contracts.Synology
{
    public interface IFileStation
    {
        #region Create Folders
        /// <summary>
        /// Creates a folder with the given "name" in the given folderPath. See parameter descriptions:
        /// </summary>
        /// <param name="folderPathList">One or more shared folder paths</param>
        /// <param name="nameList">Name of the folder to create. The number of paths must be the same as the number of names in the name parameter. 
        /// The first folder_path parameter corresponds to the first name parameter.
        /// </param>
        /// <param name="forceParent">If force_parent is "true," and folder_path does not exist, the folder_path will be created. Default value is "false".
        /// If false, folderPath must exist or a false value will be returned.
        /// </param>
        /// <param name="additional"></param>
        /// <returns>The CreateFolderResponse (with information regarding the created folder)</returns>
        Task<CreateFolderResponse> CreateFolderAsync(IList<string> folderPathList, IList<string> nameList,
            bool forceParent = false,
            CreateFolderAdditionalValues[] additional = null);

        /// <summary>
        /// Renames a folder or a list of folders.
        /// </summary>
        /// <param name="pathList">List of folders to rename</param>
        /// <param name="nameList">List of new names.</param>
        /// <param name="additional">Additional Attributes</param>
        /// <returns>RenameResponse object</returns>
        Task<RenameResponse> RenameAsync(IList<string> pathList, IList<string> nameList,
            CreateFolderAdditionalValues[] additional = null);
        #endregion

        #region FileStation Info

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
        Task<FsListResponse> ListFilesInFolderAsync(string folderPath, int offset = 0, int limit = 0,
            FileInformationSortValues sortBy = FileInformationSortValues.Name,
            SortDirection sortDirection = SortDirection.Asc,
            string globPattern = "", FileType fileType = FileType.All, string gotoPath = "",
            IEnumerable<FileStationAdditionalInfoValues> additionalInfo = null);

        /// <summary>
        /// Retrieves information on File(s).
        /// </summary>
        /// <param name="pathList">One or more folder/file path(s) started with a shared folder</param>
        /// <param name="additionalInfo">Optional. Additional requested file info.</param>
        /// <returns>The information of the requested files.</returns>
        Task<FsListInfoResponse> GetFileInfoAsync(IList<string> pathList,
            FileStationAdditionalInfoValues[] additionalInfo = null);

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
        Task<FsListShareResponse> ListSharedFoldersAsync(int offset = 0, int limit = 0,
            FileInformationSortValues sortBy = FileInformationSortValues.Name,
            SortDirection sortDirection = SortDirection.Asc, bool onlyWritable = false,
            SharesAdditionalInfo[] additionalInfo = null);

        #endregion

        //Todo: Add FS Info
        #region FileStation Info

        #endregion
    }
}
