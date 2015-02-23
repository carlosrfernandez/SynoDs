namespace SynoDs.Core.Contracts.IoC
{
    public interface IBootstrapper
    {
        void Startup();

        void Shutdown();

        void Run();
    }
}
