using Browser.Core.Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using RCP.AppFramework;
using System.Data;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using Browser.Core.Framework.Utils;
using System.Diagnostics;
using OpenQA.Selenium.Firefox;
using System.Configuration;

/// <summary>
/// These tests represent tests that can not be run in parallel. This separate class is needed because of the NUnit bug that causes browser
/// sessions to hang when using the [NonParallelizable] attribute above test methods. The latest version does not have a fix, so we have to
/// create this separate class and use the [NonParallelizable] attribute at the Test Fixture level. When there is a fix, we can put these 
/// back in their respective classes
/// </summary>
namespace RCP.UITest
{
    [BrowserMode(BrowserMode.New)]
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [LocalSeleniumTestFixture(BrowserNames.Firefox)]
    [LocalSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Firefox)]
    [RemoteSeleniumTestFixture(BrowserNames.InternetExplorer)]
    // These tests can't be run in parallel for various different reasons. See the comment above each test method for an explanation for each one.
    [NonParallelizable]
    [TestFixture]
    public class RCP_Nonparallel_Tests : TestBase
    {
        #region Constructors
        public RCP_Nonparallel_Tests(string browserName) : base(browserName) { }

        // Remote Selenium Grid Test
        public RCP_Nonparallel_Tests(string browserName, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region properties
        /// <summary>
        /// Use these properties inside your test methods when calling <see cref="UserUtils.CreateAndRegisterUser(null, UserUtils.User, UserUtils.UserRole)"/>
        /// </summary>
        public UserInfo LRUser;
        public UserInfo OBUser;
        public UserInfo PAUser;

        /// <summary>
        /// These properties represent the usernames for static users in the database that we use in certain test methods when we want to run tests in 
        /// parallel across browsers where we otherwise couldnt do that because using a single user would cross paths per browser. Also this specific
        /// learner1 user should only be able to be used in LearnerStageLabelReflectsPromotion(). If you need to setup for more users for other methodsz
        /// then do so
        /// </summary>
        public string learner1LoginPerBrowser { get; set; }
        public string learner1FullNamePerBrowser { get; set; }
        #endregion properties

        #region testfixtures

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

        /// <summary>
        /// Using this to store usernames into variables per browser
        /// </summary>
        [OneTimeSetUp]
        public void setup()
        {
            // First assign the learner username per browser. This is so that we can run these tests in parallel. Note that this test still may fail
            // when running in parallel, see the comment above the NonParallelizable attribute above. If this happens.
            if (BrowserName == BrowserNames.Chrome)
            { learner1LoginPerBrowser = UserUtils.LearnerCH1Login; learner1FullNamePerBrowser = UserUtils.LearnerCH1FullName; }
            if (BrowserName == BrowserNames.InternetExplorer)
            { learner1LoginPerBrowser = UserUtils.LearnerIE1Login; learner1FullNamePerBrowser = UserUtils.LearnerIE1FullName; }
            if (BrowserName == BrowserNames.Firefox)
            { learner1LoginPerBrowser = UserUtils.LearnerFF1Login; learner1FullNamePerBrowser = UserUtils.LearnerFF1FullName; }
        }

        /// <summary>
        /// This method will run for every test in this class. If any of the below users were created, it wil delete them.
        /// TODO: Refactor this so that it gets called at the entire UITest project level. So ill have to move it to a new
        /// class maybe, or something else. NOTE that it may not work when running in parallel. Will have to monitor when 
        /// implemented
        /// </summary>
        //[TestFixtureTearDown]
        public void DeleteUserIfCreated()
        {
            if (LRUser != null)
            {
                UserUtils.DeleteUser(LRUser.Username);
            }
            if (OBUser != null)
            {
                UserUtils.DeleteUser(OBUser.Username);
            }
            if (PAUser != null)
            {
                UserUtils.DeleteUser(PAUser.Username);
            }
        }
        #endregion testfixtures

        #region tests

        [Test]
        [Description("Given a learner is in stage 2, when a program admin promotes a learner to stage 3, then the learner page reflects this change")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        // This test cant be run in parallel across browsers because agendas in the Meeting Agenda dropdown may get mixed up between parallel runs. 
        // See RCPSC-547 for the status of a redesign that would fix that
        public void LearnerStageLabelReflectsPromotion()
        {
            // Only running this test in Chrome because it is a long test and we can not run it in parallel. Whenever JIRA task
            // https://code.premierinc.com/issues/browse/RCPSC-547 is complete, we can run this test in parallel because 
            // the UI will be redesigned so that the agendas will not get mixed up during parallel runs
            if (BrowserName == BrowserNames.Chrome)
            {
                /// 1. Login as a program admin
                LoginPage LP = Navigation.GoToLoginPage(browser);
                CBDProgAdminPage PP = LP.LoginAsExistingUser(UserUtils.UserRole.PA, UserUtils.ProgAdmin1Login, ConfigurationManager.AppSettings["LoginPassword"]);

                /// 2. Create an upcoming agenda, then choose this agenda in the Meeting Agenda dropdown
                /// 4. Click on the Set Status button and then set the status of the learner to stage 2 
                /// 5. Finalize the Agenda
                PP.CreateChangeAndFinalizeAgendaForLearner(learner1FullNamePerBrowser, "Reviewed", "High", "_TA_AStatic User_CC_001", "Progressing as Expected",
                    "Promote Learner - to Stage 3");

                /// 6. Log out then log back in as the learner
                PP.ClickAndWaitBasePage(PP.LogoutLnk);
                Navigation.GoToLoginPage(browser);
                CBDLearnerPage CLP = LP.LoginAsExistingUser(UserUtils.UserRole.LR, learner1LoginPerBrowser, ConfigurationManager.AppSettings["LoginPassword"]);

                /// 7. Assert that the text to the right of the Program label reflects the change to stage 2
                Assert.True(CLP.CurrentStageValueLbl.Text.Contains("Core of Discipline"));

                // Cleanup data by setting the learner back to stage 2
                PP.ClickAndWaitBasePage(PP.LogoutLnk);
                Navigation.GoToLoginPage(browser);
                LP.LoginAsExistingUser(UserUtils.UserRole.PA, UserUtils.ProgAdmin1Login, ConfigurationManager.AppSettings["LoginPassword"]);
                PP.CreateChangeAndFinalizeAgendaForLearner(learner1FullNamePerBrowser, null, null, null, "Progressing as Expected",
                    "Promote Learner - to Stage 2");
            }
        }

        [Test]
        [Description("Workflow test making sure nothing goes wrong when a prog director creates, edits a learner and finalizes an agenda")]
        [Property("Status", "In Progress")]
        [Author("Mike Johnston")]
        [Category("test1")]
        // This test cant be run in parallel across browsers because agendas in the Meeting Agenda dropdown may get mixed up between parallel runs. 
        // See RCPSC-547 for the status of a redesign that would fix that
        public void ProgDirectorCanCreateEditAndFinalizeAgenda()
        {
            // Only running this test in Chrome because it is a long test and we can not run it in parallel. Whenever JIRA task
            // https://code.premierinc.com/issues/browse/RCPSC-547 is complete, we can run this test in parallel because 
            // the UI will be redesigned so that the agendas will not get mixed up during parallel runs
            if (BrowserName == BrowserNames.Chrome)
            {
                /// 1. Login as a program director
                LoginPage LP = Navigation.GoToLoginPage(browser);
                CBDProgDirectorPage PP = LP.LoginAsExistingUser(UserUtils.UserRole.PD, UserUtils.ProgDirector1Login, ConfigurationManager.AppSettings["LoginPassword"]);

                /// 2. Create an upcoming agenda, then choose this agenda in the Meeting Agenda dropdown
                /// 4. Click on the Set Status button and then set the status of the learner to stage 23
                /// 5. Finalize the Agenda
                PP.CreateChangeAndFinalizeAgendaForLearner(learner1FullNamePerBrowser, "Reviewed", "High", "_TA_AStatic User_CC_001", "Progressing as Expected",
                    "Promote Learner - to Stage 3");

                // Cleanup data by setting the learner back to stage 2
                PP.CreateChangeAndFinalizeAgendaForLearner(learner1FullNamePerBrowser, null, null, null, "Progressing as Expected",
                    "Promote Learner - to Stage 2");
            }

        }
        #endregion Tests
    }
}







