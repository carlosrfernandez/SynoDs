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

        [ClassInitialize]
        public static void ClassInitialize()
        {
            if (string.IsNullOrEmpty(UserName))
                throw new ArgumentException("Username is emtpy, tests can't run.");

            if (string.IsNullOrEmpty(Password))
                throw new ArgumentException("Password is emtpy, tests can't run.");

            if (string.IsNullOrEmpty(Uri.ToString()))
                throw new ArgumentException("Uri is empty, tests can't run.");
        }

        [TestMethod]
        public void ListFilesInFolder()
        {
            var fsClient = new FileStation(UserName, Password, Uri);
            Assert.IsTrue(fsClient.LoginAsync().Result);
            var list = fsClient.ListFilesInFolderAsync("/");
            Assert.IsTrue(list.Result.Success);
            Assert.IsNotNull(list.Result.ResponseData);
        }
    }
}
