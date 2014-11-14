﻿using SynoDs.Core.CrossCutting;
using SynoDs.Core.Interfaces.Modularity;
using SynoDs.Core.Interfaces.Synology;

namespace SynoDs.Core.BaseApi.Info.Module
{
    public class InformationModule : IApiModule
    {
        public bool RequiresAuthenticatedRequests { get; private set; }

        public void Configure()
        {
            this.RequiresAuthenticatedRequests = false;
            IoCFactory.Container.Register<IInformationProvider, InformationProvider>();
        }
    }
}
