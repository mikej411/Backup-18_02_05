using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;
using LS.AppFramework.Constants;

namespace LS.AppFramework
{
    public class ProgramPage : Page, IDisposable
    {
        #region constructors
        public ProgramPage(IWebDriver driver) : base(driver)
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

            
        public IWebElement DetailsTab { get { return this.FindElement(Bys.ProgramPage.DetailsTab); } }
        public IWebElement ProgAdjustTabAddAdjustFormEffectiveDtRdo { get { return this.FindElement(Bys.ProgramPage.ProgAdjustTabAddAdjustFormEffectiveDtRdo); } }
        public IWebElement ProgAdjustTabAddAdjustFormIsVoluntNoRdo { get { return this.FindElement(Bys.ProgramPage.ProgAdjustTabAddAdjustFormIsVoluntNoRdo); } }
        public IWebElement ProgAdjustTabAddAdjustFormIsVoluntYesRdo { get { return this.FindElement(Bys.ProgramPage.ProgAdjustTabAddAdjustFormIsVoluntYesRdo); } }
        public SelectElement ProgAdjustTabAddAdjustFormLeaveCodeSelElem { get { return new SelectElement(this.FindElement(Bys.ProgramPage.ProgAdjustTabAddAdjustFormLeaveCodeSelElem)); } }
        public IWebElement ProgAdjustTabAddAdjustFormLeaveEndDtTxt { get { return this.FindElement(Bys.ProgramPage.ProgAdjustTabAddAdjustFormLeaveEndDtTxt); } }
        public IWebElement ProgAdjustTabAddAdjustFormLeaveStartDtTxt { get { return this.FindElement(Bys.ProgramPage.ProgAdjustTabAddAdjustFormLeaveStartDtTxt); } }
        public IWebElement ProgAdjustTabAddAdjustLnk { get { return this.FindElement(Bys.ProgramPage.ProgAdjustTabAddAdjustLnk); } }
        public IWebElement ProgAdjustTabAddAdjustFormIsIntnlNoRdo { get { return this.FindElement(Bys.ProgramPage.ProgAdjustTabAddAdjustFormIsIntnlNoRdo); } }
        public IWebElement ProgAdjustTabAddAdjustFormIsIntnlYesRdo { get { return this.FindElement(Bys.ProgramPage.ProgAdjustTabAddAdjustFormIsIntnlYesRdo); } }
        public IWebElement ProgAdjustTabAddAdjustFormAddAdjustBtn { get { return this.FindElement(Bys.ProgramPage.ProgAdjustTabAddAdjustFormAddAdjustBtn); } }
        public SelectElement ProgAdjustTabAddAdjustFormAdjustCodeSelElem { get { return new SelectElement(this.FindElement(Bys.ProgramPage.ProgAdjustTabAddAdjustFormAdjustCodeSelElem)); } }
        public IWebElement ProgramAdjustmentsTab { get { return this.FindElement(Bys.ProgramPage.ProgramAdjustmentsTab); } }
        public IWebElement DetailsTabProgramValueLbl { get { return this.FindElement(Bys.ProgramPage.DetailsTabProgramValueLbl); } }
        public IWebElement DetailsTabNameValueLbl { get { return this.FindElement(Bys.ProgramPage.DetailsTabNameValueLbl); } }
        public IWebElement DetailsTabStatusValueLbl { get { return this.FindElement(Bys.ProgramPage.DetailsTabStatusValueLbl); } }
        public IWebElement DetailsTabStartsValueLbl { get { return this.FindElement(Bys.ProgramPage.DetailsTabStartsValueLbl); } }
        public IWebElement DetailsTabEndsValueLbl { get { return this.FindElement(Bys.ProgramPage.DetailsTabEndsValueLbl); } }
        public IWebElement SelfReportActTab { get { return this.FindElement(Bys.ProgramPage.SelfReportActTab); } }
        public IWebElement SelfReportActTabValidStatusSelElem { get { return this.FindElement(Bys.ProgramPage.SelfReportActTabValidStatusSelElem); } }
        public IWebElement SelfReportActTabValidActivityTbl { get { return this.FindElement(Bys.ProgramPage.SelfReportActTabActivityTbl); } }
        public IWebElement SelfReportActTabValidActivityTblBody { get { return this.FindElement(Bys.ProgramPage.SelfReportActTabActivityTblBody); } }
        public IWebElement CreditValidationAcceptRdo { get { return this.FindElement(Bys.ProgramPage.CreditValidationAcceptRdo); } }
        public IWebElement CreditValidationRejectRdo { get { return this.FindElement(Bys.ProgramPage.CreditValidationRejectRdo); } }
        public IWebElement CreditValidationSubmitBtn { get { return this.FindElement(Bys.ProgramPage.CreditValidationSubmitBtn); } }


        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            try
            {
                this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.ProgramPage.PageReady);
            }
            catch
            {
                RefreshPage();
            }

        }

        /// Refreshes the page and then uses the wait criteria that is found within WaitForInitialize to wait for the page to load.
        /// This is used as a catch block inside WaitForInitialize, in case the page doesnt load initially. Can also be used to 
        /// randomly refresh the page
        /// </summary>
        public void RefreshPage()
        {
            Browser.Navigate().Refresh();
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.ProgramPage.PageReady);
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

        /// <summary>
        /// Clicks the user-specified element and then waits for a window to close or open, or an element to be visible or enabled
        /// </summary>
        /// <param name="buttonOrLinkElem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement buttonOrLinkElem)
        {
            if (Browser.Exists(Bys.ProgramPage.SelfReportActTab))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == SelfReportActTab.GetAttribute("outerHTML"))
                {
                    ElemSet.ClickAfterScroll(Browser, buttonOrLinkElem);
                    this.WaitUntil(Criteria.ProgramPage.SelfReportActTabValidActivityTblBodyVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ProgramPage.ProgramAdjustmentsTab))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == ProgramAdjustmentsTab.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntil(Criteria.ProgramPage.ProgramAdjustmentsActivityTblBodyRowVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ProgramPage.DetailsTab))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == DetailsTab.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntil(Criteria.ProgramPage.DetailsTabStatusValueLblVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ProgramPage.ProgAdjustTabAddAdjustLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == ProgAdjustTabAddAdjustLnk.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntil(Criteria.ProgramPage.ProgAdjustTabAddAdjustFormAdjustCodeSelElemVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ProgramPage.ProgAdjustTabAddAdjustFormAddAdjustBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == ProgAdjustTabAddAdjustFormAddAdjustBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntilAll(TimeSpan.FromSeconds(120), Criteria.ProgramPage.ProgAdjustTabAddAdjustFormNotExists, 
                        Criteria.ProgramPage.AdjustmentAddedBannerNotExists);
                    // Adding a little sleep here. For some reason, whenever the code proceeds after clicking this button, the next line of code doesnt execute.
                    // For example, Navigate.GoToLoginPage. That code gets past the navigation part, but if you view the test in progress, no URL is entered into
                    // the URL. Another example I have code to click on the "Sites" tab in LTS after this, and the code goes past the Click line, but if you view
                    // the test, it didnt click anything. I have never seen this before. So far, I think it only happened in Debug mode.
                    // Monitor going forward
                    Thread.Sleep(0600);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ProgramPage.CreditValidationSubmitBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == CreditValidationSubmitBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntil(Criteria.ProgramPage.SelfReportActTabValidActivityTblBodyVisible);
                    Thread.Sleep(0500);
                    return null;
                }
            }

            else
            {
                throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, or if the button is already added, then the page you were on did not contain the button.");
            }

            return null;
        }

        /// <summary>
        /// Goes to the Add Adjustment tab if we are not already there, clicks the Add Adjustment link, chooses an Adjustment Code from the select
        /// element (this overload is for the ext1, ext2, ext2f, pra, per and temp adjustments). Then clicks the Add Adjustment button
        /// </summary>
        /// <param name="adjustmentCode">The exact text of the adjustment code that you want to chooose in the Adjustment Code select element</param>
        internal void AddProgramAdjustment(LSConstants.AdjustmentCodes adjustmentCode)
        {
            ClickAndWait(ProgramAdjustmentsTab);

            ClickAndWait(ProgAdjustTabAddAdjustLnk);

            switch (adjustmentCode)
            {
                case LSConstants.AdjustmentCodes.EXT1:
                    ProgAdjustTabAddAdjustFormAdjustCodeSelElem.SelectByText("EXT1");
                    break;
                case LSConstants.AdjustmentCodes.EXT2:
                    ProgAdjustTabAddAdjustFormAdjustCodeSelElem.SelectByText("EXT2");
                    break;
                case LSConstants.AdjustmentCodes.EXT2F:
                    ProgAdjustTabAddAdjustFormAdjustCodeSelElem.SelectByText("EXT2F");
                    break;
                case LSConstants.AdjustmentCodes.PRA:
                    ProgAdjustTabAddAdjustFormAdjustCodeSelElem.SelectByText("PRA");
                    break;
                case LSConstants.AdjustmentCodes.PER:
                    ProgAdjustTabAddAdjustFormAdjustCodeSelElem.SelectByText("PER");
                    break;
                case LSConstants.AdjustmentCodes.TEMP:
                    ProgAdjustTabAddAdjustFormAdjustCodeSelElem.SelectByText("TEMP");
                    break;
            }

            ClickAndWait(ProgAdjustTabAddAdjustFormAddAdjustBtn);
            this.RefreshPage(true);
        }

        /// <summary>
        /// Goes to the Add Adjustment tab if we are not already there, clicks the Add Adjustment link, chooses an Adjustment Code from the select
        /// element, and clicks on the Yes or No radio button (this overload is for the INTNL and Voluntary program adjustments). Then 
        /// clicks the Add Adjustment button
        /// </summary>
        /// <param name="adjustmentCode">The exact text of the adjustment code that you want to chooose in the Adjustment Code select element</param>
        /// <param name="Rdo">The yes or no radio button element for INTNL or VOLUNTARY program adjustment</param>
        internal void AddProgramAdjustment(LSConstants.AdjustmentCodes adjustmentCode, IWebElement Rdo)
        {
            ClickAndWait(ProgramAdjustmentsTab);

            ClickAndWait(ProgAdjustTabAddAdjustLnk);

            switch (adjustmentCode)
            {
                case LSConstants.AdjustmentCodes.INTNL:
                    ProgAdjustTabAddAdjustFormAdjustCodeSelElem.SelectByText("INTNL");
                    break;
                case LSConstants.AdjustmentCodes.VOLUNTARY:
                    ProgAdjustTabAddAdjustFormAdjustCodeSelElem.SelectByText("VOLUNTARY");
                    break;
            }

            Thread.Sleep(1000);
            Rdo.Click();

            ClickAndWait(ProgAdjustTabAddAdjustFormAddAdjustBtn);
        }

        /// <summary>
        /// Goes to the Add Adjustment tab if we are not already there, clicks the Add Adjustment link, chooses an Adjustment Code from the select
        /// element, enters a start and end date, selects a leave code (this overload is for the Leave program adjustment). Then clicks the 
        /// Add Adjustment button
        /// </summary>
        /// <param name="adjustmentCode">The exact text of the adjustment code that you want to chooose in the Adjustment Code select element</param>
        /// <param name="leaveStartDate"></param>
        /// <param name="leaveEndDate"></param>
        /// <param name="leaveCode"></param>
        internal void AddProgramAdjustment(LSConstants.AdjustmentCodes adjustmentCode, string leaveStartDate, string leaveEndDate, string leaveCode)
        {
            ClickAndWait(ProgramAdjustmentsTab);

            ClickAndWait(ProgAdjustTabAddAdjustLnk);

            ProgAdjustTabAddAdjustFormAdjustCodeSelElem.SelectByText("LEAVE");

            Browser.WaitForElement(Bys.ProgramPage.ProgAdjustTabAddAdjustFormLeaveStartDtTxt, ElementCriteria.IsVisible);
            ProgAdjustTabAddAdjustFormLeaveStartDtTxt.SendKeys(leaveStartDate);
            ProgAdjustTabAddAdjustFormLeaveEndDtTxt.SendKeys(leaveEndDate);
            ProgAdjustTabAddAdjustFormLeaveCodeSelElem.SelectByText(leaveCode);

            ClickAndWait(ProgAdjustTabAddAdjustFormAddAdjustBtn);
        }

        /// <summary>
        /// Goes to the Add Adjustment tab if we are not already there, clicks the Add Adjustment link, chooses an Adjustment Code from the select
        /// element, then enters an effective date (this overload is for the reinstated, per program, pra program, voluntary program, international 
        /// program, main program and resident program program adjustments). Then clicks the Add Adjustment button
        /// </summary>
        /// <param name="adjustmentCode">The exact text of the adjustment code that you want to chooose in the Adjustment Code select element</param>
        /// <param name="effectiveDate"></param>
        internal void AddProgramAdjustment(LSConstants.AdjustmentCodes adjustmentCode, string effectiveDate)
        {
            ClickAndWait(ProgramAdjustmentsTab);

            ClickAndWait(ProgAdjustTabAddAdjustLnk);

            switch (adjustmentCode)
            {
                case LSConstants.AdjustmentCodes.REINSTATEDNonCompliance:
                    ProgAdjustTabAddAdjustFormAdjustCodeSelElem.SelectByText("REINSTATED - NON COMPLIANCE");
                    break;
                case LSConstants.AdjustmentCodes.REINSTATEDOther:
                    ProgAdjustTabAddAdjustFormAdjustCodeSelElem.SelectByText("REINSTATED - OTHER");
                    break;
                case LSConstants.AdjustmentCodes.PERProgram:
                    ProgAdjustTabAddAdjustFormAdjustCodeSelElem.SelectByText("PER Program");
                    break;
                case LSConstants.AdjustmentCodes.PRAProgram:
                    ProgAdjustTabAddAdjustFormAdjustCodeSelElem.SelectByText("PRA Program");
                    break;
                case LSConstants.AdjustmentCodes.VoluntaryProgram:
                    ProgAdjustTabAddAdjustFormAdjustCodeSelElem.SelectByText("Voluntary Program");
                    break;
                case LSConstants.AdjustmentCodes.InternationalProgram:
                    ProgAdjustTabAddAdjustFormAdjustCodeSelElem.SelectByText("International Program");
                    break;
                case LSConstants.AdjustmentCodes.MainProgram:
                    ProgAdjustTabAddAdjustFormAdjustCodeSelElem.SelectByText("Main Program");
                    break;
                case LSConstants.AdjustmentCodes.ResidentProgram:
                    ProgAdjustTabAddAdjustFormAdjustCodeSelElem.SelectByText("Resident Program");
                    break;
            }

            Browser.WaitForElement(Bys.ProgramPage.ProgAdjustTabAddAdjustFormEffectiveDtRdo, ElementCriteria.IsVisible);
            ProgAdjustTabAddAdjustFormEffectiveDtRdo.Clear();
            ProgAdjustTabAddAdjustFormEffectiveDtRdo.SendKeys(effectiveDate);
            ProgAdjustTabAddAdjustFormEffectiveDtRdo.SendKeys(Keys.Tab);

            ClickAndWait(ProgAdjustTabAddAdjustFormAddAdjustBtn);
        }

        /// <summary>
        /// Goes to the Self Reporting tab if we are not already there, clicks the Actions>Validate link for a user-specified activity, waits for
        /// the Credit Validation page to appear, clicks the Accept radio button, clicks the Submit button, and waits for the page be done loading
        /// </summary>
        /// <param name="activityName">The exact text of the activity inside the Self Reported Activities table table that you want to click on</param>
        internal void ChooseActivityAndValidateCredi(string activityName)
        {
            ClickAndWait(SelfReportActTab);

            IWebElement btn = ElemSet.Grid_ClickButtonOrLinkWithinRow(Browser, SelfReportActTabValidActivityTbl, Bys.ProgramPage.SelfReportActTabActivityTblBodyRow,
                activityName, "td", "Actions", "span");
            Thread.Sleep(0500);

            IWebElement btnParent = XpathUtils.GetNthParentElem(btn, 3);

            ElemSet.Grid_ClickMenuItemInsideDropdown(Browser, btnParent , "Validate");
            Browser.WaitForElement(Bys.ProgramPage.CreditValidationAcceptRdo);

            CreditValidationAcceptRdo.Click();

            ClickAndWait(CreditValidationSubmitBtn);
            Thread.Sleep(0300);
            this.WaitUntil(Criteria.ProgramPage.ExternalActivityHasBeenAcceptedBannerNotExists);
            // Adding a little sleep here. For some reason, whenever the code proceeds after clicking this button (which invokes the green banner at the top),
            // the next line of code doesnt execute. For example, Navigate.GoToLoginPage. That code gets past the navigation part, but if you view the test in
            // progress, no URL is entered into the URL. Another example I have code to click on the "Sites" tab in LTS after this, and the code goes past the
            // Click line, but if you view the test, it didnt click anything. I have never seen this before. So far, I think it only happened in Debug mode.
            // Monitor going forward
            Thread.Sleep(0600);

            // Have to refresh the page sometimes so the credits appear on the details tab
            this.RefreshPage(true);
        }

        /// <summary>
        /// Clicks on the Details tab then returns the value label of the user-specified label on the Details tab
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="detail">The name of the label on the Detail tab for which you want the value to return</param>
        internal string GetProgramDetail(IWebDriver browser, string detail)
        {
            string detailToReturn = "";

            ClickAndWait(DetailsTab);

            if (detail == "Name")
            {
                detailToReturn = DetailsTabNameValueLbl.Text;
            }
            else if (detail == "Status")
            {
                detailToReturn = DetailsTabStatusValueLbl.Text;
            }
            else if (detail == "Starts")
            {
                detailToReturn = DetailsTabStartsValueLbl.Text;
            }
            else if (detail == "Ends")
            {
                detailToReturn = DetailsTabEndsValueLbl.Text;
            }
            else if (detail == "Program")
            {
                detailToReturn = DetailsTabProgramValueLbl.Text;
            }

            return detailToReturn;
        }

        #endregion methods: page specific
    }

    #region additional classes

    public class ProgramCycle
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string Program { get; set; }

        public ProgramCycle(string name, string status, string start, string end, string program)
        {
            Name = name;
            Status = status;
            Start = start;
            End = end;
            Program = program;
        }

        #endregion additional classes

    }
}