using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace CME.AppFramework
{
    public class DistributionPage : Page, IDisposable
    {
        #region constructors
        public DistributionPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return ""; } }

        #endregion properties

        #region elements

        public IWebElement PortalTbl { get { return this.FindElement(Bys.DistributionPage.PortalTbl); } }
        public IWebElement PortalTblBody { get { return this.FindElement(Bys.DistributionPage.PortalTblBody); } }
        public IWebElement PortalTblBodyRow { get { return this.FindElement(Bys.DistributionPage.PortalTblBodyRow); } }
        public IWebElement FilterByLbl { get { return this.FindElement(Bys.DistributionPage.FilterByLbl); } }
        public IWebElement CatalogLibraryLbl { get { return this.FindElement(Bys.DistributionPage.CatalogLibraryLbl); } }

        public IWebElement CatalogsLnk { get { return this.FindElement(Bys.DistributionPage.CatalogsLnk); } }
      
        public IWebElement PortalsLnk { get { return this.FindElement(Bys.DistributionPage.PortalsLnk); } }
        public IWebElement AddNewPortalLnk { get { return this.FindElement(Bys.DistributionPage.AddNewPortalLnk); } }

        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            try
            {
                this.WaitUntil(TimeSpan.FromSeconds(180), Criteria.DistributionPage.PageReady);
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
            this.WaitUntil(TimeSpan.FromSeconds(180), Criteria.DistributionPage.PageReady);
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
           if (Browser.Exists(Bys.DistributionPage.PortalsLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == PortalsLnk.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntil(Criteria.DistributionPage.AddNewPortalsLnkVisible);
                    return null;
                }

            }
            if (Browser.Exists(Bys.DistributionPage.CatalogsLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == CatalogsLnk.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    CatalogsPage Page = new CatalogsPage(Browser);
                    Page.WaitForInitialize();
                    return Page;
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
        /// Clicks on either the pencil or X button for a user specified portal
        /// </summary>
        /// <param name="portalName">The portal name</param>
        /// <param name="button">"Edit" to click on the Pencil button, "Delete" to click on the X button or "View"</param>
        /// <returns></returns>
        public PortalPage GoToPortalDetails(string portalName,string tagName, string button)
        {
            //ClickAndWait(PortalsLnk);
            IWebElement row = ElemGet_CME360.Grid_GetRowByRowName(PortalTbl, Bys.DistributionPage.PortalTblBodyRow,
                portalName, "td");

            ElemSet_CME360.Grid_ClickElementWithoutTextInsideRow(row, tagName, button);

            PortalPage page = new PortalPage(Browser);
            page.WaitForInitialize();

            return page;
        }

        #endregion methods: page specific



    }
}