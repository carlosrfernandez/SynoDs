using System;
using System.Composition;

namespace SynoDs.Core.CrossCutting.Modularity
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class)]
    public class ApiModuleAttribute : ExportAttribute
    {
        private readonly string _moduleName;

        public ApiModuleAttribute(string name)
        {
            this._moduleName = name;
        }

        /// <summary>
        /// Gets the Module Name.
        /// </summary>
        public string ModuleName
        {
            get
            {
                return this._moduleName;
            }
        }
    }
}
