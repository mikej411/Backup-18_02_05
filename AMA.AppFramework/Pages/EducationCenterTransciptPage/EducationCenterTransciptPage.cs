using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using LOG4NET = log4net.ILog;

namespace AMA.AppFramework
{
    public class EducationCenterTransciptPage : AMAPage, IDisposable
    {
        #region constructors
        public EducationCenterTransciptPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "http://ama.releasecandidate-community360.net/transcript.aspx?ActivityGUID=650acb7c-f163-4315-aa71-3df61acacb05"; } }

        #endregion properties

        #region elements

        public IWebElement AddSelfRepaortedActivityLnk { get { return this.FindElement(Bys.EducationCenterTransciptPage.AddSelfRepaortedActivityLnk); } }
        public IWebElement FilterByDateBtn { get { return this.FindElement(Bys.EducationCenterTransciptPage.FilterByDateBtn); } }
        public IWebElement TranscriptcontrolTbl { get { return this.FindElement(Bys.EducationCenterTransciptPage.TranscriptcontrolTbl); } }
        public IWebElement FilterByBox { get { return this.FindElement(Bys.EducationCenterTransciptPage.FilterByTxt); } }
        public IWebElement TranscriptActivitiesBtn { get { return this.FindElement(Bys.EducationCenterTransciptPage.TranscriptActivitiesBtn); } }
        public IWebElement MyCoursesLnk { get { return this.FindElement(Bys.EducationCenterTransciptPage.MyCoursesLnk); } }
        //public IWebElement TranscriptLnk { get { return this.FindElement(Bys.EducationCenterPage.TranscriptLnk); } }
        public IWebElement FilterByBoxIcon { get { return this.FindElement(Bys.EducationCenterTransciptPage.FilterByBoxIcon); } }
            #endregion elements

        #region methods: per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(300), Criteria.EducationCenterTransciptPage.PageReady);
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
        public dynamic ClickButtonOrLinkToAdvance (IWebElement TranscriptActivitiesBtn)
        {
            if (Browser.Exists(Bys.EducationCenterTransciptPage.TranscriptActivitiesBtn))
            {
                if (TranscriptActivitiesBtn.GetAttribute("id") == TranscriptActivitiesBtn.GetAttribute("id"))
                {
                   


                    return new EducationCenterTransciptPage(Browser);
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