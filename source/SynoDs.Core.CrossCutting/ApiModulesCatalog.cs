using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting;
using System.Composition.Hosting.Core;
using System.ComponentModel;
using System.IO;
using System.Resources;
using SynoDs.Core.Contracts.Modularity;
using System.Reflection;

namespace SynoDs.Core.CrossCutting
{
    // TODO: this has to be implemented by 
    public abstract class ApiModulesCatalog
    {
        public abstract void InitCatalog();
    }
}
