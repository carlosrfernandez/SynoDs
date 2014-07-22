using Microsoft.VisualStudio.TestTools.UnitTesting;
using SynoDs.Core.Api;
using SynoDs.Core.Dal.BaseApi;

namespace SynologyTests
{
    [TestClass]
    public class AttributeTests
    {
        [TestMethod]
        public void TestApiAttributeForBaseApi()
        {
            var infoApiString = AttributeMapper.ReadApiNameFromInstance<InfoResponse>();
            var loginApiString = AttributeMapper.ReadApiNameFromInstance<LoginResponse>();

            Assert.AreEqual("SYNO.API.Info", infoApiString);
            Assert.AreEqual("SYNO.API.Auth", loginApiString);
        }

        [TestMethod]
        public void TestMethodAttributeForBaseApi()
        {
            var authMethodName = AttributeMapper.ReadMethodFromInstance<LoginResponse>();

            var infoMethodName = AttributeMapper.ReadMethodFromInstance<InfoResponse>();

            Assert.AreEqual("query", infoMethodName);
            Assert.AreEqual("login", authMethodName);
        }


    }
}
