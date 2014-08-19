using Microsoft.VisualStudio.TestTools.UnitTesting;
using SynoDs.Core.Interfaces;

namespace SynologyTests
{
    [TestClass]
    public class JsonTests
    {
        private const string ValidJson = "{\"data\":[{\"error\":0,\"id\":\"dbid_711\"},{\"error\":0,\"id\":\"dbid_712\"},{\"error\":0,\"id\":\"dbid_713\"},{\"error\":0,\"id\":\"dbid_714\"},{\"error\":0,\"id\":\"dbid_715\"},{\"error\":0,\"id\":\"dbid_716\"},{\"error\":0,\"id\":\"dbid_717\"},{\"error\":0,\"id\":\"dbid_718\"},{\"error\":0,\"id\":\"dbid_719\"},{\"error\":0,\"id\":\"dbid_720\"},{\"error\":0,\"id\":\"dbid_721\"},{\"error\":0,\"id\":\"dbid_722\"},{\"error\":0,\"id\":\"dbid_723\"},{\"error\":0,\"id\":\"dbid_724\"},{\"error\":0,\"id\":\"dbid_725\"},{\"error\":0,\"id\":\"dbid_726\"},{\"error\":0,\"id\":\"dbid_727\"},{\"error\":0,\"id\":\"dbid_728\"},{\"error\":0,\"id\":\"dbid_729\"},{\"error\":0,\"id\":\"dbid_730\"},{\"error\":0,\"id\":\"dbid_731\"}],\"success\":true, \"error\":0}";
        private IJsonParser JsonHandler { get; set; }

        [TestMethod]
        public void TestDeserializeMissingDataJsonObjectMethod1()
        {
            //ILoggingProvider logging 
            //JsonHandler = new JsonHandler();
            //var result = JsonHandler.FromJson<TaskActionResponse>(ValidJson);
            //Assert.IsNotNull(result);
        }
    }
}
