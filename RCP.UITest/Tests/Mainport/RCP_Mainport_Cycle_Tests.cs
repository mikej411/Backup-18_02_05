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
using LS.AppFramework.Constants;
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
    public class RCP_Mainport_Cycle_Tests : TestBase
    {
        #region Constructors
        public RCP_Mainport_Cycle_Tests(string browserName) : base(browserName) { }

        // Remote Selenium Grid Test
        public RCP_Mainport_Cycle_Tests(string browserName, string version, string platform, string hubUri, string extrasUri)
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

        // Test is somewhat not relevant because true cycle advancement testing depends on overnight windows service, but
        // am keeping this test because it tests that a user can manually advance through lifetime support if they want to
        //[Test]
        [Description("Given a Mainport user is in Main 1 cycle and enters an activity totalling 400 total credits, When a user looks at the Details " +
            "page in Lifetime Support, Then the Status label should say Complete, and When the Lifetime Support user adds a Main Program cycle adjustment, " +
            "Then the Lifetime Support tool should show that the Mainport user advanced to Main 2, and When the Mainport user logs back into Mainport, " +
            "the UI should reflect a new cycle ")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void MainCycleAdvancesToMain2CycleAfter400Credits()
        {
            // Not running this test on Feb 1st of any year, because Mainport has some weird gracce period rule where the user gets auto-advanced without
            // the Admin having to manually advance him, so it messes with the test
            if (DateTime.Now.Date.ToString() != "2/1/2018 12:00:00 AM")
            {
                /// 1. Create a Mainport user, which has a cycle start date 6 years prior, then Login. We are using a date 6 years in the past because
                /// the end date of the cycle needs to be a past date for the user to be able to advance, and the Main cycle is 5 years
                UserInfo NewUser = UserUtils.CreateAndRegisterUser(UserUtils.Application.Mainport, UserUtils.UserRole.MP, null, null,
                DateTime.Now.AddYears(-6).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
                LoginPage LP = Navigation.GoToLoginPage(browser);
                MyDashboardPage DP = LP.LoginAsNewUser(UserUtils.UserRole.MP, NewUser.Username, ConfigurationManager.AppSettings["LoginPassword"]);

                /// 2. Submit an activity totalling 400 credits, Enter an activity date that falls within the cycle date range. 
                /// NOTE: If the activity date falls within the cycle date's last year for a Main cycle, then we wont need to credit validate
                string actDate = DateTime.Now.AddYears(-1).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                EnterCPDActivityPage EAP = DP.ClickAndWait(DP.EnterACPDActivityBtn);
                Activity ConferenceAct = EAP.AddActivityThenSubmit(Constants.MainportActivityTypes.Sec1_Conference_IfNotAccreditedCutCreditsInHalf, "400", true, actDate);

                /// 3. Login to Lifetime Support and validate the credit that needs validated, if applicable. See note above in step 2 about credit validation. 
                /// Depending on what day of the year this test is run, it may need credit validation
                LSHelp.Login(browser, "lkaveti", "password");
                LSHelp.GoToParticipantProgramPage(browser, "Royal College of Physicians", NewUser.FullName, "Maintenance of Certification");
                MainportHelperMethods.ValidateCreditsIfApplicable(browser, NewUser, ConferenceAct);

                /// 4. Click on the Details tab and verify that the UI displays Complete for the Status label
                // First we have to wait for the windows service to process the credits.
                LSHelp.WaitForProgramCreditsWindowsService(browser, "400 (of 400)");
                Assert.AreEqual("Complete", LSHelp.GetProgramDetail(browser, "Status"));

                /// 5. Add a Main cycle adjustment, then verify that the UI displays In Progress for the Status label, and "Main 2nd or Later Cycle" for the
                /// program label. NOTE: We can not automate the "automatic" cycle advacement because it relies on a windows service that gets run overnight.
                /// Going from Main 1 to Main2/EXT, etc. usually occurs automatically for clients with the overnight windows service. We are sort of hacking 
                /// the system by manually setting this user to Main 2, and not truly testing that a user gets put into Main 2 once he meets his credit
                /// minimum. Basically, you can place a user into any cycle through LTS, even if the user did not meet credit minimum
                LSHelp.RCP_AddProgramAdjustment(browser, NewUser.FullName, LSConstants.AdjustmentCodes.MainProgram, DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
                Assert.AreEqual(DateTime.Now.ToString("M/d/yyyy", CultureInfo.InvariantCulture), LSHelp.GetProgramDetail(browser, "Starts"));
                Assert.AreEqual("Main 2nd or Later Cycle", LSHelp.GetProgramDetail(browser, "Program"));

                /// 6. Go back to Mainport and verify that the user is in a new cycle by checking that the credits reset to 0
                Navigation.GoToMyDashboardPage(browser);
                Assert.AreEqual("0", DP.TotalCreditsSubmittedValueLbl.Text);
                Assert.AreEqual("0", DP.TotalCreditsAppliedValueLbl.Text);
            }        
        }

        // Test is somewhat not relevant because true cycle advancement testing depends on overnight windows service, but
        // am keeping this test because it tests that a user can manually advance through lifetime support if they want to
        //[Test]
        [Description("Given a Mainport user is in Main 1 cycle and enters an activity totalling 399 total credits, When a user looks at the Details " +
            "page in Lifetime Support, Then the Status label should say In Progress")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void MainCyclNotCompleteAfter399Credits()
        {
            /// 1. Create a Mainport user, which has a cycle start date 6 years prior, then Login. We are using a date 6 years in the past because
            /// the end date of the cycle needs to be a past date for the user to be able to advance, and the Main cycle is 5 years
            UserInfo NewUser = UserUtils.CreateAndRegisterUser(UserUtils.Application.Mainport, UserUtils.UserRole.MP, null, null,
                DateTime.Now.AddYears(-6).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
            LoginPage LP = Navigation.GoToLoginPage(browser);
            MyDashboardPage DP = LP.LoginAsNewUser(UserUtils.UserRole.MP, NewUser.Username, ConfigurationManager.AppSettings["LoginPassword"]);

            /// 2. Submit an activity totalling 399 credits, Enter an activity date that falls within the cycle date range. 
            /// NOTE: If the activity date falls within cycle date's the last year for a Main cycle, then we wont need to credit validate
            string actDate = DateTime.Now.AddYears(-1).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            EnterCPDActivityPage EAP = DP.ClickAndWait(DP.EnterACPDActivityBtn);
            Activity ConferenceAct = EAP.AddActivityThenSubmit(Constants.MainportActivityTypes.Sec1_Conference_IfNotAccreditedCutCreditsInHalf, "399", true, actDate);

            /// 3. Login to Lifetime Support and validate the credits that need validated, if applicable. See note above in step 2 about credit validation. 
            /// Depending on what day of the year this test is run, it may need credit validation
            LSHelp.Login(browser, "lkaveti", "password");
            LSHelp.GoToParticipantProgramPage(browser, "Royal College of Physicians", NewUser.FullName, "Maintenance of Certification");
            MainportHelperMethods.ValidateCreditsIfApplicable(browser, NewUser, ConferenceAct);

            /// 4. Click on the Details tab and verify that the UI displays In Progress for the Status label, since we did not reach the 400 credits
            // First we have to wait for the windows service to process the credits.
            LSHelp.WaitForProgramCreditsWindowsService(browser, "399 (of 400)");
            Assert.AreEqual("In Progress", LSHelp.GetProgramDetail(browser, "Status"));
        }

        // Test is not relevant because true cycle advancement testing depends on overnight windows service, so as a team,
        // we decided to not automate this, as it is theoretically impossible. Maybe revisit sometime in future if App 
        // design changes
        //[Test]
        [Description("Given a Mainport user is in the PRA cycle and enters an activity totalling 79 total credits, When we go to the user's Program page " +
            "in Lifetime Support, Then the Lifetime Support tool should show In Progress, not Complete, for the cycle status")]
        [Property("Status", "In Progress")]
        [Author("Mike Johnston")]
        public void PRACycleNotCompleteAfter79Credits()
        {
            /// 1. Create a Mainport user, then place the user in the PRA cycle through LTS
            UserInfo NewUser = UserUtils.CreateAndRegisterUser(UserUtils.Application.Mainport, UserUtils.UserRole.MP);
            LSHelp.Login(browser, "lkaveti", "password");
            LSHelp.GoToParticipantProgramPage(browser, "Royal College of Physicians", NewUser.FullName, "Maintenance of Certification");
            LSHelp.RCP_AddProgramAdjustment(browser, NewUser.FullName, LSConstants.AdjustmentCodes.PRA);

            /// 2. Custom date the cycle end date so that it is in the past. This is so that the user is able to be marked as Complete (Sort of hacking
            /// the system). First, We have to move the start date back
            LSHelp.RCP_AdjustMOCDate(browser, NewUser.FullName, "Start", DateTime.Now.AddYears(-2).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
            LSHelp.RCP_AdjustMOCDate(browser, NewUser.FullName, "End", DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));

            /// 3. Login to Mainport and submit an activity totalling 79 credits. Enter an activity date that falls within the cycle date range.
            string actDate = DateTime.Now.AddMonths(-13).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            MyDashboardPage DP = LP.LoginAsNewUser(UserUtils.UserRole.MP, NewUser.Username, ConfigurationManager.AppSettings["LoginPassword"]);
            EnterCPDActivityPage EAP = DP.ClickAndWait(DP.EnterACPDActivityBtn);
            Activity ConferenceAct = EAP.AddActivityThenSubmit(Constants.MainportActivityTypes.Sec1_Conference_IfNotAccreditedCutCreditsInHalf, "79", true,
                actDate);

            /// 4. Login to Lifetime Support and validate the credits
            LSHelp.Login(browser, "lkaveti", "password");
            LSHelp.ValidateCredit(browser, "Royal College of Physicians", NewUser.FullName, "Maintenance of Certification", ConferenceAct.ActivityName);

            /// 5. Click on the Details tab and verify that the UI displays In Progress for the Status label
            // First we have to wait for the windows service to process the credits.
            LSHelp.WaitForProgramCreditsWindowsService(browser, "79 (of 80)");
            string actualStatusLblValue = LSHelp.GetProgramDetail(browser, "Status");
            Assert.AreEqual("In Progress", LSHelp.GetProgramDetail(browser, "Status"), string.Format("The Status label did not show 'In Progress' for " +
                "a user who only completed 79 out of 80 credits in a PRA cycle. The label printed '{0}' instead", actualStatusLblValue));
        }

        // Test is not relevant because true cycle advancement testing depends on overnight windows service, so as a team,
        // we decided to not automate this, as it is theoretically impossible. Maybe revisit sometime in future if App 
        // design changes
        //[Test] 
        [Description("Given a Mainport user is in the PRA cycle and enters an activity totalling 80 total credits, When we go to the user's Program page " +
         "in Lifetime Support, Then the Lifetime Support tool should show Complete for the cycle status")]
        [Property("Status", "In Progress")]
        [Author("Mike Johnston")]
        public void PRACycleCompleteAfter80Credits()
        {
            /// 1. Create a Mainport user, then place the user in the PRA cycle through LTS
            UserInfo NewUser = UserUtils.CreateAndRegisterUser(UserUtils.Application.Mainport, UserUtils.UserRole.MP);
            LSHelp.Login(browser, "lkaveti", "password");
            LSHelp.GoToParticipantProgramPage(browser, "Royal College of Physicians", NewUser.FullName, "Maintenance of Certification");
            LSHelp.RCP_AddProgramAdjustment(browser, NewUser.FullName, LSConstants.AdjustmentCodes.PRA);

            /// 2. Custom date the cycle end date so that it is in the past. This is so that the user is able to be marked as Complete (Sort of hacking
            /// the system). First, We have to move the start date back
            LSHelp.RCP_AdjustMOCDate(browser, NewUser.FullName, "Start", DateTime.Now.AddYears(-2).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
            LSHelp.RCP_AdjustMOCDate(browser, NewUser.FullName, "End", DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));

            /// 3. Login to Mainport and submit an activity totalling 80 credits. Enter an activity date that falls within the cycle date range.
            string actDate = DateTime.Now.AddMonths(-13).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            MyDashboardPage DP = LP.LoginAsNewUser(UserUtils.UserRole.MP, NewUser.Username, ConfigurationManager.AppSettings["LoginPassword"]);
            EnterCPDActivityPage EAP = DP.ClickAndWait(DP.EnterACPDActivityBtn);
            Activity ConferenceAct = EAP.AddActivityThenSubmit(Constants.MainportActivityTypes.Sec1_Conference_IfNotAccreditedCutCreditsInHalf, "80", true,
                actDate);

            /// 4. Login to Lifetime Support and validate the credits
            LSHelp.Login(browser, "lkaveti", "password");
            LSHelp.ValidateCredit(browser, "Royal College of Physicians", NewUser.FullName, "Maintenance of Certification", ConferenceAct.ActivityName);

            /// 5. Click on the Details tab and verify that the UI displays Complete for the Status label
            // First we have to wait for the windows service to process the credits.
            LSHelp.WaitForProgramCreditsWindowsService(browser, "80 (of 80)");
            Assert.AreEqual("Complete", LSHelp.GetProgramDetail(browser, "Status"));
        }

        #endregion Tests
    }
}

