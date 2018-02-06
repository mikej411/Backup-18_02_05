using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;


namespace CFPC.AppFramework
{
    public class DashboardPage : CFPCPage, IDisposable
    {
        #region constructors
        public DashboardPage(IWebDriver driver) : base(driver)
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
        public IWebElement EnterCPDActBtn { get { return this.FindElement(Bys.DashboardPage.EnterCPDActBtn); } }
        public IWebElement EULAButton { get { return this.FindElement(Bys.DashboardPage.EULAButton); } }

        public IWebElement TotalCreditsLinkLnk { get { return this.FindElement(Bys.DashboardPage.TotalCreditsLinkLnk); } }
        public IWebElement AppliedAMACreditsLbl { get { return this.FindElement(Bys.DashboardPage.AppliedAMACreditsLbl); } }

        public IWebElement TotalCreditsValueLbl { get { return this.FindElement(Bys.DashboardPage.TotalCreditsValueLbl); } }

        #endregion elements

        #region methods: per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(180), Criteria.DashboardPage.PageReady);
        }
        //Creating Additional Criteria for Dashboard Page Tabs
        /*   public override void WaitForInitializeCreditSummary()
           {
               this.WaitUntil(TimeSpan.FromSeconds(180), Criteria.DashboardPage.CreditSummaryLblEnabled);
           }
           public override void WaitForInitializeHoldingArea()
           {
               this.WaitUntil(TimeSpan.FromSeconds(180), Criteria.DashboardPage.PageReady);
           }
           public override void WaitForInitializeCPDActivitiesList()
           {
               this.WaitUntil(TimeSpan.FromSeconds(180), Criteria.DashboardPage.PageReady);
           }
           public override void WaitForInitializeCPDPlanning()
           {
               this.WaitUntil(TimeSpan.FromSeconds(180), Criteria.DashboardPage.PageReady);
           }
           public override void WaitForInitializeReports()
           {
               this.WaitUntil(TimeSpan.FromSeconds(180), Criteria.DashboardPage.PageReady);
           }*/

        //end dashboard page
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
            catch (Exception ex) { _log.ErrorFormat("Failed to dispose DashboardPge", activeRequests.Count, ex); }
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
            if (buttonOrLinkElem.GetAttribute("id").Equals(CreditSummaryTab.GetAttribute("id")))
            {
                buttonOrLinkElem.Click();
                CreditSummaryPage page = new CreditSummaryPage(Browser);
                page.WaitForInitialize();
                return page;
            }
            else if (buttonOrLinkElem.GetAttribute("id").Equals(EnterCPDActBtn.GetAttribute("id")))
            {
                    buttonOrLinkElem.Click();
                    //Browser.WaitForElement(Bys.EnterACPDActivityPage.CategoryDrpDn, TimeSpan.FromSeconds(20), ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
                    EnterACPDActivityPage eap = new EnterACPDActivityPage(Browser);
                    eap.WaitForInitialize();
                    return eap;
            }
           
            else
            {
                throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, or if the button is already added, then the page you were on did not contain the button.");
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

            //extract the string for the credits           
            String creditString = "";
     

              creditString = TotalCreditsValueLbl.Text;
             

            //convert the string to an integer                
            Double.TryParse(creditString, out credits);
            return credits;
        }

        public String CheckForCreditUpdate()
        {
            //repeat clicking on the dashboard button 
            int clickCount = 0;
            String TotalCreditsString = "";
            do
            {
                //click between the tabs to be
                if ((clickCount % 2) == 0)
                {
                    DashboardTab.Click();
                    Thread.Sleep(5000);
                    //get the value from the credit summary for the total number of credits
                    TotalCreditsString = TotalCreditsValueLbl.Text;
                }
                else
                {
                    JavascriptUtils.Click(Browser, CPDActivitiesTab);
                    Thread.Sleep(5000);
                }
                Thread.Sleep(10000);
                clickCount++;

            } while (clickCount < 25 && TotalCreditsString.Equals("0"));

            return TotalCreditsString;
        }

        #endregion methods: page specific


    }
}
