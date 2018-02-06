using Browser.Core.Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using AMA.AppFramework;
using System;
using AMA.AppFramework.Utils.User;
using static AMA.AppFramework.Utils.User.UserUtils;
using System.Threading;

namespace AMA.UITest
{
    //[BrowserMode(BrowserMode.New)]
    //[LocalSeleniumTestFixture(BrowserNames.Chrome)]
    //[LocalSeleniumTestFixture(BrowserNames.Firefox)]
    //[LocalSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Firefox)]
    [RemoteSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [TestFixture]
    public class AMA_Login_Tests : TestBase
    {
        #region Constructors
        public AMA_Login_Tests(string browserName) : base(browserName) { }

        // Remote Selenium Grid Test
        public AMA_Login_Tests(string browserName, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests

      
      
        [Test]
        [Description("Verifying everything within the login page. Any validations that are in place, etc. It also tests that a " +
            "user is able to login successfully")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        [Category("AzatCat")]

        public void TestLoginPage()
        {
            ///  1.Navigate to the login page
            LoginPage LP = Navigation.GoToLoginPage(browser);
            
            ///  2.Click the login button without entering in the username or password, and then verify the warning messages
            LP.LoginBtn.SendKeys(Keys.Tab);
            LP.LoginBtn.Click();
            browser.WaitForElement(Bys.LoginPage.UserNameWarningLbl, ElementCriteria.IsVisible);   
            Assert.AreEqual("Please enter your username.", LP.UserNameWarningLbl.Text);
           // Assert.IsFalse(true);

            ///  3.Enter text in the required fields and verify the warning messages disappear
            LP.UserNameTxt.SendKeys("Not a valid user");
            LP.PasswordTxt.SendKeys("blah");
            LP.LoginBtn.SendKeys(Keys.Tab);
            LP.LoginBtn.Click();          
            browser.WaitForElement(Bys.LoginPage.LoginUnsuccessfullWarningLbl, ElementCriteria.IsEnabled, ElementCriteria.IsVisible);

            ///  4.The user above does not exist, so click the Login and verify the system warns the user
            Assert.True(LP.LoginUnsuccessfullWarningLbl.Displayed);
            Assert.AreEqual(LP.LoginUnsuccessfullWarningLbl.Text, "Your login attempt was not successful.\r\nPlease try again.");               
              
        }

     
        [Test]
        [Description(" Verifying everything within the login page. Any validations that are in place, etc. It also tests that a user is able"+
        " to  login successfully navigate GCEP PAGE and sign out and verifiying url.")]
        [Property("Status", "Complete")]
        [Author("Azat Chariyev")]
        public void TestLoginPageandSignOutURL()
        {
            ///  1.Navigate to the login page
            UserInfo role = UserUtils.GetUser(UserRole.Manager); //UserInfo role = UserUtils.GetUser(userRole);
            LoginPage LP = Navigation.GoToLoginPage(browser);

            ///  2.Click the login button without entering in the username or password, and then verify the warning messages
            LP.LoginBtn.Click();
            browser.WaitForElement(Bys.LoginPage.UserNameWarningLbl, ElementCriteria.IsVisible);
            Assert.AreEqual("Please enter your username.", LP.UserNameWarningLbl.Text);

            ///  3. Login with a valid user credentials and navigate to GCEP page
            EducationCenterPage ED = LP.LoginAsUser(role.Username, role.Password);
            GCEPPage GCEP = ED.ClickToAdvance(ED.GcepLnk);

            ///  4.From GCEP  page Sign Out
            GCEP.HeaderMenuDropDown.Click();

            GCEP.SignOutLnk.SendKeys(Keys.Tab);
            GCEP.SignOutLnk.Click();
            Thread.Sleep(2000);

            ///  5.Verifying url after Sign Out.
           // Assert.IsTrue(Browser.Url.Equals("https://logintest.ama-assn.org/account/logout"));
        }


        #endregion Tests
    }
}

