using SynoDs.Core.Interfaces;

namespace SynoDs.Core.Exceptions.ErrorHandling
{
    public class CoreErrorProvider : ErrorProviderBase
    {
        public CoreErrorProvider() : this(new CoreErrorRepository())
        {
        }

        public CoreErrorProvider(IErrorRepository errorRepository)
            : base(errorRepository)
        {
        }
    }
}
