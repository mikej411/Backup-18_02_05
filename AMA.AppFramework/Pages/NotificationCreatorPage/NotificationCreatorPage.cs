using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace AMA.AppFramework
{
    public class NotificationCreatorPage : AMAPage, IDisposable
    {
        #region constructors
        public NotificationCreatorPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "/add"; } }//change this

        #endregion properties

        #region elements

        public IWebElement NotificationNameTxt { get { return this.FindElement(Bys.NotificationCreatorPage.NotificationNameTxt); } }
        public IWebElement NotificatioTitleTxt { get { return this.FindElement(Bys.NotificationCreatorPage.NotificatioTitleTxt); } }
        public IWebElement NotificationBodyTxt { get { return this.FindElement(Bys.NotificationCreatorPage.NotificationBodyTxt); } }
        public IWebElement DashBoardNotificationLbl { get { return this.FindElement(Bys.NotificationCreatorPage.DashBoardNotificationLbl); } }
        public IWebElement AddHyperlinkChk { get { return this.FindElement(Bys.NotificationCreatorPage.AddHyperlinkChk); } }
        public IWebElement SaveExitBtn { get { return this.FindElement(Bys.NotificationCreatorPage.SaveExitBtn); } }
        public IWebElement AdminsRdo { get { return this.FindElement(Bys.NotificationCreatorPage.AdminsRdo); } }
        public IWebElement ManagersRdo { get { return this.FindElement(Bys.NotificationCreatorPage.ManagersRdo); } }
        public IWebElement ResidentsRdo { get { return this.FindElement(Bys.NotificationCreatorPage.ResidentsRdo); } }

        #endregion elements

        #region methods: per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(300), Criteria.NotificationCreatorPage.PageReady);
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
            if (Browser.Exists(Bys.NotificationCreatorPage.SaveExitBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == SaveExitBtn.GetAttribute("outerHTML"))
                {
                    SaveExitBtn.Click();
                    // Browser.WaitForElement(Bys.EducationCenterPage.MyCoursesTtl, TimeSpan.FromSeconds(60), ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
                    Browser.WaitForElement(Bys.DashboardNotificationsPage.CreateNotificationBtn, TimeSpan.FromSeconds(90), ElementCriteria.IsEnabled);
                    new WebDriverWait(Browser, TimeSpan.FromSeconds(90)).Until(ExpectedConditions.UrlContains("gme-competency/admin/dashboardcontent"));
                    DashboardNotificationsPage DNP = new DashboardNotificationsPage(Browser);
                    DNP.WaitForInitialize();
                    return DNP;
                }
            }
            if (Browser.Exists(Bys.AMAPage.SignOutLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == SignOutLnk.GetAttribute("outerHTML"))
                {
                    Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);
                    SignOutLnk.SendKeys(Keys.Tab);
                    SignOutLnk.Click();
                    new WebDriverWait(Browser, TimeSpan.FromSeconds(115)).Until(ExpectedConditions.UrlMatches("https://logintest.ama-assn.org/account/logout"));
                    return this.Browser;
                }

            }
            else
            {
                throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, or if the button is already added, then the page you were on did not contain the button.");
            }           

            return null;
        }

       /// <summary>
       /// Creating random string 
       /// </summary>
       /// <param name="length">Length of string what will be created</param>
       /// <returns></returns>
        public string CreateRandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        #endregion methods: page specific



    }
}


