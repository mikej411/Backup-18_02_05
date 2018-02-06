using Browser.Core.Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using AMA.AppFramework;
using System;
using AMA.AppFramework.Utils.User;
using static AMA.AppFramework.Utils.User.UserUtils;
using System.Threading;
using System.Linq;
using System.IO;

namespace AMA.UITest
{
    //[BrowserMode(BrowserMode.New)]
    //[LocalSeleniumTestFixture(BrowserNames.Chrome)]
    //[LocalSeleniumTestFixture(BrowserNames.Firefox)]
    //[LocalSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Firefox)]
    [RemoteSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [TestFixture]
    public class AMA_ManagerFlow_Tests : TestBase
    {
        #region Constructors
        public AMA_ManagerFlow_Tests(string browserName) : base(browserName) { }

        // Remote Selenium Grid Test
        public AMA_ManagerFlow_Tests(string browserName, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, version, platform, hubUri, extrasUri)
        { }
        #endregion

        /// <summary>
        /// Example of how to override the teardown at the test class level
        /// </summary>
        //public override void TearDown() 
        //{
        //    Browser.Manage().Window.Size = new System.Drawing.Size(1040, 784);
        //    CleanupBrowser();
        //}

        #region Tests


        [Test]
        [Description("Workflow verifying manager can manage multy program.")]
        [Property("Status", "Complete")]
        [Author("Azat Chariyev")]

        public void Manage_MultyProgram_CurriculumAssignmentFlow()
        {
            ///  1.Navigate to the login page login as a Manager
            UserInfo role = UserUtils.GetUser(UserRole.Manager);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            EducationCenterPage ED = LP.LoginAsUser(role.Username, role.Password);
            if (BrowserName == BrowserNames.Firefox)
            {
                Browser.WaitForElement(Bys.EducationCenterPage.GcepLnk, ElementCriteria.IsEnabled);
            }
            ///  2.click to CGEP link  navigate to Gcep page and waiting load icon disappear
            GCEPPage GCEP = ED.ClickToAdvance(ED.GcepLnk);

            Browser.WaitForElement(Bys.GCEPPage.ProgramSelElem, ElementCriteria.IsEnabled);

            ///  3.Verifiying breadcrump is the same as a Institution name saving Institution name and count of users for Institution
            Assert.True(GCEP.SelectProgramAndVerifyBreadcrump(GCEP.ProgramSelElem));
            string ProgramName = GCEP.ReturnValueAfterSelectingProgram(1);// GCEP.ReturnValueAfterSelectingFromDropDown(GCEP.ProgramSelElem, 1);  
            int firstProgramUserCount = Int32.Parse(GCEP.TotalUsersCountLnk.Text);

            ///  4.Clicking Curruculum template link and getting breadcrump text and verifying with program name
            CurriculumMngPage CM = GCEP.ClickToAdvance(GCEP.CurriculumTemplatesLnk);
            //  var value = Browser.FindElement(Bys.AMAPage.BreadCrump).Text;
            string expectedFromCM = CM.GetBreadCrumbContainerText();
            Assert.IsTrue(expectedFromCM.Contains(ProgramName.ToLower()));
            string breadCrumpAfterAssigment = HelperMethods.CurriculumAssignmentFlow(browser,"new1111");
            Assert.True(breadCrumpAfterAssigment.Contains(ProgramName.ToLower()));

            Thread.Sleep(2000);

            ///  5.Clicking to the the breadcrump with Program name and naviagting GCEP page
            GCEP = CM.ClickToBreadCrumbContainerToReturnGcep(ProgramName);

            Browser.WaitForElement(Bys.GCEPPage.ProgramSelElem, ElementCriteria.IsEnabled);

            ///  6.Choosing second Program from  Dropdown and saving Program name and count of users for program
            string secondProgramName = GCEP.ReturnValueAfterSelectingProgram(2);  //GCEP.ReturnValueAfterSelectingFromDropDown(GCEP.ProgramSelElem, 2); 
            int secondProgramUserCount = Int32.Parse(GCEP.TotalUsersCountLnk.Text);

            ///  7.Clicking Curruculum template link and getting breadcrump text for second Porgram and verifying with Program name
            CM = GCEP.ClickToAdvance(GCEP.CurriculumTemplatesLnk);
            string secondexpectedFromCM = CM.GetBreadCrumbContainerText();
            Assert.IsTrue(secondexpectedFromCM.Contains(secondProgramName.ToLower()));
            string secondBreadcrumpAfterAssignment = HelperMethods.CurriculumAssignmentFlow(browser,"new1111");
            Assert.True(secondBreadcrumpAfterAssignment.Contains(secondProgramName.ToLower()));

            Thread.Sleep(2000);

            ///  8.Cliking to the the breadcrump with  second Program name and naviagting GCEP page  and getting breadcrump text for third Porgram 
            GCEP = CM.ClickToBreadCrumbContainerToReturnGcep(secondProgramName);
            Browser.WaitForElement(Bys.GCEPPage.ProgramSelElem, ElementCriteria.IsEnabled);
            string thirdProgramName =  GCEP.ReturnValueAfterSelectingProgram(3); //GCEP.ReturnValueAfterSelectingFromDropDown(GCEP.ProgramSelElem, 3);
            int thirdProgramUserCount = Int32.Parse(GCEP.TotalUsersCountLnk.Text);
            Thread.Sleep(2000);
            ///  9.Clicking Curruculum template link and verifying with third Program name
            CM = GCEP.ClickToAdvance(GCEP.CurriculumTemplatesLnk);
            string thirdexpectedFromCM = CM.GetBreadCrumbContainerText();
            Assert.IsTrue(thirdexpectedFromCM.Contains(thirdProgramName.ToLower()));
            string thirdBreadcrumpAfterAssignment = HelperMethods.CurriculumAssignmentFlow(browser,"new1111");
            Assert.True(thirdBreadcrumpAfterAssignment.Contains(thirdProgramName.ToLower()));

            Thread.Sleep(2000);

            GCEP = CM.ClickToBreadCrumbContainerToReturnGcep(thirdProgramName);
            Browser.WaitForElement(Bys.GCEPPage.ProgramSelElem, ElementCriteria.IsEnabled);

            ///  10.Verifying each program has different count of users.
            Assert.False(DataUtils.intsEqual(firstProgramUserCount, secondProgramUserCount, thirdProgramUserCount));

        }

        [Test]
        [Description("Workflow entering as a Manager creating curriculum for specific program verifying same curriculum not exist for different program." +
            "All workflow thru same user managing multy program.")]
        [Property("Status", "Complete")]
        [Author("Azat Chariyev")]

        public void MultyProgram_CreatingCurriculum_Flow()
        {
            string curriculumName = "";
            if (BrowserName == BrowserNames.Chrome)
            {
                curriculumName = "Chrome3CurrName";
            }
            if (BrowserName == BrowserNames.Firefox)
            {
                curriculumName = "FireFox3CurrName";
            }
            if (BrowserName == BrowserNames.InternetExplorer)
            {
                curriculumName = "IE3CurrName";
            }
            ///  1.Navigate to the login page login as a Manager
            UserInfo role = UserUtils.GetUser(UserRole.Manager);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            EducationCenterPage ED = LP.LoginAsUser(role.Username, role.Password);
            if (BrowserName == BrowserNames.Firefox)
            {
                Browser.WaitForElement(Bys.EducationCenterPage.GcepLnk, ElementCriteria.IsEnabled);
            }
            ///  2.click to CGEP link  navigate to Gcep page and waiting load icon disappear
            GCEPPage GCEP = ED.ClickToAdvance(ED.GcepLnk);
            Browser.WaitForElement(Bys.GCEPPage.ProgramSelElem, ElementCriteria.IsEnabled);

            /// 3.Verifiying breadcrump is the same as a Program name saving Program name 
            Assert.True(GCEP.SelectProgramAndVerifyBreadcrump(GCEP.ProgramSelElem));
            string ProgramName =  GCEP.ReturnValueAfterSelectingProgram(1);  // GCEP.ReturnValueAfterSelectingFromDropDown(GCEP.ProgramSelElem, 1);

            ///  4.Clicking Curruculum template link creating curriculum searching for curriculum if its not exist then creating curriculum with name depending on browser name
            CurriculumMngPage CurTemp = GCEP.ClickToAdvance(GCEP.CurriculumTemplatesLnk);
            CurTemp.Search(curriculumName);
            if (Browser.FindElements(Bys.CurriculumMngPage.NoRecordLbl).Count > 0)
            {
                CurriculumCoursePage CurCoursPage = CurTemp.ClickToAdvance(CurTemp.CreateCurriculumTemplateBtn);
                HelperMethods.CurriculumCreationFlow(browser,curriculumName);
            }

            ///  5.Cliking to the the breadcrump with Program name and naviagting GCEP page        
            GCEP = CurTemp.ClickToBreadCrumbContainerToReturnGcep(ProgramName);
            Browser.WaitForElement(Bys.GCEPPage.ProgramSelElem, ElementCriteria.IsEnabled);

            ///  6.Choosing second Program from  Dropdown and saving Institution name 
            string secondProgramName = GCEP.ReturnValueAfterSelectingProgram(2);  //GCEP.ReturnValueAfterSelectingFromDropDown(GCEP.ProgramSelElem, 2);
            Assert.True(GCEP.SelectProgramAndVerifyBreadcrump(GCEP.ProgramSelElem));
            Browser.WaitForElement(Bys.GCEPPage.CurriculumTemplatesLnk, ElementCriteria.IsEnabled); //delete after performance issue removed           

            ///  7.Clicking Curruculum template link searching for curriculum what we create for first Program
            CurTemp = GCEP.ClickToAdvance(GCEP.CurriculumTemplatesLnk);
            CurTemp.Search(curriculumName);

            ///  8. Verifiying curriculum not displayed for different Program 
            Assert.True(CurTemp.NoRecordLabel.Displayed);

            ///  9.Clicking breadcrump with second Program name and navigating to GCEP page
            GCEP = CurTemp.ClickToBreadCrumbContainerToReturnGcep(secondProgramName);

            Browser.WaitForElement(Bys.GCEPPage.ProgramSelElem, ElementCriteria.IsEnabled);

            ///  10.Verifiying breadcrump is the same as a Program name saving Program name for third Program
            Assert.True(GCEP.SelectProgramAndVerifyBreadcrump(GCEP.ProgramSelElem));
            string thirdProgramName = GCEP.ReturnValueAfterSelectingProgram(3);  //GCEP.ReturnValueAfterSelectingFromDropDown(GCEP.ProgramSelElem, 3);

            Browser.WaitForElement(Bys.GCEPPage.CurriculumTemplatesLnk, ElementCriteria.IsEnabled);  //delete after performance issue removed

            ///  11.Clicking Curruculum template link searching for curriculum what we create for first Program
            CurTemp = GCEP.ClickToAdvance(GCEP.CurriculumTemplatesLnk);
            CurTemp.Search(curriculumName);

            ///  12. Verifiying curriculum not displayed for different Program 
            Assert.True(CurTemp.NoRecordLabel.Displayed);

            ///  13.Clicking breadcrump with third Program name and navigating to GCEP page
            GCEP = CurTemp.ClickToBreadCrumbContainerToReturnGcep(thirdProgramName);
            Browser.WaitForElement(Bys.GCEPPage.ProgramSelElem, ElementCriteria.IsEnabled);

            ///  14. Choosing first Program from dropdown waiting for initialize page 
            GCEP.ProgramSelElem.SelectByText(ProgramName);
            GCEP.WaitForInitialize();
            Assert.True(GCEP.SelectProgramAndVerifyBreadcrump(GCEP.ProgramSelElem));

            Browser.WaitForElement(Bys.GCEPPage.CurriculumTemplatesLnk, ElementCriteria.IsEnabled);  //delete after performance issue removed

            ///  15.Clicking Curruculum template link searching for curriculum what we create for first Program
            CurTemp = GCEP.ClickToAdvance(GCEP.CurriculumTemplatesLnk);
            CurTemp.Search(curriculumName);

            ///  16.Deleting curriculum and verifying label is displayed
            CurTemp.DeleteCurriculum(curriculumName);
            Assert.IsTrue(CurTemp.NoRecordLabel.Displayed);
        }

        [TestCase(UserRole.Admin)]
        [TestCase(UserRole.Ama_Staff)]
        [TestCase(UserRole.Manager)]
        [Description("As Admin, Manager, Ama_Staff users count should on GCEP Page and User Management Page should be same.")]
        [Property("Status", "Complete")]
        [Author("Azat Chariyev")]

        public void UsersCount(UserRole userRole)
        {
            ///  1.Navigate to the login page login as a Manager
            UserInfo role = UserUtils.GetUser(userRole);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            EducationCenterPage ED = LP.LoginAsUser(role.Username, role.Password);
            if (BrowserName == BrowserNames.Firefox)
            {
                Browser.WaitForElement(Bys.EducationCenterPage.GcepLnk, ElementCriteria.IsEnabled);
            }
           
            ///  2.click to CGEP link  navigate to Gcep page and waiting load icon disappear
            GCEPPage GCEP = ED.ClickToAdvance(ED.GcepLnk);
            if (userRole == UserRole.Manager) { Browser.WaitForElement(Bys.GCEPPage.SendEmailNotificationLnk, TimeSpan.FromSeconds(150), ElementCriteria.IsVisible); }

            ///  3.From GCEP page getting count of user from Users link and saving in Count of user integer then clicking user management link
            int CountOfUserOnGCEP = Convert.ToInt32(GCEP.TotalUsersCountLnk.Text);
            GCEPUserMngPage GUMP = GCEP.ClickToAdvance(GCEP.UserManageLnk);  
            
            ///  4.Getting count of user from user management label on user management page and verifying thatthey are equals.
            int CountOfUsersOnUserMngPage = GUMP.GetCountOfUsersFromUserManagementLabel();            
            Assert.AreEqual(CountOfUserOnGCEP , CountOfUsersOnUserMngPage,"Counts of Users are not the same");            
           
        }       

    }
    #endregion Tests
}


