using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SynoDs.Core.AttributeReader;
using SynoDs.Core.Dal.DownloadStation.Task;
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

        [TestMethod]
        public void TestAuthenticationAttributeFromLoginModule()
        {
            var attributeReader = new AttributeReader();
            var authRequired = attributeReader.ReadAuthenticationFlagFromT<LoginResponse>();

            Assert.IsFalse(authRequired);
        }

        [TestMethod]
        public void TestAuthenticationAttributeFromDownloadModule()
        {
            var attributeReader = new AttributeReader();
            var authRequired = attributeReader.ReadAuthenticationFlagFromT<ResumeTaskResponse>();

            Assert.IsTrue(authRequired);
        }

        [TestMethod]
        public void TestNonExistingAuthenticationAttribute()
        {
            var attrubuteReader = new AttributeReader();
            var result = attrubuteReader.ReadAuthenticationFlagFromT<InfoResponse>();

            Assert.IsFalse(result, "This should never be set to true if an exception is thrown");
        }
    }
}
