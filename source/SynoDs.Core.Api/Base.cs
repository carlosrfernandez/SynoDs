using System;
using SynoDs.Core.CrossCutting.Common;
using SynoDs.Core.Dal.BaseApi;

namespace SynoDs.Core.Api
{
    /// <summary>
    /// This is the API base class. It contains a Generic PerformOperationAsync method that 
    /// can be used by the rest of the API's in order to communicate with the Diskstation.
    /// DONE: Convert to abstract.
    /// Done: Remove Login and InformationProvider Methods into separate projects.
    /// Done: Refactor the Info cache so that it is properly used in the base class (use information interface to access the Api Cache)
    /// TODO: Add the File Upload method for uploading torrents from the client application.
    /// TODO: Add known error handling of the API
    /// </summary>
    public abstract class Base
    {
        // Api properties
        protected string DsUsername { get; set; }
        protected string DsPassword { get; set; }
        protected Uri DsAddress { get; set; }
        protected string SessionId { get; set; }
        protected const string SessionName = "DsBase";

        /// <summary>
        /// Overridable method to get the session name used to log out.
        /// </summary>
        /// <returns>The current session's name</returns>
        protected virtual string GetSessionName()
        {
            return SessionName;
        }

        /// <summary>
        /// Default parameterless constructor
        /// </summary>
        protected Base(DsStationInfo dsInfo, LoginCredentials credentials)
        {
            Validate.ArgumentIsNotNullOrEmpty(dsInfo);
            Validate.ArgumentIsNotNullOrEmpty(credentials);
        }
    }
}
