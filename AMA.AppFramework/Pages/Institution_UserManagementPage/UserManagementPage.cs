using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace AMA.AppFramework
{
    public class UserManagementPage : AMAPage, IDisposable
    {
        #region constructors
        public UserManagementPage(IWebDriver driver) : base(driver)
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

        public IWebElement UserNameTxt { get { return this.FindElement(Bys.UserManagementPage.UserNameTxt); } }
        public IWebElement UserEmailTxt { get { return this.FindElement(Bys.UserManagementPage.UserEmailTxt); } }
        public IWebElement UserInstitutionLbl { get { return this.FindElement(Bys.UserManagementPage.UserInstitutionLbl); } }
        public IWebElement UserMangementLbl { get { return this.FindElement(Bys.UserManagementPage.UserMangementLbl); } }
        public IWebElement UserProgramLbl { get { return this.FindElement(Bys.UserManagementPage.UserProgramLbl); } }
        public IWebElement SaveBtn { get { return this.FindElement(Bys.UserManagementPage.SaveBtn); } }
        public IWebElement CancelBtn { get { return this.FindElement(Bys.UserManagementPage.CancelBtn); } }      
        public SelectElement UserPgySelElem { get { return new SelectElement(this.FindElement(Bys.UserManagementPage.UserPgySelElem)); } }
        public SelectElement UserRoleSelElem { get { return new SelectElement(this.FindElement(Bys.UserManagementPage.UserRoleSelElem)); } }
        



        #endregion elements

        #region methods: per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(300), Criteria.UserManagementPage.PageReady);
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
            if (Browser.Exists(Bys.UserManagementPage.SaveBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == SaveBtn.GetAttribute("outerHTML"))
                {
                    SaveBtn.Click();
                     Browser.WaitForElement(Bys.GCEPUserMngPage.UsersManagementTbl, TimeSpan.FromSeconds(120), ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
                    //Browser.WaitForElement(Bys.EducationCenterPage.GcepLnk, TimeSpan.FromSeconds(60), ElementCriteria.IsEnabled);
                    new WebDriverWait(Browser, TimeSpan.FromSeconds(90)).Until(ExpectedConditions.UrlContains("users"));

                    return new GCEPUserMngPage(Browser);
                    
                }

                else
                {
                    throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, or if the button is already added, then the page you were on did not contain the button.");
                }
            }

            return null;
        }



        #endregion methods: page specific



    }
}


