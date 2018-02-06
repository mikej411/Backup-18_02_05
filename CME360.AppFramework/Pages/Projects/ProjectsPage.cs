using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace CME.AppFramework
{
    public class ProjectsPage : Page, IDisposable
    {
        #region constructors
        public ProjectsPage(IWebDriver driver) : base(driver)
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

        public IWebElement ManageActivitiesLnk { get { return this.FindElement(Bys.ProjectsPage.ManageActivitiesLnk); } }
        public IWebElement ActivitiesSearchBtn { get { return this.FindElement(Bys.ProjectsPage.ActivitiesSearchBtn); } }
        public IWebElement ActivitiesSearchTxt { get { return this.FindElement(Bys.ProjectsPage.ActivitiesSearchTxt); } }
        public IWebElement ManageActivitiesTbl { get { return this.FindElement(Bys.ProjectsPage.ManageActivitiesTbl); } }


        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(180), Criteria.ProjectsPage.PageReady);
        }

        /// Refreshes the page and then uses the wait criteria that is found within WaitForInitialize to wait for the page to load.
        /// This is used as a catch block inside WaitForInitialize, in case the page doesnt load initially. Can also be used to 
        /// randomly refresh the page
        /// </summary>
        public void RefreshPage()
        {
            Browser.Navigate().Refresh();
            this.WaitUntil(TimeSpan.FromSeconds(180), Criteria.ProjectsPage.PageReady);
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
        /// Clicks the user-specified element, and then waits for a window to close or open, or a page to load, depending on the element that was clicked
        /// </summary>
        /// <param name="buttonOrLinkElem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement buttonOrLinkElem)
        {
            // Error handler to make sure that the button that the tester passed in the parameter is actually on the page
            if (Browser.Exists(Bys.ProjectsPage.ManageActivitiesLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == ManageActivitiesLnk.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntil(Criteria.ProjectsPage.ActivitiesSearchTxtVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ProjectsPage.ActivitiesSearchBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == ActivitiesSearchBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    // Adding a static wait here because cme360 technology is ancient so there is nothing to wait for
                    // dynamically inside the HTML
                    Thread.Sleep(3000);
                    return null;
                }

            }
            else
            {
                throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, " +
                    "or if the button is already added, then the page you were on did not contain the button.");
            }

            return null;
        }

        /// <summary>
        /// Clicks on Manage Activities, enters user-specified text into the Search text box, clicks Search, click the Pencil icon
        /// and waits for the Activity page to load
        /// </summary>
        /// <param name="activitySearchText">The text you want to enter in the search text box. Note that CME360 has a bug where a lot of searches dont work. So you have to make your search text short</param>
        /// <param name="activityName">The full name of the activity</param>
        /// <returns></returns>
        public ActivityMainPage GoToEditActivity(string activitySearchText, string activityName)
        {
            ClickAndWait(ManageActivitiesLnk);

            ActivitiesSearchTxt.SendKeys(activitySearchText);
            ClickAndWait(ActivitiesSearchBtn);

            IWebElement row = ElemGet_CME360.Grid_GetRowByRowName(ManageActivitiesTbl, Bys.ProjectsPage.ManageActivitiesTblBodyRow,
                activityName, "td");

            ElemSet_CME360.Grid_ClickElementWithoutTextInsideRow(row, "img", "Edit");

            ActivityMainPage page = new ActivityMainPage(Browser);
            page.WaitForInitialize();

            return page;
        }


        #endregion methods: page specific



    }
}