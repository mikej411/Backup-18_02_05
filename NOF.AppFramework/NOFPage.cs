using Browser.Core.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using Microsoft.CSharp;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using System;
using OpenQA.Selenium.Interactions;

namespace NOF.AppFramework
{
    public abstract class NOFPage : Page
    {
        #region Constructors

        public NOFPage(IWebDriver driver): base(driver){}

        #endregion

        #region Elements


        // Menu Items
        public IWebElement LoadIcon { get { return this.FindElement(Bys.NOFPage.LoadIcon); } }
        public IWebElement LogoutLnk { get { return this.FindElement(Bys.NOFPage.LogoutLnk); } }
        public IWebElement TOSAcceptBtn { get { return this.FindElement(Bys.NOFPage.TOSAcceptBtn); } }
        public IWebElement Menu_MyCmeLnk { get { return this.FindElement(Bys.NOFPage.Menu_MyCmeLnk); } }
        public IWebElement Menu_MyCME_Curriculum { get { return this.FindElement(Bys.NOFPage.Menu_MyCME_Curriculum); } }
        public IWebElement Menu_MyCME_TranscriptLnk { get { return this.FindElement(Bys.NOFPage.Menu_MyCME_TranscriptLnk); } }

        #endregion Elements

        #region methods: page specific

        /// <summary>
        /// Navigates to a given page through a single or multi-layered menu system
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="menuItems">If it is a single-layered menu, then only 1 By type will be needed. If it is multi-layered, the tester should
        /// pass the By types in the order they would click the menu items manually</param>
        /// <returns>The page object, which contains all elements of the page and any page related methods</returns>
        public dynamic NavigateThroughMenuItems(IWebDriver browser, params By[] menuItems) //By menu1, By menu2 = null, By menu3 = null,
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
                    IWebElement elemToClick = browser.WaitForElement(menuItems[0], ElementCriteria.IsVisible);
                    elemToClick.Click();
                }


            }

            IWebElement elemtToClick = browser.FindElement(menuItems[menuItems.Length - 1]);
            elemtToClick.Click();


            return null;
        }

        ///// <summary>
        ///// Clicks the user-specified element and then waits for a window to close or open, or a page to load,
        ///// depending on the element that was clicked
        ///// </summary>
        ///// <param name="buttonOrLinkElem">The element to click on</param>
        //public dynamic ClickAndWait(IWebElement buttonOrLinkElem)
        //{
        //    // Error handler to make sure that the button that the tester passed in the parameter is actually on the page
        //    if (Browser.Exists(Bys.CurriculumPage.HomeLnk))
        //    {
        //        if (buttonOrLinkElem.GetAttribute("href") == CurriculumLnk.GetAttribute("href"))
        //        {
        //            CurriculumLnk.Click();
        //            //Thread.Sleep(5000);
        //            //CurriculumLnk.Click();                   
        //            CurriculumPage CP = new CurriculumPage(Browser);
        //            return CP;
        //        }
        //    }


        //    if (Browser.Exists(Bys.NOFPage.MyCmeLnk))
        //    {
        //        // This is a workaround to be able to use an IF statement on an IWebElement type.
        //        if (buttonOrLinkElem.GetAttribute("outerHTML") == SaveChangesBtn.GetAttribute("outerHTML"))
        //        {
        //            buttonOrLinkElem.Click();
        //            this.WaitUntilAll(Criteria.TraineePage.SaveChangesButtonNotVisible);
        //            return;
        //        }
        //    }

        //    else
        //    {
        //        throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, or if the button is already added, then the page you were on did not contain the button.");
        //    }

        //    return null;
        //}

        #endregion methods: page specific
    }
}