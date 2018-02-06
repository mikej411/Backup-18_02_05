using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace NOF.AppFramework
{
    public class CurriculumPage : NOFPage, IDisposable
    {
        #region constructors
        public CurriculumPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "login.aspx?action=enablelogin"; } }

        #endregion properties

        #region elements

        public IWebElement MyTranscriptLink { get { return this.FindElement(Bys.CurriculumPage.MyTranscriptLink); } }
        public IWebElement HomeLnk { get { return this.FindElement(Bys.CurriculumPage.HomeLnk); } }
        public IWebElement CurriculumTab { get { return this.FindElement(Bys.CurriculumPage.CurriculumTab); } }
        public IWebElement TranscriptTab { get { return this.FindElement(Bys.CurriculumPage.TranscriptTab); } }
        public IWebElement CurriculumLbl { get { return this.FindElement(Bys.CurriculumPage.CurriculumLbl); } }
        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(180), Criteria.CurriculumPage.PageReady);
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

        #endregion methods: repeated per page

        #region methods: wrappers
        /// <summary>
        /// Clicks the user-specified element and then waits for a window to close or open, or a page to load,
        /// depending on the element that was clicked
        /// </summary>
        /// <param name="buttonOrLinkElem">The button element</param>
        public dynamic ClickAndWait(IWebElement buttonOrLinkElem)
        {
            // Error handler to make sure that the button that the tester passed in the parameter is actually on the page
            if (Browser.Exists(Bys.CurriculumPage.HomeLnk))
            {
                if (buttonOrLinkElem.GetAttribute("href") == HomeLnk.GetAttribute("href"))
                {
                    HomeLnk.Click();
                    HomePage HP = new HomePage(Browser);
                    return HP;
                }
            }

            //if (Browser.Exists(Bys.TraineePage.SaveChangesBtn))
            //{
            //    // This is a workaround to be able to use an IF statement on an IWebElement type.
            //    if (buttonOrLinkElem.GetAttribute("outerHTML") == SaveChangesBtn.GetAttribute("outerHTML"))
            //    {
            //        buttonOrLinkElem.Click();
            //        this.WaitUntilAll(Criteria.TraineePage.SaveChangesButtonNotVisible);
            //        return;
            //    }
            //}

            else
            {
                throw new Exception("No button or link was found with your passed parameter");
            }

            return null;
        }






        ///// <summary>
        ///// Clicks the user-specified button or link and then waits for a window to close or open, or a page to load,
        ///// depending on the button that was clicked
        ///// </summary>
        ///// <param name="buttonOrLinkElem">The button element</param>
        //public MyTranscriptPage ClickToAdvance(IWebElement buttonOrLinkElem)
        //{

        //    if (Browser.Exists(Bys.MyCurriculumPage.MyTranscriptLink))
        //    {
        //        if (buttonOrLinkElem.GetAttribute("href") == MyTranscriptLink.GetAttribute("href"))
        //        {
        //            MyTranscriptLink.Click();
        //            MyTranscriptPage TP = new MyTranscriptPage(Browser);
        //            return TP;
        //        }
        //    }

        //    else
        //    {
        //        throw new Exception("No button or link was found with your passed parameter");
        //    }

        //    return null;
        //}

        #endregion wrappers
    }







}
