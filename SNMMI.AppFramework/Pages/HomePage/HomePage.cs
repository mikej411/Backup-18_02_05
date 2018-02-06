using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace SNMMI.AppFramework
{
    public class HomePage : SNMMIPage, IDisposable
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
        public IWebElement JointProviderLnk { get { return this.FindElement(Bys.HomePage.JointProviderLnk); } }

        public IWebElement LogoutLnk { get { return this.FindElement(Bys.HomePage.LogoutLnk); } }

        public IWebElement EducationLnk { get { return this.FindElement(Bys.HomePage.EducationLnk); } }



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

        #region methods: wrappers




        #endregion wrappers



    }
}