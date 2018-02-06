using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace AMA.AppFramework
{
    public class AssignProgramPage : AMAPage, IDisposable
    {
        #region constructors
        public AssignProgramPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "https://ama.releasecandidate-community360.net/"; } }

        #endregion properties

        #region elements

     
        public IWebElement StartDateCalenderBox { get { return this.FindElement(Bys.AssignProgramPage.StartDateCalenderBox); } }
        public IWebElement NextBtn { get { return this.FindElement(Bys.AssignProgramPage.NextBtn); } }
        public IWebElement GetProgramsBtn { get { return this.FindElement(Bys.AssignProgramPage.GetProgramsBtn); } }
        public IWebElement AssignAvailableProgramTbl { get { return this.FindElement(Bys.AssignProgramPage.AssignAvailableProgramTbl); } }
        public IWebElement AddSelectedBtn { get { return this.FindElement(Bys.AssignProgramPage.AddSelectedBtn); } }
        public IWebElement RemoveSelectedBtn { get { return this.FindElement(Bys.AssignProgramPage.RemoveSelectedBtn); } }
        //public IWebElement StartDateCalenderBox { get { return this.FindElement(Bys.AssignProgramPage.StartDateCalenderBox); } }

        #endregion elements

        #region methods: per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(300), Criteria.AssignProgramPage.PageReady);
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
            if (Browser.Exists(Bys.AssignProgramPage.NextBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == NextBtn.GetAttribute("outerHTML"))
                {
                    //ElemSet.ScrollToElement(Browser, NextBtn);
                    NextBtn.Click();
                    Browser.WaitForElement(Bys.AMAPage.LoadIcon, TimeSpan.FromSeconds(120), ElementCriteria.IsNotVisible);
                    AssignSummaryPage ASP = new AssignSummaryPage(Browser);
                    ASP.WaitForInitialize();
                    return ASP;
                }
             }           
            else
            {
                throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, or if the button is already added, then the page you were on did not contain the button.");
            }            

            return null;
        }

        /// <summary>
        /// creating today date in format we can use on calendar and choosing date
        /// returning string format date to assert
        /// </summary>
        /// <param name="buttonOrLinkElem">The element to click on</param>
        public string ChoosingStartDate()
        {
            DateTime today = DateTime.Now;
            string StartingDate = today.ToString("M/d/yyyy");
            string Startyear = today.Year.ToString();
            string Startmonth = today.ToString("MMMM");
            string Startday = today.ToString("dd");
            ChooseStartDate(Browser, Startyear, Startmonth, Startday);
            return StartingDate;
        }

        /// <summary>
        ///creating today date in format we can use on calendar and  adding month choosing date
        /// returning string format date to assert
        /// </summary>
        /// <param name="monhtToAdd"> how many month to add on ending calendar</param>
        /// <returns></returns>
        public string ChoosingEndDate(int monhtToAdd,string dateFormat)
        {
            DateTime today = DateTime.Now;
            DateTime nextdate = today.AddMonths(monhtToAdd);
            string EndingDate = nextdate.ToString(dateFormat);
            string Endyear = nextdate.Year.ToString();
            string Endmonth = nextdate.ToString("MMMM");
            string Endday = nextdate.ToString("dd");
            ChooseEndDate(Browser, Endyear, Endmonth, Endday);
            return EndingDate;
        }

        /// <summary>
        /// Choosing date from date picker from start calendar
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="yr"></param>
        /// <param name="monthName"></param>
        /// <param name="dayOfMonth"></param>
        /// <returns></returns>
        public  string ChooseStartDate(IWebDriver browser, string yr, string monthName, string dayOfMonth)
        {
            IWebElement dateControlTxt = browser.FindElement(By.XPath("//input[@type='text' and @name='startDate']"));
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
            return dateControlTxt.GetAttribute("value");
        }

        /// <summary>
        /// Choosing date from date picker from end calendar
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="yr"></param>
        /// <param name="monthName"></param>
        /// <param name="dayOfMonth"></param>
        /// <returns></returns>
        public string ChooseEndDate(IWebDriver browser, string yr, string monthName, string dayOfMonth)
        {
            IWebElement dateControlTxt = browser.FindElement(By.XPath("//input[@type='text' and @name='endDate']"));
            dateControlTxt.Clear();
            Thread.Sleep(0600);
            IWebElement expandBtn = dateControlTxt.FindElement(By.XPath(".//.././/span[@class='input-group-btn']//button"));//input[@type='text' and @name='endDate']//.././/span[@class='input-group-btn']//button[2]//span
            expandBtn.Click();
            Thread.Sleep(0600);

            IWebElement topMiddleBtn = dateControlTxt.FindElement(By.XPath(".././/button[@role='heading']"));
            topMiddleBtn.Click();

            IWebElement topMiddleBtn2 = dateControlTxt.FindElement(By.XPath(".././/button[@role='heading']"));
            topMiddleBtn2.Click();

            IWebElement yearBtn = dateControlTxt.FindElement(By.XPath(string.Format(".././/span[text()='{0}' and contains(@class ,'binding')]", yr)));
            yearBtn.Click();

            IWebElement monthBtn = dateControlTxt.FindElement(By.XPath(string.Format(".././/span[text()='{0}' and contains(@class ,'binding')]", monthName)));
            monthBtn.Click();

            IWebElement dayOfMonthBtn = dateControlTxt.FindElement(By.XPath(string.Format(".././/span[text()='{0}' and @class='ng-binding']", dayOfMonth)));
            dayOfMonthBtn.Click();
            Thread.Sleep(0600);
            return dateControlTxt.GetAttribute("value");
        }

      
        /// <summary>
        /// Getting programs from available program table
        /// </summary>
        public void AssignProgramm()
        {
            GetProgramsBtn.Click();
            Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);
           // ElemSet.ScrollToElement(Browser, Browser.FindElement(By.XPath("//*[@ng-model='row.isSelected']")));
            Thread.Sleep(0500);
            Browser.FindElement(By.XPath("//*[@ng-model='row.isSelected']")).Click();
            Thread.Sleep(0500);
           // ElemSet.ScrollToElement(Browser, AddSelectedBtn);
            AddSelectedBtn.Click();
        }
        #endregion methods: page specific

    }
}


