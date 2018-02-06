using Browser.Core.Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using CME.AppFramework;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using OpenQA.Selenium.Firefox;
using Browser.Core.Framework.Resources;
using OpenQA.Selenium.Remote;
using System.Reflection;
using System.IO;

namespace CME.UITest
{
    //[BrowserMode(BrowserMode.New)]
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    //[LocalSeleniumTestFixture(BrowserNames.Firefox)]
    //[LocalSeleniumTestFixture(BrowserNames.InternetExplorer)]
    //[RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    //[RemoteSeleniumTestFixture(BrowserNames.Firefox)]
    //[RemoteSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [TestFixture]
    public class EnvironmentConfig : TestBase
    {
        #region Constructors
        public EnvironmentConfig(string browserName) : base(browserName) { }

        // Remote Selenium Grid Test
        public EnvironmentConfig(string browserName, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests

        //[Test]
        [Description("The following manual steps need to be performed on a new environment before you can run any tests on this new environment" +
            " 1. https://code.premierinc.com/docs/display/PGHLMSDOCS/CME360+And+Test+Portal+Static+Data" +
            " 2.")]
        [Property("Status", "In Progress")]
        [Author("Mike Johnston")]
        public void FirstTestTemporary()
        {

        }


        #endregion Tests
    }
}

