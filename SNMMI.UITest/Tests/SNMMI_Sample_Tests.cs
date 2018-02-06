using Browser.Core.Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using SNMMI.AppFramework;
using System.Data;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using Browser.Core.Framework.Utils;
using System.Diagnostics;
using OpenQA.Selenium.Firefox;

namespace SNMMI.UITest
{
    [BrowserMode(BrowserMode.New)]
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [LocalSeleniumTestFixture(BrowserNames.Firefox)]
    [LocalSeleniumTestFixture(BrowserNames.InternetExplorer)]
    //[RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    //[RemoteSeleniumTestFixture(BrowserNames.Firefox)]
    //[RemoteSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [TestFixture]
    // If you dont want a class to be tested in parrelel, then include this attribute. You can also place this above a test method
    //[NonParallelizable]
    public class SNMMI_Sample_Tests : TestBase
    {
        #region Constructors
        public SNMMI_Sample_Tests(string browserName) : base(browserName) { }

        // Remote Selenium Grid Test
        public SNMMI_Sample_Tests(string browserName, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, version, platform, hubUri, extrasUri)
        { }
        #endregion

       

        #region Tests
        [Test]
        [Description("Workflow test making sure each page appears after clicking on the tabs")]
        [Property("Status", "In Progress")]
        [Author("Bala")]
        public void TestLogin()
        {
            ///1. Navigate to the login page and login as existing user
            LoginPage LP = Navigation.GoToLoginPage(browser);
            HomePage HP = LP.LoginAsExistingUser("360971", "password");


        }
        #endregion Tests
    }
}







