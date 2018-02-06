using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace AMA.AppFramework
{
    public class HelpPage : AMAPage, IDisposable
    {
        #region constructors
        public HelpPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "gme-competency/help"; } }//change this

        #endregion properties

        #region elements

        public IWebElement HelpLbl { get { return this.FindElement(Bys.HelpPage.HelpLbl); } }
        public IWebElement ContactUsLbl { get { return this.FindElement(Bys.HelpPage.ContactUsLbl); } }
        public IWebElement ResidentLaunchResourceLnk { get { return this.FindElement(Bys.HelpPage.ResidentLaunchResourceLnk); } }
        public IWebElement ResidentComingSoonLnk { get { return this.FindElement(Bys.HelpPage.ResidentComingSoonLnk); } }
        public IWebElement ManagerLaunchResourceLnk { get { return this.FindElement(Bys.HelpPage.ManagerLaunchResourceLnk); } }
        public IWebElement AdminLaunchResourceLnk { get { return this.FindElement(Bys.HelpPage.AdminLaunchResourceLnk); } }
        public IWebElement AdminWatchVideoLnk { get { return this.FindElement(Bys.HelpPage.AdminWatchVideoLnk); } }
        public IWebElement AMAMemberLaunchResourceLnk { get { return this.FindElement(Bys.HelpPage.AMAMemberLaunchResourceLnk); } }
        public IWebElement AMAResidentLaunchResourceLnk { get { return this.FindElement(Bys.HelpPage.AMAResidentLaunchResourceLnk); } }
        public IWebElement JAMALaunchResourceLnk { get { return this.FindElement(Bys.HelpPage.JAMALaunchResourceLnk); } }
        public IWebElement ContactGCEPatAMALnk { get { return this.FindElement(Bys.HelpPage.ContactGCEPatAMALnk); } }
        public IWebElement ContactInvolvedInstitutionEmailLnk { get { return this.FindElement(Bys.HelpPage.ContactInvolvedInstitutionEmailLnk); } }



        #endregion elements

        #region methods: per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(300), Criteria.HelpPage.PageReady);
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
            if (Browser.Exists(Bys.AMAPage.GMECompetencyEducationProgramLnk)) //AdministrationLnk
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == GMECompetencyEducationProgramLnk.GetAttribute("outerHTML"))     // AdministrationLnk.GetAttribute("outerHTML"))
                {
                    GMECompetencyEducationProgramLnk.Click();           //AdministrationLnk.Click();
                    // Browser.WaitForElement(Bys.EducationCenterPage.MyCoursesTtl, TimeSpan.FromSeconds(60), ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
                    //Browser.WaitForElement(Bys.EducationCenterPage.GcepLnk, TimeSpan.FromSeconds(60), ElementCriteria.IsEnabled);
                    // new WebDriverWait(Browser, TimeSpan.FromSeconds(90)).Until(ExpectedConditions.UrlContains("Courses.aspx"));

                    GCEPPage GP = new GCEPPage(Browser);
                    GP.WaitForInitialize();
                    return GP;
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


