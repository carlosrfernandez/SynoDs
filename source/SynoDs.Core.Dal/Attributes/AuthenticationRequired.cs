using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynoDs.Core.Dal.Attributes
{
    public class AuthenticationRequired : Attribute
    {
        public AuthenticationRequired(bool requiresAuthentication)
        {
            RequiresAuthentication = requiresAuthentication;
        }

        private bool RequiresAuthentication { get; set; }

       
        public bool GetAuthenticationRequirements()
        {
            return this.RequiresAuthentication;
        }
    }
}
