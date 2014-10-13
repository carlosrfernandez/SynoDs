using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SynoDs.Core.Interfaces;

namespace SynoDs.Core.BaseApi.Info
{
    public class InfoErrorProvider : IErrorProvider
    {
        //TODO: Add information dictionary. 
        public InfoErrorProvider()
        {
            
        }

        public IErrorRepository ErrorRepository { get; private set; }
        
        public string GetErrorDescriptionForType(int errorCode)
        {
            return "Error on Information API.";
        }
    }
}
