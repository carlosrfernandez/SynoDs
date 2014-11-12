using SynoDs.Core.Interfaces;

namespace SynoDs.Core.Exceptions.ErrorHandling
{
    public class CoreErrorRepository : IErrorRepository
    {
        //Todo add error access.
        public CoreErrorRepository()
        {

        }

        public string GetErrorDescription(int errorCode)
        {
            return "Unknown error while getting info.";
        }
    }
}
