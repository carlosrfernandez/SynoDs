using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SynoDs.Core.Dal.Enums;
using SynoDs.Core.Dal.HttpBase;
using SynologyTests.Abstract;

namespace SynologyTests
{
    [TestClass]
    public class DlManagerTests : TestBase
    {
        [TestMethod]
        public void TestUriSchemes()
        {
            const string magnetLink =
                @"magnet:?xt=urn:btih:YYVC3ICFO6VVZS4CIJ4LNWOZ5FT3IEIJ&dn=Falling.Skies.S04E05.720p.HDTV.x264-KILLERS&tr=udp://tracker.openbittorrent.com:80&tr=udp://tracker.publicbt.com:80&tr=udp://tracker.istole.it:80&tr=udp://open.demonii.com:80&tr=udp://tracker.coppersurfer.tk:80";
            const string edk =
                @"ed2k://|file|The_Two_Towers-The_Purist_Edit-Trailer.avi|14997504|965c013e991ee246d63d45ea71954c4d|/";

            Uri result;

            Assert.IsTrue(Uri.TryCreate(magnetLink, UriKind.Absolute, out result), "Magnet link error");

            Assert.IsTrue(Uri.TryCreate(edk, UriKind.Absolute, out result), "Edonkey link error.");
        }

        [TestMethod]
        public void TestEnumJoin()
        {
            var varEnumList = new List<TaskAdditionalInfoValues>
            {
                TaskAdditionalInfoValues.Detail,
                TaskAdditionalInfoValues.File
            };

            var joint = string.Join(",", varEnumList).ToLower();

            Assert.AreEqual("detail,file", joint);
        }
    }
}
