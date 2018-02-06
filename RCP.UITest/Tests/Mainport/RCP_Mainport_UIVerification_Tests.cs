using Browser.Core.Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using RCP.AppFramework;
using System.Data;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using OpenQA.Selenium.Firefox;
using Browser.Core.Framework.Resources;
using OpenQA.Selenium.Remote;
using System.Reflection;
using System.IO;
using LS.AppFramework.HelperMethods;
using System.Globalization;
using System.Configuration;

namespace RCP.UITest
{
    //[BrowserMode(BrowserMode.New)]
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [LocalSeleniumTestFixture(BrowserNames.Firefox)]
    [LocalSeleniumTestFixture(BrowserNames.InternetExplorer)]
    // Commented out until https://code.premierinc.com/issues/browse/RCPSC-885 is fixed
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Firefox)]
    [RemoteSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [TestFixture]
    public class RCP_Mainport_UIVerification_Tests : TestBase
    {
        #region Constructors
        public RCP_Mainport_UIVerification_Tests(string browserName) : base(browserName) { }

        // Remote Selenium Grid Test
        public RCP_Mainport_UIVerification_Tests(string browserName, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region properties

        LSHelperMethods LSHelp = new LSHelperMethods();

        #endregion properties

        #region test fixtures

        ///// <summary>
        ///// How to override base test setup class
        ///// </summary>
        //[SetUp]
        //public override void TestSetup()
        //{
        //    base.TestSetup();
        //    browser = base.Browser;
        //    Assert.Pass();
        //}

        #endregion test fixtures

        #region tests

        [Test]
        [Description("Given a user completes an activity, When the user clicks to My CPD Activities List page, Then that activity should appear in " +
            "the grid, and When a user clicks X to delete that activity, Then it should disappear from the grid")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void NewActivityAppearsAndDisappearsFromGrid()
        {
            /// 1. Login as an existing user
            LoginPage LP = Navigation.GoToLoginPage(browser);
            MyDashboardPage DP = LP.LoginAsExistingUser(UserUtils.UserRole.MP, UserUtils.MainportUser1Login, ConfigurationManager.AppSettings["LoginPassword"]);

            /// 2. Add an activity 
            EnterCPDActivityPage EAP = DP.ClickAndWait(DP.EnterACPDActivityBtn);
            Activity NewConferenceActivity = EAP.AddActivityThenSubmit(Constants.MainportActivityTypes.Sec1_Conference_IfNotAccreditedCutCreditsInHalf, "10", false);

            /// 3. Navigate to the My CPD Activities List page and verify that the activity got added to the the table
            MyCPDActivitiesListPage MCP = DP.ClickAndWaitBasePage(DP.MyCPDActivitiesTab);
            Assert.True(ElemGet.Grid_ContainsRecord(browser, MCP.ActivityTblBody, Bys.MyCPDActivitiesListPage.ActivityTblBody, 0,
                NewConferenceActivity.ActivityName, "a"), "The activity has not been added to the grid");

            /// 4. Click the X button on the activity inside the grid, then click Ok on the popup to delete the activity, and make sure the activity
            /// disappears from the grid
            MCP.DeleteActivityFromGrid(NewConferenceActivity.ActivityName);
            Assert.False(ElemGet.Grid_ContainsRecord(browser, MCP.ActivityTblBody, Bys.MyCPDActivitiesListPage.ActivityTblBody, 0,
                NewConferenceActivity.ActivityName, "a"), "The activity has not been deleted");
        }

        [Test]
        [Description("Given a user completes a non accredited activity, a fixed credit activity, an activity with a non-fixed credit increase rule, " +
            "and an activity that requires validation, When we validate the credit, Then all labels on all tabs within the Mainport application " +
            "should reflect the correct amount of credits per each rule")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void CreditLabelVerification()
        {
            // Not runing firefox right now due to Lifetime support Actions button being a hover dropdown. Revisit this and fix in firefox,
            // Will probably just need to add a line of code to hover over Actions instead of clicking on it. 
            if (BrowserName == BrowserNames.Chrome || BrowserName == BrowserNames.InternetExplorer)
            {
                /// 1. Create a Mainport user and Login
                UserInfo NewUser = UserUtils.CreateAndRegisterUser(UserUtils.Application.Mainport, UserUtils.UserRole.MP);
                LoginPage LP = Navigation.GoToLoginPage(browser);
                MyDashboardPage DP = LP.LoginAsNewUser(UserUtils.UserRole.MP, NewUser.Username, ConfigurationManager.AppSettings["LoginPassword"]);

                /// 2. Add at least 1 activity for each activity type 

                /// 3. Choose 1 activity which cuts the credits in half per hour/article when the No radio button is clicked. 
                EnterCPDActivityPage EAP = DP.ClickAndWait(DP.EnterACPDActivityBtn);
                Activity ConferenceActivity = EAP.AddActivityThenSubmit(Constants.MainportActivityTypes.Sec1_Conference_IfNotAccreditedCutCreditsInHalf, "10", false);

                /// 4. Choose 1 activity which has a fixed amount of credits
                DP.ClickAndWait(DP.EnterACPDActivityBtn);
                Activity FellowshipActivity = EAP.AddActivityThenSubmit(Constants.MainportActivityTypes.Sec2_Fellowship_FixedCredits25);

                /// 5. Choose 1 activity which triples credits
                DP.ClickAndWait(DP.EnterACPDActivityBtn);
                Activity AccreditedSelAssessActivity = EAP.AddActivityThenSubmit(Constants.MainportActivityTypes.Sec3_AccreditedSelfAssessmentPrograms_CreditsTripled, "10");

                /// 6. Choose 1 activity that requires validation
                DP.ClickAndWait(DP.EnterACPDActivityBtn);
                Activity BulkJournalActivity = EAP.AddActivityThenSubmit(Constants.MainportActivityTypes.Sec2_BulkJournalReadingwithTranscript_ValidationRequired, "10");

                /// 7. Log on to the Lifetime Support site and validate the credit
                LSHelp.Login(browser, "lkaveti", "password");
                LSHelp.ValidateCredit(browser, "Royal College of Physicians", NewUser.FullName, "Maintenance of Certification", BulkJournalActivity.ActivityName);

                /// 8. Go back to the Mainport app, and on the My Dashboard page, verify that the Total Credits Submitted and Applied labels reflect the amount of 
                /// credits that were added above. Also verify the "MOC Section Requirements" graph labels get reflected. Based on the above activites, the 
                /// Submitted label should have 5 for Conference, + 25 for fellowship, + 30 for accreditedselfassess, + 10 for bulkjournal. So total we should see 70
                Navigation.GoToMyDashboardPage(browser);
                MainportHelperMethods.WaitForCreditsToBeApplied(browser, DP, Bys.MyDashboardPage.TotalCreditsAppliedValueLbl, "70");
                Assert.AreEqual("70", DP.TotalCreditsSubmittedValueLbl.Text);
                Assert.AreEqual("70", DP.TotalCreditsAppliedValueLbl.Text);
                Assert.AreEqual(string.Format("Section 1Group Learning{0}/25 Credits", ConferenceActivity.Credits),
                    DP.MOCSectionReqsGraphGroupLearningCreditLbl.GetAttribute("textContent"));
                int totalSelfLearningCredits = Int32.Parse(FellowshipActivity.Credits) + Int32.Parse(BulkJournalActivity.Credits); // For the Section 2 Self Learning activities, we completed 2 of them, so we have to add those 2 credits to get the total label
                Assert.AreEqual(string.Format("Section 2Self Learning{0}/25 Credits", totalSelfLearningCredits.ToString()),
                    DP.MOCSectionReqsGraphSelfLearningCreditLbl.GetAttribute("textContent"));
                Assert.AreEqual(string.Format("Section 3Assessment{0}/25 Credits", AccreditedSelAssessActivity.Credits),
                    DP.MOCSectionReqsGraphAssessmentCreditLbl.GetAttribute("textContent"));

                /// 9. On the My MOC page, verify that the credits are reflected in the Overall label, reflected in each Credits Applied to Date labels, 
                /// and in each row under the Credits Reported column
                MyMOCPage MP = DP.ClickAndWaitBasePage(DP.MyMOCTab);
                // Overall label
                Assert.AreEqual("70 of 400", MP.OverallCreditsAppliedLbl.Text);
                // Credits Applied to Date labels
                Assert.AreEqual(ConferenceActivity.Credits + " Credits", MP.GroupLearnTblCreditsAppliedValueLbl.Text);
                Assert.AreEqual(totalSelfLearningCredits.ToString() + " Credits", MP.SelfLearningTblCreditsAppliedValueLbl.Text);
                Assert.AreEqual(AccreditedSelAssessActivity.Credits + " Credits", MP.AssessmentTblCreditsAppliedValueLbl.Text);
                // Credits Reported column
                Assert.AreEqual(ConferenceActivity.Credits, MP.GroupLearnTblUnaccrActRowCredsRptLbl.Text);
                Assert.AreEqual(FellowshipActivity.Credits, MP.SelfLearningTblPlanLearnActRowCredsRptLbl.Text);
                Assert.AreEqual(BulkJournalActivity.Credits, MP.SelfLearningTblScanActRowCredsRptLbl.Text);
                Assert.AreEqual(AccreditedSelAssessActivity.Credits, MP.AssessmentTblKnowledgeAssRowCredsRptLbl.Text);

                /// 10. Click on the View link for one of the activities, verify the credits are reflected correctly on the popup, click the Pencil icon
                /// to view the activity in read-only mode to verify that it appears
                MP.OpenViewActivitiesForm(MP.GroupLearnTblUnaccrActRowViewLnk);
                Assert.AreEqual(ElemGet.Grid_GetCellTextByRowNameAndColumnName(MP.ViewActivitiesFormActivitiesTbl, MP.ViewActivitiesFormActivitiesTblThead,
                    Bys.MyMOCPage.ViewActivitiesFormActivitiesTblBody, ConferenceActivity.ActivityName, "td", "Credits Reported"), ConferenceActivity.Credits);
                MP.ClickAndWait(MP.ViewActivitiesFormCloseBtn);
            }
        }

        [Test]
        [Description("Given a user adds an activity that requires credit validation, Whe a user clicks to the My Holding " +
            "Area tab, that activity should show in the Credits Awaiting Validation table")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ActivityAwaitingValidationShowsInCoorespondingTable()
        {
            /// 1. Create a Mainport user and Login
            UserInfo NewUser = UserUtils.CreateAndRegisterUser(UserUtils.Application.Mainport, UserUtils.UserRole.MP);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            MyDashboardPage DP = LP.LoginAsNewUser(UserUtils.UserRole.MP, NewUser.Username, ConfigurationManager.AppSettings["LoginPassword"]);

            /// 2. Enter an activity which requires credit validation
            EnterCPDActivityPage EAP = DP.ClickAndWait(DP.EnterACPDActivityBtn);
            Activity Actvity = EAP.AddActivityThenSubmit(Constants.MainportActivityTypes.Sec2_BulkJournalReadingwithTranscript_ValidationRequired, "10");

            /// 3. Click the My Holding Area table, then verify that the activity appears in the Activity Awaiting Credit Validation table
            MyHoldingAreaPage MP = DP.ClickAndWaitBasePage(DP.MyHoldingAreaTab);
            Assert.True(ElemGet.Grid_ContainsRecord(browser, MP.AwaitingCredValidationTbl, Bys.MyHoldingAreaPage.AwaitingCredValidationTblBody, 0,
               Actvity.ActivityName, "a"));
        }

        [Test]
        [Description("Given a user completes an activity in each section which fills the required amount of credits for each section's mininum, When the page " +
            "refreshes, Then all labels on all tabs within the Mainport application should reflect that the user has met each section's minimum requirement")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void UIUpdatesAfterUserMeetsSectionCreditMinimumRequirement()
        {
            /// 1. Create a Mainport user and Login
            UserInfo NewUser = UserUtils.CreateAndRegisterUser(UserUtils.Application.Mainport, UserUtils.UserRole.MP);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            MyDashboardPage DP = LP.LoginAsNewUser(UserUtils.UserRole.MP, NewUser.Username, ConfigurationManager.AppSettings["LoginPassword"]);

            /// 2. Submit an activity in each section, ensuring that 25 credits have been met in each section
            EnterCPDActivityPage EAP = DP.ClickAndWait(DP.EnterACPDActivityBtn);
            Activity ConferenceAct = EAP.AddActivityThenSubmit(Constants.MainportActivityTypes.Sec1_Conference_IfNotAccreditedCutCreditsInHalf, "25", true);
            DP.ClickAndWait(DP.EnterACPDActivityBtn);
            Activity FellowshipAct = EAP.AddActivityThenSubmit(Constants.MainportActivityTypes.Sec2_Fellowship_FixedCredits25);
            DP.ClickAndWait(DP.EnterACPDActivityBtn);
            Activity MultiSourceFeedbackAct = EAP.AddActivityThenSubmit(Constants.MainportActivityTypes.Sec3_MultisourceFeedback_CreditsTripled, "10");
            // Have to wait for the credits to be applied before we can proceed
            MainportHelperMethods.WaitForCreditsToBeApplied(browser, DP, Bys.MyDashboardPage.TotalCreditsAppliedValueLbl, "80");

            /// 3. Verify that the MOC Section Requirements labels show as blue/gray, and that the checkmark shows for each box
            Assert.AreEqual("showGrey", DP.MOCSectionReqsGraphGroupLearningSquare.GetAttribute("class"));
            Assert.AreEqual("showGrey", DP.MOCSectionReqsGraphSelfLearningSquare.GetAttribute("class"));
            Assert.AreEqual("showGrey", DP.MOCSectionReqsGraphAssessmentSquare.GetAttribute("class"));
            Assert.True(DP.MOCSectionReqsGraphGroupLearningTickBox.Displayed);
            Assert.True(DP.MOCSectionReqsGraphSelfLearningTickBox.Displayed);
            Assert.True(DP.MOCSectionReqsGraphAssessmentTickBox.Displayed);

            /// 4. Go to the My MOC tab and verify that the "section minimum requirement was met" label is shown
            DP.ClickAndWaitBasePage(DP.MyMOCTab);
            Assert.True(Browser.Exists(Bys.MyMOCPage.GroupLearnTblYouHaveMetMinCredsLbl, ElementCriteria.IsVisible));
            Assert.True(Browser.Exists(Bys.MyMOCPage.SelfLearningTblYouHaveMetMinCredsLbl, ElementCriteria.IsVisible));
            Assert.True(Browser.Exists(Bys.MyMOCPage.AssessmentTblYouHaveMetMinCredsLbl, ElementCriteria.IsVisible));
        }

        #endregion Tests
    }
}

