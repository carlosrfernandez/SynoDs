using Microsoft.VisualStudio.TestTools.UnitTesting;
using SynoDs.Core.Exceptions;
using SynoDs.Core.Dal.BaseApi;

namespace SynologyTests
{
    [TestClass]
    public class AttributeTests
    {
        [TestMethod]
        public void TestApiAttributeForBaseApi()
        {
            var attributeReader = new AttributeReader();
            var infoApiString = attributeReader.ReadApiNameFromT<InfoResponse>();
            var loginApiString = attributeReader.ReadApiNameFromT<LoginResponse>();

            Assert.AreEqual("SYNO.API.Info", infoApiString);
            Assert.AreEqual("SYNO.API.Auth", loginApiString);
        }

        [TestMethod]
        public void TestMethodAttributeForBaseApi()
        {
            var attributeReader = new AttributeReader();
            var authMethodName = attributeReader.ReadMethodAttributeFromT<LoginResponse>();

            var infoMethodName = attributeReader.ReadMethodAttributeFromT<InfoResponse>();

            Assert.AreEqual("query", infoMethodName);
            Assert.AreEqual("login", authMethodName);
        }
    }
}
