using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace SNMMI.AppFramework
{
    public class EducationPage : SNMMIPage, IDisposable
    {
        #region constructors
        public EducationPage(IWebDriver driver) : base(driver)
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
        public IWebElement SearchBtn { get { return this.FindElement(Bys.EducationPage.SearchBtn); } }

        public IWebElement JointProviderPortalLnk { get { return this.FindElement(Bys.EducationPage.JointProviderPortalLnk); } }


        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(100), Criteria.EducationPage.PageReady);
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
            if (Browser.Exists(Bys.EducationPage.JointProviderPortalLnk))
            {
                if (buttonOrLinkElem.GetAttribute("href") == JointProviderPortalLnk.GetAttribute("href"))
                {
                    buttonOrLinkElem.Click();
                    CurriculumPage CP = new CurriculumPage(Browser);
                    CP.WaitForInitialize();
                    return CP;
                }
            }

            else
            {
                throw new Exception("No button or link was found with your passed parameter");
            }

            return null;
        }




        #endregion wrappers



    }
}