using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SynoDs.Core.CrossCutting;
using SynoDs.Core.Interfaces;
using SynoDs.Core.Interfaces.Modularity;

namespace SynoDs.Core.JsonParser.Module
{
    public class JsonParserModule : IApiModule
    {
        public bool RequiresAuthenticatedRequests { get; private set; }

        public void Configure()
        {
            RequiresAuthenticatedRequests = false;
            IoCFactory.Container.Register<IJsonParser, JsonParser>();
        }
    }
}
