using Browser.Core.Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using LS.AppFramework;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using OpenQA.Selenium.Firefox;
using Browser.Core.Framework.Resources;
using OpenQA.Selenium.Remote;
using System.Reflection;
using System.IO;

namespace LS.UITest
{
    //[BrowserMode(BrowserMode.New)]
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    //[LocalSeleniumTestFixture(BrowserNames.Firefox)]
    //[LocalSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Firefox)]
    [RemoteSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [TestFixture]
    public class LSTests : TestBase
    {
        #region Constructors
        public LSTests(string browserName) : base(browserName) { }

        // Remote Selenium Grid Test
        public LSTests(string browserName, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests

       // [Test]
        [Description("")]
        [Property("Status", "")]
        [Author("Mike Johnston")]
        public void TestLoginPage()
        {
            /// 1. Navigate to the login page
           // LSLoginPage LP = LSNavigation.GoToLoginPage(browser);
           // LP.Login("", "");


        }
  
        #endregion Tests
    }
}

