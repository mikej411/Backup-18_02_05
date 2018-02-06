using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace AMA.AppFramework
{
    public class CopyCurriculumEditsPage : AMAPage, IDisposable
    {
        #region constructors
        public CopyCurriculumEditsPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "gme-competency/admin/institutions/1972ef33-621e-4e1d-a9c3-b3ff470b0698/programs/copycurriculumedits"; } }//change this

        #endregion properties

        #region elements

        public IWebElement CurriculumNameLbl { get { return this.FindElement(Bys.CopyCurriculumEditsPage.CurriculumNameLbl); } }
        public IWebElement ProgramNameLbl { get { return this.FindElement(Bys.CopyCurriculumEditsPage.ProgramNameLbl); } }
        public IWebElement TimeFrameLbl { get { return this.FindElement(Bys.CopyCurriculumEditsPage.TimeFrameLbl); } }
        public IWebElement CopyEditProgramTbl { get { return this.FindElement(Bys.CopyCurriculumEditsPage.CopyEditProgramTbl); } }
        public IWebElement NextBtn { get { return this.FindElement(Bys.CopyCurriculumEditsPage.NextBtn); } }
       

        #endregion elements

        #region methods: per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(300), Criteria.CopyCurriculumEditsPage.PageReady);
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
            if (Browser.Exists(Bys.CopyCurriculumEditsPage.NextBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == NextBtn.GetAttribute("outerHTML"))
                {
                    NextBtn.Click();                    
                    Browser.WaitForElement(Bys.AssignConfirmationPage.ConfirmBtn, TimeSpan.FromSeconds(120), ElementCriteria.IsEnabled);                    
                    return new AssignConfirmationPage(Browser);
                    
                }                
                else
                {
                    throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, or if the button is already added, then the page you were on did not contain the button.");
                }
            }

            return null;
        }

       /// <summary>
       /// Adding or Removing programs for Curriculum
       /// </summary>
       /// <param name="tableName"></param>
       /// <param name="indexes"></param>
       /// <returns></returns>
        public int AddOrRemovePrograms(IWebElement tableName,  params int[] indexes)
        {
            Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);
            foreach (var index in indexes)
            {
                Browser.WaitForElement(Bys.CopyCurriculumEditsPage.CopyEditProgramTbl, ElementCriteria.IsEnabled);              
                Thread.Sleep(0500);
                //ElemSet.ScrollToElement(Browser, Browser.FindElement(By.XPath($"(//*[@ng-model='row.isSelected'])[{index}]")));
                Thread.Sleep(0500);
                Browser.FindElement(By.XPath($"(//*[@ng-model='row.isSelected'])[{index}]")).Click();
                Thread.Sleep(0500);             
                Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);

            }
            int countofProgramms = indexes.Length;
            return countofProgramms;
        }


        #endregion methods: page specific



    }
}


