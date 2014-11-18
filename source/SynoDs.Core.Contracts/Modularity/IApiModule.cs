namespace SynoDs.Core.Contracts.Modularity
{
    public interface IApiModule
    {
        bool RequiresAuthenticatedRequests { get; }
        void Configure();
    }
}