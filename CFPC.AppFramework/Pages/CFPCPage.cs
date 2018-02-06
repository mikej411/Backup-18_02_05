﻿using Browser.Core.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using Microsoft.CSharp;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using System;
using OpenQA.Selenium.Interactions;

namespace CFPC.AppFramework
{
    public abstract class CFPCPage : Page
    {
        #region Constructors

        public CFPCPage(IWebDriver driver) : base(driver) { }

        #endregion

        #region Elements


        // Menu Items
        public IWebElement Menu_About { get { return this.FindElement(Bys.CFPCPage.Menu_MyDashboard); } }
        public IWebElement Menu_MyCPDActivitiesList { get { return this.FindElement(Bys.CFPCPage.Menu_MyCPDActivitiesList); } }
        public IWebElement LoadIcon { get { return this.FindElement(Bys.CFPCPage.LoadIcon); } }
        public IWebElement LogoutLnk { get { return this.FindElement(Bys.CFPCPage.LogoutLnk); } }
        public IWebElement DashboardTab { get { return this.FindElement(Bys.CFPCPage.DashboardTab); } }
        public IWebElement CreditSummaryTab { get { return this.FindElement(Bys.CFPCPage.CreditSummaryTab); } }
        public IWebElement HoldingAreaTab { get { return this.FindElement(Bys.CFPCPage.HoldingAreaTab); } }
        public IWebElement CPDActivitiesTab { get { return this.FindElement(Bys.CFPCPage.CPDActivitiesTab); } }
        public IWebElement CPDPlanningTab { get { return this.FindElement(Bys.CFPCPage.CPDPlanningTab); } }
        public IWebElement ReportsTab { get { return this.FindElement(Bys.CFPCPage.ReportsTab); } }

        #endregion Elements

        #region Methods

        /// <summary>
        /// Navigates to a given page through a single or multi-layered menu system
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="menuItems">If it is a single-layered menu, then only 1 By type will be needed. If it is multi-layered, the tester should
        /// pass the By types in the order they would click them manually</param>
        /// <returns>The page object, which contains all elements of the page and any page related methods</returns>
        public static dynamic NavigateThroughMenuItems(IWebDriver browser, params By[] menuItems) //By menu1, By menu2 = null, By menu3 = null,
        {
            if (menuItems.Length == 1)
            {
                IWebElement elemToClick = browser.FindElement(menuItems[0]);
                elemToClick.Click();
            }

            else
            {
                for (int i = 0; i < menuItems.Length - 1; i++)
                {
                    WebDriverWait wait = new WebDriverWait(browser, TimeSpan.FromSeconds(1));
                    IWebElement elemToHover = wait.Until(ExpectedConditions.ElementIsVisible(menuItems[0]));
                    Actions action = new Actions(browser);
                    action.MoveToElement(elemToHover).Perform();
                }

                IWebElement elemtToClick = browser.FindElement(menuItems[menuItems.Length - 1]);
                elemtToClick.Click();
            }

            if (browser.Url.Contains("CPDActivities.aspx"))
            {
                //var APPage = new ActivitiesListPage(browser);
                //APPage.WaitForInitialize();
                //return APPage;
            }


            //if (browser.Url.Contains("About Me"))
            //{
            //    var aboutPage = new AboutPage(browser);
            //    aboutPage.WaitForInitialize();
            //    return aboutPage;
            //}


            return null;
        }



        #endregion Methods
    }
}
