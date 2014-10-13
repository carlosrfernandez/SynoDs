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

        public TestContext TestContext { get; set; }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            // Example credentials. 
            // URI can be:
            // Uri = new Uri("192.168.1.XXX:5000"); // ip address
            Credentials = new LoginCredentials
            {
                Uri = new Uri("http://192.168.1.140:5000"),
                UserName = "admin",
                Password = "L3tm31nt0myn4s!",
                UseSsl = false
            };

            if (string.IsNullOrEmpty(Credentials.UserName))
                throw new ArgumentException("Username is emtpy, tests can't run.");

            if (string.IsNullOrEmpty(Credentials.Password))
                throw new ArgumentException("Password is emtpy, tests can't run.");

            if (string.IsNullOrEmpty(Credentials.Uri.ToString()))
                throw new ArgumentException("Uri is empty, tests can't run.");
        }
    }
}
