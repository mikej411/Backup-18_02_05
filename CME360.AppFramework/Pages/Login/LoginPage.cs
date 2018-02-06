using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace CME.AppFramework
{
    public class LoginPage : Page, IDisposable
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

        public override string PageUrl { get { return "Apps/Authenticate/SignIn.aspx?"; } }

        #endregion properties

        #region elements

        public IWebElement UserNameTxt { get { return this.FindElement(Bys.LoginPage.UserNameTxt); } }
        public IWebElement PasswordTxt { get { return this.FindElement(Bys.LoginPage.PasswordTxt); } }
        public IWebElement LoginBtn { get { return this.FindElement(Bys.LoginPage.LoginBtn); } }


        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            try
            {
                this.WaitUntil(TimeSpan.FromSeconds(120), Criteria.LoginPage.PageReady);
            }
            catch
            {
                RefreshPage();
            }

        }

        /// Refreshes the page and then uses the wait criteria that is found within WaitForInitialize to wait for the page to load.
        /// This is used as a catch block inside WaitForInitialize, in case the page doesnt load initially. Can also be used to 
        /// randomly refresh the page
        /// </summary>
        public void RefreshPage()
        {
            Browser.Navigate().Refresh();
            this.WaitUntil(TimeSpan.FromSeconds(5), Criteria.LoginPage.PageReady);
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

        #region methods: page specific

        /// <summary>
        /// Clicks the user-specified element and then waits for a window to close or open, or a page to load, depending on the element that was clicked
        /// </summary>
        /// <param name="buttonOrLinkElem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement buttonOrLinkElem)
        {
            if (Browser.Exists(Bys.LoginPage.LoginBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == LoginBtn.GetAttribute("outerHTML"))
                {
                    LoginBtn.Click();
                    //new WebDriverWait(Browser, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.UrlContains("Default2"));
                    MyDashboardPage page = new MyDashboardPage(Browser);
                    return page;
                }
            }

            else
            {
                throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, or if the button is already added, then the page you were on did not contain the button.");
            }

            return null;
        }

        /// <summary>
        /// Enters text in the username and password field, clicks the login button, then waits for the URL 
        /// of the Dashboard page to load
        /// </summary>
        /// <param name="userName">The cme360 username</param>
        /// <param name="password"></param>
        public MyDashboardPage Login(string userName, string password)
        {
            // Login with a valid user
            UserNameTxt.Clear();
            PasswordTxt.Clear();
            UserNameTxt.SendKeys(userName);
            PasswordTxt.SendKeys(password);
            PasswordTxt.SendKeys(Keys.Tab);
            MyDashboardPage page = ClickAndWait(LoginBtn);

            return page;
        }

        #endregion methods: page specific



    }
}