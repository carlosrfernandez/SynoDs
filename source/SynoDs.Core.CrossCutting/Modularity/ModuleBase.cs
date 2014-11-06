namespace SynoDs.Core.CrossCutting.Modularity
{
    using Interfaces.IoC;
    using Interfaces.Modularity;

    public abstract class ModuleBase : IModule
    {
        private bool _requiresAuthentication;
        private string _apiName;
        
        protected ModuleBase(string apiModuleName, bool requiresLogin)
        {
            _apiName = apiModuleName;
            _requiresAuthentication = requiresLogin;
        }

        public abstract void Configure();
    }
}
