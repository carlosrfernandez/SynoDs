namespace SynoDs.Core.BaseApi.Info.ErrorHandling
{
    using Interfaces;

    public class InfoErrorRepository : IErrorRepository
    {
        //Todo add error access.
        public InfoErrorRepository()
        {

        }

        public string GetErrorDescription(int errorCode)
        {
            return "Unknown error while getting info.";
        }
    }
}
