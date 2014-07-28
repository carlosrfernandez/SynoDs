using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SynoDs.Core.Api.DownloadStation;
using SynoDs.Core.Dal.Enums;
using SynologyTests.Abstract;

namespace SynologyTests
{
    [TestClass]
    public class DlManagerTests : TestBase
    {

        // This is the developer API PDF file from synology. We will test downloading this. 
        public const string Resource = "http://ukdl.synology.com/ftp/other/Synology_Download_Station_Official_API_V3.pdf";

        [ClassInitialize]
        public static void MyClassInitialize(TestContext context)
        {
            ClassInitialize(context);
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

        [TestMethod]
        [Description("Before executing this test, provide a total number of tasks on your box's DownloadStation.")]
        public void GetTasksInDs()
        {
            var dlStation = new DownloadManager(UserName, Password, Uri);
            
            // Make sure we can login first.
            Assert.IsTrue(dlStation.LoginAsync().Result);

            // Let's get the current downloads.

            var downloads = dlStation.ListTasksAsync();

            Assert.IsTrue(downloads.Result.Success);
            Assert.IsNotNull(downloads.Result.ResponseData);

            var taskCount = downloads.Result.ResponseData.Total;
            //var tasksCount = downloads.Result.ResponseData.Tasks;

            Assert.AreEqual(0, taskCount);
         }

        [TestMethod]
        public void CreateTaskTest()
        {

            var dlStation = new DownloadManager(UserName, Password, Uri);

            Assert.IsTrue(dlStation.LoginAsync().Result);

            var totalTaskCount = dlStation.ListTasksAsync().Result.ResponseData.Total;

            // test creation of a download task
            var result = dlStation.CreateTaskAsync(Resource);

            Assert.IsTrue(result.Result.Success);

            Assert.AreEqual(totalTaskCount + 1, dlStation.ListTasksAsync().Result.ResponseData.Total);
        }

        [TestMethod]
        public void DeleteTaskTest()
        {
            var dlStation = new DownloadManager(UserName, Password, Uri);

            Assert.IsTrue(dlStation.LoginAsync().Result);

            Assert.IsTrue(dlStation.CreateTaskAsync(Resource).Result.Success);

            var tasksAvailable = dlStation.ListTasksAsync().Result.ResponseData.Tasks;

            if (tasksAvailable.Count == 0)
            {
                Assert.Fail("No tasks to delete, or it was not created properly.");
            }

            var task = tasksAvailable[0];

            var deletedTasksResponse = dlStation.DeleteTaskAsync(new List<string>() { task.Id }, true).Result;

            foreach (var responseItem in deletedTasksResponse.ResponseData.Where(responseItem => responseItem.Id != task.Id && responseItem.Error != 0))
            {
                Assert.Fail("Something that was not supposed to be deleted got deleted...");
            }
        }
    }
}
