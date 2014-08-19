using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SynoDs.Core.Api.FileStation;

namespace SynologyTests
{
    [TestClass]
    public class FileStationTests : Abstract.TestBase
    {
        internal static string FolderToListContents { get; set; }

        [ClassInitialize]
        public static void MyClassInitialize(TestContext context)
        {
            ClassInitialize(context);
            FolderToListContents = "/Development";
        }
        
        [TestMethod]
        public void ListFilesInFolder()
        {
            Assert.AreNotEqual("", FolderToListContents , "Folder param is empty. Test will not run");
            var fsClient = new FileStation();
            Assert.IsTrue(fsClient.LoginAsync(Credentials).Result);
            var list = fsClient.ListFilesInFolderAsync(FolderToListContents);
            Assert.IsTrue(list.Result.Success);
            Assert.IsNotNull(list.Result.ResponseData);
            Assert.IsTrue(fsClient.LogoutAsync().Result);
        }

        [TestMethod]
        public void UrlTest()
        {
            const string url = "/path/to/folder,/another/path";
            var encoded = WebUtility.UrlEncode(url);
            Assert.AreEqual("%2Fpath%2Fto%2Ffolder%2C%2Fanother%2Fpath", encoded);
        }
    }
}
