using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using LOG4NET = log4net.ILog;

namespace AMA.AppFramework
{
    public class LibraryPage : AMAPage, IDisposable
    {
        #region constructors
        public LibraryPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that I start so I can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "Education.aspx"; } } 
        //?

        #endregion properties

        #region elements

        // Main page
        public IWebElement ResetFiltersBtn { get { return this.FindElement(Bys.LibraryPage.ResetFiltersBtn); } }
        
       // public IWebElement AmaDropdownMenu { get { return this.FindElement(Bys.LibraryPage.AmaDropdownMenu); } }

      //  public IWebElement GcepLnk { get { return this.FindElement(Bys.LibraryPage.GcepLnk); } }


        #endregion elements

        #region methods: per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(300), Criteria.LibraryPage.PageReady);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            try { activeRequests.Clear(); }
            catch (Exception ex) { _log.ErrorFormat("Failed to dispose LibraryPge", activeRequests.Count, ex); }
        }

        #endregion methods: per page

        #region methods: page specific

        ///// <summary>
        ///// Clicks the user-specified button or link and then waits for a window to close or open, or a page to load,
        ///// depending on the button that was clicked
        ///// </summary>
        ///// <param name="buttonOrLinkElem">The element to click on</param>
        public dynamic ClickToAdvance(IWebElement buttonOrLinkElem)
        {
            if (Browser.Exists(Bys.LibraryPage.ResetFiltersBtn))
            {
                if (buttonOrLinkElem.GetAttribute("id") ==ResetFiltersBtn.GetAttribute("id"))
                {
                    //AmaDropdownMenu.Click();
                    Browser.WaitForElement(Bys.LibraryPage.ResetFiltersBtn, ElementCriteria.IsVisible);                  
                   
                   // Browser.SwitchTo().Frame(G.EnterACPDFrame);
                    return ResetFiltersBtn ;
                }
            }

            else
            {
                throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, or if the button is already added, then the page you were on did not contain the button.");
            }

            return null;
        }

        #endregion methods: page specific


    }
}