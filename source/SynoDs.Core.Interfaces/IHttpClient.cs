using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynoDs.Core.Interfaces
{
    public interface IHttpClient
    {
        Task<string> SendRequestAsync();
    }
}
