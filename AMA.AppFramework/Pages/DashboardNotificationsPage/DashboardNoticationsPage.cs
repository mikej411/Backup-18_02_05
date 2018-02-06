using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace AMA.AppFramework
{
    public class DashboardNotificationsPage : AMAPage, IDisposable
    {
        #region constructors
        public DashboardNotificationsPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "/dashboardcontent"; } }//change this

        #endregion properties

        #region elements

        public IWebElement NotifacionsMngTbl { get { return this.FindElement(Bys.DashboardNotificationsPage.NotifacionsMngTbl); } }
        public IWebElement DashboardLbl { get { return this.FindElement(Bys.DashboardNotificationsPage.DashboardLbl); } }
        public IWebElement SearchBySelElem { get { return this.FindElement(Bys.DashboardNotificationsPage.SearchBySelElem); } }
        public IWebElement CreateNotificationBtn { get { return this.FindElement(Bys.DashboardNotificationsPage.CreateNotificationBtn); } }
        public IWebElement GearActionBtn { get { return this.FindElement(Bys.DashboardNotificationsPage.GearActionBtn); } }
        public IWebElement AcceptFormBtn { get { return this.FindElement(Bys.DashboardNotificationsPage.AcceptFormBtn); } }
        public IWebElement EditLnk { get { return this.FindElement(Bys.DashboardNotificationsPage.EditLnk); } }
        public IWebElement RemoveLnk { get { return this.FindElement(Bys.DashboardNotificationsPage.RemoveLnk); } }

        #endregion elements

        #region methods: per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(300), Criteria.DashboardNoticationsPage.PageReady);
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
            if (Browser.Exists(Bys.DashboardNotificationsPage.CreateNotificationBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == CreateNotificationBtn.GetAttribute("outerHTML"))
                {
                    CreateNotificationBtn.Click();
                    //Browser.WaitForElement(Bys.NotificationCreatorPage.SaveExitBtn, TimeSpan.FromSeconds(120), ElementCriteria.IsVisible, ElementCriteria.IsEnabled);               
                    //new WebDriverWait(Browser, TimeSpan.FromSeconds(90)).Until(ExpectedConditions.UrlContains("/add"));
                    NotificationCreatorPage NCP = new NotificationCreatorPage(Browser);
                    NCP.WaitForInitialize();
                    return NCP;
                }
                if (Browser.Exists(Bys.AMAPage.SignOutLnk))
                {
                    if (buttonOrLinkElem.GetAttribute("outerHTML") == SignOutLnk.GetAttribute("outerHTML"))
                    {
                        Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);
                        HeaderMenuDropDown.Click();
                        SignOutLnk.SendKeys(Keys.Tab);
                        SignOutLnk.Click();
                        //new WebDriverWait(Browser, TimeSpan.FromSeconds(65)).Until(ExpectedConditions.UrlMatches("https://logintest.ama-assn.org/account/logout"));
                        return this.Browser;
                    }

                }
                if (Browser.Exists(Bys.AMAPage.GMECompetencyEducationProgramLnk))
                {
                    if (buttonOrLinkElem.GetAttribute("outerHTML") == GMECompetencyEducationProgramLnk.GetAttribute("outerHTML"))
                    {
                        GMECompetencyEducationProgramLnk.Click();
                        //Browser.WaitForElement(Bys.NotificationCreatorPage.SaveExitBtn, TimeSpan.FromSeconds(120), ElementCriteria.IsVisible, ElementCriteria.IsEnabled);               
                        //new WebDriverWait(Browser, TimeSpan.FromSeconds(90)).Until(ExpectedConditions.UrlContains("/add"));
                        GCEPPage GCEP = new GCEPPage(Browser);
                        GCEP.WaitForInitialize();
                        return GCEP;
                    }
                }
                else
                {
                    throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, or if the button is already added, then the page you were on did not contain the button.");
                }
            }

            return null;
        }


        /// <summary>
        /// deleting Notification
        /// </summary>
        /// <param name="curriculumName"></param>
        public void DeleteNotification()
        {

            if (Browser.FindElements(Bys.CurriculumMngPage.NoRecordLbl).Count > 0)
            {

            }
            else
            {
                do
                {
                    Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);
                    Browser.WaitForElement(Bys.DashboardNotificationsPage.NotifacionsMngTbl, TimeSpan.FromSeconds(120), ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
                    Thread.Sleep(0500);
                    GearActionBtn.Click();
                    RemoveLnk.Click();
                    Thread.Sleep(0500);
                    AcceptFormBtn.Click();
                    Browser.WaitForElement(Bys.AMAPage.LoadIcon, TimeSpan.FromSeconds(90), ElementCriteria.IsNotVisible);
                    Browser.WaitForElement(Bys.DashboardNotificationsPage.NotifacionsMngTbl, TimeSpan.FromSeconds(120), ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
                } while (Browser.FindElements(Bys.CurriculumMngPage.NoRecordLbl).Count <= 0);
            }
        }



        #endregion methods: page specific



    }
}


