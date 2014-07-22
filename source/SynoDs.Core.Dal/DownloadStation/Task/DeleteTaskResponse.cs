using System.Collections.Generic;
using System.Runtime.Serialization;
using SynoDs.Core.Dal.HttpBase;

namespace SynoDs.Core.Dal.DownloadStation.Task
{
    [DataContract]
    public class DeleteTaskResponse : ResponseWrapper<IEnumerable<DeleteTask>>
    {
        
    }
}