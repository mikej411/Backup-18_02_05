using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace AAFPRS.AppFramework
{
    public class LoginPage : AAFPRSPage, IDisposable
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
        public IWebElement UserNameWarningLbl { get { return this.FindElement(Bys.LoginPage.UserNameWarningLbl); } }
        public IWebElement PasswordTxt { get { return this.FindElement(Bys.LoginPage.PasswordTxt); } }
        public IWebElement PasswordWarningLbl { get { return this.FindElement(Bys.LoginPage.PasswordWarningLbl); } }
        public IWebElement RememberMeChk { get { return this.FindElement(Bys.LoginPage.RememberMeChk); } }
        public IWebElement LoginBtn { get { return this.FindElement(Bys.LoginPage.LoginBtn); } }
        public IWebElement ForgotPasswordLnk { get { return this.FindElement(Bys.LoginPage.ForgotPasswordLnk); } }
        public IWebElement LoginUnsuccessfullWarningLbl { get { return this.FindElement(Bys.LoginPage.LoginUnsuccessfullWarningLbl); } }
        public IWebElement iAcceptBtn { get { return this.FindElement(Bys.LoginPage.iAcceptBtn); } }

        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(15), Criteria.LoginPage.PageReady);
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
        /// Clicks the user-specified button or link and then waits for a window to close or open, or a page to load,
        /// depending on the button that was clicked
        /// </summary>
        /// <param name="buttonOrLinkElem">The element to click on</param>
        public bool ClickButtonOrLinkToAdvance(IWebElement buttonOrLinkElem)
        {
            bool buttonClicked = false;

            if (Browser.Exists(Bys.LoginPage.LoginBtn))
            {
                if (buttonOrLinkElem.GetAttribute("id") == LoginBtn.GetAttribute("id"))
                {
                    LoginBtn.Click();
                    //new WebDriverWait(Browser, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.UrlContains("Default2"));
                    buttonClicked = true;
                    return buttonClicked;
                }
            }

            else
            {
                throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, or if the button is already added, then the page you were on did not contain the button.");
            }

            return buttonClicked;
        }


        

        #endregion methods: page specific



    }
}