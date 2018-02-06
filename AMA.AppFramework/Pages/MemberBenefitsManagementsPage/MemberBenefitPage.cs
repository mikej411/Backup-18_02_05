using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace AMA.AppFramework
{
    public class MemberBenefitPage : AMAPage, IDisposable
    {
        #region constructors
        public MemberBenefitPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "https://ama.releasecandidate-community360.net/gme-competency/library#lnk4649629"; } }

        #endregion properties

        #region elements

        public IWebElement MemberBenefitsManagementLbl { get { return this.FindElement(Bys.MemberBenefitPage.MemberBenefitsManagementLbl); } }
        public IWebElement TitleTxt { get { return this.FindElement(Bys.MemberBenefitPage.TitleTxt); } }       
        public IWebElement URLTxt { get { return this.FindElement(Bys.MemberBenefitPage.URLTxt); } }
        public IWebElement ChooseFileBtn { get { return this.FindElement(Bys.MemberBenefitPage.ChooseFileBtn); } }
        public IWebElement ClearBtn { get { return this.FindElement(Bys.MemberBenefitPage.ClearBtn); } }
        public IWebElement SaveBtn { get { return this.FindElement(Bys.MemberBenefitPage.SaveBtn); } }
        public IWebElement PublishBtn { get { return this.FindElement(Bys.MemberBenefitPage.PublishBtn); } }
        public IWebElement MembershipFormBrowseHiddenBtn { get { return this.FindElement(Bys.MemberBenefitPage.MembershipFormBrowseHiddenBtn); } }
        public IWebElement AcceptBtn { get { return this.FindElement(Bys.MemberBenefitPage.AcceptBtn); } }



        #endregion elements

        #region methods: per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(300), Criteria.MemberBenefitPage.PageReady);
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
            if (Browser.Exists(Bys.AMAPage.GMECompetencyEducationProgramLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == GMECompetencyEducationProgramLnk.GetAttribute("outerHTML"))
                {
                    GMECompetencyEducationProgramLnk.Click();
                    GCEPPage GP = new GCEPPage(Browser);
                    GP.WaitForInitialize();
                    return GP;
                    
                }
            }
            // if (Browser.Exists(Bys.GCEPLibraryPage.BeginCourseBtn))
            // {
            //      if (buttonOrLinkElem.GetAttribute("outerHTML") == BeginCourseBtn.GetAttribute("outerHTML"))
            //      {
            //            BeginCourseBtn.Click();                    
            //            return new CourseTestPage(Browser);
            //      }
            // }

            //if (Browser.Exists(Bys.GCEPLibraryPage.TranscriptLnk))
            //{
            //    if (buttonOrLinkElem.GetAttribute("outerHTML") == TranscriptLnk.GetAttribute("outerHTML"))
            //    {
            //        TranscriptLnk.Click();
            //         Browser.WaitForElement(Bys.GCEPTranscriptPage.CompletedTestTbl, TimeSpan.FromSeconds(60), ElementCriteria.IsVisible, ElementCriteria.IsEnabled);

            //        return new GCEPTranscriptPage(Browser);
            //    }
            //}
            else
            {

                throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, or if the button is already added, then the page you were on did not contain the button.");

            }


            return null;
        }

       
        #endregion methods: page specific



    }
}


