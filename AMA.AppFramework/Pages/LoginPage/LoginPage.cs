using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace AMA.AppFramework
{
    public class LoginPage : AMAPage, IDisposable
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

        public override string PageUrl { get { return "login.aspx"; } }//change this

        #endregion properties

        #region elements

        public IWebElement UserNameTxt { get { return this.FindElement(Bys.LoginPage.UserNameTxt); } }
        public IWebElement UserNameWarningLbl { get { return this.FindElement(Bys.LoginPage.UserNameWarningLbl); } }
        public IWebElement PasswordTxt { get { return this.FindElement(Bys.LoginPage.PasswordTxt); } }
        public IWebElement PasswordWarningLbl { get { return this.FindElement(Bys.LoginPage.PasswordWarningLbl); } }
        public IWebElement RememberMeChk { get { return this.FindElement(Bys.LoginPage.RememberMeChk); } }
        public IWebElement LoginBtn { get { return this.FindElement(Bys.LoginPage.LoginBtn); } }
        public IWebElement ForgotPasswordLnk { get { return this.FindElement(Bys.LoginPage.ForgotPasswordLnk); } }
        public IWebElement LoginUnsuccessfullWarningLbl { get { return this.FindElement(Bys.LoginPage.LoginUnsuccessfullWarningLbl); } }

        #endregion elements

        #region methods: per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(300), Criteria.LoginPage.PageReady);
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

        #endregion methods: per page

        #region methods: page specific

        /// <summary>
        /// Clicks the user-specified button or link and then waits for a window to close or open, or a page to load,
        /// depending on the button that was clicked
        /// </summary>
        /// <param name="buttonOrLinkElem">The element to click on</param>
        public dynamic ClickToAdvance(IWebElement buttonOrLinkElem)
        {
            if (Browser.Exists(Bys.LoginPage.LoginBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == LoginBtn.GetAttribute("outerHTML"))
                {
                    LoginBtn.Click();
                    // Browser.WaitForElement(Bys.EducationCenterPage.MyCoursesTtl, TimeSpan.FromSeconds(60), ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
                    //Browser.WaitForElement(Bys.EducationCenterPage.GcepLnk, TimeSpan.FromSeconds(60), ElementCriteria.IsEnabled);
                    new WebDriverWait(Browser, TimeSpan.FromSeconds(90)).Until(ExpectedConditions.UrlContains("Courses.aspx"));

                    return new EducationCenterPage(Browser);                  
                }
                else
                {
                    throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, or if the button is already added, then the page you were on did not contain the button.");
                }
            }

            return null;
        }

        /// <summary>
        /// Enters text in the username and password field, clicks the login button, then waits for the URL 
        /// of the Library page to load
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="Password"></param>
        public dynamic LoginAsUser(string userName, string Password)
        {
            // Login with a valid user credentials
            UserNameTxt.Clear();
            PasswordTxt.Clear();
            UserNameTxt.SendKeys(userName);
            PasswordTxt.SendKeys(Password);
            PasswordTxt.SendKeys(Keys.Tab);
            ClickToAdvance(LoginBtn);
            Browser.WaitForElement(Bys.EducationCenterPage.GcepLnk, TimeSpan.FromSeconds(60), ElementCriteria.IsEnabled);
            return new EducationCenterPage(Browser);
        }

        public bool SmartClick(IWebElement ButtonToClick)
        {
            bool status = false;
            int i = 0;
            while (i == 0)
                try
                {
                    ButtonToClick.Click();
                    status = true;
                    break;
                }
                catch (StaleElementReferenceException )
                {
                }
            return status;
        }

        #endregion methods: page specific



    }
}


