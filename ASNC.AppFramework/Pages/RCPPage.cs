using Browser.Core.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using Microsoft.CSharp;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using System;
using OpenQA.Selenium.Interactions;

namespace RCP.AppFramework
{
    public abstract class RCPPage : Page
    {
        #region Constructors

        public RCPPage(IWebDriver driver): base(driver){}

        #endregion

        #region Elements


        // Menu Items
        public IWebElement Menu_About { get { return this.FindElement(Bys.RCPPage.Menu_MyDashboard); } }
        public IWebElement Menu_MyCPDActivitiesList { get { return this.FindElement(Bys.RCPPage.Menu_MyCPDActivitiesList); } }
        public IWebElement LoadIcon { get { return this.FindElement(Bys.RCPPage.LoadIcon); } }
        public IWebElement LogoutLnk { get { return this.FindElement(Bys.RCPPage.LogoutLnk); } }
        public IWebElement TOSAcceptBtn { get { return this.FindElement(Bys.RCPPage.TOSAcceptBtn); } }

        #endregion Elements

        #region methods: page specific


        #endregion methods: page specific
    }
}