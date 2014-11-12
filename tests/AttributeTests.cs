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
            //var infoApiString = AttributeReader.ReadApiNameFromT<InfoResponse>();
            //var loginApiString = AttributeReader.ReadApiNameFromT<LoginResponse>();

            //Assert.AreEqual("SYNO.API.Info", infoApiString);
            //Assert.AreEqual("SYNO.API.Auth", loginApiString);
        }

        [TestMethod]
        public void TestMethodAttributeForBaseApi()
        {
            //var authMethodName = AttributeReader.ReadMethodAttributeFromT<LoginResponse>();

            //var infoMethodName = AttributeReader.ReadMethodAttributeFromT<InfoResponse>();

            //Assert.AreEqual("query", infoMethodName);
            //Assert.AreEqual("login", authMethodName);
        }
    }
}
