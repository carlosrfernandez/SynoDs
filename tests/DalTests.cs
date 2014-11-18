using Microsoft.VisualStudio.TestTools.UnitTesting;
using SynoDs.Core.Dal.Attributes;
using SynoDs.Core.Dal.HttpBase;

namespace SynologyTests
{
    [TestClass]
    public class DalTests
    {
        [TestMethod]
        public void RequestBaseTest()
        {
            var apiRequestBase = new RequestBase
            {
                ApiName = "SYNO.API.Info",
                Version = "1",
                Path = "query.cgi",
                Method = "query",
                Sid = string.Empty,
                RequestParameters = new RequestParameters { { "query", "all" } }
            };

            Assert.AreEqual("webapi/query.cgi?api=SYNO.API.Info&version=1&method=query&query=all", apiRequestBase.ToString());
        }

        [TestMethod]
        public void ApiAttributesTest()
        {
            var api = new Api(SynoDs.Core.Dal.Enums.RootApi.API, SynoDs.Core.Dal.Enums.ChildApi.Auth);
            Assert.AreEqual("API", api.GetRootApi());
            Assert.AreEqual("Auth", api.GetChildApi());
            Assert.AreEqual("SYNO.API.Auth", api.GetApi());
        }

        [TestMethod]
        public void NullParametersTest()
        {
            var apiRequestBase = new RequestBase
            {
                ApiName = "SYNO.API.Info",
                Version = "1",
                Path = "query.cgi",
                Method = "query",
                Sid = string.Empty,
                RequestParameters = null
            };

            Assert.AreEqual("webapi/query.cgi?api=SYNO.API.Info&version=1&method=query", apiRequestBase.ToString());
        }

        [TestMethod]
        public void SidParameterFilledTest()
        {
            var sid = "IGIU7868JHGKUTY";
            var apiRequestBase = new RequestBase
            {
                ApiName = "SYNO.API.Info",
                Version = "1",
                Path = "query.cgi",
                Method = "query",
                Sid = sid,
                RequestParameters = new RequestParameters { { "query", "all" } }
            };
            
            Assert.AreEqual(string.Format("webapi/query.cgi?api=SYNO.API.Info&version=1&method=query&query=all&_sid={0}", sid), apiRequestBase.ToString());
        }
    }
}
