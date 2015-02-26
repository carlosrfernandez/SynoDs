using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynoDs.Universal.Dtos
{
    public class LoginDto
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Url { get; set; }

        public bool RememberMe { get; set; }
        public bool UseSsl { get; set; }
    }
}
