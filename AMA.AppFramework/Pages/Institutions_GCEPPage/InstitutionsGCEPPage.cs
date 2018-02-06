using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace AMA.AppFramework
{
    public class InstitutionsGCEPPage : AMAPage, IDisposable
    {
        #region constructors
        public InstitutionsGCEPPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "gme-competency/admin/institutions"; } }//change this

        #endregion properties

        #region elements

        public IWebElement InstitutionCurriculumTmpLnk { get { return this.FindElement(Bys.InstitutionsGCEPPage.InstitutionCurriculumTmpLnk); } }
        public IWebElement InstitutionDashboarNotificationManagementLnk { get { return this.FindElement(Bys.InstitutionsGCEPPage.InstitutionDashboarNotificationManagementLnk); } }
        public IWebElement InstitutionProgramManagmentLnk { get { return this.FindElement(Bys.InstitutionsGCEPPage.InstitutionProgramManagmentLnk); } }
        public IWebElement InstitutionPromotePgyLnk { get { return this.FindElement(Bys.InstitutionsGCEPPage.InstitutionPromotePgyLnk); } }
        public IWebElement InstitutionRunReportLnk { get { return this.FindElement(Bys.InstitutionsGCEPPage.InstitutionRunReportLnk); } }
        public IWebElement InstitutionSendEmailNoticationLnk { get { return this.FindElement(Bys.InstitutionsGCEPPage.InstitutionSendEmailNoticationLnk); } }
        public IWebElement InstitutionUserManagementLnk { get { return this.FindElement(Bys.InstitutionsGCEPPage.InstitutionUserManagementLnk); } }
        public IWebElement DashboardDirective { get { return this.FindElement(Bys.InstitutionsGCEPPage.DashboardDirectiveLnk); } }
        public IWebElement TotalUserCountLnk { get { return this.FindElement(Bys.InstitutionsGCEPPage.TotalUserCountLnk); } }
        public IWebElement TotalProgramCountLnk { get { return this.FindElement(Bys.InstitutionsGCEPPage.TotalProgramCountLnk); } }
        public IWebElement TotalCurriculumTemplatesCountLnk { get { return this.FindElement(Bys.InstitutionsGCEPPage.TotalCurriculumTemplatesCountLnk); } }

        #endregion elements

        #region methods: per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(300), Criteria.InstitutionsGCEPPage.PageReady);
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
            if (Browser.Exists(Bys.InstitutionsGCEPPage.InstitutionProgramManagmentLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == InstitutionProgramManagmentLnk.GetAttribute("outerHTML"))
                {
                    Browser.WaitForElement(Bys.AMAPage.LoadIcon, TimeSpan.FromSeconds(90), ElementCriteria.IsNotVisible);
                    InstitutionProgramManagmentLnk.Click();
                    Browser.WaitForElement(Bys.ProgramsPage.ProgramMngTbl, TimeSpan.FromSeconds(120), ElementCriteria.IsEnabled, ElementCriteria.IsVisible);
                    Browser.WaitForElement(Bys.AMAPage.LoadIcon, TimeSpan.FromSeconds(90), ElementCriteria.IsNotVisible);                   
                    return new ProgramsPage(Browser);
                }
            }
            if (Browser.Exists(Bys.InstitutionsGCEPPage.InstitutionCurriculumTmpLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == InstitutionCurriculumTmpLnk.GetAttribute("outerHTML"))
                {
                    Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);
                    InstitutionCurriculumTmpLnk.Click();
                    Browser.WaitForElement(Bys.AMAPage.LoadIcon, TimeSpan.FromSeconds(120), ElementCriteria.IsNotVisible);
                    return new CurriculumMngPage(Browser);
                }
            }
            if (Browser.Exists(Bys.InstitutionsGCEPPage.InstitutionUserManagementLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == InstitutionUserManagementLnk.GetAttribute("outerHTML"))
                {
                    Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);
                    InstitutionUserManagementLnk.Click();
                    Browser.WaitForElement(Bys.AMAPage.LoadIcon, TimeSpan.FromSeconds(120), ElementCriteria.IsNotVisible);
                    return new GCEPUserMngPage(Browser);
                }
            }
            if (Browser.Exists(Bys.InstitutionsGCEPPage.InstitutionPromotePgyLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == InstitutionPromotePgyLnk.GetAttribute("outerHTML"))
                {
                    //WaitForInitialize();
                    Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);
                    InstitutionPromotePgyLnk.Click();
                    //Browser.WaitForElement(Bys.PromotePGYPage.AvailableResidentsPromotePGYTblFirstRowChk, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
                    PromotePGYPage PPGYP = new PromotePGYPage(Browser);
                   // PPGYP.WaitForInitialize();
                    return PPGYP;
                }
            }
            else
                {
                    throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, or if the button is already added, then the page you were on did not contain the button.");
                }           

            return null;
        }

      

        #endregion methods: page specific



    }
}


