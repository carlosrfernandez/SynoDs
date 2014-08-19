using Microsoft.VisualStudio.TestTools.UnitTesting;
using SynoDs.Core.Api;
using SynoDs.Core.CrossCutting;
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
            var ds = new Base();
            Assert.IsTrue(ds.LoginAsync(Credentials).Result);
        }
        
        [TestMethod]
        public void SynologyLogoutTest()
        {
            var ds = new Base();
            var ot = new PrivateObject(ds);

            Assert.IsTrue(ds.LoginAsync(Credentials).Result);
            var sid = ot.GetFieldOrProperty("SessionId");
            Assert.AreNotEqual(string.Empty, sid);
            Assert.IsTrue(ds.LogoutAsync().Result);
            sid = ot.GetFieldOrProperty("SessionId");
            Assert.AreEqual(string.Empty, sid);
        }

        [TestMethod]
        public void TestGetApiInfo()
        {
            var ds = new Base();
            var result = ds.GetApiInformation("SYNO.API.Auth");
            Assert.IsTrue(result.Result.Success);
            Assert.IsNotNull(result.Result.ResponseData);
            Assert.AreEqual(3, result.Result.ResponseData["SYNO.API.Auth"].MaxVersion);
            Assert.AreEqual(1, result.Result.ResponseData["SYNO.API.Auth"].MinVersion);
            Assert.AreEqual("auth.cgi", result.Result.ResponseData["SYNO.API.Auth"].Path);
        }
    }
}
