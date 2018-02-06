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

namespace RCP.UITest
{
    [BrowserMode(BrowserMode.New)]
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [LocalSeleniumTestFixture(BrowserNames.Firefox)]
    [LocalSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Firefox)]
    [RemoteSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [TestFixture]
    public class RCP_CBD_LearnerUIAsserts_Tests : TestBase
    {
        #region Constructors
        public RCP_CBD_LearnerUIAsserts_Tests(string browserName) : base(browserName) { }

        // Remote Selenium Grid Test
        public RCP_CBD_LearnerUIAsserts_Tests(string browserName, string version, string platform, string hubUri, string extrasUri)
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

        #region Tests
        [Test]
        [Description("After a learner adds a reflection, it is populated into the Reflections table")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ReflectionShowsInTableAfterAdded()
        {
            // TEMPORARY: When the Delete User API is developed, we can remove the below code and uncomment the code below it, which creates new users
            // on the fly. Decide this when the delete API is complete
            /// 1. Login as a learner
            LoginPage LP = Navigation.GoToLoginPage(browser);
            CBDLearnerPage CLP = LP.LoginAsExistingUser(UserUtils.UserRole.LR, UserUtils.Learner1Login, ConfigurationManager.AppSettings["LoginPassword"]);

            /// 2. Add a reflection
            LearnerRelectionObject LR = CLP.AddReflection();

            /// 3. Switch to the Reflection tab and verify the reflection is shown in the table
            CLP.SwitchToTab(CLP.ReflectionsTab, Bys.CBDLearnerPage.ReflectionsTab);
            Assert.True(ElemGet.Grid_ContainsRecord(browser, CLP.ReflectionsTbl, Bys.CBDLearnerPage.ReflectionsTblBdy, 0, LR.ReflectionTitle, "td",
                Bys.CBDLearnerPage.TableFirstBtn, Bys.CBDLearnerPage.TableNextBtn));

            ///// 1. Login as learner
            //LoginPage LP = Navigation.GoToLoginPage(browser);
            //UserInfo LRUser = UserUtils.CreateAndRegisterUser(null, UserUtils.Application.CBD, UserUtils.UserRole.LR);
            //CBDLearnerPage CLP = LP.Login(UserUtils.UserRole.LR, LRUser.Username, LRUser.Password, false);

            ///// 2. Add a reflection
            //LearnerRelectionObject LR = CLP.AddReflection();

            ///// 3. Switch to the Reflection tab and verify the reflection is shown in the table
            //CLP.SwitchToTab(CLP.ReflectionsTab, Bys.CBDLearnerPage.ReflectionsTab);
            //Assert.True(ElemGet.Grid_ContainsRecord(browser, CLP.ReflectionsTbl, LR.ReflectionTitle, Bys.CBDLearnerPage.TableFirstBtn, Bys.CBDLearnerPage.TableNextBtn));
        }

        [Test]
        [Description("After a learner add a reflections, the reflection shows in labels and in the table")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void UIUpdatesAfterLearnerAddsReflection()
        {
            /// 1. Create learner user and login
            LoginPage LP = Navigation.GoToLoginPage(browser);
            LRUser = UserUtils.CreateAndRegisterUser(UserUtils.Application.CBD, UserUtils.UserRole.LR);
            CBDLearnerPage CLP = LP.LoginAsNewUser(UserUtils.UserRole.LR, LRUser.Username, LRUser.Password);

            /// 2. Store the number from the label text of the Reflections tab, and also "Showing" label, for the purpose of a future Assert within this 
            /// test. If there are no Reflections yet, this showing label will not appear, so skip it if so
            CLP.SwitchToTab(CLP.ReflectionsTab, Bys.CBDLearnerPage.ReflectionsTab);
            string origNumbOfReflectionsOnReflectionsTab = DataUtils.GetStringBetweenCharacters(CLP.ReflectionsTab.Text, "(", ")");
            if (browser.Exists(Bys.CBDLearnerPage.ShowingLbl, ElementCriteria.IsVisible))
            {
                string origNumbOfReflectionsOnShowingLbl = DataUtils.GetStringAfterCharacter(CLP.ShowingLbl.Text, "f", 2);
            }

            /// 3. Add a reflection
            LearnerRelectionObject LR = CLP.AddReflection();

            /// 4. Assert that the Reflections tab label increased by 1
            Assert.AreEqual(Int32.Parse(origNumbOfReflectionsOnReflectionsTab) + 1, Int32.Parse(DataUtils.GetStringBetweenCharacters(CLP.ReflectionsTab.Text, "(", ")")));

            /// 5. Assert that the label within the Reflections tab increased by 1
            // Bug RCPSC-264: "Learner->Add Reflection: "Showing" label does not update after learner adds reflection" 
            // Uncomment and run the test when fixed
            if (browser.Exists(Bys.CBDLearnerPage.ShowingLbl, ElementCriteria.IsVisible))
            {
                //Assert.AreEqual(Int32.Parse(origNumbOfReflectionsOnShowingLbl) + 1, Int32.Parse(DataUtils.GetStringAfterCharacter(CLP.ShowingLbl.Text, "f", 2)));
            }

            /// 5. Assert that the table contains the reflection
            Assert.True(ElemGet.Grid_ContainsRecord(browser, CLP.ReflectionsTbl, Bys.CBDLearnerPage.ReflectionsTblBdy, 0, LR.ReflectionTitle,
                "td", Bys.CBDLearnerPage.TableFirstBtn, Bys.CBDLearnerPage.TableNextBtn));
        }

        #endregion Tests
    }
}







