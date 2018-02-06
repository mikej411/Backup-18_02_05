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
using System.IO;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;
using System.Configuration;

namespace RCP.UITest
{
    [BrowserMode(BrowserMode.New)]
    //[LocalSeleniumTestFixture(BrowserNames.Chrome)]
    //[LocalSeleniumTestFixture(BrowserNames.Firefox)]
    //[LocalSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Firefox)]
    [RemoteSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [TestFixture]
    public class RCP_CBD_ProgDirectorWorkflow_Tests : TestBase
    {
        #region Constructors
        public RCP_CBD_ProgDirectorWorkflow_Tests(string browserName) : base(browserName) { }

        // Remote Selenium Grid Test
        public RCP_CBD_ProgDirectorWorkflow_Tests(string browserName, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region properties
        /// <summary>
        /// Use these properties inside your test methods when calling <see cref="UserUtils.CreateAndRegisterUser(null, UserUtils.User, UserUtils.UserRole)"/>
        /// </summary>
        public List<UserInfo> LRUsers;
        public List<UserInfo> OBUsers;
        public List<UserInfo> PAUsers;
        public UserInfo LRUser;
        public UserInfo OBUser;
        public UserInfo PAUser;

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

        /// <summary>
        /// This method will run for every test in this class. If any of the below users were created, it wil delete them.
        /// TODO: Refactor this so that it gets called at the entire UITest project level. So ill have to move it to a new
        /// class maybe, or something else. Note that parallel testing may cause this to not work properly as tests may 
        /// share the same thread with parallel testing
        /// </summary>
        //[TestFixtureTearDown]
        public void DeleteUserIfCreated()
        {
            foreach (var learner in LRUsers)
            {
                UserUtils.DeleteUser(learner.Username);
            }
            foreach (var learner in OBUsers)
            {
                UserUtils.DeleteUser(learner.Username);
            }
            foreach (var learner in PAUsers)
            {
                UserUtils.DeleteUser(learner.Username);
            }
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

        #region Tests

        [Test]
        [Description("Workflow test make sure nothing go worng when we add supporting documents from the learner tab")]
        [Property("Status", "In Progress")]
        [Author("Lakshmi")]
        public void ProgDirectorCanAddSupportingDocuments()
        {
            /// 1. Login as a program director, choose a learner, click the Actions menu item, then the Add Supporting Documentation
            /// button, fill in the form and click Submit
            LoginPage LP = Navigation.GoToLoginPage(browser);
            CBDProgDirectorPage PA = LP.LoginAsExistingUser(UserUtils.UserRole.PD, UserUtils.ProgDirector1Login, ConfigurationManager.AppSettings["LoginPassword"]);
            PA.AddSupportDocumentation(UserUtils.Learner1FullName, "C:\\Myfolder");
        }

        [Test]
        [Description("Workflow test make sure nothing go wrong when we add notes to a learner from the learner tab")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ProgDirectorCanAddNotes()
        {
            /// 1. Login as a Program Director
            LoginPage LP = Navigation.GoToLoginPage(browser);
            CBDProgDirectorPage PA = LP.LoginAsExistingUser(UserUtils.UserRole.PD, UserUtils.ProgDirector1Login, ConfigurationManager.AppSettings["LoginPassword"]);

            /// 2. Choose a learner in the learners table, click on the Actions button, click on Add Notes, add some notes
            /// and click Submit
            PA.AddNotes(UserUtils.Learner1FullName, "C:\\Myfolder");
        }

        [Test]
        [Description("Workflow test make sure nothing go wrong when we schedule a progress meeting with a learner from the learner tab")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ProgDirectorCanSchedProgMeet()
        {
            /// 1. Login as a Program Director
            LoginPage LP = Navigation.GoToLoginPage(browser);
            CBDProgDirectorPage PA = LP.LoginAsExistingUser(UserUtils.UserRole.PD, UserUtils.ProgDirector1Login, ConfigurationManager.AppSettings["LoginPassword"]);

            /// 2. Choose a learner in the learners table, click on the Actions button, click on Schedule Progress Meeting, fill in 
            /// all of the fields and click Submit
            PA.ScheduleProgressMeeting(UserUtils.Learner1FullName);
        }

        [Test]
        [Description("Workflow test make sure nothing go wrong when we add and also remove a flag from the learner table")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ProgDirectorCanAddAndRemoveFlag()
        {
            /// 1. Login as a Program Director
            LoginPage LP = Navigation.GoToLoginPage(browser);
            CBDProgDirectorPage PA = LP.LoginAsExistingUser(UserUtils.UserRole.PD, UserUtils.ProgDirector1Login, ConfigurationManager.AppSettings["LoginPassword"]);

            /// 2. Choose a learner in the learners table, click on the Actions button, click on Add/Remove Flag, 
            /// fill in all of the fields and click Save Flag
            PA.AddFlag(UserUtils.Learner1FullName);

            /// 3. Choose a learner in the learners table, click on the Actions button, click on Add/Remove Flag,
            /// fill in all of the fields and click Remove Flag
            PA.RemoveFlag(UserUtils.Learner1FullName);
        }

        #endregion Tests
    }
}






