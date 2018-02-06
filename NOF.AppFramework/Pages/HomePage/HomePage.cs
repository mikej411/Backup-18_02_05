using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace NOF.AppFramework
{
    public class HomePage : NOFPage, IDisposable
    {
        #region constructors
        public HomePage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return ""; } }

        #endregion properties

        #region elements
        public IWebElement SearchBtn { get { return this.FindElement(Bys.HomePage.SearchBtn); } }



        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(100), Criteria.HomePage.PageReady);
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

        #region methods: page specific
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




        ///// <summary>
        ///// After going to the home page, click on the Curriculum link under My CME link
        ///// </summary>
        ///// <param name="buttonOrLinkElem"></param>
        ///// <returns></returns>
        //public dynamic ClickOnCurriculumLink(IWebElement buttonOrLinkElem)
        //{
        //    MyCmeLnk.Click();
        //    Thread.Sleep(5000);
        //    CurriculumPage CP = ClickToAdvance(CurriculumLnk);
        //    return CP;
        //}


        ///// <summary>
        ///// Clicks the user-specified button or link and then waits for a window to close or open, or a page to load,
        ///// depending on the button that was clicked
        ///// </summary>
        ///// <param name = "buttonOrLinkElem" > The button element</param>
        //public TranscriptPage Click(IWebElement buttonOrLinkElem)
        //{

        //    if (Browser.Exists(Bys.HomePage.TranscriptLnk))
        //    {
        //        if (buttonOrLinkElem.GetAttribute("href") == TranscriptLnk.GetAttribute("href"))
        //        {
        //            TranscriptLnk.Click();
        //            //Thread.Sleep(5000);
        //            //CurriculumLnk.Click();                   
        //            TranscriptPage TP = new TranscriptPage(Browser);
        //            return TP;
        //        }
        //    }

        //    else
        //    {
        //        throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, or if the button is already added, then the page you were on did not contain the button.");
        //    }

        //    return null;
        //}
        ///// <summary>
        /////  After going to the home page, click on the Transcript link under My CME link
        ///// </summary>
        ///// <param name="buttonOrLinkElem"></param>
        ///// <returns></returns>
        //public dynamic ClickOnTranscriptLink(IWebElement buttonOrLinkElem)
        //{
        //    MyCmeLnk.Click();
        //    Thread.Sleep(5000);
        //    TranscriptPage TP = Click(TranscriptLnk);
        //    return TP;
        //}





        #endregion methods: page specific



    }
}