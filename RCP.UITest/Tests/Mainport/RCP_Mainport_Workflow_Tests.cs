using Browser.Core.Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using RCP.AppFramework;
using System;
using System.Threading;
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
    public class RCP_Mainport_Workflow_Tests : TestBase
    {
        #region Constructors
        public RCP_Mainport_Workflow_Tests(string browserName) : base(browserName) { }

        // Remote Selenium Grid Test
        public RCP_Mainport_Workflow_Tests(string browserName, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, version, platform, hubUri, extrasUri)
        { }
        #endregion

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
        [Description("Given a user is on the Dashboard, When a user completes every single activity that is available in the system, nothing should " +
            "go wrong")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void UserCanCompleteAllActivities()
        {
            // Only going to run this in Chrome. I create activities in other test methods, so we already cross-browser test that an activity can be
            // completed in all browsers in those tests. If we were to run this method in all browsers, especially IE, it would just increase run time
            // unneccessarily
            if (BrowserName == BrowserNames.Chrome)
            {
                /// 1. Create a Mainport user and Login
                UserInfo NewUser = UserUtils.CreateAndRegisterUser(UserUtils.Application.Mainport, UserUtils.UserRole.MP);
                LoginPage LP = Navigation.GoToLoginPage(browser);
                MyDashboardPage DP = LP.LoginAsNewUser(UserUtils.UserRole.MP, NewUser.Username, ConfigurationManager.AppSettings["LoginPassword"]);

                /// 2. Add 1 activity for all of section 1's activity types
                /// 3. Add 1 activity for all of section 2's activity types
                /// 4. Add 1 activity for all of section 3's activity types
                foreach (Constants.MainportActivityTypes activityType in Enum.GetValues(typeof(Constants.MainportActivityTypes)))
                {
                    // This will fail until https://code.premierinc.com/issues/browse/RCPSC-772 is fixed. Once fixed, then make sure the Reading dropdown
                    // By is updated and run the test removing this If statement
                    if (activityType != Constants.MainportActivityTypes.Sec2_Reading_FixedCredits1)
                    {
                        EnterCPDActivityPage EAP = DP.ClickAndWait(DP.EnterACPDActivityBtn);
                        EAP.AddActivityThenSubmit(activityType, "10", true);
                    }
                }
            }
        }

        [Test]
        [Description("Given a user enters an activity, When the user clicks to the MyMOC tab and edits the activity, Then this should occur without issue, and " +
            "Then the edited information (Activity name, accredited or not, amount of credits) should be reflected on the MyMOC tables, as well as the " +
            "MyDashboard page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void UserCanEditActivityOnMyMOCPage()
        {
            /// 1. Create a Mainport user and Login
            UserInfo NewUser = UserUtils.CreateAndRegisterUser(UserUtils.Application.Mainport, UserUtils.UserRole.MP);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            MyDashboardPage DP = LP.LoginAsNewUser(UserUtils.UserRole.MP, NewUser.Username, ConfigurationManager.AppSettings["LoginPassword"]);

            /// 2. Enter an activity
            EnterCPDActivityPage EAP = DP.ClickAndWait(DP.EnterACPDActivityBtn);
            Activity NewConferenceActivity = EAP.AddActivityThenSubmit(Constants.MainportActivityTypes.Sec1_Conference_IfNotAccreditedCutCreditsInHalf, "20", true);

            /// 3. Click to the MyMOC page, click on the View link for the activity that was created, click on the Pencil icon in the Activities table on the popup, 
            /// then verify that the information on this form is correct
            // Gotta wait for the windows service first to apply the credits
            MainportHelperMethods.WaitForCreditsToBeApplied(browser, DP, Bys.MyDashboardPage.TotalCreditsAppliedValueLbl, "20");
            MyMOCPage MP = DP.ClickAndWaitBasePage(DP.MyMOCTab);
            MP.OpenEditActivityForm(MP.GroupLearnTblAccrActRowViewLnk, NewConferenceActivity.ActivityName);

            /// 4. Edit the activity. Change accredited to not accredited. Change the credits from 20 to 10. Change the name. Close the form. Verify that the 
            /// amount of credits gets updated on all related labels, verify that the activity was moved from the accredited table to the not accredited table,
            /// and verify the name change
            Activity EditedConferenceActivity = EAP.EditActivity(Constants.MainportActivityTypes.Sec1_Conference_IfNotAccreditedCutCreditsInHalf, "10", false);
            Browser.SwitchTo().DefaultContent();
            MP.ClickAndWaitBasePage(MP.MyDashboardTab);
            MainportHelperMethods.WaitForCreditsToBeApplied(browser, DP, Bys.MyDashboardPage.TotalCreditsAppliedValueLbl, "5");
            // Overall label
            DP.ClickAndWaitBasePage(DP.MyMOCTab);
            Assert.AreEqual("5 of 400", MP.OverallCreditsAppliedLbl.Text);
            // Credits Applied to Date labels
            Assert.AreEqual(EditedConferenceActivity.Credits + " Credits", MP.GroupLearnTblCreditsAppliedValueLbl.Text);
            // Credits Reported column
            Assert.AreEqual("-", MP.GroupLearnTblAccrActRowCredsRptLbl.Text);
            Assert.AreEqual(EditedConferenceActivity.Credits, MP.GroupLearnTblUnaccrActRowCredsRptLbl.Text);

            /// 5. Click back to the Dashboard page and verify that the credit amount change is reflected there
            MP.ClickAndWaitBasePage(MP.MyDashboardTab);
            Assert.AreEqual("5", DP.TotalCreditsSubmittedValueLbl.Text);
            Assert.AreEqual("5", DP.TotalCreditsAppliedValueLbl.Text);
        }

        [Test]
        [Description("Given a user enters 2 activies, When the user clicks to the MyMOC tab, opens the activity from the View link, and tries to delete an " +
            "activity, Then this should occur without issue, and the deletion should be reflected on the page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void UserCanDeleteActivityOnMyMOCPage()
        {
            /// 1. Create a Mainport user and Login
            UserInfo NewUser = UserUtils.CreateAndRegisterUser(UserUtils.Application.Mainport, UserUtils.UserRole.MP);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            MyDashboardPage DP = LP.LoginAsNewUser(UserUtils.UserRole.MP, NewUser.Username, ConfigurationManager.AppSettings["LoginPassword"]);

            /// 2. Enter 2 activities
            EnterCPDActivityPage EAP = DP.ClickAndWait(DP.EnterACPDActivityBtn);
            Activity ConferenceActivity = EAP.AddActivityThenSubmit(Constants.MainportActivityTypes.Sec1_Conference_IfNotAccreditedCutCreditsInHalf, "10", false);
            DP.ClickAndWait(DP.EnterACPDActivityBtn);
            Activity ConferenceActivity2 = EAP.AddActivityThenSubmit(Constants.MainportActivityTypes.Sec1_Conference_IfNotAccreditedCutCreditsInHalf, "10", false);
            // Have to wait for the credits to be applied before we can proceed
            MainportHelperMethods.WaitForCreditsToBeApplied(browser, DP, Bys.MyDashboardPage.TotalCreditsAppliedValueLbl, "10");

            /// 3. On the My MOC page, open the View Activity form, click the X button for an activity, and verify that it got removed from the table
            MyMOCPage MP = DP.ClickAndWaitBasePage(DP.MyMOCTab);
            MP.OpenViewActivitiesForm(MP.GroupLearnTblUnaccrActRowViewLnk);
            IWebElement row = ElemGet.Grid_GetRowByRowName(MP.ViewActivitiesFormActivitiesTbl, Bys.MyMOCPage.ViewActivitiesFormActivitiesTblBodyRow,
                ConferenceActivity.ActivityName, "td");
            ElemSet.Grid_ClickElementWithoutTextInsideRow(row, "input");
            MP.ClickAndWait(MP.DeleteActivityWarningFormYesBtn);
            Assert.False(ElemGet.Grid_ContainsRecord(Browser, MP.ViewActivitiesFormActivitiesTbl, Bys.MyMOCPage.ViewActivitiesFormActivitiesTblBodyRow, 0,
                ConferenceActivity.ActivityName, "td"));
        }

        [Test]
        [Description("Given a user is on the My Dashboard page, When a user clicks the Add a Goal button and enters information on the form and clicks " +
            "Next then Close, Then the goal will appear in the Goals table")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void UserCanCreateAGoal()
        {
            // Remove this if statement when bug https://code.premierinc.com/issues/browse/RCPSC-793 is fixed
            if (BrowserName == BrowserNames.Chrome)
            {
                /// 1. Create a Mainport user and Login
                UserInfo NewUser = UserUtils.CreateAndRegisterUser(UserUtils.Application.Mainport, UserUtils.UserRole.MP);
                LoginPage LP = Navigation.GoToLoginPage(browser);
                MyDashboardPage DP = LP.LoginAsNewUser(UserUtils.UserRole.MP, NewUser.Username, ConfigurationManager.AppSettings["LoginPassword"]);

                /// 2. Click the Add a Goal button, fill in the information, click Next, then click Close
                string goalName = DP.AddAGoal();

                /// 3. Verify that the goals table populates with this goal
                Assert.True(ElemGet.Grid_ContainsRecord(browser, DP.GoalsTbl, Bys.MyDashboardPage.GoalsTblBody, 0, goalName, "span"));

            }
        }

        [Test]
        [Description("Given a user creates an activity, When the user clicks the Send To Holding Area button on the Activity form, Then the activity should show " +
            "in the My Holding Area table and also the Incomplete Activities table, and When the user completes the activity, the activity should disappear from " +
            "the Incomplete Activities table")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void UserCanSendActivityToHoldingAreaThenCompleteActivity()
        {
            /// 1. Create a Mainport user and Login
            UserInfo NewUser = UserUtils.CreateAndRegisterUser(UserUtils.Application.Mainport, UserUtils.UserRole.MP);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            MyDashboardPage DP = LP.LoginAsNewUser(UserUtils.UserRole.MP, NewUser.Username, ConfigurationManager.AppSettings["LoginPassword"]);

            /// 2. Click the Enter a CPD Activity button, fill in the fields, click Send to Holding Area, then click Close
            /// Do this again for a second activity
            EnterCPDActivityPage EAP = DP.ClickAndWait(DP.EnterACPDActivityBtn);
            Activity ConferenceActivity = EAP.AddActivityThenSendToHoldingArea(Constants.MainportActivityTypes.Sec1_Conference_IfNotAccreditedCutCreditsInHalf,
                "10", true);
            DP.ClickAndWait(DP.EnterACPDActivityBtn);
            Activity ConferenceActivity2 = EAP.AddActivityThenSendToHoldingArea(Constants.MainportActivityTypes.Sec1_Conference_IfNotAccreditedCutCreditsInHalf,
                "10", true);

            /// 3. Verify that the activity shows in the My Holding Area table
            Assert.True(ElemGet.Grid_ContainsRecord(browser, DP.MyHoldingAreaTbl, Bys.MyDashboardPage.MyHoldingAreaTblBody, 0,
                ConferenceActivity2.ActivityName, "span"));

            /// 4. Click the View More button underneath the My Holding Area table, then verify that the activity is showing in the 
            /// Incomplete Activities. Click Complete Activity, then click the Continue button and complete the activity and verify 
            /// that it disappears from the Incomplete Activites table
            MyHoldingAreaPage MP = DP.ClickAndWait(DP.ViewMoreBtn);
            Assert.True(ElemGet.Grid_ContainsRecord(browser, MP.IncompleteActivitiesTbl, Bys.MyHoldingAreaPage.IncompleteActivitiesTblBody, 0,
                ConferenceActivity2.ActivityName, "a"));
            MP.ClickCompleteActivityBtn(MP.IncompleteActivitiesTbl, Bys.MyHoldingAreaPage.IncompleteActivitiesTblBodyRow, ConferenceActivity2.ActivityName);
            // Not running this in firefox. For some reason, when I enter the date in the above AddActivityThenSendToHoldingArea method, the date enters fine
            // and the activity completes, but when I open the activity from this window, then the date isnt there, so this is an automation bug. Revisit
            if (BrowserName != BrowserNames.Firefox)
            {
                EAP.ClickAndWait(EAP.ContinueBtn);
                EAP.ClickAndWait(EAP.OptionalTabSubmitBtn);
                EAP.CloseBtn.Click();
                // Very tired right now. Add dynamic wait here later
                Thread.Sleep(5000);
                browser.SwitchTo().DefaultContent();
                Assert.False(ElemGet.Grid_ContainsRecord(browser, MP.IncompleteActivitiesTbl, Bys.MyHoldingAreaPage.IncompleteActivitiesTblBody, 0,
                    ConferenceActivity2.ActivityName, "a"));
            }
        }

        [Test]
        [Description("Given a user clicks on the X button in the Incomplete Activities table on the My Holding Area tab, When the user clicks the Yes " +
            "button on the Delete Activity form, then the activity should disappear form the Incomplete Activities table")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void UserCanDeleteActivityFromMyHoldingArea()
        {
            /// 1. Create a Mainport user and Login
            UserInfo NewUser = UserUtils.CreateAndRegisterUser(UserUtils.Application.Mainport, UserUtils.UserRole.MP);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            MyDashboardPage DP = LP.LoginAsNewUser(UserUtils.UserRole.MP, NewUser.Username, ConfigurationManager.AppSettings["LoginPassword"]);

            /// 2. Click the Enter a CPD Activity button, fill in the fields, click Continue, then click Send to Holding Area, then click Close
            EnterCPDActivityPage EAP = DP.ClickAndWait(DP.EnterACPDActivityBtn);
            Activity ConferenceActivity = EAP.AddActivityThenSendToHoldingArea(Constants.MainportActivityTypes.Sec1_Conference_IfNotAccreditedCutCreditsInHalf,
                "10", true);

            /// 3. Click the My Holding Area tab, click the X button to delete the activity, then verify that it disappears from the Incomplete Activites table
            MyHoldingAreaPage MP = DP.ClickAndWaitBasePage(DP.MyHoldingAreaTab);
            MP.DeleteActivity(ConferenceActivity.ActivityName);
            Assert.False(ElemGet.Grid_ContainsRecord(browser, MP.IncompleteActivitiesTbl, Bys.MyHoldingAreaPage.IncompleteActivitiesTblBody, 0,
                ConferenceActivity.ActivityName, "a"));
        }

        //[Test]
        public void sandboxtest()
        {
            UserInfo NewUser = UserUtils.CreateAndRegisterUser(UserUtils.Application.Mainport, UserUtils.UserRole.MP);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            MyDashboardPage DP = LP.LoginAsNewUser(UserUtils.UserRole.MP, NewUser.Username, ConfigurationManager.AppSettings["LoginPassword"]);

            EnterCPDActivityPage EAP = DP.ClickAndWait(DP.EnterACPDActivityBtn);
            Activity ConferenceActivity = EAP.AddActivityThenSendToHoldingArea(Constants.MainportActivityTypes.Sec1_Conference_IfNotAccreditedCutCreditsInHalf,
                "10", true);


        }

        #endregion Tests
    }
}

