using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace AMA.AppFramework
{
    public class AssignConfirmationPage : AMAPage, IDisposable
    {
        #region constructors
        public AssignConfirmationPage(IWebDriver driver) : base(driver)
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

        public IWebElement ProgramSummaryTbl { get { return this.FindElement(Bys.AssignConfirmationPage.ProgramSummaryTbl); } }
        public IWebElement ConfirmBtn { get { return this.FindElement(Bys.AssignConfirmationPage.ConfirmBtn); } }
        public IWebElement BackBtn { get { return this.FindElement(Bys.AssignConfirmationPage.BackBtn); } }
        public IWebElement EditConfirmBtn { get { return this.FindElement(Bys.AssignConfirmationPage.EditConfirmBtn); } }
        public IWebElement TimeFrameLbl { get { return this.FindElement(Bys.AssignConfirmationPage.TimeFrameLbl); } }

        #endregion elements

        #region methods: per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(300), Criteria.AssignConfirmationPage.PageReady);
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
            if (Browser.Exists(Bys.AssignConfirmationPage.EditConfirmBtn))
            {
                if (buttonorElem.GetAttribute("outerHTML") == EditConfirmBtn.GetAttribute("outerHTML"))
                {
                    Thread.Sleep(0500);
                    EditConfirmBtn.Click();                    
                    //Browser.WaitForElement(Bys.ProgramsPage.ProgramMngTbl, TimeSpan.FromSeconds(120), ElementCriteria.IsEnabled, ElementCriteria.IsVisible);
                    //new WebDriverWait(Browser, TimeSpan.FromSeconds(15)).Until(ExpectedConditions.UrlContains("programs"));
                    ProgramsPage PP = new ProgramsPage(Browser);
                    PP.WaitForInitialize();
                    return PP;
                }
            }
            if (Browser.Exists(Bys.AssignConfirmationPage.ConfirmBtn))
            {
                if (buttonorElem.GetAttribute("outerHTML") == ConfirmBtn.GetAttribute("outerHTML"))
                {
                    Thread.Sleep(0500);                
                    ConfirmBtn.Click();                    
                    //Browser.WaitForElement(Bys.ProgramsPage.ProgramMngTbl, TimeSpan.FromSeconds(120), ElementCriteria.IsEnabled, ElementCriteria.IsVisible);
                    //new WebDriverWait(Browser, TimeSpan.FromSeconds(15)).Until(ExpectedConditions.UrlContains("programs"));
                    ProgramsPage PP = new ProgramsPage(Browser);
                    PP.WaitForInitialize();
                    return PP;
                }
            }
            else
            {
                throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, or if the button is already added, then the page you were on did not contain the button.");
            }            
            return null;
        }      

        /// <summary>
        /// Looking for specific row and cell for text and return boolean
        /// </summary>
        /// <param name="tableBodyElem"></param>
        /// <param name="expectedText"></param>
        /// <returns>boolean</returns>
        public bool Grid_CellTextFound(IWebElement tableBodyElem, string expectedText)
        {
            Browser.WaitForElement(Bys.AssignConfirmationPage.ProgramSummaryTbl, ElementCriteria.IsVisible);
            IWebElement row = tableBodyElem.FindElement(By.XPath(".//tr[7]"));
            IWebElement cell = row.FindElement(By.XPath("./td[3]"));
            if (cell.Text ==expectedText)
            {
                return true;
            }
            else
            {
                return false;
            }
           
        }
        
        /// <summary>
        /// This method for specific test.
        /// </summary>
        /// <returns>int count of Row</returns>
        public int GetCountOfPrograms()
        {
            List<IWebElement> Rows = new List<IWebElement>( Browser.FindElements(By.XPath(" //span[@class='col-xs-9 copy-edit-val']/span")));
            int count = Rows.Count;
            return count;
        }
    }
}
#endregion methods: page specific


