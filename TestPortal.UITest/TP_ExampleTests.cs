using Browser.Core.Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using TP.AppFramework;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using OpenQA.Selenium.Firefox;
using Browser.Core.Framework.Resources;
using OpenQA.Selenium.Remote;
using System.Reflection;
using System.IO;
using System.Configuration;

namespace TP.UITest
{
    //[BrowserMode(BrowserMode.New)]
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    //[LocalSeleniumTestFixture(BrowserNames.Firefox)]
    //[LocalSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Firefox)]
    [RemoteSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [TestFixture]
    public class TestPortal_ExampleTests : TestBase
    {
        #region Constructors
        public TestPortal_ExampleTests(string browserName) : base(browserName) { }

        // Remote Selenium Grid Test
        public TestPortal_ExampleTests(string browserName, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests

        [Test]
        [Description("")]
        [Property("Status", "In Progress")]
        [Author("Mike Johnston")]
        public void TestLoginPage()
        {
            UserInfo blah = UserUtils.CreateUser("testujdfsvhkjshbhertestusertestuser1", null, "mike", "johnston", null);

            /// 1. Navigate to the login page
            LoginPage LP = Navigation.GoToLoginPage(browser);

            LP.Login(browser, UserUtils.User1, ConfigurationManager.AppSettings["LoginPassword"]);

        }

        #endregion Tests
    }
}

