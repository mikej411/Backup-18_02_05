using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace AMA.AppFramework
{
    public class AssignSummaryPage : AMAPage, IDisposable
    {
        #region constructors
        public AssignSummaryPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "https://ama.releasecandidate-community360.net/gme-competency/admin/programcurriculumtemplates/curriculumprogramtimeframe/curriculumprogramsummary"; } }

        #endregion properties

        #region elements

        public IWebElement CreatedProgramName { get { return this.FindElement(Bys.AssignSummaryPage.CreatedProgramNameLbl); } }
        public IWebElement NextBtn { get { return this.FindElement(Bys.AssignSummaryPage.NextBtn); } }
        public IWebElement ProgramSummaryTbl { get { return this.FindElement(Bys.AssignSummaryPage.ProgramSummaryTbl); } }
        public IWebElement BackBtn { get { return this.FindElement(Bys.AssignSummaryPage.BackBtn); } }
        public IWebElement EditProgramSummarytbl { get { return this.FindElement(Bys.AssignSummaryPage.EditProgramSummarytbl); } }
        
        

        #endregion elements

        #region methods: per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(300), Criteria.AssignSummaryPage.PageReady);
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
        public dynamic ClickToAdvance(IWebElement buttonorElem)
        {
            if (Browser.Exists(Bys.AssignSummaryPage.NextBtn))
            {
                if (buttonorElem.GetAttribute("id") == NextBtn.GetAttribute("id"))
                {
                    Browser.WaitForElement(Bys.AssignSummaryPage.NextBtn, ElementCriteria.IsEnabled);
                    Thread.Sleep(0500);
                    ElemSet.ScrollToElement(Browser, NextBtn);                 
                    NextBtn.Click();                   
                    Browser.WaitForElement(Bys.AMAPage.BreadCrumbLnksContainer, TimeSpan.FromSeconds(90), ElementCriteria.IsEnabled, ElementCriteria.IsVisible);                   
                    AssignConfirmationPage ACP = new AssignConfirmationPage(Browser);
                    ACP.WaitForInitialize();
                    return ACP;
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


