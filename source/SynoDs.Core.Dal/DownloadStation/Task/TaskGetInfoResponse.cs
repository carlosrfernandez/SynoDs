﻿using System.Runtime.Serialization;
using SynoDs.Core.Dal.HttpBase;

namespace SynoDs.Core.Dal.DownloadStation.Task
{
    [DataContract]
    public class TaskGetInfoResponse : ResponseWrapper<TaskGetInfo>
    {

    }
}
