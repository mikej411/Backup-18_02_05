using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using LOG4NET = log4net.ILog;


namespace CFPC.AppFramework
{
    public class CPDPlanningPage : CFPCPage, IDisposable
    {
        #region constructors
        public CPDPlanningPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that I start so I can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "Default.aspx"; } }

        #endregion properties

        #region elements

        // Main page
        public IWebElement EnterCPDActBtn { get { return this.FindElement(Bys.CreditSummaryPage.EnterCPDActBtn); } }
        
        public IWebElement MyCreditSummaryLbl { get { return this.FindElement(Bys.CreditSummaryPage.MyCreditSummaryLbl); } }

        public IWebElement TotalCreditsValueLbl { get { return this.FindElement(Bys.CreditSummaryPage.TotalCreditsValueLbl); } }

        #endregion elements

        #region methods: per page

        public override void WaitForInitialize()
        {
           // this.WaitUntil(TimeSpan.FromSeconds(120), Criteria.CreditSummaryPage.PageReady);
        }
        public void Wait(int time)
        {
            System.Threading.Thread.Sleep(time);
        }
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            try { activeRequests.Clear(); }
            catch (Exception ex) { _log.ErrorFormat("Failed to dispose CreditSummaryPge", activeRequests.Count, ex); }
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


            if (buttonOrLinkElem.GetAttribute("id") == ReportsTab.GetAttribute("id"))
                {
                    buttonOrLinkElem.Click();
                    //Browser.WaitForElement(Bys.EnterACPDActivityPage.CategoryDrpDn, TimeSpan.FromSeconds(20), ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
                    ReportsPage rp = new ReportsPage(Browser);
                    //rp.WaitForInitialize();
                    return rp;
                }
           

              
            

            else
            {
                throw new Exception("No button or link was found with your passed parameter");
            }

            return null;
        }

        /// <summary>
        /// Gets the current value of the credits for
        /// later storage in a variable
        /// </summary>
        public double GetTotalCredits()
        {
            double credits = 0;
            
            //extract the string for the credits           // MIKE: Added an end line above this line
            String creditString = TotalCreditsValueLbl.Text;

            //convert the string to an integer                // MIKE: Added an end line above this line
            Double.TryParse(creditString, out credits);
            return credits;
        }

        #endregion methods: page specific


    }
}
