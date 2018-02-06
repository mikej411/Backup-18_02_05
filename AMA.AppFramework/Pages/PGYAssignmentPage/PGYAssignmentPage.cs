using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace AMA.AppFramework
{
    public class PGYAssignmentPage : AMAPage, IDisposable
    {
        #region constructors
        public PGYAssignmentPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that I start so I can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "Education.aspx"; } } 
        //?

        #endregion properties

        #region elements

       public IWebElement ResetFiltersBtn { get { return this.FindElement(Bys.PGYAssignmentPage.CurriculumCoursePGYLbl); } }
        
       public IWebElement CurriculumNameTbl { get { return this.FindElement(Bys.PGYAssignmentPage.CurriculumNameTbl); } }     

       public IWebElement CancelBtn { get { return this.FindElement(Bys.PGYAssignmentPage.CancelBtn); } }

       public IWebElement SaveExitBtn { get { return this.FindElement(Bys.PGYAssignmentPage.SaveExitBtn); } }

        public IWebElement CourseTbl { get { return this.FindElement(Bys.PGYAssignmentPage.CourseTbl); } }

        public IWebElement NextBtn { get { return this.FindElement(Bys.PGYAssignmentPage.NextBtn); } }
        public IWebElement EditCoursePgyTbl { get { return this.FindElement(Bys.PGYAssignmentPage.EditCoursePgyTbl); } }
        public IWebElement UltimateTbl { get { return this.FindElement(Bys.PGYAssignmentPage.UltimateTbl); } }



        #endregion elements

        #region methods: per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(300), Criteria.PGYAssignmentPage.PageReady);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            try { activeRequests.Clear(); }
            catch (Exception ex) { _log.ErrorFormat("Failed to dispose LibraryPge", activeRequests.Count, ex); }
        }

        #endregion methods: per page

        #region methods: page specific

        ///// <summary>
        ///// Clicks the user-specified button or link and then waits for a window to close or open, or a page to load,
        ///// depending on the button that was clicked
        ///// </summary>
        ///// <param name="buttonOrLinkElem">The element to click on</param>
        public dynamic ClickToAdvance(IWebElement buttonOrLinkElem)
        {
            if (Browser.Exists(Bys.PGYAssignmentPage.SaveExitBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") ==SaveExitBtn.GetAttribute("outerHTML"))
                {
                    Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);
                   // ElemSet.ScrollToElement(Browser, buttonOrLinkElem);
                    SaveExitBtn.Click();                  
                    Browser.WaitForElement(Bys.CurriculumMngPage.SearchTxt,TimeSpan.FromSeconds(300), ElementCriteria.IsVisible);
                    Browser.WaitForElement(Bys.AMAPage.LoadIcon, TimeSpan.FromSeconds(120), ElementCriteria.IsNotVisible);                   
                    return new CurriculumMngPage(Browser) ;                  
                }
            }
            if (Browser.Exists(Bys.PGYAssignmentPage.NextBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == NextBtn.GetAttribute("outerHTML"))
                {
                    Browser.WaitForElement(Bys.AMAPage.LoadIcon, TimeSpan.FromSeconds(120), ElementCriteria.IsNotVisible);
                    Thread.Sleep(1000);
                   // ElemSet.ScrollToElement(Browser, buttonOrLinkElem);
                    NextBtn.Click();                   
                    Browser.WaitForElement(Bys.AssignSummaryPage.EditProgramSummarytbl, TimeSpan.FromSeconds(180), ElementCriteria.IsVisible,ElementCriteria.IsEnabled);
                    Browser.WaitForElement(Bys.AMAPage.LoadIcon, TimeSpan.FromSeconds(120), ElementCriteria.IsNotVisible);
                    return new AssignSummaryPage(Browser);                   
                }
            }

            else
            {
                throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, or if the button is already added, then the page you were on did not contain the button.");
            }

            return null;
        }  

           /// <summary>
           /// chosing  the check box from table by index
           /// </summary>
           /// <param name="courseTbl"></param>
           /// <param name="rowNum">Choosing row by index to perform clicking </param>
           /// <param name="cellNum">choosing the cell a by index from row what specified above"</param>
           /// this method no more need to use ,reason Xpath is really hard to understand

        public void ClickOnCellsOfRow(int rowIndex, params int[] cellIndexes)
        {
            Browser.WaitForElement(Bys.PGYAssignmentPage.CourseTbl, ElementCriteria.IsVisible);
            Thread.Sleep(0500);   
            foreach (var cell in cellIndexes)
            {        Browser
                    .FindElement(By.XPath(("((//*[@ng-class=\"{'ui-grid-row-selected': row.isSelected}\"])[" + rowIndex + "]/div/div)[" + cell + "]")))
                    .Click();
                Thread.Sleep(0500);
              
            }
        }           
      
        /// <summary>
        /// chosing  the check box from table by index
        /// </summary>
        /// <param name="courseTbl">Table with check box</param>
        /// <param name="rowNum">Send the row number where trhe check box exists in the table </param>
        /// <param name="cellNum">Send the column number where the chec box exists in the table"</param>   
        public void Grid_ClickElementWithoutTextInsideRow(IWebElement courseTbl, int rowNum, int colNum)
        {
            Browser.WaitForElement(Bys.PGYAssignmentPage.UltimateTbl, ElementCriteria.IsEnabled);
            Thread.Sleep(0500);
            string xpathStringforRow = string.Format("//div[@class='ui-grid-canvas']/div[{0}]", rowNum);
            IWebElement row = courseTbl.FindElement(By.XPath(xpathStringforRow));

            string xpathStringforCell = string.Format("./div/div[{0}]/div/input", colNum);
            IWebElement cell = row.FindElement(By.XPath(xpathStringforCell));

            cell.Click();
        }

        //public void ClickCheckboxesInTable(IWebElement courseTbl,params int[][] grid)
        //{
        //    foreach (var rowNum in grid)
        //    {
        //        foreach (var cellNum in rowNum)
        //            Browser.WaitForElement(Bys.PGYAssignmentPage.PGYTbl, ElementCriteria.IsVisible);
        //        string xpathStringforRow = string.Format("//div[@class='ui-grid-canvas']/div[{0}]", rowNum);
        //        IWebElement row = courseTbl.FindElement(By.XPath(xpathStringforRow));

        //        string xpathStringforCell = string.Format("./div/div[{0}]/div/input", cellNum);
        //        IWebElement cell = row.FindElement(By.XPath(xpathStringforCell));

        //        cell.Click();
        //    }                                                                                 //for (var i = 0; i < cellNum.Length; i++)
        //                                                                                      //{
        //    ClickCheckboxesInTable(courseTbl, rowNum, cellNum[i]);
        //}
    }



            #endregion methods: page specific


        
}