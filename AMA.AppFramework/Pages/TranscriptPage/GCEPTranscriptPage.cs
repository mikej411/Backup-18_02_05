using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace AMA.AppFramework
{
    public class GCEPTranscriptPage : AMAPage, IDisposable
    {
        #region constructors
        public GCEPTranscriptPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "http://ama.releasecandidate-community360.net/gme-competency/transcript"; } }

        #endregion properties

        #region elements

        public IWebElement TranscriptHeaderTbl { get { return this.FindElement(Bys.GCEPTranscriptPage.TranscriptHeaderTbl); } }
        public IWebElement CompletedTestTbl { get { return this.FindElementOrDefault(Bys.GCEPTranscriptPage.CompletedTestTbl); } }
        public SelectElement CompletionDateSelElem { get { return new SelectElement(this.FindElement(Bys.GCEPTranscriptPage.CompletionDateSelElem)); } }
       

        #endregion elements

        #region methods: per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(120), Criteria.GCEPTranscriptPage.PageReady);
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
        //public dynamic ClickButtonOrLinkToAdvance(IWebElement buttonOrLinkElem)
        //{
        //    if (Browser.Exists(GCEPTranscriptPage.TranscriptHeaderTbl))
        //    {
        //        if (buttonOrLinkElem.GetAttribute("outerHTML") == buttonOrLinkElem.GetAttribute("outerHTML"))
        //        {
        //            buttonOrLinkElem.Click();
        //            // Browser.WaitForElement(Bys.EducationCenterPage.MyCoursesTtl, TimeSpan.FromSeconds(60), ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
        //            //Browser.WaitForElement(Bys.EducationCenterPage.GcepLnk, TimeSpan.FromSeconds(60), ElementCriteria.IsEnabled);
        //            new WebDriverWait(Browser, TimeSpan.FromSeconds(15)).Until(ExpectedConditions.UrlContains("Courses.aspx"));

        //            return new EducationCenterPage(Browser);
        //            // }
        //        }
        //    }              
        //            else
        //            {
        //            throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, or if the button is already added, then the page you were on did not contain the button.");
        //             }


        //    return null;
        //}

                   

        /// <summary>
        /// verifiying  text inside of table and asserting returns boolean
        /// </summary>
        /// <param name="tableBodyElem">table weblelemnt where to find text to verify</param>
        /// <param name="expectedText"> text what you expecting to get</param>   
        public  bool Grid_CellTextFound(IWebElement tableBodyElem, string expectedText)
        {
            IList<IWebElement> allRows = tableBodyElem.FindElements(By.XPath("./div/div")); // Store all TR (rows) from the table into a variable
            foreach (var row in allRows)  // Loop through each row
            {
                if (row.FindElements(By.XPath("./div")).Count > 0) // If the given row contains any cells
                {
                  IWebElement cell = row.FindElements(By.XPath("./div"))[0]; // Get the cell  column
                  
                    if (cell.Text == expectedText)
                    {
                       return true;
                    }
                    
                }
            }

            return false;
        }
       
        #endregion methods: page specific

    }
}


