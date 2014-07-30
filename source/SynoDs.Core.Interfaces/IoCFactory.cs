using SynoDs.Core.Interfaces.IoC;

namespace SynoDs.Core.Interfaces
{
// ReSharper disable once InconsistentNaming
    public interface IoCFactory
    {
        IContainer Container { get; }
    }
}
