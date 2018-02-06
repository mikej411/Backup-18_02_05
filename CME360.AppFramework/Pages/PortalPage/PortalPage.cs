using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace CME.AppFramework
{
    public class PortalPage : Page, IDisposable
    {
        #region constructors
        public PortalPage(IWebDriver driver) : base(driver)
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

        public IWebElement TermsAndConditionsLnk { get { return this.FindElement(Bys.PortalPage.TermsAndConditionsLnk); } }
        public IWebElement CatAndActTabSelCatalogTbl { get { return this.FindElement(Bys.PortalPage.CatAndActTabSelCatalogTbl); } }
        public IWebElement CatAndActTab { get { return this.FindElement(Bys.PortalPage.CatAndActTab); } }
        public IWebElement PortalTbl { get { return this.FindElement(Bys.PortalPage.PortalTbl); } }
        public IWebElement PortalTblBody { get { return this.FindElement(Bys.PortalPage.PortalTblBody); } }
        public IWebElement PortalTblBodyRow { get { return this.FindElement(Bys.PortalPage.PortalTblBodyRow); } }
        public IWebElement PortalTbl2 { get { return this.FindElement(Bys.PortalPage.PortalTbl2); } }
        public IWebElement PortalTblBody2 { get { return this.FindElement(Bys.PortalPage.PortalTblBody2); } }
        public IWebElement PortalTblBodyRow2 { get { return this.FindElement(Bys.PortalPage.PortalTblBodyRow2); } }

        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            try
            {
                this.WaitUntil(TimeSpan.FromSeconds(180), Criteria.PortalPage.PageReady);
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
            this.WaitUntil(TimeSpan.FromSeconds(180), Criteria.PortalPage.PageReady);
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
            if (Browser.Exists(Bys.PortalPage.CatAndActTabSelCatalogTbl))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == CatAndActTabSelCatalogTbl.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntil(Criteria.PortalPage.CatAndActTabSelCatalogTblVisible);
                    Browser.WaitForElement(Bys.PortalPage.CatAndActTabSelCatalogTbl, ElementCriteria.IsVisible);
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
        /// Clicks on either the pencil or X button for a user specified portal
        /// </summary>
        /// <param name="portalName">The portal name</param>
        /// <param name="button">"Edit" to click on the Pencil button, "Delete" to click on the X button or "View"</param>
        /// <returns></returns>
        public void AddCatalog(string portalName, string tagName, string button)
        {
            //ClickAndWait(PortalsLnk);
            IWebElement row = ElemGet_CME360.Grid_GetRowByRowName(PortalTbl, Bys.PortalPage.PortalTblBodyRow,
                portalName, "td");

            ElemSet_CME360.Grid_ClickElementWithoutTextInsideRow(row, tagName, button);

            //PortalPage page = new PortalPage(Browser);
            //page.WaitForInitialize();

            return ;
        }

        /// <summary>
        /// Clicks on either the pencil or X button for a user specified portal
        /// </summary>
        /// <param name="portalName">The portal name</param>
        /// <param name="button">"Edit" to click on the Pencil button, "Delete" to click on the X button or "View"</param>
        /// <returns></returns>
        public void RemoveCatalog(string portalName, string tagName, string button)
        {
            //ClickAndWait(PortalsLnk);
            IWebElement row = ElemGet_CME360.Grid_GetRowByRowName(PortalTbl2, Bys.PortalPage.PortalTblBodyRow2,
                portalName, "td");

            ElemSet_CME360.Grid_ClickElementWithoutTextInsideRow(row, tagName, button);

            //PortalPage page = new PortalPage(Browser);
            //page.WaitForInitialize();

            return;
        }

        #endregion methods: page specific



    }
}