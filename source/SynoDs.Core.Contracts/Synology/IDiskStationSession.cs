using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynoDs.Core.Contracts.Synology
{
    public interface IDiskStationSession
    {
        Uri Host { get; set; }
        string SessionId { get; set; }

        bool IsLoggedIn();
    }
}
