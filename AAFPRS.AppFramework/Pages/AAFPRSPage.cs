using Browser.Core.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using Microsoft.CSharp;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using System;
using OpenQA.Selenium.Interactions;

namespace AAFPRS.AppFramework
{
    public abstract class AAFPRSPage : Page
    {
        #region Constructors

        public AAFPRSPage(IWebDriver driver): base(driver){}

        #endregion

        #region Elements


        // Menu Items
        public IWebElement Menu_Home { get { return this.FindElement(Bys.AAFPRSPage.Menu_Home); } }
        public IWebElement Menu_Community { get { return this.FindElement(Bys.AAFPRSPage.Menu_Community); } }
        public IWebElement Menu_MyAccount { get { return this.FindElement(Bys.AAFPRSPage.Menu_MyAccount); } }
        public IWebElement Menu_ContactUs { get { return this.FindElement(Bys.AAFPRSPage.Menu_ContactUs); } }
        public IWebElement Menu_Support { get { return this.FindElement(Bys.AAFPRSPage.Menu_Support); } }

        #endregion Elements

        #region methods: page specific

  

        ///// <summary>
        ///// Clicks the user-specified button or link and then waits for a window to close or open, or a page to load,
        ///// depending on the button that was clicked
        ///// </summary>
        ///// <param name="buttonOrLinkElem">The element to click on</param>
        //public void ClickButtonOrLinkToAdvance(IWebElement buttonOrLinkElem)
        //{
        //    // Error handler to make sure that the button that the tester passed in the parameter is actually on the page
        //    if (Browser.Exists(Bys.RCPPage.LogoutLnk))
        //    {
        //        if (buttonOrLinkElem.GetAttribute("outerHTML") == LogoutLnk.GetAttribute("outerHTML"))
        //        {
        //            LogoutLnk.Click();
        //            new WebDriverWait(Browser, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.UrlContains("login"));
        //            return;
        //        }
        //    }

        //    else
        //    {
        //        throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, or if the button is already added, then the page you were on did not contain the button.");
        //    }
        //}


        #endregion methods: page specific
    }
}