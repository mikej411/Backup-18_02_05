using Browser.Core.Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using NOF.AppFramework;
using System.Data;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using Browser.Core.Framework.Utils;
using System.Diagnostics;
using OpenQA.Selenium.Firefox;

namespace NOF.UITest
{
    [BrowserMode(BrowserMode.New)]
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    //[LocalSeleniumTestFixture(BrowserNames.Firefox)]
    [LocalSeleniumTestFixture(BrowserNames.InternetExplorer)]
    //[RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    //[RemoteSeleniumTestFixture(BrowserNames.Firefox)]
    //[RemoteSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [TestFixture]
    public class NOF_Sample_Tests : TestBase
    {
        #region Constructors
        public NOF_Sample_Tests(string browserName) : base(browserName) { }

        // Remote Selenium Grid Test
        public NOF_Sample_Tests(string browserName, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, version, platform, hubUri, extrasUri)
        { }
        #endregion


        #region Tests
        [Test]
        [Description("Workflow test making sure each page appears after clicking on the tabs")]
        [Property("Status", "In Progress")]
        [Author("Bala")]
        public void TestLoginPage()
        {   
            ///1. Navigate to the login page and login as existing user
            LoginPage LP = Navigation.GoToLoginPage(browser);
            HomePage HP = LP.LoginAsExistingUser("testaccountw","password");

            /////2. Click on each tab on the homepage and verify that the page appears by asserting that 
            ///// some elements are present
            //HP.ClickAndWaitBasePage(HP.Menu_MyCmeLnk);
            //CurriculumPage CP = HP.ClickAndWaitBasePage(HP.Menu_MyCME_Curriculum);
            //Assert.AreEqual("Curriculum", CP.CurriculumLbl.Text);

            //HP.ClickAndWaitBasePage(HP.Menu_MyCmeLnk);
            //TranscriptPage TP = HP.ClickAndWaitBasePage(HP.Menu_MyCME_TranscriptLnk);
            //Assert.AreEqual("Transcript", TP.TranscriptLbl.Text);


        }



        #endregion Tests
    }
}







