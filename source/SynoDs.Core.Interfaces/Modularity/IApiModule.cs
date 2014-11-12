namespace SynoDs.Core.Interfaces.Modularity
{
    public interface IApiModule
    {
        bool RequiresAuthenticatedRequests { get; }
        void Configure();
    }
}