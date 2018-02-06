using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace CME.AppFramework
{
    public class SearchResultsPage : Page, IDisposable
    {
        #region constructors
        public SearchResultsPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "Apps/Authenticate/SignIn.aspx?"; } }

        #endregion properties

        #region elements

        public IWebElement ActivitiesTbl { get { return this.FindElement(Bys.SearchResultsPage.ActivitiesTbl); } }
        public IWebElement ActivitiesTblBody { get { return this.FindElement(Bys.SearchResultsPage.ActivitiesTblBody); } }
        public IWebElement ActivitiesTblBodyRow { get { return this.FindElement(Bys.SearchResultsPage.ActivitiesTblBodyRow); } }

        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            try
            {
                this.WaitUntil(TimeSpan.FromSeconds(120), Criteria.SearchResultsPage.PageReady);
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
            this.WaitUntil(TimeSpan.FromSeconds(5), Criteria.SearchResultsPage.PageReady);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            try { activeRequests.Clear(); }
            catch (Exception ex) { _log.ErrorFormat("Failed to dispose SearchResultsPage", activeRequests.Count, ex); }
        }

        #endregion methods: repeated per page

        #region methods: page specific

        /// <summary>
        /// Clicks on the pencil icon of a user-specified activity and then waits for the main activity page to looad
        /// </summary>
        /// <param name="activityName">The full name of the activity</param>
        /// <returns></returns>
        public ActivityMainPage GoToActivity(string activityName)
        {
            IWebElement row = ElemGet_CME360.Grid_GetRowByRowName(ActivitiesTbl, Bys.SearchResultsPage.ActivitiesTblBodyRow,
                activityName, "td");

            ElemSet_CME360.Grid_ClickElementWithoutTextInsideRow(row, "img", "Edit");

            ActivityMainPage page = new ActivityMainPage(Browser);
            page.WaitForInitialize();

            return page;
        }


        #endregion methods: page specific



    }
}