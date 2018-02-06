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
    public class CurriculumCoursePage : AMAPage, IDisposable
    {
        #region constructors
        public CurriculumCoursePage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that I start so I can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "gme-competency/admin/curriculumtemplates/course"; } }
        //?

        #endregion properties

        #region elements

        // Main page
        public IWebElement CurrulumCourseHeader { get { return this.FindElement(Bys.CurriculumCoursePage.CurrulumCourseHeaderLbl); } }

        public IWebElement CurriculumNameTxt { get { return this.FindElement(Bys.CurriculumCoursePage.CurriculumNameTxt); } }

        public IWebElement ChosenCoursesTbl { get { return this.FindElement(Bys.CurriculumCoursePage.ChosenCoursesTbl); } }

        public IWebElement AvailableCoursesTbl { get { return this.FindElement(Bys.CurriculumCoursePage.AvailableCoursesTbl); } }

        public IWebElement AddSelectedBtn { get { return this.FindElement(Bys.CurriculumCoursePage.AddSelectedBtn); } }

        public IWebElement RemoveSelectedBtn { get { return this.FindElement(Bys.CurriculumCoursePage.RemoveSelectedBtn); } }

        public IWebElement CancelBtn { get { return this.FindElement(Bys.CurriculumCoursePage.CancelBtn); } }

        public IWebElement SaveFinishLaterBtn { get { return this.FindElement(Bys.CurriculumCoursePage.SaveFinishLaterBtn); } }

        public IWebElement NextBtn { get { return this.FindElement(Bys.CurriculumCoursePage.NextBtn); } }

        public IWebElement CurrentDateWarningLbl { get { return this.FindElement(Bys.CurriculumCoursePage.CurrentDateWarningLbl); } }

        public IWebElement GreaterDateWarningLbl { get { return this.FindElement(Bys.CurriculumCoursePage.GreaterDateWarningLbl); } }

        #endregion elements

        #region methods: per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(300), Criteria.CurriculumCoursePage.PageReady);
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
            if (Browser.Exists(Bys.CurriculumCoursePage.NextBtn))
            {           
                if (buttonOrLinkElem.GetAttribute("outerHTML") == NextBtn.GetAttribute("outerHTML"))
                {
                   // ElemSet.ScrollToElement(Browser, buttonOrLinkElem);
                    NextBtn.Click();
                    //Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);                   
                    //Browser.WaitForElement(Bys.PGYAssignmentPage.UltimateTbl, TimeSpan.FromSeconds(120), ElementCriteria.IsEnabled,ElementCriteria.IsVisible);                   
                    // Browser.SwitchTo().Frame(G.EnterACPDFrame);
                    PGYAssignmentPage PGYAP = new PGYAssignmentPage(Browser);
                    PGYAP.WaitForInitialize();
                    return PGYAP;
                }
            }
          
            else
            {
                throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, or if the button is already added, then the page you were on did not contain the button.");
            }

            return null;
        }



        /// <summary>
        ///  Adding or Removing programs for Curriculum
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="ButtonToAddOrRemove"></param>
        /// <param name="indexes"></param>
        /// Removed Scroll temperarly cause of header issue;
        public  void AddOrRemoveCourses(IWebElement tableName,IWebElement ButtonToAddOrRemove, params int[] indexes)
        {
            Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);
            foreach (var index in indexes)
            {
                Browser.WaitForElement(Bys.CurriculumCoursePage.AvailableCoursesTbl, ElementCriteria.IsEnabled);
               // ElemSet.ScrollToElement(Browser, AvailableCoursesTbl);
                Thread.Sleep(0500);
               // ElemSet.ScrollToElement(Browser, Browser.FindElement(By.XPath($"(//*[@ng-model='row.isSelected'])[{index}]")));
                Thread.Sleep(0500);
                Browser.FindElement(By.XPath($"(//*[@ng-model='row.isSelected'])[{index}]")).Click();
                Thread.Sleep(0500);
               // ElemSet.ScrollToElement(Browser, ButtonToAddOrRemove);
                ButtonToAddOrRemove.Click();
                Browser.WaitForElement(Bys.AMAPage.LoadIcon, TimeSpan.FromSeconds(60), ElementCriteria.IsNotVisible);
              
            }
        }

        /// <summary>
        /// Setting end date to sort calendars 
        /// </summary>
        /// <param name="monthtoAdd"></param>
        /// <returns>date what we set calendar on stirng format</returns>
        public string SetEndDatForProgramm(int monthtoAdd)
        {
            AssignProgramPage ProgramPage = new AssignProgramPage(Browser);
            string assignedDay=ProgramPage.ChoosingEndDate(monthtoAdd,"MM/dd/yyyy");
            return assignedDay;
        }

        public List<string> GetTheNamesChoosenCourses()
        {
           List <string> CourseNames = new List<string>(Browser.FindElements(By.XPath("//*[@id='gridCurriculumTemplateChosenCourses']//a")).Select(iw => iw.Text));
           return CourseNames;
        }
      
        #endregion methods: page specific

    }
}