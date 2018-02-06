using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace AMA.AppFramework
{
    public class InstitutionsPage : AMAPage, IDisposable
    {
        #region constructors
        public InstitutionsPage(IWebDriver driver) : base(driver)
        {

        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that I start so I can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "gme-competency/admin/institutions"; } }
       

        #endregion properties

        #region elements

        // Main page
        public IWebElement InstitutionsTbl { get { return this.FindElement(Bys.InstitutionsPage.InstitutionsTbl); } }

        public IWebElement CreateInstitutionsLnk { get { return this.FindElement(Bys.InstitutionsPage.CreateInstitutionsLnk); } }

        public IWebElement CancelBtn { get { return this.FindElement(Bys.InstitutionsPage.CancelBtn); } }

        public IWebElement AcceptBtn { get { return this.FindElement(Bys.InstitutionsPage.AcceptBtn); } }

        public IWebElement DismissBtn { get { return this.FindElement(Bys.InstitutionsPage.DismissBtn); } }

        public IWebElement EditInstitutionLnk { get { return this.FindElement(Bys.InstitutionsPage.EditInstitutionLnk); } }
        public IWebElement MarkInactiveLnk { get { return this.FindElement(Bys.InstitutionsPage.MarkInactiveLnk); } }
        public IWebElement CreateProgramLnk { get { return this.FindElement(Bys.InstitutionsPage.CreateProgramLnk); } }
        public IWebElement ActionGearBtn { get { return this.FindElement(Bys.InstitutionsPage.ActionGearBtn); } }



        #endregion elements

        #region methods: per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(300), Criteria.InstitutionsPage.PageReady);
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

        /// <summary>
        /// Clicks the user-specified button or link and then waits for a window to close or open, or a page to load,
        /// depending on the button that was clicked
        /// </summary>
        /// <param name="buttonOrLinkElem">The element to click on</param>
        public dynamic ClickToAdvance(IWebElement buttonOrLinkElem)
        {
            if (Browser.Exists(Bys.InstitutionsPage.EditInstitutionLnk))
            {
                if (buttonOrLinkElem.GetAttribute("id") == EditInstitutionLnk.GetAttribute("id"))
                {                  
                    EditInstitutionLnk.Click();
                    EditInstitutionPage EIP = new EditInstitutionPage(Browser);
                    EIP.WaitForInitialize();
                    return EIP;
                }
            }
            if (Browser.Exists(Bys.AMAPage.AdministrationLnk))
            {
                if (buttonOrLinkElem.GetAttribute("id") == GMECompetencyEducationProgramLnk.GetAttribute("id"))  // AdministrationLnk.GetAttribute("outerHTML"))
                {
                    GMECompetencyEducationProgramLnk.Click();   // AdministrationLnk.Click();
                    GCEPPage GP = new GCEPPage(Browser);
                    GP.WaitForInitialize();
                    return GP;
                }
            }
            else
            {
                throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, or if the button is already added, then the page you were on did not contain the button.");
            }

            return null;
        }


        public dynamic SearchforInstitutions(string InstitutionName)
        {
            //try
            //{
                this.WaitUntilAll(Criteria.InstitutionsPage.LoadIconAppear, Criteria.InstitutionsPage.InstitutionTableVisible);
                Search(InstitutionName);
                Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);
                Browser.FindElement(By.LinkText(InstitutionName)).Click();
                Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);
                return new InstitutionsGCEPPage(Browser);
            //} use commented line if Institution name will change
            //catch 
            //{
            //    Browser.FindElement(By.XPath(" //div[@class='ui-grid-cell-hide-overflow ng-scope']/a")).Click();
            //    Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);
            //    return new InstitutionsGCEPPage(Browser);
            //}

        }

        /// <summary>
        /// sgfsfs
        /// </summary>
        /// <param name="topLevelMenuId"></param>
        /// <param name="subMenuLinkText"></param>
        public void Select(string topLevelMenuId, string subMenuLinkText)
        {
            try
            {
                // var wait = new WebDriverWait(D.Instance, TimeSpan.FromSeconds(ConfigSettings.WebDriverWait));
                // WebDriverWait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(@class, " + topLevelMenuId + ")]//button[contains(@class, 'ins-admin-btn-cp')]")));
                Browser.WaitForElement(Bys.InstitutionsPage.InstitutionsTbl, ElementCriteria.IsVisible);
                new WebDriverWait(Browser, TimeSpan.FromSeconds(25)).Until(ExpectedConditions.ElementToBeClickable(ActionGearBtn));

                var toplevelMenuList = Browser.FindElements(By.XPath("//div[contains(@class, " + topLevelMenuId + ")]//button[contains(@class, 'ins-admin-btn-cp')]")).ToList();
                if (toplevelMenuList.Any())
                {
                    toplevelMenuList.FirstOrDefault().Click();
                }

                Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);

                List<IWebElement> subLevelMenuLinks = Browser.FindElements(By.XPath("//a/span[.='" + subMenuLinkText + "']/..")).Where(g => g.Displayed && g.Enabled).ToList();
                if (subLevelMenuLinks.Any())
                {
                    subLevelMenuLinks.FirstOrDefault().Click();
                    Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);
                    
                }
               
            }
            catch (NoSuchElementException)
            {
                List<IWebElement> subLevelMenuLinks = Browser.FindElements(By.XPath("//a/span[.='" + subMenuLinkText + "']/..")).Where(g => g.Displayed && g.Enabled).ToList();
                subLevelMenuLinks.FirstOrDefault().Click();
            }

        }


        //public void FromGearButton (string topMenuLinkText)
        //{
        //    ActionGearBtn.Click();
        //    Thread.Sleep(0500);
        //    List<IWebElement> subLevelMenuLinks = Browser.FindElements(By.XPath("//a/span[.='" + topMenuLinkText + "']/.."));
            

        //} 


        #endregion methods: page specific
     }
 }