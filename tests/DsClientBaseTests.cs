using Microsoft.VisualStudio.TestTools.UnitTesting;
using SynoDs.Core.Api;
using SynologyTests.Abstract;

namespace SynologyTests
{
    [TestClass]
    public class DsClientBaseTests : TestBase
    {
        [ClassInitialize]
        public static void MyClassInitialize(TestContext context)
        {
            ClassInitialize(context);
        }
        
        [TestMethod]
        public void SynologyLoginTest()
        {
            var ds = new DsClientBase(UserName, Password, Uri);
            Assert.IsTrue(ds.LoginAsync().Result);
        }
        
        [TestMethod]
        public void SynologyLogoutTest()
        {
            var ds = new DsClientBase(UserName, Password, Uri);
            var ot = new PrivateObject(ds);
            Assert.IsTrue(ds.LoginAsync().Result);
            var sid = ot.GetFieldOrProperty("SessionId");
            Assert.AreNotEqual(string.Empty, sid);
            Assert.IsTrue(ds.LogoutAsync().Result);
            sid = ot.GetFieldOrProperty("SessionId");
            Assert.AreEqual(string.Empty, sid);
        }

        [TestMethod]
        public void TestGetApiInfo()
        {
            var ds = new DsClientBase(UserName, Password, Uri);
            var result = ds.GetApiInformation("SYNO.API.Auth");
            Assert.IsTrue(result.Result.Success);
            Assert.IsNotNull(result.Result.ResponseData);
            Assert.AreEqual(3, result.Result.ResponseData["SYNO.API.Auth"].MaxVersion);
            Assert.AreEqual(1, result.Result.ResponseData["SYNO.API.Auth"].MinVersion);
            Assert.AreEqual("auth.cgi", result.Result.ResponseData["SYNO.API.Auth"].Path);
        }
    }
}
