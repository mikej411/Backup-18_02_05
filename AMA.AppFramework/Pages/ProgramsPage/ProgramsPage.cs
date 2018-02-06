using Browser.Core.Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace AMA.AppFramework
{
    public class ProgramsPage : AMAPage, IDisposable
    {
        #region constructors
        public ProgramsPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "programs"; } }//change this

        #endregion properties

        #region elements

        public IWebElement CreateProgramBtn { get { return this.FindElement(Bys.ProgramsPage.CreateProgramBtn); } }
        public IWebElement ProgramMngTbl { get { return this.FindElement(Bys.ProgramsPage.ProgramMngTbl); } }
        public IWebElement ActionBtn { get { return this.FindElement(Bys.ProgramsPage.ActionBtn); } }
        public IWebElement EditCurriculumLnk { get { return this.FindElement(Bys.ProgramsPage.EditCurriculumLnk); } }
        public IWebElement UnassignCurriculumLnk { get { return this.FindElement(Bys.ProgramsPage.UnassignCurriculumLnk); } }
        public IWebElement AcceptBtn { get { return this.FindElement(Bys.ProgramsPage.AcceptBtn); } }
        public IWebElement ActionGearBtn { get { return this.FindElement(Bys.ProgramsPage.ActionGearBtn); } }
        public IWebElement FromDateControlText { get { return this.FindElement(Bys.ProgramsPage.FromDateControlText); } }
        public IWebElement CopyEditsLnk { get { return this.FindElement(Bys.ProgramsPage.CopyEditsLnk); } }
        public IWebElement CurriculumTempDetailsFormTbl { get { return this.FindElement(Bys.ProgramsPage.CurriculumTempDetailsFormTbl); } }
        public IWebElement FormCloseBtn { get { return this.FindElement(Bys.ProgramsPage.FormCloseBtn); } }
       


        #endregion elements

        #region methods: per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(300), Criteria.ProgramsPage.ProgramMngtTbl);
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
        public dynamic ClickButtonToAdvance(IWebElement buttonOrLinkElem)
        {
            if (Browser.Exists(Bys.ProgramsPage.UnassignCurriculumLnk))
            {
                if (buttonOrLinkElem.GetAttribute("id") == UnassignCurriculumLnk.GetAttribute("id"))
                {
                    UnassignCurriculumLnk.Click();                    
                    new WebDriverWait(Browser, TimeSpan.FromSeconds(60)).Until(ExpectedConditions.UrlContains("Courses.aspx"));
                    return new EducationCenterPage(Browser);
              
                }
                else
                {
                    throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, or if the button is already added, then the page you were on did not contain the button.");
                }
            }

            return null;
        }

        /// <summary>
        /// Enters text in the username and password field, clicks the login button, then waits for the URL 
        /// of the Library page to load
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public dynamic UnassignCurriculum()
        {
            try {
                do
                {
                    ActionBtn.Click();
                    UnassignCurriculumLnk.Click();
                    Thread.Sleep(0500);
                    AcceptBtn.Click();
                    Browser.WaitForElement(Bys.AMAPage.LoadIcon, TimeSpan.FromSeconds(90), ElementCriteria.IsNotVisible);
                  
                }
                while (ActionBtn.Displayed);
            }
            catch (NoSuchElementException)
            {
                GCEPPage Gcep = new GCEPPage(Browser);
                // Gcep.GMECompetencyEducationProgramLnk.Click();
                Browser.Navigate().Back();
                Gcep.WaitForInitialize();                
                return Gcep;
            }
            return null;
        
        }

        /// <summary>
        /// something
        /// </summary>
        public void VeryfiyingDates()
        {
            string actualDateOnCalender = FromDateControlText.GetAttribute("value");
            string expectedDateOnCalender = CalenderDateBeforeToday(-6);
            Assert.IsTrue(actualDateOnCalender.Equals(expectedDateOnCalender));
            string todayDateFromCalendar = ChoosingFromDate();
        }

        /// <summary>
        /// something
        /// </summary>
        /// <returns></returns>
        public dynamic EditProgramm()
        {
            ActionBtn.Click();
            Thread.Sleep(0500);
            EditCurriculumLnk.Click();
            Thread.Sleep(0500);
            Browser.WaitForElement(Bys.AMAPage.LoadIcon, TimeSpan.FromSeconds(90), ElementCriteria.IsNotVisible);
            return new CurriculumCoursePage(Browser);
        }

        /// <summary>
        /// something
        /// </summary>
        /// <returns></returns>
        public dynamic CopyEditsProgramm()
        {
            ActionBtn.Click();
            Thread.Sleep(0500);
            CopyEditsLnk.Click();
            Thread.Sleep(0500);
            Browser.WaitForElement(Bys.AMAPage.LoadIcon, TimeSpan.FromSeconds(90), ElementCriteria.IsNotVisible);
            return new CopyCurriculumEditsPage(Browser);
        }

        /// <summary>
        /// something
        /// </summary>
        /// <param name="topLevelMenuId"></param>
        /// <param name="subMenuLinkText"></param>
        public void Selectselect(string topLevelMenuId, string subMenuLinkText)
        {
            try
            {

                WaitForInitialize();
                var toplevelMenuList = Browser.FindElements(By.XPath("//div[contains(@class, " + topLevelMenuId + ")]//button[contains(@class, 'ins-admin-btn-cp')]")).ToList();
                if (toplevelMenuList.Any())
                {
                    toplevelMenuList.First().Click();
                }

                Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);

                List<IWebElement> subLevelMenuLinks = Browser.FindElements(By.XPath("//a/span[.='" + subMenuLinkText + "']/..")).Where(g => g.Displayed && g.Enabled).ToList();
                if (subLevelMenuLinks.Any())
                {
                    subLevelMenuLinks.First().Click();
                    Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);

                }
            }
            catch (NoSuchElementException)
            {
                List<IWebElement> subLevelMenuLinks = Browser.FindElements(By.XPath("//a/span[.='" + subMenuLinkText + "']/..")).Where(g => g.Displayed && g.Enabled).ToList();
                subLevelMenuLinks.First().Click();
            }

        }

        /// <summary>
        /// something
        /// </summary>
        /// <param name="gridElem"></param>
        /// <param name="expectedDate"></param>
        /// <returns></returns>
        public Boolean GetValueOfRow(IWebElement gridElem,string expectedDate)
        {
            IList<IWebElement> allRows = gridElem.FindElements(By.XPath(".//div[@class='ng-isolate-scope']"));
          foreach (IWebElement row in allRows) 
          {
                IList<IWebElement> firstColumnCells = null;
                firstColumnCells = row.FindElements(By.XPath(".//div[@class='ng-scope']"));
                foreach (IWebElement cell in firstColumnCells)
                {
                    if (cell.Text.Contains(expectedDate))
                    {
                        return true;
                    }
                    else
                    { continue; }
                }
               
          }
                return false;
        }

        /// <summary>
        /// something
        /// </summary>
        /// <returns></returns>
        public Boolean DateIsBiggerOrEqualsThenCurrentDate()
        {
        List<string> rowDates = new List<string>(Browser.FindElements(By.XPath("//*[@id='gridProgramManagement']//div[@class='ng-isolate-scope']/div[7]//div")).S‌​elect(iw => iw.Text));
            if (rowDates.Count > 0)
            {
                foreach (string date in rowDates)
                {
                    DateTime currentDate = DateTime.Now;
                    DateTime rowDate = Convert.ToDateTime(date);
                    Assert.True(AssertUtils.DateGreaterThanOrEquals(currentDate, rowDate));
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// something
        /// </summary>
        /// <param name="monhtToAdd"></param>
        /// <returns></returns>
        public string CalenderDateBeforeToday(int monhtToAdd)
        {
            DateTime today = DateTime.Now;
            DateTime nextdate = today.AddMonths(monhtToAdd);
            string EndingDate = nextdate.ToString("MM/dd/yyyy");
            string Endyear = nextdate.Year.ToString();
            string Endmonth = nextdate.ToString("MMMM");
            string Endday = nextdate.ToString("dd");
            return EndingDate;
        }

        /// <summary>
        /// something
        /// </summary>
        /// <returns></returns>
        public string ChoosingFromDate()
        {
            DateTime today = DateTime.Now;
            string StartingDate = today.ToString("MM/dd/yyyy");
            string Startyear = today.Year.ToString();
            string Startmonth = today.ToString("MMMM");
            string Startday = today.ToString("dd");
            ChooseFromDate(Browser, Startyear, Startmonth, Startday);
            return StartingDate;
        }

        /// <summary>
        /// Date picker choose the date from calendar
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="yr"></param>
        /// <param name="monthName"></param>
        /// <param name="dayOfMonth"></param>
        public void  ChooseFromDate(IWebDriver browser, string yr, string monthName, string dayOfMonth)
        {
            IWebElement dateControlTxt = browser.FindElement(By.XPath("//input[@type='text' and @name='fromDate']"));
            dateControlTxt.Clear();
            Thread.Sleep(0600);
            IWebElement expandBtn = dateControlTxt.FindElement(By.XPath(".//.././/span[@class='input-group-btn']//button"));//.././/span[@class='input-group-btn']//button[2]//span
            expandBtn.Click();
            Thread.Sleep(0600);

            IWebElement topMiddleBtn = dateControlTxt.FindElement(By.XPath(".././/button[@role='heading']"));                   //button[text()='Today']
            topMiddleBtn.Click();

            IWebElement topMiddleBtn2 = dateControlTxt.FindElement(By.XPath(".././/button[@role='heading']"));
            topMiddleBtn2.Click();

            IWebElement yearBtn = dateControlTxt.FindElement(By.XPath(string.Format(".././/span[text()='{0}' and contains(@class ,'binding')]", yr)));
            yearBtn.Click();

            IWebElement monthBtn = dateControlTxt.FindElement(By.XPath(string.Format(".././/span[text()='{0}' and contains(@class ,'binding')]", monthName)));
            monthBtn.Click();

            IWebElement dayOfMonthBtn = dateControlTxt.FindElement(By.XPath(string.Format(".././/span[text()='{0}' and contains(@class ,'binding text-info')]", dayOfMonth)));
            dayOfMonthBtn.Click();
            Thread.Sleep(0600);
            Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);
          
        }

        /// <summary>
        /// something
        /// </summary>
        /// <returns></returns>
        public int GetCountOfCourse()
        {
            Browser.FindElement(By.XPath("//*[@id='gridProgramManagement']//a[@title]")).Click();
            Thread.Sleep(0500);
            int count = ElemGet.Grid_GetRowCount(CurriculumTempDetailsFormTbl);
            FormCloseBtn.Click();
            Thread.Sleep(0500);
            return count;
        }


        public dynamic SearchforProgram(string programName)
        {           
            Search(programName);
            Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);
            Browser.FindElement(By.LinkText(programName)).Click();
            Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);
            return new GCEPPage(Browser);
         
        }

        #endregion wrappers


    }
}


