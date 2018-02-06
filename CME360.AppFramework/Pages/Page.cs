using Browser.Core.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using Microsoft.CSharp;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using System;
using OpenQA.Selenium.Interactions;
using CME.AppFramework.Constants;

namespace CME.AppFramework
{
    public abstract class Page : Browser.Core.Framework.Page
    {
        #region Constructors

        public Page(IWebDriver driver) : base(driver) { }

        #endregion

        #region Elements
        
        public IWebElement SearchBtn { get { return this.FindElement(Bys.Page.SearchBtn); } }
        public IWebElement RecentItemsTbl { get { return this.FindElement(Bys.Page.RecentItemsTbl); } }

        public IWebElement SearchTxt { get { return this.FindElement(Bys.Page.SearchTxt); } }
        public IWebElement ProjectsTab { get { return this.FindElement(Bys.Page.ProjectsTab); } }
        public IWebElement PlanningTab { get { return this.FindElement(Bys.Page.PlanningTab); } }
        public IWebElement DistributionTab { get { return this.FindElement(Bys.Page.DistributionTab); } }

        #endregion Elements

        #region methods: page specific

        /// <summary>
        /// Clicks the user-specified element that exists on the base page of cme, and then waits for a window to close or open,
        /// or a page to load, depending on the element that was clicked
        /// </summary>
        /// <param name="buttonOrLinkElem">The element to click on</param>
        public dynamic ClickAndWaitBasePage(IWebElement buttonOrLinkElem)
        {
            // Error handler to make sure that the button that the tester passed in the parameter is actually on the page
            if (Browser.Exists(Bys.Page.ProjectsTab))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == ProjectsTab.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    ProjectsPage page = new ProjectsPage(Browser);
                    page.WaitForInitialize();
                    return page;
                }
            }

            if (Browser.Exists(Bys.Page.DistributionTab))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == DistributionTab.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    DistributionPage page = new DistributionPage(Browser);
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


        public dynamic GoToRecentItem(CMEConstants.RecentItemCategory category, string itemText)
        {
            IWebElement link = RecentItemsTbl.FindElement(By.LinkText(itemText));

            switch (category)
            {
                case CMEConstants.RecentItemCategory.Activity:
                    link.Click();
                    ActivityMainPage page = new ActivityMainPage(Browser);
                    page.WaitForInitialize();
                    return page;


                case CMEConstants.RecentItemCategory.Project:
                    return null;
            }

            return null;
        }


        
        #endregion methods: page specific
    }
}