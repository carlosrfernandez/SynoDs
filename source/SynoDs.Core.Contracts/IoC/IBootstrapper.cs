using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynoDs.Core.Contracts.IoC
{
    public interface IBootstrapper
    {
        void Startup();

        void Shutdown();
    }
}
