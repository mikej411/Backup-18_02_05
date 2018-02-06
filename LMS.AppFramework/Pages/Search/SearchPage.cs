using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;
using LS.AppFramework.Constants;

namespace LS.AppFramework
{
    public class SearchPage : Page, IDisposable
    {
        #region constructors
        public SearchPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "sites"; } }

        #endregion properties

        #region elements

        public IWebElement AllParticpantsTbl { get { return this.FindElement(Bys.SearchPage.AllParticpantsTbl); } }
        public IWebElement AllParticpantsTblBody { get { return this.FindElement(Bys.SearchPage.AllParticpantsTblBody); } }
        public IWebElement GoBtn { get { return this.FindElement(Bys.SearchPage.GoBtn); } }
        public IWebElement SitesTbl { get { return this.FindElement(Bys.SearchPage.SitesTbl); } }
        public IWebElement SearchTxt { get { return this.FindElement(Bys.SearchPage.SearchTxt); } }
        public IWebElement GenericTblBodyRow { get { return this.FindElement(Bys.SearchPage.GenericTblBodyRow); } }


        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.SearchPage.PageReady);
        }

        /// Refreshes the page and then uses the wait criteria that is found within WaitForInitialize to wait for the page to load.
        /// This is used as a catch block inside WaitForInitialize, in case the page doesnt load initially. Can also be used to 
        /// randomly refresh the page
        /// </summary>
        public void RefreshPage()
        {
            Browser.Navigate().Refresh();
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.SearchPage.PageReady);
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
        /// For any table within LS, this method enters text in the search box, then either clicks Go or hits Enter and waits for table to 
        /// get returned by waiting for the the tbody element's "class" attribute to not have a value of "loading"
        /// </summary>
        /// <param name="tblBody">The tbody element in your table</param>
        /// <param name="searchText">What you want to search for</param>
        public void Search(By tblBody, string searchText)
        {
            SearchTxt.SendKeys(searchText);

            SearchTxt.SendKeys(Keys.Enter);

            Thread.Sleep(0400);
            Browser.WaitForElement(tblBody, TimeSpan.FromSeconds(240), ElementCriteria.AttributeValueNot("class", "loading"));
            Thread.Sleep(1000);
        }

        /// <summary>
        /// Clicks on any website within the sites table, then waits for the next page to load
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="siteName">The exact text of the link for the site that you want to click on</param>
        internal void ClickSiteAndWait(IWebDriver browser, string siteName)
        {
            ElemSet.Grid_ClickButtonOrLinkWithinRow(browser, SitesTbl, Bys.SearchPage.SitesTblBodyRow,
                siteName + "...", "a", siteName + "...", "a");
            browser.WaitForElement(Bys.Page.AllParticipantsLnk, ElementCriteria.IsVisible);
        }

        /// <summary>
        /// Clicks on any participant within the participant table, then waits for the Participant page to load
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="particpantName">The exact text of the link for the participant that you want to click on from the participants table</param>
        internal ParticipantsPage ClickParticpantAndWait(IWebDriver browser, string particpantName)
        {
            ElemSet.Grid_ClickButtonOrLinkWithinRow(browser, AllParticpantsTblBody, Bys.SearchPage.AllParticpantsTblBodyRow,
                    particpantName, "a", particpantName, "a");

            ParticipantsPage page = new ParticipantsPage(browser);
            page.WaitForInitialize();
            return page;
        }

        /// <summary>
        /// For any table within LS, this method enters text in the search box, then either clicks Go or hits Enter and waits for table to
        /// get returned by waiting for the the tbody element's "class" attribute to not have a value of "loading". Then it will click on
        /// a site, particpant, etc. and wait for the cooresponding table to appear
        /// </summary>
        /// <param name="tblBody">The tbody element in your table</param>
        /// <param name="searchResults"><see cref="LSConstants.SearchResults"/> for a list of criteria that you can search for. For example, if you are on the Sites table, then you would obviously pass Sites as your search result</param>
        /// <param name="recordName">The exact text of the link for the site/participant/program/activity that you want to click on inside the table</param>
        internal dynamic SearchAndSelect(By tblBody, LSConstants.SearchResults searchResults, string recordName)
        {
            Search(tblBody, recordName);
            
            switch (searchResults)
            {
                case LSConstants.SearchResults.Sites:
                    ClickSiteAndWait(Browser, recordName);
                    return null;
                    break;
                case LSConstants.SearchResults.Participants:
                    ParticipantsPage page = ClickParticpantAndWait(Browser, recordName);
                    page.WaitForInitialize();
                    return page;
                    break;
            }

            return null;
        }


        #endregion methods: page specific



    }
}