﻿using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace AMA.AppFramework
{
    public class GCEPLibraryPage : AMAPage, IDisposable
    {
        #region constructors
        public GCEPLibraryPage(IWebDriver driver) : base(driver)
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

        public IWebElement BeginCourseBtn { get { return this.FindElement(Bys.GCEPLibraryPage.BeginCourseBtn); } }
        public IWebElement LibraryTxt { get { return this.FindElement(Bys.GCEPLibraryPage.LibraryLbl); } }       
        public IWebElement RememberMeChk { get { return this.FindElement(Bys.LoginPage.RememberMeChk); } }
        public IWebElement TranscriptLnk { get { return this.FindElement(Bys.GCEPLibraryPage.TranscriptLnk); } }
      
       
       
        #endregion elements

        #region methods: per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(300), Criteria.GCEPLibraryPage.PageReady);
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
            if (Browser.Exists(Bys.GCEPLibraryPage.SearchBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == SearchBtn.GetAttribute("outerHTML"))
                {
                    SearchBtn.Click();
                    // Browser.WaitForElement(Bys.EducationCenterPage.MyCoursesTtl, TimeSpan.FromSeconds(60), ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
                    //Browser.WaitForElement(Bys.EducationCenterPage.GcepLnk, TimeSpan.FromSeconds(60), ElementCriteria.IsEnabled);
                    new WebDriverWait(Browser, TimeSpan.FromSeconds(15)).Until(ExpectedConditions.UrlContains("Courses.aspx"));

                    return new EducationCenterPage(Browser);
                    // }
                }
            }
             if (Browser.Exists(Bys.GCEPLibraryPage.BeginCourseBtn))
             {
                  if (buttonOrLinkElem.GetAttribute("outerHTML") == BeginCourseBtn.GetAttribute("outerHTML"))
                  {
                        BeginCourseBtn.Click();                    
                        return new CourseTestPage(Browser);
                  }
             }

            if (Browser.Exists(Bys.GCEPLibraryPage.TranscriptLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == TranscriptLnk.GetAttribute("outerHTML"))
                {
                    TranscriptLnk.Click();
                     Browser.WaitForElement(Bys.GCEPTranscriptPage.CompletedTestTbl, TimeSpan.FromSeconds(60), ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
                
                    return new GCEPTranscriptPage(Browser);
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


