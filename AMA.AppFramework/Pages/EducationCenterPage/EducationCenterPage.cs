using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace AMA.AppFramework
{
    public class EducationCenterPage : AMAPage, IDisposable
    {
        #region constructors
        public EducationCenterPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that I start so I can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "http://ama.releasecandidate-community360.net/Courses.aspx"; } }


        #endregion properties

        #region elements

        // Main page
        //  public IWebElement ResetFiltersBtn { get { return this.FindElement(Bys.LibraryPage.ResetFiltersBtn); } }

        public IWebElement AmaDropdownMenu { get { return this.FindElement(Bys.EducationCenterPage.AmaDropdownMenuLnk); } }

        public IWebElement GcepLnk { get { return this.FindElement(Bys.EducationCenterPage.GcepLnk); } }

        public IWebElement TranscriptLnk { get { return this.FindElement(Bys.EducationCenterPage.TranscriptLnk); } }

        public IWebElement FilterByBox { get { return this.FindElement(Bys.EducationCenterPage.FilterByTxt); } }

        public IWebElement CourseTbl { get { return this.FindElement(Bys.EducationCenterPage.CourseTbl); } }
        #endregion elements

        #region methods: per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(300), Criteria.EducationCenterPage.PageReady);
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
            if (Browser.Exists(Bys.EducationCenterPage.GcepLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == GcepLnk.GetAttribute("outerHTML"))
                {
                    GcepLnk.SendKeys(Keys.Tab);  // Have to tab here, because in firefox, the menu item doesnt expand when we use the Selenium Click method. We first have to tab to expand
                    GcepLnk.Click();
                    Browser.WaitForElement(Bys.AMAPage.LoadIcon, TimeSpan.FromSeconds(120), ElementCriteria.IsNotVisible);                 
                    GCEPPage GP = new GCEPPage(Browser);
                    GP.WaitForInitialize();
                    return GP;
                    // new WebDriverWait(Browser, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.UrlContains("gme-competency/admin"));
                    //new WebDriverWait(Browser, TimeSpan.FromSeconds(360)).Until(ExpectedConditions.InvisibilityOfElementLocated(Bys.AMAPage.LoadIcon));
                    // Browser.SwitchTo().Frame(G.EnterACPDFrame);
                }
            }
            if (Browser.Exists(Bys.EducationCenterPage.TranscriptLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == TranscriptLnk.GetAttribute("outerHTML"))
                {                  
                    TranscriptLnk.Click();                 
                    return new EducationCenterTransciptPage(Browser);
                    // Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);
                    // Browser.SwitchTo().Frame(G.EnterACPDFrame);
                    // new WebDriverWait(Browser, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.UrlContains("gme-competency/admin"));
                }
            }
            else
            {
                throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, or if the button is already added, then the page you were on did not contain the button.");
            }
            
            return null;

        }
        /// <summary>
        /// Finding Courses from table and clicking on it
        /// </summary>
        /// <param name="tableBodyElem"> TableName</param>
        /// <param name="courseName"> Course name to be clicked</param>
        /// <returns></returns>

        public  dynamic FindCourse(IWebElement tableBodyElem, string courseName)
        {
            IList<IWebElement> allRows = tableBodyElem.FindElements(By.TagName("tr")); // Store all TR (rows) from the table into a variable
            for (var i=1; i<allRows.Count; i++)// row in allRows)  // Loop through each row
            {
                if (allRows[i].FindElements(By.TagName("td")).Count > 0) // If the given row contains any cells
                {
                  IList<IWebElement> allCell = allRows[i].FindElements(By.TagName("td")); // Get the cell under the first column
                    foreach(var cell in allCell)
                     {
                        if (cell.FindElements(By.TagName("a")).Count > 0)
                        {
                            IWebElement aElem = cell.FindElement(By.TagName("a"));
                            if (aElem.Text == courseName)
                            {
                                aElem.Click();
                                Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);                              
                                return new CourseTestPage(Browser);

                            }

                        }
                        else
                        {
                            if (cell.Text == courseName)
                            {
                                cell.Click();
                                Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);                              
                                return new CourseTestPage(Browser); ;
                            }

                        }
                    }
                }
            }

            return null;
        }





        #endregion methods: page specific
    }
}

