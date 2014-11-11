using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SynoDs.Core.BaseApi.Info.ErrorHandling;
using SynoDs.Core.CrossCutting;
using SynoDs.Core.Interfaces;
using SynoDs.Core.Interfaces.Modularity;
using SynoDs.Core.Interfaces.Synology;

namespace SynoDs.Core.BaseApi.Info.Module
{
    public class InformationModule : IApiModule
    {
        public void Configure()
        {
            IoCFactory.Container.Register<IInformationProvider, InformationProvider>();
            IoCFactory.Container.Register<IErrorProvider, InfoErrorProvider>();
        }
    }
}
