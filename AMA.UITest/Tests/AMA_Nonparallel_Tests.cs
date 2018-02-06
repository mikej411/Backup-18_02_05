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
using AMA.AppFramework;
using AMA.AppFramework.Utils.User;
using static AMA.AppFramework.Utils.User.UserUtils;

/// <summary>
/// This class was created because of the NUnit bug that causes browser sessions to hang when using the [NonParallelizable] attribute above test
/// methods. The latest version does not have a fix, so we have to create this separate class and use the [NonParallelizable] attribute 
/// at the Test Fixture level. When there is a fix, we can put these back in their respective classes
/// </summary>
namespace AMA.UITest
{
    [BrowserMode(BrowserMode.New)]
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    //[LocalSeleniumTestFixture(BrowserNames.Firefox)]
    //[LocalSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Firefox)]
    [RemoteSeleniumTestFixture(BrowserNames.InternetExplorer)]
    // These tests cant be run in parallel for various different reasons. See the comment above each test method for an explanation for each one.
    [NonParallelizable]
    [TestFixture]
    public class AMA_Nonparallel_Tests : TestBase

    {
        #region Constructors
        public AMA_Nonparallel_Tests(string browserName) : base(browserName) { }

        // Remote Selenium Grid Test
        public AMA_Nonparallel_Tests(string browserName, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region tests

        [Test]
        [Description("Workflow Login as Admin choose Institution assign program check  as aresident program is assigned then unassign programm  and" +
          "check as a resident again that is not programm is removed")]
        [Property("Status", "Complete")]
        [Author("Azat Chariyev")]
      

        public void Assignment_Flow()
        {
            string InstitutionName = "Ellis Hospital";
            string CurriculumName = "CurriculumName1!";

            /// 1.Navigate to the login page login as a Manager
            UserInfo role = UserUtils.GetUser(UserRole.Ama_Staff);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            EducationCenterPage ED = LP.LoginAsUser(role.Username, role.Password);

            if (BrowserName == BrowserNames.Firefox)
            {
                Browser.WaitForElement(Bys.EducationCenterPage.GcepLnk, ElementCriteria.IsEnabled);
            }
            ///  2.click to GCEP link  navigate to Gcep page and waiting load icon disappear
            GCEPPage Gcep = ED.ClickToAdvance(ED.GcepLnk);

            ///  3.from Gcep navigating to institution managment searching for institution looking for curriculum and if their any curriculum with the same name deleting and starting create new curriculum.
            InstitutionsPage Instute = Gcep.ClickToAdvance(Gcep.InstitutionManagLnk);
            InstitutionsGCEPPage InsGcep = Instute.SearchforInstitutions(InstitutionName);
            CurriculumMngPage Curriculum = InsGcep.ClickToAdvance(InsGcep.InstitutionCurriculumTmpLnk);
            Curriculum.Search(CurriculumName);
            Curriculum.DeleteCurriculum(CurriculumName);
            CurriculumCoursePage CurCoursPage = Curriculum.ClickToAdvance(Curriculum.CreateCurriculumTemplateBtn);

            ///  4.Form course page choosing available courses from table by index
            CurCoursPage.AddOrRemoveCourses(CurCoursPage.AvailableCoursesTbl, CurCoursPage.AddSelectedBtn, 1, 7, 12);

            ///  5. Giving the name for curriculum passing parameter from TestCase as a string
            CurCoursPage.CurriculumNameTxt.Clear();
            CurCoursPage.CurriculumNameTxt.SendKeys(CurriculumName);

            ///  6.Saving curriculum and navigating to the pgy pages to assigne course to students
            PGYAssignmentPage PGY = CurCoursPage.ClickToAdvance(CurCoursPage.NextBtn);

            ///  7.Choosing student years to assign course by index for each course
            PGY.Grid_ClickElementWithoutTextInsideRow(PGY.CourseTbl, 1, 4);
            PGY.Grid_ClickElementWithoutTextInsideRow(PGY.CourseTbl, 2, 5);
            PGY.Grid_ClickElementWithoutTextInsideRow(PGY.CourseTbl, 3, 5);

            ///  8. Saving curriculum and navigating to curriculum management page again
            PGY.ClickToAdvance(PGY.SaveExitBtn);

            /// 9.Finding curriculum what we create and assigning to the programm
            Curriculum.Search(CurriculumName);
            Curriculum.Actioncell.Click();
            AssignProgramPage Assign = Curriculum.ClickToAdvance(Curriculum.AssignToProgrammLnk);

            ///  10.Choosing starting date and ending date for  program  and clicking next button
            string StartingDate = Assign.ChoosingStartDate();
            string EndingDate = Assign.ChoosingEndDate(1, "M/d/yyyy");
            Assign.AssignProgramm();
            AssignSummaryPage Summary = Assign.ClickToAdvance(Assign.NextBtn);

            ///  11.Verifying from Assign Summary page program is displayed
            Assert.IsTrue(Summary.CreatedProgramName.Displayed, "The curriculum name was not displayed");
            Assert.AreEqual(CurriculumName, Summary.CreatedProgramName.Text, string.Format("The expected curriculum name {0} is not equal to the actual name {1}", Summary.CreatedProgramName.Text, CurriculumName));

            ///  12.Verifying Assing confirmation test page curriculum name and starting date and ending dates are there which we choose.
            AssignConfirmationPage Confirmation = Summary.ClickToAdvance(Summary.NextBtn);
            Assert.IsTrue(Confirmation.Grid_CellTextFound(Confirmation.ProgramSummaryTbl, StartingDate + " - " + EndingDate));
            Confirmation.ConfirmBtn.Click();

            ///  13.Signing out and Signing in as a Resindent counting required courses from myRequiredCourses
            Curriculum.ClickToAdvance(Curriculum.SignOutLnk);
            LP = Navigation.GoToLoginPage(browser);
            ED = LP.LoginAsUser("10021387", "password");
            if (BrowserName == BrowserNames.Firefox)
            {
                Browser.WaitForElement(Bys.EducationCenterPage.GcepLnk, ElementCriteria.IsEnabled);
            }
            Gcep = ED.ClickToAdvance(ED.GcepLnk);
            // int startingCountCourse = Gcep.GetCountOfCourses(Gcep.MyRegiuredCourseTbl, Gcep.MyRequiredCourseLnk);
            //Gcep.MyRequiredCourseLnk.SendKeys(Keys.Tab);
            //Gcep.MyRequiredCourseLnk.Click();
            //Thread.Sleep(0500);
            int startingCountCourse = Gcep.Grid_GetRowCount(Gcep.ResidentCourseTbl);

            ///  14.Signing out and Signing in as Admin or AMA_Staff lookingfor programm what we created and Unassigning
            Curriculum.ClickToAdvance(Curriculum.SignOutLnk);
            LP = Navigation.GoToLoginPage(browser);
            ED = LP.LoginAsUser(role.Username, role.Password);
            if (BrowserName == BrowserNames.Firefox)
            {
                Browser.WaitForElement(Bys.EducationCenterPage.GcepLnk, ElementCriteria.IsEnabled);
            }
            Gcep = ED.ClickToAdvance(ED.GcepLnk);
            Gcep.ClickToAdvance(Gcep.InstitutionManagLnk);
            Instute.SearchforInstitutions(InstitutionName);
            ProgramsPage Program = InsGcep.ClickToAdvance(InsGcep.InstitutionProgramManagmentLnk);
            Program.UnassignCurriculum();
            Gcep.ClickToAdvance(Gcep.SignOutLnk);
            // Curriculum.ClickToAdvance(Curriculum.SignOutLnk);

            ///  15.Signing out and Signing in as a Resindent counting required courses from myRequiredCourses        
            LP = Navigation.GoToLoginPage(browser);
            ED = LP.LoginAsUser("10021387", "password");
            if (BrowserName == BrowserNames.Firefox)
            {
                Browser.WaitForElement(Bys.EducationCenterPage.GcepLnk, ElementCriteria.IsEnabled);
            }
            Gcep = ED.ClickToAdvance(ED.GcepLnk);
            int finalCountCourse = Gcep.Grid_GetRowCount(Gcep.ResidentCourseTbl);
            //int finalCountCourse = Gcep.GetCountOfCourses(Gcep.MyRegiuredCourseTbl, Gcep.MyRequiredCourseLnk);

            ///  16.Verifiying that starting count does not match with final count courses after unassigning
            Assert.IsFalse(startingCountCourse == finalCountCourse, "Course count are equal");

        }

       

        [Test]
        [Description("AMA_Staff taking counts of curriculums from GCEP page then comparing counts of curriculums from curriculum template table, " +
            "and choosing Institution then getting count of users, curriculums, programs from Instution GCEp page and comparing counts from depending table.")]
        [Property("Status", "Not Complete")]
        [Author("Azat Chariyev")]
        
        public void Checking_AllCounts_FromGcep()
        {
            ///  1.Navigate to the login page with valid credentials below
            UserInfo role = UserUtils.GetUser(UserRole.Ama_Staff);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            EducationCenterPage ED = LP.LoginAsUser(role.Username, role.Password);
            if (BrowserName == BrowserNames.Firefox)
            {
                Browser.WaitForElement(Bys.EducationCenterPage.GcepLnk, ElementCriteria.IsEnabled);
            }
            ///  2.click to CGEP link  navigate to Gcep page and waiting load icon disappear and getting count of curriculum templates
            GCEPPage GCEP = ED.ClickToAdvance(ED.GcepLnk);
            int countOfCurriculumTemplatesFromGCEP = Convert.ToInt32(GCEP.TotalCurriculumTmtsCountLnk.Text);

            ///  3.From GCEP Navigate to Curriculum Template saving count of curriculum on the table and verifying counts of curriculum from GCEP are equals
            CurriculumMngPage CMP = GCEP.ClickToAdvance(GCEP.CurriculumTemplatesLnk);
            int countofCurriculumTemplatesFromPage = CMP.GetCountOfCurriculumonTable("of", "items");
            //string something = CMP.CountTableItemLbl.Text;   
            //something = new string(something.Where(x => char.IsDigit(x)).ToArray());
            Assert.AreEqual(countOfCurriculumTemplatesFromGCEP, countofCurriculumTemplatesFromPage);

            /// 4.Navigating back to GCEP  
            if (BrowserName == BrowserNames.Firefox)
            {
                Browser.Navigate().Back();
            }
            CMP.GMECompetencyEducationProgramLnk.Click();  //AdministrationLnk.Click();
            GCEP.WaitForInitialize();
            if (BrowserName == BrowserNames.Firefox)
            {
                Browser.WaitForElement(Bys.GCEPPage.InstitutionManagLnk, ElementCriteria.IsEnabled);

            }

            ///  5.from Gcep navigating to institution managment searching for institution 
            InstitutionsPage Instute = GCEP.ClickToAdvance(GCEP.InstitutionManagLnk);
            InstitutionsGCEPPage InsGcep = Instute.SearchforInstitutions("Albert Einstein Medical Center");

            ///  6.Saving counts of program,user,curriculum template from Institution GCEP
            int totalProgramCountonGCEP = Convert.ToInt32(InsGcep.TotalProgramCountLnk.Text);
            int totalUsersCountonGCEP = Convert.ToInt32(InsGcep.TotalUserCountLnk.Text);
            int totalCurTempCountonGCEP = Convert.ToInt32(InsGcep.TotalCurriculumTemplatesCountLnk.Text);

            ///  7.Navigating Program Management Page and getting counts of Programs Page and comparing countsof program from Institution GCEP page.
            ProgramsPage PP = InsGcep.ClickToAdvance(InsGcep.InstitutionProgramManagmentLnk);
            int countsOfProgramsOnPP = PP.Grid_GetCountOfItemsOnTable("of", "items");
            Assert.AreEqual(totalProgramCountonGCEP, countsOfProgramsOnPP);

            ///  8.Navigating to Institution GCEP Page
            InsGcep = PP.ClickToBreadCrumbContainerToReturnInsGCEP("Albert Einstein Medical Center");

            ///  9.From Instution GCEP clicking user management link and navigating GCEP User Management page getting counts of users from User Management table
            ///  verifying counts of users count are equals from Institution GCEP and Users Management Page.
            GCEPUserMngPage GUMP = InsGcep.ClickToAdvance(InsGcep.InstitutionUserManagementLnk);
            int CountOfUsersOnUserMngPage = GUMP.GetCountOfUsersFromUserManagementLabel();
            Assert.AreEqual(totalUsersCountonGCEP, CountOfUsersOnUserMngPage);

            ///  10.Navigating to Institution GCEP Page
            InsGcep = GUMP.ClickToBreadCrumbContainerToReturnInsGCEP("Albert Einstein Medical Center");

            ///  11.From Institutiom GCEP clicking curriculum templates and navigating Curriculum Mng Page and getting count of curriculum templates and asserting
            ///  counts of curriculum template from Institution GCEP page are equals.
            CMP = InsGcep.ClickToAdvance(InsGcep.InstitutionCurriculumTmpLnk);
            int countOfCurTempFromCurrimMngPage = CMP.Grid_GetCountOfItemsOnTable("of", "items");
            Assert.AreEqual(totalCurTempCountonGCEP, countOfCurTempFromCurrimMngPage);

        }


        [Test]
        [Description("As a Staff within GCEP, I want the capability to Edit or Update End Date on Program Management Page and Assigned Curricula Page")]
        [Property("Status", "Complete")]
        [Author("Azat Chariyev")]
        public void Changing_CurriculumEndingDate()
        {
            string InstitutionName = "20170824_Institution";
            string ProgramName = "Program 1";
            ///  1.Navigate to the login page login as a Manager
            UserInfo role = UserUtils.GetUser(UserRole.Ama_Staff);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            EducationCenterPage ED = LP.LoginAsUser(role.Username, role.Password);
            if (BrowserName == BrowserNames.Firefox)
            {
                Browser.WaitForElement(Bys.EducationCenterPage.GcepLnk, ElementCriteria.IsEnabled);
            }
            ///  2.click to GCEP link  navigate to Gcep page and waiting load icon disappear
            GCEPPage Gcep = ED.ClickToAdvance(ED.GcepLnk);

            ///  3.from Gcep navigating to institution managment searching for institution 
            InstitutionsPage Instute = Gcep.ClickToAdvance(Gcep.InstitutionManagLnk);
            InstitutionsGCEPPage InsGcep = Instute.SearchforInstitutions(InstitutionName);

            ///  4.From Institution Gcep Page going to Program Managment Page and searching for specific programm 
            ProgramsPage Program = InsGcep.ClickToAdvance(InsGcep.InstitutionProgramManagmentLnk);
            Program.Search(ProgramName);

            ///  5.Clicking edit program and navigating to Curriculum Course Page
            Program.ChoosingFromDate();
            CurriculumCoursePage CoursePage = Program.EditProgramm();

            ///  6.From Curriculum course page setting end date calender today date minus 1 month and verifying warning message
            string todayDateMinus = CoursePage.SetEndDatForProgramm(-1);
            Assert.IsTrue(CoursePage.CurrentDateWarningLbl.Displayed);

            ///  7.setting end date calender today date plus 36 month and verifying warning message
            string todayDatePlus = CoursePage.SetEndDatForProgramm(24);
            Assert.IsTrue(CoursePage.GreaterDateWarningLbl.Displayed);
            //Program.Selectselect("ins-admin-grid-action cursor-default ng-scope", "Edit Curriculum");

            ///  8.setting end date calender today date plus 9 month and clicking next buttton
            string assignedDate = CoursePage.SetEndDatForProgramm(10);

            ///  9.Navigating PGYAssignment Page clicking next button
            PGYAssignmentPage PGyPages = CoursePage.ClickToAdvance(CoursePage.NextBtn);

            ///  10.Navigating Assign Summary Page verifying Program naem displayed and next button clicking 
            AssignSummaryPage Summary = PGyPages.ClickToAdvance(PGyPages.NextBtn);
            Assert.IsTrue(Summary.CreatedProgramName.Displayed);

            ///  11.Navigating Assign Confirmation Page clicking edit confirmation button
            AssignConfirmationPage ConfirmationPage = Summary.ClickToAdvance(Summary.NextBtn);
            Program = ConfirmationPage.ClickToAdvance(ConfirmationPage.EditConfirmBtn);

            ///  12.Navigating Institution Program Page searching for program and  verifying date what we changed is there          
            Program.Search(ProgramName);
            Assert.IsTrue(Program.GetValueOfRow(Program.ProgramMngTbl, assignedDate));

        }

        [TestCase(UserRole.Admin)]
        //[TestCase(UserRole.Ama_Staff)]
        [Description("Workflow ")]
        [Property("Status", "Complete")]
        [Author("Azat Chariyev")]

        public void Update_BulkCurriculum(UserRole userRole)
        {
            string InstitutionName = "Beaumont Health System";
            ///  1. Navigate to the login page and login as a Admin
            UserInfo role = UserUtils.GetUser(userRole);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            EducationCenterPage ED = LP.LoginAsUser(role.Username, role.Password);
            if (BrowserName == BrowserNames.Firefox)
            {
                Browser.WaitForElement(Bys.EducationCenterPage.GcepLnk, ElementCriteria.IsEnabled);
            }
            ///  2.click to GCEP link  navigate to Gcep page and waiting load icon disappear
            GCEPPage Gcep = ED.ClickToAdvance(ED.GcepLnk);

            ///  3.from Gcep navigating to institution managment searching for institution 
            InstitutionsPage Instute = Gcep.ClickToAdvance(Gcep.InstitutionManagLnk);

            ///  4.from institution page searching for specific institution choosing and navigating InstitutionGcep page
            InstitutionsGCEPPage InsGcep = Instute.SearchforInstitutions(InstitutionName);

            ///  5.Clicking program managment link and navigating Programs page, setting calendar for today date
            ProgramsPage PM = InsGcep.ClickToAdvance(InsGcep.InstitutionProgramManagmentLnk);
            PM.ChoosingFromDate();

            ///  6.Searching for specific program and if count of course on program less then 3 adding corsess else searching for second program also saving count of courses in variable.
            PM.Search("Anesthesiology");
            int AnesPC1 = PM.GetCountOfCourse();
            if (AnesPC1 < 3)
            {
                HelperMethods.AddCourseToCurriculum(browser);
            }
            PM.Search("Cardiology - Electrophysiology");
            int CardioPC2 = PM.GetCountOfCourse();
            //Assert.True(AnesPC1 == CardioPC2, "Count of courses for curriculum are not the same");

            /// 7.Searching for first program and removing to courses from it. 
            PM.Search("Anesthesiology");
            CurriculumCoursePage CoursePage = PM.EditProgramm();
            CoursePage.AddOrRemoveCourses(CoursePage.ChosenCoursesTbl, CoursePage.RemoveSelectedBtn, 1, 2);
            PGYAssignmentPage Pgy = CoursePage.ClickToAdvance(CoursePage.NextBtn);
            AssignSummaryPage Summary = Pgy.ClickToAdvance(Pgy.NextBtn);
            AssignConfirmationPage Confirm = Summary.ClickToAdvance(Summary.NextBtn);
            PM = Confirm.ClickToAdvance(Confirm.EditConfirmBtn);

            ///  8.Saving count of course after removing courses.
            PM.Search("Anesthesiology");
            int AnesPC1AfterRemoving = PM.GetCountOfCourse();

            ///  9.Clicking copy edit link and navigating copy edit page
           // Assert.AreNotEqual(AnesPC1, AnesPC1AfterRemoving);
            CopyCurriculumEditsPage CCEP = PM.CopyEditsProgramm();
            string TimeFrame = CCEP.TimeFrameLbl.Text;

            ///  10.Verifying the program name are the same what we are searched for
            Assert.True(CCEP.ProgramNameLbl.Text.Equals("Anesthesiology"));
            int ActualCountOfprogramsBeforeEdit = CCEP.AddOrRemovePrograms(CCEP.CopyEditProgramTbl, 1);

            ///  11.After clicking next and copying course to other program verifying time fram are the same and verifying that copy same count of program
            Confirm = CCEP.ClickToAdvance(CCEP.NextBtn);
            Assert.True(TimeFrame.Equals(Confirm.TimeFrameLbl.Text));
            int ExpectedCountOfProgramsAfterEdit = Confirm.GetCountOfPrograms();
            Assert.True(ActualCountOfprogramsBeforeEdit == ExpectedCountOfProgramsAfterEdit);
            if (BrowserName == BrowserNames.Firefox)
            {
                Thread.Sleep(0500);
            }
            ///  12.clicking confirmation button navigating to the program page
            PM = Confirm.ClickToAdvance(Confirm.ConfirmBtn);

            ///  13.searching for second program and asserting courses from first program copied.
            PM.Search("Cardiology - Electrophysiology");
            int CountOfCourses2AfterCopy = PM.GetCountOfCourse();
            Assert.True(AnesPC1AfterRemoving == CountOfCourses2AfterCopy, "Count of courses after copy not equal,most likely test running parallel");

            ///  14. adding courses again to the program where we removed and copying for second program.
            PM.Search("Anesthesiology");
            HelperMethods.AddCourseToCurriculum(browser);
            int CountOfCoursesAfterAdding = PM.GetCountOfCourse();
            Assert.AreEqual(AnesPC1, CountOfCoursesAfterAdding, "count of courses are not the same in Curriculum");

            CCEP = PM.CopyEditsProgramm();
            int ActualCountOfprograms1 = CCEP.AddOrRemovePrograms(CCEP.CopyEditProgramTbl, 1);
            Confirm = CCEP.ClickToAdvance(CCEP.NextBtn);
            Thread.Sleep(1000);
            PM = Confirm.ClickToAdvance(Confirm.ConfirmBtn);

        }



        //[Test]
        [Description("Help Page elements changing depends on role verifying elements loging with different roles" +
           "Log in as a AMA staff then as a Admin Manager and a Resident")]
        [Property("Status", "Progress")]
        [Author("Azat Chariyev")]
    
        public void CourseTracker()
        {
            ///  1.Navigate to the login page login as a AMA staff
            UserInfo role = UserUtils.GetUser(UserRole.Ama_Staff);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            EducationCenterPage ED = LP.LoginAsUser("10021373","password");

            if (BrowserName == BrowserNames.Firefox)
            {
                Browser.WaitForElement(Bys.EducationCenterPage.GcepLnk, ElementCriteria.IsEnabled);
            }

            ///  2.click to GCEP link  navigate to Gcep page and waiting load icon disappear
           GCEPPage Gcep = ED.ClickToAdvance(ED.GcepLnk);

           // Thread.Sleep(5000);
            string CourseTracker = Gcep.ResidentCourseTrackerLbl.Text;           
            
            if (!CourseTracker.Contains("0"))
            {
                string[] CourseyrackernotNull = CourseTracker.Split(' ');
                string[] countofcoursewasAssigned = CourseyrackernotNull[2].Split('/');
                //string[] courseword = CourseTracker.Split(' ');
                int regCoursCount = Convert.ToInt16(countofcoursewasAssigned[2]);
                Thread.Sleep(5000);
            }
            else
            {
                string[] CoursetrackerwithNull = CourseTracker.Split(' ');
                int NoRegCourse = Convert.ToInt16(CoursetrackerwithNull[2]);
            }
            Gcep.ClickToAdvance(Gcep.SignOutLnk);

            LP = Navigation.GoToLoginPage(browser);
            ED = LP.LoginAsUser(role.Username, role.Password);
            if (BrowserName == BrowserNames.Firefox)
            {
                Browser.WaitForElement(Bys.EducationCenterPage.GcepLnk, ElementCriteria.IsEnabled);
            }
            Gcep = ED.ClickToAdvance(ED.GcepLnk);

            ///  3.from Gcep navigating to institution managment searching for institution looking for curriculum and if their any curriculum with the same name deleting and starting create new curriculum.
            InstitutionsPage Instute = Gcep.ClickToAdvance(Gcep.InstitutionManagLnk);
            InstitutionsGCEPPage InsGcep = Instute.SearchforInstitutions("Ellis Hospital");

            ProgramsPage Program = InsGcep.ClickToAdvance(InsGcep.InstitutionProgramManagmentLnk);
            Program.UnassignCurriculum(); 
            

            CurriculumMngPage Curriculum = InsGcep.ClickToAdvance(InsGcep.InstitutionCurriculumTmpLnk);
            Curriculum.Search("Learning111!!!");
            Curriculum.DeleteCurriculum("Learning111!!!");
            CurriculumCoursePage CurCoursPage = Curriculum.ClickToAdvance(Curriculum.CreateCurriculumTemplateBtn);

            ///  4.Form course page choosing available courses from table by index
            CurCoursPage.AddOrRemoveCourses(CurCoursPage.AvailableCoursesTbl, CurCoursPage.AddSelectedBtn, 8, 9, 10,11,12,13,14,15,16);

            // List<string> CourseNames = new List<string>();
            List<string> CourseNames = CurCoursPage.GetTheNamesChoosenCourses();


            int CountofCoursewasAssigned = CourseNames.Count;


            ///  5. Giving the name for curriculum passing parameter from TestCase as a string
            CurCoursPage.CurriculumNameTxt.Clear();
            CurCoursPage.CurriculumNameTxt.SendKeys("Learning111!!!");

            ///  6.Saving curriculum and navigating to the pgy pages to assigne course to students
            PGYAssignmentPage PGY = CurCoursPage.ClickToAdvance(CurCoursPage.NextBtn);

            ///  7.Choosing student years to assign course by index for each course
            PGY.Grid_ClickElementWithoutTextInsideRow(PGY.CourseTbl, 1, 5);
            PGY.Grid_ClickElementWithoutTextInsideRow(PGY.CourseTbl, 2, 5);
            PGY.Grid_ClickElementWithoutTextInsideRow(PGY.CourseTbl, 3, 5);
            PGY.Grid_ClickElementWithoutTextInsideRow(PGY.CourseTbl, 4, 5);
            PGY.Grid_ClickElementWithoutTextInsideRow(PGY.CourseTbl, 5, 5);
            PGY.Grid_ClickElementWithoutTextInsideRow(PGY.CourseTbl, 6, 5);
            PGY.Grid_ClickElementWithoutTextInsideRow(PGY.CourseTbl, 7, 5);
            PGY.Grid_ClickElementWithoutTextInsideRow(PGY.CourseTbl, 8, 5);
            PGY.Grid_ClickElementWithoutTextInsideRow(PGY.CourseTbl, 9, 5);
            
            PGY.Grid_ClickElementWithoutTextInsideRow(PGY.CourseTbl, 1, 4);
            PGY.Grid_ClickElementWithoutTextInsideRow(PGY.CourseTbl, 2, 4);
            PGY.Grid_ClickElementWithoutTextInsideRow(PGY.CourseTbl, 3, 4);
            PGY.Grid_ClickElementWithoutTextInsideRow(PGY.CourseTbl, 4, 4);
            PGY.Grid_ClickElementWithoutTextInsideRow(PGY.CourseTbl, 5, 4);
            PGY.Grid_ClickElementWithoutTextInsideRow(PGY.CourseTbl, 6, 4);
            PGY.Grid_ClickElementWithoutTextInsideRow(PGY.CourseTbl, 7, 4);
            PGY.Grid_ClickElementWithoutTextInsideRow(PGY.CourseTbl, 8, 4);
            PGY.Grid_ClickElementWithoutTextInsideRow(PGY.CourseTbl, 9, 4);
          

            ///  8. Saving curriculum and navigating to curriculum management page again
            PGY.ClickToAdvance(PGY.SaveExitBtn);

            /// 9.Finding curriculum what we create and assigning to the programm
            Curriculum.Search("Learning111!!!");
            Curriculum.Actioncell.Click();
            AssignProgramPage Assign = Curriculum.ClickToAdvance(Curriculum.AssignToProgrammLnk);

            ///  10.Choosing starting date and ending date for  program  and clicking next button
            string StartingDate = Assign.ChoosingStartDate();
            string EndingDate = Assign.ChoosingEndDate(1, "MM/d/yyyy");
            Assign.AssignProgramm();
            AssignSummaryPage Summary = Assign.ClickToAdvance(Assign.NextBtn);

            ///  11.Verifying from Assign Summary page program is displayed
            Assert.IsTrue(Summary.CreatedProgramName.Displayed);
            Assert.AreEqual((Summary.CreatedProgramName.Text), "Learning111!!!");

            ///  12.Verifying Assing confirmation test page curriculum name and starting date and ending dates are there which we choose.
            AssignConfirmationPage Confirmation = Summary.ClickToAdvance(Summary.NextBtn);
            // Assert.IsTrue(Confirmation.Grid_CellTextFound(Confirmation.ProgramSummaryTbl, StartingDate + " - " + EndingDate));
            Thread.Sleep(0500);
            Confirmation.ConfirmBtn.Click();

            ///  13.Signing out and Signing in as a Resindent counting required courses from myRequiredCourses
            Curriculum.ClickToAdvance(Curriculum.SignOutLnk);

            Thread.Sleep(2500);
            LP = Navigation.GoToLoginPage(browser);
            ED = LP.LoginAsUser("10021373", "password");//10021375,10021377,21387
            Gcep = ED.ClickToAdvance(ED.GcepLnk);
            //Assert.True(Gcep.VerificationOfChoosenCoursesAssignedForResident(browser, CourseNames), "Course count are not equal");
            Thread.Sleep(2000);
            string CourseTrackerAfterAssignment = Gcep.ResidentCourseTrackerLbl.Text;
            string[] courseword2 = CourseTrackerAfterAssignment.Split(' ');
            //string[] countofcourses1 = courseword2[2].Split('/');
            //string[] courseword = CourseTracker.Split(' ');
           int CountofcoursesOnResidentGcepaftercourseAssignment = Convert.ToInt16(courseword2[4]);

            Thread.Sleep(2000);
            int CountofcoursesOnResidentGcepaftercourseAssigmentCompleted = Convert.ToInt16(courseword2[2]);

            Thread.Sleep(2000);

            Assert.True(CountofcoursesOnResidentGcepaftercourseAssignment.Equals(CountofCoursewasAssigned));

            do { ElemSet.ScrollToElement(browser, Gcep.FaceBookLnk); }
            while (!Gcep.ResidentGcepShowElectiveCourseLnk.Displayed);
            CourseTestPage Course = Gcep.ResidentStartCourseOrContinue(browser, CourseNames[1]);


            Gcep = Course.TestPass();

            do { ElemSet.ScrollToElement(browser, Gcep.FaceBookLnk); }
            while (!Gcep.ResidentGcepShowElectiveCourseLnk.Displayed);

            Assert.IsTrue(Gcep.VerificationCourseCompletion(browser, CourseNames[1], "View Certificate"), "View Certificet button not visible");

            string CourseTrackerAfterTestCompletion = Gcep.ResidentCourseTrackerLbl.Text;
            string[] courseword3 = CourseTrackerAfterTestCompletion.Split(' ');
            int CountOfCompletedRegCourseafterPassingTest = Convert.ToInt16(courseword3[2]);
            //int some = CountofcoursesOnResidentGcepaftercourseAssigmentCompleted + 1;
            Assert.True(CountofcoursesOnResidentGcepaftercourseAssigmentCompleted + 1 == (CountOfCompletedRegCourseafterPassingTest));
            Gcep.ClickToAdvance(Gcep.SignOutLnk);

            Thread.Sleep(2500);
            LP = Navigation.GoToLoginPage(browser);
            ED = LP.LoginAsUser(role.Username, role.Password);//10021375,10021377,21387
            Gcep = ED.ClickToAdvance(ED.GcepLnk);

            Instute = Gcep.ClickToAdvance(Gcep.InstitutionManagLnk);
            InsGcep = Instute.SearchforInstitutions("Ellis Hospital");

            Program = InsGcep.ClickToAdvance(InsGcep.InstitutionProgramManagmentLnk);
            Program.UnassignCurriculum();

            Curriculum = InsGcep.ClickToAdvance(InsGcep.InstitutionCurriculumTmpLnk);
            Curriculum.Search("Learning111!!!");
            Curriculum.DeleteCurriculum("Learning111!!!");

        }

        #endregion Tests
    }
    }







