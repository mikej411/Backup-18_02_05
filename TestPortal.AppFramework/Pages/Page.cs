using Browser.Core.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using Microsoft.CSharp;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using System;
using OpenQA.Selenium.Interactions;

namespace TP.AppFramework
{
    public abstract class Page : Browser.Core.Framework.Page
    {
        #region Constructors

        public Page(IWebDriver driver) : base(driver) { }

        #endregion

        #region Elements       
        public IWebElement LogoutLnk { get { return this.FindElement(Bys.Page.LogoutLnk); } }
        public IWebElement HomeTab { get { return this.FindElement(Bys.Page.HomeTab); } }

        #endregion Elements

        #region methods: page specific

        ///// <summary>
        ///// Clicks the user-specified element that exists on every page of RCP, and then waits for a window to close or open,
        ///// or a page to load, depending on the element that was clicked
        ///// </summary>
        ///// <param name="buttonOrLinkElem">The element to click on</param>
        //public dynamic ClickAndWaitBasePage(IWebElement buttonOrLinkElem)
        //{
        //    // Error handler to make sure that the button that the tester passed in the parameter is actually on the page
        //    if (Browser.Exists(Bys.Page.AllParticipantsLnk))
        //    {
        //        if (buttonOrLinkElem.GetAttribute("outerHTML") == AllParticipantsLnk.GetAttribute("outerHTML"))
        //        {
        //            buttonOrLinkElem.Click();
        //            SearchPage page = new SearchPage(Browser);
        //            page.WaitUntil(Criteria.SearchPage.AllParticpantsTblBodyNotLoading);
        //            return page;
        //        }
        //    }

        //    else
        //    {
        //        throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, " +
        //            "or if the button is already added, then the page you were on did not contain the button.");
        //    }

        //    return null;
        //}











        #endregion methods: page specific
    }
}