using SynoDs.Core.Api.ErrorHandling;
using SynoDs.Core.Api.Http;
using SynoDs.Core.CrossCutting.Modularity;
using SynoDs.Core.Interfaces;
using SynoDs.Core.Interfaces.IoC;
using SynoDs.Core.Interfaces.Synology;
using SynoDs.Core.JsonParser;

namespace SynoDs.Core.Api.Module
{
    public class ApiCoreModule : ModuleBase
    {
        public ApiCoreModule(IContainer container) : base(container)
        {

        }

        public override void Configure()
        {
            base.Container.RegisterWithInstance<IErrorProvider, CoreErrorProvider>(new CoreErrorProvider());
            base.Container.Register<IOperationProvider, OperationProvider>();
            base.Container.Register<IHttpClient, HttpGetRequestClient>();
            base.Container.Register<IJsonParser, JsonParser.JsonParser>();
        }
    }
}
