namespace SynoDs.Core.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Basic interface to work with a very basic FTP client.
    /// It is not intended to be a full working client. Just read only operations.
    /// It is up to the client to store the information.
    /// </summary>
    public interface IFtpClient
    {
        string UserName { get; set; }
        string Password { get; set; }
        Uri HostUri { get; set; }
        void ConnectAsync();
        void DisconnectAsync();
        Task<IList<string>> ListFolderContentAsync(string path);
    }
}
