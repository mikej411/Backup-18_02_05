using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace SNMMI.AppFramework
{
    public class LoginPage : SNMMIPage, IDisposable
    {
        #region constructors
        public LoginPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "login.aspx?action=enablelogin"; } }

        #endregion properties

        #region elements

        public IWebElement UserNameTxt { get { return this.FindElement(Bys.LoginPage.UserNameTxt); } }

        public IWebElement PasswordTxt { get { return this.FindElement(Bys.LoginPage.PasswordTxt); } }

        public IWebElement LoginBtn { get { return this.FindElement(Bys.LoginPage.LoginBtn); } }
        public IWebElement ForgotPasswordLnk { get { return this.FindElement(Bys.LoginPage.ForgotPasswordLnk); } }


        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.LoginPage.PageReady);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            try { activeRequests.Clear(); }
            catch (Exception ex) { _log.ErrorFormat("Failed to dispose LoginPage", activeRequests.Count, ex); }
        }

        #endregion methods: repeated per page

        #region methods: wrappers

        /// <summary>
        /// Clicks the user-specified element and then waits for a window to close or open, or a page to load,
        /// depending on the element that was clicked
        /// </summary>
        /// <param name="buttonOrLinkElem">The button element</param>
        public HomePage ClickAndWait(IWebElement buttonOrLinkElem)
        {
            if (Browser.Exists(Bys.LoginPage.LoginBtn))
            {
                if (buttonOrLinkElem.GetAttribute("id") == LoginBtn.GetAttribute("id"))
                {
                    LoginBtn.Click();
                    //new WebDriverWait(Browser, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.UrlContains("Default2"));
                    HomePage HP = new HomePage(Browser);
                    return HP;
                }
            }

            else
            {
                throw new Exception("No button or link was found with your passed parameter");
            }
            return null;
        }



        /// <summary>
        /// Enters text in the username and password field, clicks the login button, then waits for the URL 
        /// of the Dashboard page to load
        /// </summary>
        /// <param name="role">Either "learner", "observer", "program admin", etc.</param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public HomePage LoginAsExistingUser(string userName, string password)
        {
            // Login with a valid user
            UserNameTxt.Clear();
            PasswordTxt.Clear();
            UserNameTxt.SendKeys(userName);
            PasswordTxt.SendKeys(password);
            PasswordTxt.SendKeys(Keys.Tab);
            HomePage HP = ClickAndWait(LoginBtn);

            return HP;
        }

        #endregion wrappers



    }
}