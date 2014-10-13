using System;
using SynoDs.Core.Interfaces;

namespace SynoDs.Core.BaseApi.Auth
{
    public class AuthenticationErrorProvider : IErrorProvider
    {
        public IErrorRepository ErrorRepository { get; private set; }

        public string GetErrorDescriptionForType(int errorCode)
        {
            return "This is an authentication error";
        }
    }
}
