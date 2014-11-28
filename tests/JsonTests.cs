using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SynoDs.Core.Dal.BaseApi;
using SynoDs.Core.Exceptions;
using SynoDs.Core.Contracts;
using SynoDs.Core.JsonParser;

namespace SynologyTests
{
    [TestClass]
    public class JsonTests
    {
        private const string ValidJson = 
            "{\"data\":[{\"error\":0,\"id\":\"dbid_711\"}," +
            "{\"error\":0,\"id\":\"dbid_712\"},{\"error\":0," +
            "\"id\":\"dbid_713\"},{\"error\":0,\"id\":\"dbid_714\"}" +
            ",{\"error\":0,\"id\":\"dbid_715\"},{\"error\":0,\"id\":\"dbid_716\"}" +
            ",{\"error\":0,\"id\":\"dbid_717\"},{\"error\":0,\"id\":\"dbid_718\"}," +
            "{\"error\":0,\"id\":\"dbid_719\"},{\"error\":0,\"id\":\"dbid_720\"}," +
            "{\"error\":0,\"id\":\"dbid_721\"},{\"error\":0,\"id\":\"dbid_722\"}," +
            "{\"error\":0,\"id\":\"dbid_723\"},{\"error\":0,\"id\":\"dbid_724\"}," +
            "{\"error\":0,\"id\":\"dbid_725\"},{\"error\":0,\"id\":\"dbid_726\"}," +
            "{\"error\":0,\"id\":\"dbid_727\"},{\"error\":0,\"id\":\"dbid_728\"}," +
            "{\"error\":0,\"id\":\"dbid_729\"},{\"error\":0,\"id\":\"dbid_730\"}," +
            "{\"error\":0,\"id\":\"dbid_731\"}],\"success\":true, \"error\":0}";

        private IJsonParser JsonHandler { get; set; }

        [TestMethod]
        public void TestDeserializeMissingDataJsonObjectMethod1()
        {
            var errorMock = new Mock<IErrorProvider>();
            
            errorMock.Setup(p => p.GetErrorDescriptionForType<InfoResponse>(0)).Returns("Some error");

            var json = new JsonParser(errorMock.Object);
            var res = json.FromJson<LoginResponse>("{\"data\":{\"sid\":\"9769a8dhf\"},\"error\":\"0\", \"success\":\"true\"}");
            Assert.AreEqual("9769a8dhf",res.ResponseData.Sid );
        }

        [TestMethod]
        [ExpectedException(typeof(SynologyException), AllowDerivedTypes = true)]
        public void TestErrorDataDeserializationWithLogin()
        {
            var errorMock = new Mock<IErrorProvider>();
            errorMock.Setup(p=>p.GetErrorDescriptionForType<object>(402)).Returns("Some error");

            var json = new JsonParser(errorMock.Object);
            var res = json.FromJson<LoginResponse>("{\"error\":\"101\", \"success\":\"false\"}");
         }

        [TestMethod]
        public void TestErrorDataWithSuccessFlagAndNoErrorCode()
        {
            var errorMock = new Mock<IErrorProvider>();
            errorMock.Setup(p => p.GetErrorDescriptionForType<object>(101)).Returns("Some error");

            var json = new JsonParser(errorMock.Object);
            var res = json.FromJson<LoginResponse>("{\"error\":\"0\", \"success\":\"true\"}");

            Assert.AreEqual(0,res.ErrorCode);
            Assert.IsNull(res.ResponseData);
            Assert.IsTrue(res.Success);
        }

    }
}
