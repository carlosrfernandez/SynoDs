namespace SynoDs.Core.Dal.BaseApi
{
    using System;

    /// <summary>
    /// Stores the Host URI for the Synology DiskStation 
    /// </summary>
    public class DsStationInfo
    {
        /// <summary>
        /// The URI (ip or hostname with port). 
        /// </summary>
        public Uri HostName { get; set; }

        /// <summary>
        /// Flag to indicate whether we use SSL or not.
        /// </summary>
        public bool UseSSL { get; set; }
    }
}
