using Browser.Core.Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using AAFPRS.AppFramework;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace AAFPRS.UITest
{
    //[BrowserMode(BrowserMode.New)]
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    //[LocalSeleniumTestFixture(BrowserNames.Firefox)]
    //[LocalSeleniumTestFixture(BrowserNames.InternetExplorer)]
    //[RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    //[RemoteSeleniumTestFixture(BrowserNames.Firefox)]
    //[RemoteSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [TestFixture]
    public class AAFPRS_Example_Tests : TestBase
    {
        #region Constructors
        public AAFPRS_Example_Tests(string browserName) : base(browserName) { }

        // Remote Selenium Grid Test
        public AAFPRS_Example_Tests(string browserName, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, version, platform, hubUri, extrasUri)
        { }
        #endregion

        /// <summary>
        /// Example of how to override the teardown at the test class level
        /// </summary>
        //public override void TearDown() 
        //{
        //    Browser.Manage().Window.Size = new System.Drawing.Size(1040, 784);
        //    CleanupBrowser();
        //}

        #region Tests

        /// <summary>
        /// Verifying everything within the login page. Any validations that are in place, etc. It also tests that a user is able
        /// to login successfully
        /// </summary>
        /// Author: Mike Johnston
        /// Status: Complete
        [Test]
        [Category("Integration"), Category("IntegrationQA")]
        public void TestLoginPage()
        {
            
 
        }


        [Test]
        public void LakshmiTest()
        {
            LoginPage Lp = Navigation.GoToLoginPage(browser);
            Assert.True(false);
            Lp.UserNameTxt.SendKeys("testaccount1");
            Lp.PasswordTxt.SendKeys("password");
            Lp.LoginBtn.Click();
            Assert.True(Lp.Menu_Home.Displayed);
            Assert.True(Lp.Menu_Community.Displayed);
            Assert.True(Lp.Menu_MyAccount.Displayed);
            Assert.True(Lp.Menu_ContactUs.Displayed);
            Assert.True(Lp.Menu_Support.Displayed);

            Thread.Sleep(2000);
            Lp.Menu_Community.Click();
            Lp.Menu_Community.Click();
            Thread.Sleep(2000);
            Lp.Menu_Home.Click();
            Lp.Menu_Home.Click();
            Thread.Sleep(2000);
            Lp.Menu_MyAccount.Click();
            Lp.Menu_MyAccount.Click();
            Thread.Sleep(2000);
            Lp.Menu_ContactUs.Click();
            Lp.Menu_ContactUs.Click();
            Thread.Sleep(2000);
            Lp.Menu_Support.Click();
            Lp.Menu_Support.Click();
            Thread.Sleep(2000);


        }

  

        #endregion Tests
    }
}

