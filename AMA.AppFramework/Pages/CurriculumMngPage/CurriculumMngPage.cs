using Browser.Core.Framework;
using Browser.Core.Framework.Data;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace AMA.AppFramework
{
    public class CurriculumMngPage : AMAPage, IDisposable
    {
        #region constructors
        public CurriculumMngPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that I start so I can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "gme-competency/admin/curriculumtemplates"; } } 
        //?

        #endregion properties

        #region elements

        // Main page
        public IWebElement CurrulumTemplatesHeader { get { return this.FindElement(Bys.CurriculumMngPage.CurrulumTemplatesHeader); } }      

        public IWebElement CreateCurriculumTemplateBtn { get { return this.FindElement(Bys.CurriculumMngPage.CreateCurriculumTemplateBtn); } }

        public IWebElement CurriculumTemplateTbl { get { return this.FindElement(Bys.CurriculumMngPage.CurriculumTemplateTbl); } }

        public IWebElement AcceptBtn { get { return this.FindElement(Bys.CurriculumMngPage.AcceptBtn); } }

        public IWebElement NoRecordLabel { get { return this.FindElement(Bys.CurriculumMngPage.NoRecordLbl); } }

        public IWebElement Actioncell { get { return this.FindElement(Bys.CurriculumMngPage.Actioncell); } }

        public IWebElement Deletecell { get { return this.FindElement(Bys.CurriculumMngPage.Deletecell); } }

        public IWebElement Editcell { get { return this.FindElement(Bys.CurriculumMngPage.Editcell); } }

        public IWebElement SpecificCurriculumTbl { get { return this.FindElement(Bys.CurriculumMngPage.SpecificCurriculumTbl); } }

        public IWebElement CurriculumName { get { return this.FindElement(Bys.CurriculumMngPage.CurriculumNameLbl); } }

        public IWebElement CurriculumWinClose { get { return this.FindElement(Bys.CurriculumMngPage.CurriculumWinCloseBtn); } }

        public IWebElement AssignToProgrammLnk { get { return this.FindElement(Bys.CurriculumMngPage.AssignToProgrammLnk); } }

        public IWebElement CountTableItemLbl { get { return this.FindElement(Bys.CurriculumMngPage.CountTableItemLbl); } }




        //  public IWebElement GcepLnk { get { return this.FindElement(Bys.LibraryPage.GcepLnk); } }


        #endregion elements

        #region methods: per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(300), Criteria.CurriculumMngPage.PageReady);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            try { activeRequests.Clear(); }
            catch (Exception ex) { _log.ErrorFormat("Failed to dispose GCEEPUserMngPage", activeRequests.Count, ex); }
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
            if (Browser.Exists(Bys.AMAPage.SignOutLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == SignOutLnk.GetAttribute("outerHTML"))
                {
                    Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);
                    HeaderMenuDropDown.Click();
                    SignOutLnk.SendKeys(Keys.Tab);
                    SignOutLnk.Click();
                    //Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);

                    // return  new CurriculumMngPage(Browser);
                   // new WebDriverWait(Browser, TimeSpan.FromSeconds(115)).Until(ExpectedConditions.UrlMatches("https://logintest.ama-assn.org/account/logout"));
                    return this.Browser;
                }
            }

            if (Browser.Exists(Bys.CurriculumMngPage.CurrulumTemplatesHeader))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == CreateCurriculumTemplateBtn.GetAttribute("outerHTML"))
                {
                  Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);
                   // new WebDriverWait(Browser, TimeSpan.FromSeconds(15)).Until(ExpectedConditions.UrlContains("gme-competency/admin/curriculumtemplates"));
                    CreateCurriculumTemplateBtn.Click();
                    //Thread.Sleep(3000);
                    //Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsEnabled);
                    Browser.WaitForElement(Bys.CurriculumCoursePage.AvailableCoursesTbl, ElementCriteria.IsVisible);
                    //  new WebDriverWait(Browser, TimeSpan.FromSeconds(35)).Until(ExpectedConditions.UrlContains("gme-competency/admin/curriculumtemplates/course"));

                    CurriculumCoursePage CCP = new CurriculumCoursePage(Browser);
                    CCP.WaitForInitialize();
                    return CCP;
                }
            }
           
            if (Browser.Exists(Bys.CurriculumMngPage.AssignToProgrammLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AssignToProgrammLnk.GetAttribute("outerHTML"))
                {
                    // Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);
                    //  new WebDriverWait(Browser, TimeSpan.FromSeconds(15)).Until(ExpectedConditions.UrlContains("gme-competency/admin/curriculumtemplates"));
                   // CurriculumMngPage.Actioncell.Click();
                    AssignToProgrammLnk.Click();                
                    Browser.WaitForElement(Bys.AssignProgramPage.StartDateCalenderBox, TimeSpan.FromSeconds(240), ElementCriteria.IsVisible);
                    // new WebDriverWait(Browser, TimeSpan.FromSeconds(35)).Until(ExpectedConditions.UrlContains("gme-competency/admin/curriculumtemplates/curriculumprogramtimeframe"));

                    // Browser.SwitchTo().Frame(G.EnterACPDFrame);
                    AssignProgramPage APP = new AssignProgramPage(Browser);
                    APP.WaitForInitialize();
                    return APP;
                }
            }
            if (Browser.Exists(Bys.AMAPage.AdministrationLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == GMECompetencyEducationProgramLnk.GetAttribute("outerHTML")) //AdministrationLnk.GetAttribute("outerHTML"))
                {
                    GMECompetencyEducationProgramLnk.Click();     // AdministrationLnk.Click();
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
             
    
      
        ///// <summary>
        ///// Deleting  Curriculum       
        ///// </summary>
        ///// <param string curriculumName= The name of curriculum to  perform delete action></param>

        public void DeleteCurriculum(string curriculumName)
        {
          
                if (Browser.FindElements(Bys.CurriculumMngPage.NoRecordLbl).Count > 0)
                {

                }
                else
                {
                    do
                    {
                        Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);
                        Browser.WaitForElement(Bys.CurriculumMngPage.CurriculumTemplateTbl, ElementCriteria.IsVisible);
                        Thread.Sleep(0500);
                        Actioncell.Click();
                        Deletecell.Click();
                        Thread.Sleep(0500);
                        AcceptBtn.Click();
                        Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);
                        Browser.WaitForElement(Bys.CurriculumMngPage.CurriculumTemplateTbl, ElementCriteria.IsVisible);
                    } while (Browser.FindElements(Bys.CurriculumMngPage.NoRecordLbl).Count <= 0);
                }    
        }


        #endregion methods: page specific
        ///// <summary>
        ///// will find curriculum with param and clicking edit     
        ///// </summary>
        ///// <param string curriculumName= The name of curriculum to edit action></param>
        public void EditCurriculum(string curriculumName)
        {
            Thread.Sleep(0500);
            Actioncell.Click();
            Thread.Sleep(0500);
            Editcell.Click();           
            Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);
            Browser.WaitForElement(Bys.CurriculumCoursePage.CurrulumCourseHeaderLbl, ElementCriteria.IsVisible);
            new WebDriverWait(Browser, TimeSpan.FromSeconds(35)).Until(ExpectedConditions.UrlContains("gme-competency/admin/curriculumtemplates"));
          
        }

        /// <summary>
        /// Returns the row count of any table
        /// </summary>
        /// <param gridElem="The driver instance"></param>
        public int GetCountofRow(IWebElement tableName)
        {
            int count;
            IList<IWebElement> allRows = tableName.FindElements(By.TagName("tr"));
            count = allRows.Count;
            return count;
        }

        public int GetCountOfCurriculumonTable(string firsSeparator,string secondSeparator)
        {
            WaitForInitialize();
            Browser.WaitForElement(Bys.GCEPUserMngPage.ActionGearBtn, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
            string CountOfUsersOnUserMngPage = CountTableItemLbl.Text;
            string[] separatingChars = { firsSeparator, secondSeparator };
            string[] s = CountOfUsersOnUserMngPage.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);
            int CountOfUsers = Convert.ToInt32(s[1]);
            return CountOfUsers;
        }


    }
}