using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace CME.AppFramework
{
    public class MyDashboardPage : Page, IDisposable
    {
        #region constructors
        public MyDashboardPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "Apps/CommandCenter/Dashboard/Dashboard.aspx"; } }

        #endregion properties

        #region elements

        public IWebElement MyDashboardsLbl { get { return this.FindElement(Bys.MyDashboardPage.MyDashboardsLbl); } }
   

        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            try
            {
                this.WaitUntil(TimeSpan.FromSeconds(180), Criteria.MyDashboardPage.PageReady);
            }
            catch
            {
                RefreshPage();
            }

        }

        /// Refreshes the page and then uses the wait criteria that is found within WaitForInitialize to wait for the page to load.
        /// This is used as a catch block inside WaitForInitialize, in case the page doesnt load initially. Can also be used to 
        /// randomly refresh the page
        /// </summary>
        public void RefreshPage()
        {
            Browser.Navigate().Refresh();
            this.WaitUntil(TimeSpan.FromSeconds(180), Criteria.MyDashboardPage.PageReady);
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

        #endregion methods: repeated per page

        #region methods: page specific

        /// <summary>
        /// Enters text into the top left hand Search field, clicks Go, then waits for the Search Results page to load
        /// </summary>
        /// <param name="textToEnter">The text you want to enter into the Search field</param>
        /// <returns></returns>
        public SearchResultsPage Search(string textToEnter)
        {
            SearchTxt.Clear();
            SearchTxt.SendKeys(textToEnter);
            SearchBtn.Click();

            SearchResultsPage page = new SearchResultsPage(Browser);
            page.WaitForInitialize();
            return page;
        }


        #endregion methods: page specific



    }
}