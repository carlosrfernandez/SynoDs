using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SynologyTests.Abstract
{
    /// <summary>
    /// This class stores the values that will be used to authenticate with the DS.
    /// Use the ClassInitialize method to set the values before executing the tests.
    /// </summary>
    [TestClass]
    public abstract class TestBase
    {
        public static string UserName { get; set; }
        public static string Password { get; set; }
        public static Uri Uri { get; set; }

        public TestContext TestContext { get; set; }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            // Example credentials. 
            // UserName = "username";
            // Password = "password";
            // URI can be:
            // Uri = new Uri("http://yoursynologySubDomain.synology.me:5000");
            // Uri = new Uri("192.168.1.XXX:5000"); // ip address

            if (string.IsNullOrEmpty(UserName))
                throw new ArgumentException("Username is emtpy, tests can't run.");

            if (string.IsNullOrEmpty(Password))
                throw new ArgumentException("Password is emtpy, tests can't run.");

            if (string.IsNullOrEmpty(Uri.ToString()))
                throw new ArgumentException("Uri is empty, tests can't run.");
        }
    }
}
