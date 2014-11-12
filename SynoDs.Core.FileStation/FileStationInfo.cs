namespace SynoDs.Core.FileStation
{
    using System.Threading.Tasks;
    using Interfaces;
    using Dal.FileStation.Info;

    /// <summary>
    /// Contains the Information retrieval side of the FileStation
    /// </summary>
    public partial class FileStation
    {
        /// <summary>
        /// Provide File Station information
        /// </summary>
        /// <returns>Returns a FileStation InfoResponse <see cref="FsInfo"/></returns>
        public async Task<FsInfoResponse> GetFileStationInfoAsync()
        {
            //return await PerformOperationAsync<FsInfoResponse>();
            return null; //todo implement
        }
    }
}
