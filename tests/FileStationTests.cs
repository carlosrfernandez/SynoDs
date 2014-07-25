using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SynoDs.Core.Api.FileStation;

namespace SynologyTests
{
    [TestClass]
    public class FileStationTests
    {
        internal static Uri Uri { get; set; }
        internal static string UserName { get; set; }
        internal static string Password { get; set; }
        internal static string FolderToListContents { get; set; }
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Uri = new Uri("http://carlosrfa.synology.me:5000");
            UserName = "dev";
            Password = "d3v3l0p3r";
            FolderToListContents = "/TVShows";

            if (string.IsNullOrEmpty(UserName))
                throw new ArgumentException("Username is emtpy, tests can't run.");

            if (string.IsNullOrEmpty(Password))
                throw new ArgumentException("Password is emtpy, tests can't run.");

            if (string.IsNullOrEmpty(Uri.ToString()))
                throw new ArgumentException("Uri is empty, tests can't run.");

            if (string.IsNullOrEmpty(FolderToListContents))
                throw new ArgumentException("FolderToListis emtpy.");
        }

        [TestMethod]
        public void ListFilesInFolder()
        {
            var fsClient = new FileStation(UserName, Password, Uri);
            Assert.IsTrue(fsClient.LoginAsync().Result);
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
