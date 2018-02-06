using Browser.Core.Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Data;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using Browser.Core.Framework.Utils;
using System.Diagnostics;
using OpenQA.Selenium.Firefox;
using LS.AppFramework.Constants;
using LS.AppFramework;

namespace LS.AppFramework.HelperMethods
{
    /// <summary>
    /// A class that consists of methods which combine custom page methods to accomplish various tasks for this application. This is mainly
    /// called/used when a tester is automating another application, and needs to also access this application to setup data or verify functionality
    /// </summary>
    public class LSHelperMethods
    {
        #region properties



        #endregion properties

        #region methods

        #region RCP

        /// <summary>
        /// Goes to the Participant Program page if we are not already there, clicks on the the Self Reporting tab, clicks the 
        /// Actions>Validate link for a user-specified activity, waits for the Credit Validation page to appear, clicks the Accept radio
        /// button, clicks the Submit button, and waits for the page be done loading
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="siteName">The exact text of the site inside sites table that you want to click on</param>
        /// <param name="participantFirstAndLastName">The exact text of the participant inside the participants table that you want to click on</param>
        /// <param name="program">The exact text of the program inside the programs table that you want to click on</param>
        /// <param name="activityName">The exact text of the activity inside the Self Reported Activities table table that you want to click on</param>
        public void ValidateCredit(IWebDriver browser, string siteName, string participantFirstAndLastName, string program, string activityName)
        {
            ProgramPage PP = new ProgramPage(browser);

            // If we are not on the participant program page, then go there
            if (browser.FindElements(Bys.ProgramPage.SelfReportActTab).Count == 0)
            {
                GoToParticipantProgramPage(browser, siteName, participantFirstAndLastName, program);
            }

            PP.ChooseActivityAndValidateCredi(activityName);
        }

        /// <summary>
        /// Clicks on the Details tab of the Program page and then returns the Status label value from the details tab on the Program page
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="detail">The name of the label on the Detail tab for which you want the value to return</param>
        /// <returns></returns>
        public string GetProgramDetail(IWebDriver browser, string detail)
        {
            ProgramPage PP = new ProgramPage(browser);
            return PP.GetProgramDetail(browser, detail);
        }

        /// <summary>
        /// Searches for a user-specified site, clicks on that site, clicks on the All Particpants link, searches for a user-specified participant, 
        /// clicks on the participant, clicks on the Programs tab of the particpant, then clicks on a user-specified program and waits for that program page
        /// to load
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="siteName">The exact text of the site inside sites table that you want to click on</param>
        /// <param name="participantFirstAndLastName">The exact text of the participant inside participants table that you want to click on</param>
        /// <param name="program">The exact text of the program inside programs table that you want to click on</param>
        /// <returns></returns>
        public ProgramPage GoToParticipantProgramPage(IWebDriver browser, string siteName, string participantFirstAndLastName, string program)
        {          
            ParticipantsPage PAP = GoToParticipantPage(browser, siteName, participantFirstAndLastName);

            PAP.ClickAndWait(PAP.ProgramsTab);

            return PAP.ClickProgramAndWait(browser, program);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="numberOfCreditsToWaitFor"></param>
        public void WaitForProgramCreditsWindowsService(IWebDriver browser, string numberOfCreditsToWaitFor)
        {
            ProgramPage PP = new ProgramPage(browser);
            PP.ClickAndWait(PP.DetailsTab);
            ApplicationUtils.WaitForCreditsToBeApplied(PP, Bys.ProgramPage.DetailsTabCreditsValueLbl, numberOfCreditsToWaitFor);
        }

        /// <summary>
        /// Searches for a user-specified site, clicks on that site, clicks on the All Particpants link, searches for a user-specified participant, 
        /// clicks on the participant and waits for the Participant page to load
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="siteName">The exact text of the site inside sites table that you want to click on</param>
        /// <param name="participantFirstAndLastName">The exact text of the participant inside participants table that you want to click on</param>
        /// <returns></returns>
        public ParticipantsPage GoToParticipantPage(IWebDriver browser, string siteName, string participantFirstAndLastName)
        {
            // Very sloppy code. Need to refactor this. I am basically instantiating a page, then clicking a link on the base page using that instance.
            // not good
            SearchPage SP = new SearchPage(browser);

            SP.ClickAndWaitBasePage(SP.SitesTab);

            // If there is only 1 site in the environment, then the Sites table will not appear, so we won't need to search for a site because that site 
            // page will immediately show
            if (browser.Exists(Bys.SearchPage.SitesTbl))
            {
                SP.SearchAndSelect(Bys.SearchPage.SitesTblBody, LSConstants.SearchResults.Sites, siteName);
            }

            SP.ClickAndWaitBasePage(SP.AllParticipantsLnk);

            return SP.SearchAndSelect(Bys.SearchPage.AllParticpantsTblBody, LSConstants.SearchResults.Participants, participantFirstAndLastName);
        }

        /// <summary>
        /// Clicks the Add Adjustment tab if we are not already there, clicks the Add Adjustment link, chooses an Adjustment Code from 
        /// the select element (this overload is for the ext1, ext2, ext2f, pra, per and temp adjustments). Then clicks the Add Adjustment button
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="siteName">The exact text of the site inside sites table that you want to click on</param>
        /// <param name="participantFirstAndLastName">The exact text of the participant inside participants table that you want to click on</param>
        /// <param name="adjustmentCode">The exact text of the adjustment code that you want to chooose in the Adjustment Code select element</param>
        public void RCP_AddProgramAdjustment(IWebDriver browser, string participantFirstAndLastName, LSConstants.AdjustmentCodes adjustmentCode)
        {
            ProgramPage PP = new ProgramPage(browser);
            PP.AddProgramAdjustment(adjustmentCode);
        }

        /// <summary>
        /// Clicks the Add Adjustment tab if we are not already there, clicks the Add Adjustment link, chooses an Adjustment Code 
        /// from the select element, and clicks on the Yes or No radio button (this overload is for the INTNL and Voluntary program adjustments).
        /// Then clicks the Add Adjustment button
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="siteName">The exact text of the site inside sites table that you want to click on</param>
        /// <param name="participantFirstAndLastName">The exact text of the participant inside participants table that you want to click on</param>
        /// <param name="adjustmentCode">The exact text of the adjustment code that you want to chooose in the Adjustment Code select element</param>
        /// <param name="Rdo">The yes or no radio button element for INTNL or VOLUNTARY program adjustment</param>
        public void RCP_AddProgramAdjustment(IWebDriver browser, string participantFirstAndLastName, LSConstants.AdjustmentCodes adjustmentCode, IWebElement Rdo)
        {
            ProgramPage PP = new ProgramPage(browser);
            PP.AddProgramAdjustment(adjustmentCode, Rdo);
        }

        /// <summary>
        /// Clicks the Add Adjustment tab if we are not already there, clicks the Add Adjustment link, chooses an Adjustment Code 
        /// from the select element, enters a start and end date, selects a leave code (this overload is for the Leave program adjustment). 
        /// Then clicks the Add Adjustment button
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="siteName">The exact text of the site inside sites table that you want to click on</param>
        /// <param name="participantFirstAndLastName">The exact text of the participant inside participants table that you want to click on</param>
        /// <param name="adjustmentCode">The exact text of the adjustment code that you want to chooose in the Adjustment Code select element</param>
        /// <param name="leaveStartDate"></param>
        /// <param name="leaveEndDate"></param>
        /// <param name="leaveCode"></param>
        public void RCP_AddProgramAdjustment(IWebDriver browser, string participantFirstAndLastName, LSConstants.AdjustmentCodes adjustmentCode, string leaveStartDate, string leaveEndDate, string leaveCode)
        {
            ProgramPage PP = new ProgramPage(browser);
            PP.AddProgramAdjustment(adjustmentCode, leaveStartDate, leaveEndDate, leaveCode);
        }

        /// <summary>
        /// Clicks the Add Adjustment tab if we are not already there, clicks the Add Adjustment link, chooses an Adjustment Code from 
        /// the select element, enters an effective date (this overload is for the reinstated, per program, pra program, voluntary program, 
        /// international program, main program and resident program program adjustments). Then clicks the Add Adjustment buttons
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="siteName">The exact text of the site inside sites table that you want to click on</param>
        /// <param name="participantFirstAndLastName">The exact text of the participant inside participants table that you want to click on</param>
        /// <param name="adjustmentCode">The exact text of the adjustment code that you want to chooose in the Adjustment Code select element</param>
        /// <param name="effectiveDate"></param>
        public void RCP_AddProgramAdjustment(IWebDriver browser, string participantFirstAndLastName, LSConstants.AdjustmentCodes adjustmentCode, string effectiveDate)
        {
            ProgramPage PP = new ProgramPage(browser);
            PP.AddProgramAdjustment(adjustmentCode, effectiveDate);
        }


        /// <summary>
        /// Goes to the Participant page if we are not already there, clicks on the Programs tab of the Participants, clcks Actions>Adjust 
        /// Dates link for the Maintenance Of Certification program,fills in a user-specified start or end date, then clicks the Yes button
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="participantFirstAndLastName">The exact text of the participant inside participants table that you want to click on</param>
        /// <param name="startOrEndDate">"Start" or "End"</param>
        /// <param name="date">The date to enter, in the format "yyyy-mm-dd"</param>
        /// ToDo: Make this more universal by putting the Site name as a parameter, then refactoring this method and code which calls this method
        public void RCP_AdjustMOCDate(IWebDriver browser, string participantFirstAndLastName, string startOrEndDate, string date)
        {
            ParticipantsPage PAP = new ParticipantsPage(browser);

            // If we are not on the participant page, then go there
            if (browser.FindElements(Bys.ParticipantsPage.RegeneratePasswordTab).Count == 0)
            {
                GoToParticipantPage(browser, "Royal College of Physicians", participantFirstAndLastName);
            }

            PAP.AdjustProgramCycleDates(startOrEndDate, date);
        }

        #endregion RCP

        /// <summary>
        /// Logs in then returns the SearchPage object. This Login method should only be called/used from within this class (mainly when coding tests
        /// on another application). Otherwise, the Login page class's Login method should be used
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public void Login(IWebDriver browser, string username, string password)
        {
            LoginPage LP = new LoginPage(browser);
            LP.Login(browser, "lkaveti", "password");


            //SearchPage SP = new SearchPage(browser);
            //IWebElement btn = ElemSet.Grid_HoverButtonOrLinkWithinRow(browser, SP.SitesTbl, Bys.SearchPage.SitesTblBodyRow, "AAFP Education", null, "Actions");
            //Thread.Sleep(0500);
            //IWebElement btnParent = XpathUtils.GetNthParentElem(btn, 3);
            //ElemSet.Grid_ClickMenuItemInsideDropdown(browser, btnParent, "All Participants");
        }





        #endregion methods

    }
}
