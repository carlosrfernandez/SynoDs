namespace SynoDs.Core.Dal.Attributes
{
    using System;
    using Enums;

    public class Api : Attribute
    {
        private RootApi RootApi { get; set; }
        private ChildApi ChildApi { get; set; }

        public Api(RootApi rootApi, ChildApi chidApi)
        {
            RootApi = rootApi;
            ChildApi = chidApi;
        }

        public string GetApi()
        {
            return string.Format("SYNO.{0}.{1}", RootApi, ChildApi);
        }
    }
}