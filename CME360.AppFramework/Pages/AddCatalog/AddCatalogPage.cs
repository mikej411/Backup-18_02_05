using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace CME.AppFramework
{
    public class AddCatalogPage : Page, IDisposable
    {
        #region constructors
        public AddCatalogPage(IWebDriver driver) : base(driver)
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
        
        public IWebElement DetailsTabCatalogNameTxt { get { return this.FindElement(Bys.AddCatalogPage.DetailsTabCatalogNameTxt); } }
        public IWebElement DetailsTabShortLabelTxt { get { return this.FindElement(Bys.AddCatalogPage.DetailsTabShortLabelTxt); } }
        public IWebElement DetailsTabDescriptionTxt { get { return this.FindElement(Bys.AddCatalogPage.DetailsTabDescriptionTxt); } }
        public IWebElement DetailsTabSaveBtn { get { return this.FindElement(Bys.AddCatalogPage.DetailsTabSaveBtn); } }

        public IWebElement DetailsTabCancelBtn { get { return this.FindElement(Bys.AddCatalogPage.DetailsTabCancelBtn); } }


        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            try
            {
                this.WaitUntil(TimeSpan.FromSeconds(180), Criteria.AddCatalogPage.PageReady);
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
            this.WaitUntil(TimeSpan.FromSeconds(180), Criteria.AddCatalogPage.PageReady);
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
            if (Browser.Exists(Bys.AddCatalogPage.DetailsTabCancelBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == DetailsTabCancelBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    CatalogsPage page = new CatalogsPage(Browser);
                    page.WaitForInitialize();

                    return page;
                }
            }  

            else
            {
                throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, " +
                    "or if the button is already added, then the page you were on did not contain the button.");
            }

            return null;
        }

        #endregion methods: page specific

    }


}
