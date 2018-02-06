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
    public class EditInstitutionPage : AMAPage, IDisposable
    {
        #region constructors
        public EditInstitutionPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "/update"; } }//change this

        #endregion properties

        #region elements

        public IWebElement CreateInstitutionLbl { get { return this.FindElement(Bys.EditInstitutionPage.CreateInstitutionLbl); } }
        public IWebElement InstitutionDetailsLbl { get { return this.FindElement(Bys.EditInstitutionPage.InstitutionDetailsLbl); } }
        public IWebElement InstitutionIdTxt { get { return this.FindElement(Bys.EditInstitutionPage.InstitutionIdTxt); } }
        public IWebElement InstitutionNameTxt { get { return this.FindElement(Bys.EditInstitutionPage.InstitutionNameTxt); } }
        public IWebElement InstitutionDetailsChooseFileBtn { get { return this.FindElement(Bys.EditInstitutionPage.InstitutionDetailsChooseFileBtn); } }
        public IWebElement InstitutionPrimaryContactLbl { get { return this.FindElement(Bys.EditInstitutionPage.InstitutionPrimaryContactLbl); } }
        public IWebElement InstitutionPrimaryContactNameTxt { get { return this.FindElement(Bys.EditInstitutionPage.InstitutionPrimaryContactNameTxt); } }
        public IWebElement InstitutionPrimaryContactPhoneTxt{ get { return this.FindElement(Bys.EditInstitutionPage.InstitutionPrimaryContactPhoneTxt); } }
        public IWebElement InstitutionPrimaryContactEmailTxt { get { return this.FindElement(Bys.EditInstitutionPage.InstitutionPrimaryContactEmailTxt); } }
        public IWebElement InstitutionCertificateSignatureLbl { get { return this.FindElement(Bys.EditInstitutionPage.InstitutionCertificateSignatureLbl); } }
        public IWebElement InstitutionCancelBtn { get { return this.FindElement(Bys.EditInstitutionPage.InstitutionCancelBtn); } }
        public IWebElement InstitutionSaveBtn { get { return this.FindElement(Bys.EditInstitutionPage.InstitutionSaveBtn); } }


        #endregion elements

        #region methods: per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(300), Criteria.EditInstitutionPage.PageReady);
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

        #endregion methods: per page

        #region methods: page specific

        /// <summary>
        /// Clicks the user-specified button or link and then waits for a window to close or open, or a page to load,
        /// depending on the button that was clicked
        /// </summary>
        /// <param name="buttonOrLinkElem">The element to click on</param>
        public dynamic ClickToAdvance(IWebElement buttonOrLinkElem)
        {
            if (Browser.Exists(Bys.EditInstitutionPage.InstitutionSaveBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == InstitutionSaveBtn.GetAttribute("outerHTML"))
                {
                    ElemSet.ScrollToElement(Browser, InstitutionSaveBtn);
                    Thread.Sleep(0500);
                    InstitutionSaveBtn.Click();
                    // Browser.WaitForElement(Bys.EducationCenterPage.MyCoursesTtl, TimeSpan.FromSeconds(60), ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
                    //Browser.WaitForElement(Bys.EducationCenterPage.GcepLnk, TimeSpan.FromSeconds(60), ElementCriteria.IsEnabled);
                    //new WebDriverWait(Browser, TimeSpan.FromSeconds(90)).Until(ExpectedConditions.UrlContains("Courses.aspx"));

                    InstitutionsPage IP = new InstitutionsPage(Browser);
                    IP.WaitForInitialize();
                    return IP;
                }
            }
            if (Browser.Exists(Bys.EditInstitutionPage.InstitutionCancelBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == InstitutionCancelBtn.GetAttribute("outerHTML"))
                {
                    ElemSet.ScrollToElement(Browser, InstitutionCancelBtn);
                    Thread.Sleep(0500);
                    InstitutionCancelBtn.Click();
                    // Browser.WaitForElement(Bys.EducationCenterPage.MyCoursesTtl, TimeSpan.FromSeconds(60), ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
                    //Browser.WaitForElement(Bys.EducationCenterPage.GcepLnk, TimeSpan.FromSeconds(60), ElementCriteria.IsEnabled);
                    //new WebDriverWait(Browser, TimeSpan.FromSeconds(90)).Until(ExpectedConditions.UrlContains("Courses.aspx"));

                    InstitutionsPage IP = new InstitutionsPage(Browser);
                    IP.WaitForInitialize();
                    return IP;
                }
            }
            if (Browser.Exists(Bys.AMAPage.HelpLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == HelpLnk.GetAttribute("outerHTML"))
                {
                    Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);
                    HelpLnk.SendKeys(Keys.Tab);
                    HelpLnk.Click();
                    HelpPage HP = new HelpPage(Browser);
                    HP.WaitForInitialize();
                    return HP;
                }
            }
            if (Browser.Exists(Bys.AMAPage.HelpfromYourInstitutionLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == HelpfromYourInstitutionLnk.GetAttribute("outerHTML"))
                {
                    Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);
                    HeaderMenuDropDown.Click();
                    HelpfromYourInstitutionLnk.SendKeys(Keys.Tab);
                    HelpfromYourInstitutionLnk.Click();                  
                    Browser.Manage().Window.Maximize();                  
                   
                    Browser.SwitchTo().Window(Browser.WindowHandles.Last());
                    Browser.Manage().Window.Maximize();
                    HelpPage HP = new HelpPage(Browser);
                    HP.WaitForInitialize();
                    return HP;
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


