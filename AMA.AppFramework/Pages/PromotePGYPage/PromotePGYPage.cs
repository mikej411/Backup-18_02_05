using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace AMA.AppFramework
{
    public class PromotePGYPage : AMAPage, IDisposable
    {
        #region constructors
        public PromotePGYPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "/promotepgy"; } }//change this

        #endregion properties

        #region elements

        public IWebElement AddAllBtn { get { return this.FindElement(Bys.PromotePGYPage.AddAllBtn); } }
        public IWebElement AddSelectedBtn { get { return this.FindElement(Bys.PromotePGYPage.AddSelectedBtn); } }
        public IWebElement CancelBtn { get { return this.FindElement(Bys.PromotePGYPage.CancelBtn); } }
        public IWebElement AvailableResidentsPromotePGYTbl { get { return this.FindElement(Bys.PromotePGYPage.AvailableResidentsPromotePGYTbl); } }
        public IWebElement ChoosenResidentsPromotePGYTbl { get { return this.FindElement(Bys.PromotePGYPage.ChoosenResidentsPromotePGYTbl); } }
        public IWebElement PromoteBtn { get { return this.FindElement(Bys.PromotePGYPage.PromoteBtn); } }
        public IWebElement RemoveAllBtn { get { return this.FindElement(Bys.PromotePGYPage.RemoveAllBtn); } }
        public IWebElement RemoveSelectedBtn { get { return this.FindElement(Bys.PromotePGYPage.RemoveSelectedBtn); } }
        public IWebElement FormConfirmBtn { get { return this.FindElement(Bys.PromotePGYPage.FormConfirmBtn); } }
        public IWebElement FormResidentsDescriptionTbl { get { return this.FindElement(Bys.PromotePGYPage.FormResidentsDescriptionTbl); } }
        public IWebElement AvailableResidentsPromotePGYTblFirstRowChk { get { return this.FindElement(Bys.PromotePGYPage.AvailableResidentsPromotePGYTblFirstRowChk); } }


        #endregion elements

        #region methods: per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(300), Criteria.PromotePGYPage.PageReady);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            try { activeRequests.Clear(); }
            catch (Exception ex) { _log.ErrorFormat("Failed to dispose PromotePGY page", activeRequests.Count, ex); }
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
            if (Browser.Exists(Bys.PromotePGYPage.FormConfirmBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == FormConfirmBtn.GetAttribute("outerHTML"))
                {
                    //Browser.SwitchTo().Frame(PostTestFrame);
                    FormConfirmBtn.Click();
                    Browser.WaitForElement(Bys.GCEPPage.SendEmailNotificationLnk, TimeSpan.FromSeconds(120), ElementCriteria.IsVisible, ElementCriteria.IsEnabled);                    
                    new WebDriverWait(Browser, TimeSpan.FromSeconds(90)).Until(ExpectedConditions.UrlContains("gme-competency/admin"));

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

        /// <summary>
        ///  Adding or Removing programs for Curriculum
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="ButtonToAddOrRemove"></param>
        /// <param name="indexes"></param>
        public void AddOrRemoveResidents(IWebElement tableName, IWebElement ButtonToAddOrRemove, params int[] indexes)
        {
            Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);
            foreach (var index in indexes)
            {
                Browser.WaitForElement(Bys.PromotePGYPage.AvailableResidentsPromotePGYTbl, ElementCriteria.IsEnabled);
               // ElemSet.ScrollToElement(Browser,AvailableResidentsPromotePGYTbl);
                Thread.Sleep(0500);
              //  ElemSet.ScrollToElement(Browser, Browser.FindElement(By.XPath($"(//*[@ng-model='row.isSelected'])[{index}]")));
                Thread.Sleep(0500);
                Browser.FindElement(By.XPath($"(//*[@ng-model='row.isSelected'])[{index}]")).Click();
                Thread.Sleep(0500);
               // ElemSet.ScrollToElement(Browser, ButtonToAddOrRemove);
                ButtonToAddOrRemove.Click();
                Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);

            }
        }
        
        public string Grid_GetValueOfCell(IWebElement gridElem)
        {
            IList<IWebElement> allRows = gridElem.FindElements(By.XPath(".//div[@class='ng-isolate-scope']"));
            foreach (IWebElement row in allRows)
            {
                try
                {
                    IWebElement firstColumnCells = row.FindElements(By.XPath(".//div[@role='gridcell']"))[1];
                    return firstColumnCells.Text;
                }
                catch (ArgumentOutOfRangeException)
                {

                }
                            
            }
            return null;
        }

        public string GetValueOfCell(IWebElement gridElem, int sellindex)
        {
            IList<IWebElement> allRows = gridElem.FindElements(By.XPath("./tbody//tr"));
            foreach (IWebElement row in allRows)
            {
                try
                {
                    IWebElement firstColumnCells = row.FindElements(By.XPath(".//td"))[sellindex];
                    return firstColumnCells.Text;
                }
                catch (ArgumentOutOfRangeException)
                {

                }

            }
            return null;
        }
        #endregion methods: page specific



    }
}


