using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SynoDs.Core.Dal.BaseApi;

namespace SynologyTests.Abstract
{
    /// <summary>
    /// This class stores the values that will be used to authenticate with the DS.
    /// Use the ClassInitialize method to set the values before executing the tests.
    /// </summary>
    [TestClass]
    public abstract class TestBase
    {
        public static LoginCredentials Credentials { get; set; }

        public static DsStationInfo StationInfo { get; set; }

        public TestContext TestContext { get; set; }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            // Example credentials. 
            // URI can be:
            // Uri = new Uri("192.168.1.XXX:5000"); // ip address
            
            if (string.IsNullOrEmpty(Credentials.UserName))
                throw new ArgumentException("Username is emtpy, tests can't run.");

            if (string.IsNullOrEmpty(Credentials.Password))
                throw new ArgumentException("Password is emtpy, tests can't run.");

            if (string.IsNullOrEmpty(StationInfo.HostName.ToString()))
                throw new ArgumentException("Uri is empty, tests can't run.");
        }
    }
}
