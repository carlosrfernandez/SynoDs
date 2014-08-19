namespace SynoDs.Core.CrossCutting
{
    using System;

    public class LoginCredentials
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Uri Uri { get; set; }
        public bool UseSsl { get; set; }
    }
}
