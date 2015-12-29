using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SynoDs.Core.Api;
using SynoDs.Core.Contracts;
using SynoDs.Core.Contracts.Synology;
using SynoDs.Core.CrossCutting;
using SynoDs.Core.Dal.BaseApi;
using SynoDs.Core.Dal.HttpBase;

namespace SynologyTests
{
    [TestClass]
    // todo: add tests
    public class InformationTests
    {
        private IInformationProvider InfoProvider { get; set; }
        private IJsonParser JsonParser { get; set; }

        [ClassInitialize]
        public static void ClassInitialize()
        {
        }

        [TestInitialize]
        public void TestInitialize()
        {

        }

        [TestMethod]
        public void TestLoadInfoCache()
        {
            
        }
    }
}
