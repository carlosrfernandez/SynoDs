using SynoDs.Core.Contracts.Synology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynoDs.UWP.Models
{
    public class Connection
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool RememberMe { get; set; }
        public bool UseSsl { get; set; }
        public string Username { get; set; }
    }
}
