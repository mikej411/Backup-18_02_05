using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace CFPC.AppFramework
{
    public class CPDActivitiesListPage : CFPCPage, IDisposable
    {
        #region constructors
        public CPDActivitiesListPage(IWebDriver driver) : base(driver)
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

        #endregion elements

        #region methods: per page

        public override void WaitForInitialize()
        {
          //  this.WaitUntil(TimeSpan.FromSeconds(30), Criteria.LoginPage.PageReady);
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

          
            if (buttonOrLinkElem.GetAttribute("id") == CPDPlanningTab.GetAttribute("id"))
            {
                buttonOrLinkElem.Click();
                //Browser.WaitForElement(Bys.EnterACPDActivityPage.CategoryDrpDn, TimeSpan.FromSeconds(20), ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
                CPDPlanningPage CPP = new CPDPlanningPage(Browser);
               // CPP.WaitForInitialize();
                return CPP;
            }
            else
            {
                throw new Exception("No button or link was found with your passed parameter");
            }

            return buttonOrLinkElem;
        }

    

        #endregion methods: page specific



    }
}
