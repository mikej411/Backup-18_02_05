using System;

using Browser.Core.Framework;
using NUnit.Framework;
using OpenQA.Selenium;

using AMA.AppFramework;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using AMA.AppFramework.Utils.User;
using static AMA.AppFramework.Utils.User.UserUtils;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AMA.UITest
{
    [BrowserMode(BrowserMode.New)]
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    //[LocalSeleniumTestFixture(BrowserNames.Firefox)]
    //[LocalSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Firefox)]
    [RemoteSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [TestFixture]
    public class AMA_ResidentTestFlow_Tests : TestBase
    {
       
        #region Constructors
        public AMA_ResidentTestFlow_Tests(string browserName) : base(browserName) { }

        // Remote Selenium Grid Test
        public AMA_ResidentTestFlow_Tests(string browserName, string version, string platform, string hubUri, string extrasUri)
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

        
       


        //[TestCase("Ellis Hospital", "Learning111!!!")]
        [Description("Workflow Login as Admin choose Institution assign program check  as aresident program is assigned then unassign programm  and" +
           "check as a resident again that is not programm is removed")]
        [Property("Status", "not Complete")]
        [Author("Azat Chariyev")]

        public void Resident_Pass_Test(string InstitutionName, string CurriculumName)
        {
            UserInfo role = UserUtils.GetUser(UserRole.Ama_Staff);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            EducationCenterPage ED = LP.LoginAsUser(role.Username, role.Password);

            ///  2.click to GCEP link  navigate to Gcep page and waiting load icon disappear
            GCEPPage Gcep = ED.ClickToAdvance(ED.GcepLnk);

            ///  3.from Gcep navigating to institution managment searching for institution looking for curriculum and if their any curriculum with the same name deleting and starting create new curriculum.
            InstitutionsPage Instute = Gcep.ClickToAdvance(Gcep.InstitutionManagLnk);
            InstitutionsGCEPPage InsGcep = Instute.SearchforInstitutions(InstitutionName);

            ProgramsPage Program = InsGcep.ClickToAdvance(InsGcep.InstitutionProgramManagmentLnk);
            Program.UnassignCurriculum();

            CurriculumMngPage Curriculum = InsGcep.ClickToAdvance(InsGcep.InstitutionCurriculumTmpLnk);
            Curriculum.Search(CurriculumName);
            Curriculum.DeleteCurriculum(CurriculumName);
            CurriculumCoursePage CurCoursPage = Curriculum.ClickToAdvance(Curriculum.CreateCurriculumTemplateBtn);

            ///  4.Form course page choosing available courses from table by index
            CurCoursPage.AddOrRemoveCourses(CurCoursPage.AvailableCoursesTbl, CurCoursPage.AddSelectedBtn, 1, 4, 5);

           // List<string> CourseNames = new List<string>();
            List<string> CourseNames = CurCoursPage.GetTheNamesChoosenCourses();

            ///  5. Giving the name for curriculum passing parameter from TestCase as a string
            CurCoursPage.CurriculumNameTxt.Clear();
            CurCoursPage.CurriculumNameTxt.SendKeys(CurriculumName);

            ///  6.Saving curriculum and navigating to the pgy pages to assigne course to students
            PGYAssignmentPage PGY = CurCoursPage.ClickToAdvance(CurCoursPage.NextBtn);

            ///  7.Choosing student years to assign course by index for each course
            PGY.Grid_ClickElementWithoutTextInsideRow(PGY.CourseTbl, 1, 5);
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
            string EndingDate = Assign.ChoosingEndDate(1, "MM/d/yyyy");
            Assign.AssignProgramm();
            AssignSummaryPage Summary = Assign.ClickToAdvance(Assign.NextBtn);

            ///  11.Verifying from Assign Summary page program is displayed
            Assert.IsTrue(Summary.CreatedProgramName.Displayed);
            Assert.AreEqual((Summary.CreatedProgramName.Text), CurriculumName);

            ///  12.Verifying Assing confirmation test page curriculum name and starting date and ending dates are there which we choose.
            AssignConfirmationPage Confirmation = Summary.ClickToAdvance(Summary.NextBtn);
            // Assert.IsTrue(Confirmation.Grid_CellTextFound(Confirmation.ProgramSummaryTbl, StartingDate + " - " + EndingDate));
            Thread.Sleep(0500);
            Confirmation.ConfirmBtn.Click();

            ///  13.Signing out and Signing in as a Resindent counting required courses from myRequiredCourses
            Curriculum.ClickToAdvance(Curriculum.SignOutLnk);
            LP = Navigation.GoToLoginPage(browser);
            ED = LP.LoginAsUser("10021387", "password");//10021375,10021377,21387
            Gcep = ED.ClickToAdvance(ED.GcepLnk);
            Assert.True(Gcep.VerificationOfChoosenCoursesAssignedForResident(browser, CourseNames),"Course count are not equal");
            CourseTestPage Course = Gcep.ResidentStartCourseOrContinue(browser, CourseNames[1]);
            Gcep = Course.TestPass();
            Assert.IsTrue(Gcep.VerificationCourseCompletion(browser, CourseNames[1], "View Certificate"),"View Certificet button not visible");

        }

        //[Test]
        [Description("")]
        [Property("Status", "not Complete")]
        [Author("Azat Chariyev")]

        public void ResidentPageGCEPPractice()
        {
            char[] delimitersChars = {  '.', ',','/' };
            
            LoginPage LP = Navigation.GoToLoginPage(browser);
            EducationCenterPage ED = LP.LoginAsUser("10021387", "password");
            StringBuilder sb = new StringBuilder();
            ///  2.click to GCEP link  navigate to Gcep page and waiting load icon disappear
            GCEPPage Gcep = ED.ClickToAdvance(ED.GcepLnk);
            string CourseTracker = Gcep.ResidentCourseTrackerLbl.Text;
            if (!CourseTracker.Contains("0"))
            {
                string[] courseword = CourseTracker.Split(' ');
                string[] countofcourses = courseword[2].Split('/');
                int completecourses = Convert.ToInt16(countofcourses[0]);
                int totalcourses = Convert.ToInt16(countofcourses[1]);
                List<string> something = new List<string>(Browser.FindElements(By.XPath("//div[@class='activity-info-header margin-top-bottom-10']")).Select(iw => iw.Text));
                something.ForEach(somethings => sb.Append(somethings + '/'));
                String about = sb.ToString();
                string[] words = about.Split(delimitersChars);
                Thread.Sleep(10000);
            }
            else
            {
                string[] courseword = CourseTracker.Split(' ');
                int nocourses = Convert.ToInt16(courseword[2]);
            }
        }

        //[TestCase("Ellis Hospital", "Learning111!!!")]
        [Description("")]
        [Property("Status", "not Complete")]
        [Author("Azat Chariyev")]

        public void Resident_Fail_Test(string InstitutionName, string CurriculumName)
        {
            UserInfo role = UserUtils.GetUser(UserRole.Ama_Staff);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            EducationCenterPage ED = LP.LoginAsUser(role.Username, role.Password);

            ///  2.click to GCEP link  navigate to Gcep page and waiting load icon disappear
            GCEPPage Gcep = ED.ClickToAdvance(ED.GcepLnk);

            ///  3.from Gcep navigating to institution managment searching for institution looking for curriculum and if their any curriculum with the same name deleting and starting create new curriculum.
            InstitutionsPage Instute = Gcep.ClickToAdvance(Gcep.InstitutionManagLnk);
            InstitutionsGCEPPage InsGcep = Instute.SearchforInstitutions(InstitutionName);

            ProgramsPage Program = InsGcep.ClickToAdvance(InsGcep.InstitutionProgramManagmentLnk);
            Program.UnassignCurriculum();

            CurriculumMngPage Curriculum = InsGcep.ClickToAdvance(InsGcep.InstitutionCurriculumTmpLnk);
            Curriculum.Search(CurriculumName);
            Curriculum.DeleteCurriculum(CurriculumName);
            CurriculumCoursePage CurCoursPage = Curriculum.ClickToAdvance(Curriculum.CreateCurriculumTemplateBtn);

            ///  4.Form course page choosing available courses from table by index
            CurCoursPage.AddOrRemoveCourses(CurCoursPage.AvailableCoursesTbl, CurCoursPage.AddSelectedBtn, 1, 8, 9);

            // List<string> CourseNames = new List<string>();
            List<string> CourseNames = CurCoursPage.GetTheNamesChoosenCourses();

            ///  5. Giving the name for curriculum passing parameter from TestCase as a string
            CurCoursPage.CurriculumNameTxt.Clear();
            CurCoursPage.CurriculumNameTxt.SendKeys(CurriculumName);

            ///  6.Saving curriculum and navigating to the pgy pages to assigne course to students
            PGYAssignmentPage PGY = CurCoursPage.ClickToAdvance(CurCoursPage.NextBtn);

            ///  7.Choosing student years to assign course by index for each course
            PGY.Grid_ClickElementWithoutTextInsideRow(PGY.CourseTbl, 1, 5);
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
            string EndingDate = Assign.ChoosingEndDate(1, "MM/d/yyyy");
            Assign.AssignProgramm();
            AssignSummaryPage Summary = Assign.ClickToAdvance(Assign.NextBtn);

            ///  11.Verifying from Assign Summary page program is displayed
            Assert.IsTrue(Summary.CreatedProgramName.Displayed);
            Assert.AreEqual((Summary.CreatedProgramName.Text), CurriculumName);

            ///  12.Verifying Assing confirmation test page curriculum name and starting date and ending dates are there which we choose.
            AssignConfirmationPage Confirmation = Summary.ClickToAdvance(Summary.NextBtn);
            // Assert.IsTrue(Confirmation.Grid_CellTextFound(Confirmation.ProgramSummaryTbl, StartingDate + " - " + EndingDate));
            Thread.Sleep(0500);
            Confirmation.ConfirmBtn.Click();

            ///  13.Signing out and Signing in as a Resindent counting required courses from myRequiredCourses
            Curriculum.ClickToAdvance(Curriculum.SignOutLnk);
            LP = Navigation.GoToLoginPage(browser);
            ED = LP.LoginAsUser("10021377", "password");//10021375,10021377,21387
            Gcep = ED.ClickToAdvance(ED.GcepLnk);
            Assert.True(Gcep.VerificationOfChoosenCoursesAssignedForResident(browser, CourseNames), "Course count are not equal");
            CourseTestPage Course = Gcep.ResidentStartCourseOrContinue(browser, CourseNames[2]);
            Gcep = Course.LearnerTestFail();
            Assert.IsTrue(Gcep.VerificationCourseCompletion(browser, CourseNames[2], "Administrator Notified"), "View Certificet button not visible");
        }


        [Test]
        [Description("Learner Verifying Sort by Due Date Controls")]
        [Property("Status", "Complete")]
        [Author("Azat Chariyev")]
       

        public void Resident_GCEP_SortBy_DueDate()
        {
            StringBuilder sb = new StringBuilder();
            ///  1.Navigate to the login page login as
            LoginPage LP = Navigation.GoToLoginPage(browser);
            EducationCenterPage ED = LP.LoginAsUser("10031194", "password");

            ///  2.click to GCEP link  navigate to Gcep page and waiting load icon disappear
            GCEPPage Gcep = ED.ClickToAdvance(ED.GcepLnk);
          
            ///  3. Waiting to course tracker dislay and gennting count of courses if there is not any course assigned assign courses
            Browser.WaitForElement(Bys.GCEPPage.ResidentCourseTrackerLbl,TimeSpan.FromSeconds(120), ElementCriteria.IsVisible);
            string CourseTracker = Gcep.ResidentCourseTrackerLbl.Text;
            if (Browser.FindElements(Bys.GCEPPage.ResidentGcepShowElectiveCourseLnk).Count>0)
            {
                if (CourseTracker.Contains("0"))
                {
                    Gcep.ClickToAdvance(Gcep.SignOutLnk);
                    LP = Navigation.GoToLoginPage(browser);
                    ED = LP.LoginAsUser("10031315", "password");
                    Gcep = ED.ClickToAdvance(ED.GcepLnk);
                    ///  3.from Gcep navigating to institution managment searching for institution looking for curriculum and if their any curriculum with the same name deleting and starting create new curriculum.
                    InstitutionsPage Instute = Gcep.ClickToAdvance(Gcep.InstitutionManagLnk);
                    InstitutionsGCEPPage InsGcep = Instute.SearchforInstitutions("");

                    ProgramsPage Program = InsGcep.ClickToAdvance(InsGcep.InstitutionProgramManagmentLnk);
                    Program.UnassignCurriculum();

                    CurriculumMngPage Curriculum = InsGcep.ClickToAdvance(InsGcep.InstitutionCurriculumTmpLnk);
                    Curriculum.Search("");
                    Curriculum.DeleteCurriculum("");
                    CurriculumCoursePage CurCoursPage = Curriculum.ClickToAdvance(Curriculum.CreateCurriculumTemplateBtn);

                    ///  4.Form course page choosing available courses from table by index
                    CurCoursPage.AddOrRemoveCourses(CurCoursPage.AvailableCoursesTbl, CurCoursPage.AddSelectedBtn, 1, 8, 9);

                    // List<string> CourseNames = new List<string>();
                    List<string> CourseNames = CurCoursPage.GetTheNamesChoosenCourses();

                    ///  5. Giving the name for curriculum passing parameter from TestCase as a string
                    CurCoursPage.CurriculumNameTxt.Clear();
                    CurCoursPage.CurriculumNameTxt.SendKeys("");

                    ///  6.Saving curriculum and navigating to the pgy pages to assigne course to students
                    PGYAssignmentPage PGY = CurCoursPage.ClickToAdvance(CurCoursPage.NextBtn);

                    ///  7.Choosing student years to assign course by index for each course
                    PGY.Grid_ClickElementWithoutTextInsideRow(PGY.CourseTbl, 1, 5);
                    PGY.Grid_ClickElementWithoutTextInsideRow(PGY.CourseTbl, 2, 5);
                    PGY.Grid_ClickElementWithoutTextInsideRow(PGY.CourseTbl, 3, 5);

                    ///  8. Saving curriculum and navigating to curriculum management page again
                    PGY.ClickToAdvance(PGY.SaveExitBtn);

                    /// 9.Finding curriculum what we create and assigning to the programm
                    Curriculum.Search("");
                    Curriculum.Actioncell.Click();
                    AssignProgramPage Assign = Curriculum.ClickToAdvance(Curriculum.AssignToProgrammLnk);

                    ///  10.Choosing starting date and ending date for  program  and clicking next button
                    string StartingDate = Assign.ChoosingStartDate();
                    string EndingDate = Assign.ChoosingEndDate(1, "MM/d/yyyy");
                    Assign.AssignProgramm();
                    AssignSummaryPage Summary = Assign.ClickToAdvance(Assign.NextBtn);

                    ///  11.Verifying from Assign Summary page program is displayed
                    Assert.IsTrue(Summary.CreatedProgramName.Displayed);
                    Assert.AreEqual((Summary.CreatedProgramName.Text), "");

                    ///  12.Verifying Assing confirmation test page curriculum name and starting date and ending dates are there which we choose.
                    AssignConfirmationPage Confirmation = Summary.ClickToAdvance(Summary.NextBtn);
                    // Assert.IsTrue(Confirmation.Grid_CellTextFound(Confirmation.ProgramSummaryTbl, StartingDate + " - " + EndingDate));
                    Thread.Sleep(0500);
                    Confirmation.ConfirmBtn.Click();
                    ///  13.Signing out and Signing in as a Resindent counting required courses from myRequiredCourses
                    Curriculum.ClickToAdvance(Curriculum.SignOutLnk);
                    LP = Navigation.GoToLoginPage(browser);
                    ED = LP.LoginAsUser("10021377", "password");//10021375,10021377,21387
                    Gcep = ED.ClickToAdvance(ED.GcepLnk); //31224
                }                

                ///  4.Scrolling down to get all informartion about courses and verify that explore elective course link displayed.
                do { ElemSet.ScrollToElement(browser, Gcep.FaceBookLnk);Thread.Sleep(1000); }
                while (!Gcep.ResidentGcepShowElectiveCourseLnk.Displayed);
                Assert.True(Gcep.ResidentGcepShowElectiveCourseLnk.Displayed);

                ///  5.Getting all due date and verifying its in ascending order
                Assert.True(Gcep.ResidentCourseCompareDate());                
              
            }
            else { }
        }



        [Test]
        [Description("Learner Verifying Sort by Duration Controls")]
        [Property("Status", "Complete")]
        [Author("Azat Chariyev")]
      

        public void Resident_GCEP_SortBy_Duration()
        {          
            ///  1.Navigate to the login page login as
            LoginPage LP = Navigation.GoToLoginPage(browser);
            EducationCenterPage ED = LP.LoginAsUser("10031194", "password");

            ///  2.click to GCEP link  navigate to Gcep page and waiting load icon disappear
            GCEPPage Gcep = ED.ClickToAdvance(ED.GcepLnk);

            ///  3. Waiting to course tracker dislay and gennting count of courses if there is not any course assigned assign courses
            Browser.WaitForElement(Bys.GCEPPage.ResidentCourseTrackerLbl, TimeSpan.FromSeconds(120), ElementCriteria.IsVisible);
            string CourseTracker = Gcep.ResidentCourseTrackerLbl.Text;

            ///  4.Clicking to Sort By duration button and wait to page reload.
            Gcep.ResidentGCEPSortBYDurationBtn.Click();
            Gcep.WaitForInitialize();
         
            if (CourseTracker.Contains("0"))
            {
                Gcep.ClickToAdvance(Gcep.SignOutLnk);
                LP = Navigation.GoToLoginPage(browser);
                ED = LP.LoginAsUser("10031315", "password");
                Gcep = ED.ClickToAdvance(ED.GcepLnk);
                ///  3.from Gcep navigating to institution managment searching for institution looking for curriculum and if their any curriculum with the same name deleting and starting create new curriculum.
                InstitutionsPage Instute = Gcep.ClickToAdvance(Gcep.InstitutionManagLnk);
                InstitutionsGCEPPage InsGcep = Instute.SearchforInstitutions("");

                ProgramsPage Program = InsGcep.ClickToAdvance(InsGcep.InstitutionProgramManagmentLnk);
                Program.UnassignCurriculum();

                CurriculumMngPage Curriculum = InsGcep.ClickToAdvance(InsGcep.InstitutionCurriculumTmpLnk);
                Curriculum.Search("");
                Curriculum.DeleteCurriculum("");
                CurriculumCoursePage CurCoursPage = Curriculum.ClickToAdvance(Curriculum.CreateCurriculumTemplateBtn);

                ///  4.Form course page choosing available courses from table by index
                CurCoursPage.AddOrRemoveCourses(CurCoursPage.AvailableCoursesTbl, CurCoursPage.AddSelectedBtn, 1, 8, 9);

                // List<string> CourseNames = new List<string>();
                List<string> CourseNames = CurCoursPage.GetTheNamesChoosenCourses();

                ///  5. Giving the name for curriculum passing parameter from TestCase as a string
                CurCoursPage.CurriculumNameTxt.Clear();
                CurCoursPage.CurriculumNameTxt.SendKeys("");

                ///  6.Saving curriculum and navigating to the pgy pages to assigne course to students
                PGYAssignmentPage PGY = CurCoursPage.ClickToAdvance(CurCoursPage.NextBtn);

                ///  7.Choosing student years to assign course by index for each course
                PGY.Grid_ClickElementWithoutTextInsideRow(PGY.CourseTbl, 1, 5);
                PGY.Grid_ClickElementWithoutTextInsideRow(PGY.CourseTbl, 2, 5);
                PGY.Grid_ClickElementWithoutTextInsideRow(PGY.CourseTbl, 3, 5);

                ///  8. Saving curriculum and navigating to curriculum management page again
                PGY.ClickToAdvance(PGY.SaveExitBtn);

                /// 9.Finding curriculum what we create and assigning to the programm
                Curriculum.Search("");
                Curriculum.Actioncell.Click();
                AssignProgramPage Assign = Curriculum.ClickToAdvance(Curriculum.AssignToProgrammLnk);

                ///  10.Choosing starting date and ending date for  program  and clicking next button
                string StartingDate = Assign.ChoosingStartDate();
                string EndingDate = Assign.ChoosingEndDate(1, "MM/d/yyyy");
                Assign.AssignProgramm();
                AssignSummaryPage Summary = Assign.ClickToAdvance(Assign.NextBtn);

                ///  11.Verifying from Assign Summary page program is displayed
                Assert.IsTrue(Summary.CreatedProgramName.Displayed);
                Assert.AreEqual((Summary.CreatedProgramName.Text), "");

                ///  12.Verifying Assing confirmation test page curriculum name and starting date and ending dates are there which we choose.
                AssignConfirmationPage Confirmation = Summary.ClickToAdvance(Summary.NextBtn);
                // Assert.IsTrue(Confirmation.Grid_CellTextFound(Confirmation.ProgramSummaryTbl, StartingDate + " - " + EndingDate));
                Thread.Sleep(0500);
                Confirmation.ConfirmBtn.Click();
                ///  13.Signing out and Signing in as a Resindent counting required courses from myRequiredCourses
                Curriculum.ClickToAdvance(Curriculum.SignOutLnk);
                LP = Navigation.GoToLoginPage(browser);
                ED = LP.LoginAsUser("10021377", "password");//10021375,10021377,21387
                Gcep = ED.ClickToAdvance(ED.GcepLnk); //31224
            }

            ///  5.Scrolling down to get all informartion about courses and verify that exploer elective course link displayed.
            do { ElemSet.ScrollToElement(browser, Gcep.FaceBookLnk);Thread.Sleep(1000); }
            while (!Gcep.ResidentGcepShowElectiveCourseLnk.Displayed);  //while (j< countofCourse);
            Assert.True(Gcep.ResidentGcepShowElectiveCourseLnk.Displayed);
          
            ///  6.Getting all duration for courses and verifying its in ascending order
            Assert.True(Gcep.ResidentCourseCompareDuration());            
           
            
        }



        [Test]
        [Description("Learner Verifying Sort by Progress Controls")]
        [Property("Status", "Complete")]
        [Author("Azat Chariyev")]    

        public void Resident_GCEP_SortBy_Progress()
        {
            ///  1.Navigate to the login page login as
            UserInfo role = UserUtils.GetUser(UserRole.Ama_Staff);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            EducationCenterPage ED = LP.LoginAsUser("10031193", "password");

            ///  2.click to GCEP link  navigate to Gcep page and waiting load icon disappear
            GCEPPage Gcep = ED.ClickToAdvance(ED.GcepLnk);

            ///  3. Clicking resident sort by progrees button.
            Gcep.ResidentGCEPSortBYProgressBtn.Click();
            Gcep.WaitForInitialize();

            ///  4.Clicking to Sort By duration button and wait to page reload.            
            do { ElemSet.ScrollToElement(browser, Gcep.FaceBookLnk); }
            while (!Gcep.ResidentGcepShowElectiveCourseLnk.Displayed);

            ///  5.Getting all progress information checking all conditions by AC#28

            Assert.True(Gcep.ResidentCourseProgressVerification(Gcep.ResidentCoutseStatusFailedLbl, Gcep.ResidentCourseStatusLockedLbl));
        
        }


        [Test]
        [Description("Learner Verifying Transcript and Certificates buttons are displayed")]
        [Property("Status", "Complete")]
        [Author("Azat Chariyev")]

        public void Resident_CompletedCourse_Listing()
        {
            ///  1.Navigate to the login page login as
            LoginPage LP = Navigation.GoToLoginPage(browser);
            EducationCenterPage ED = LP.LoginAsUser("10031198", "password");

            ///  2.click to GCEP link  navigate to Gcep page and waiting load icon disappear
            GCEPPage Gcep = ED.ClickToAdvance(ED.GcepLnk);

            ///  3. 
            Gcep.ResidentGCEPCompletedSwitchBtn.Click();

            ///  4.Verifying transcript and certificate buttons are displayed.
            Assert.IsTrue(Gcep.ResidentGCEPTranscriptBtn.Displayed);
            Assert.IsTrue(Gcep.ResidentGCEPCertificatesDownloadBtn.Displayed);


            ///  5. Verifying completion dates are descending order.
            List<string> CompletionDateText = new List<string>();
            List<string> CompletionDateTextRaw = new List<string>(Browser.FindElements(By.XPath("//div[@class='col-xs-12 col-md-2 activity-status-action']/button/../span[@class='completed-date ng-binding']")).Select(iw => iw.Text));
            for(int i=0;i<CompletionDateTextRaw.Count;i++)
            {
                CompletionDateText.Add( CompletionDateTextRaw[i].Remove(0, 11));
            }
            if (CompletionDateText.Count > 1)
            {
                for(int j=0; j < CompletionDateText.Count-1;  j++)
                {
                    DateTime date = Convert.ToDateTime(CompletionDateText[j]);
                    DateTime dateNext = Convert.ToDateTime(CompletionDateText[j+1]);
                    Assert.True(AssertUtils.DateGreaterThanOrEquals(dateNext, date));
                    Assert.True(date >= dateNext);
                }
            }
        }

        [Test]
        [Description("Learner Verifying Transcript and Certificates based on Acdemic years")]
        [Property("Status", "Complete")]
        [Author("Azat Chariyev")]

        public void Resident_CompletedCourse_Listing_AcedemicYear()
        {
            int countOfMatchingCoursesFor2018_2019 = 0;
            int countOfMatchingCoursesFor2017_2018 = 0;
            int countOfMatchingCoursesFor2016_2017 = 0;

            ///  1.Navigate to the login page login as
            LoginPage LP = Navigation.GoToLoginPage(browser);
            EducationCenterPage ED = LP.LoginAsUser("10031194", "password");//30248

            ///  2.click to GCEP link  navigate to Gcep page and waiting load icon disappear
            GCEPPage Gcep = ED.ClickToAdvance(ED.GcepLnk);

            ///  3. Waiting to course tracker dislay and getting count of courses if there is not any course assigned assign courses
            Gcep.ResidentGCEPCompletedSwitchBtn.Click();

            Browser.WaitForElement(Bys.GCEPPage.ResidentGCEPAcedimicYearSelElem,TimeSpan.FromSeconds(90), ElementCriteria.IsVisible,ElementCriteria.IsEnabled);
            Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);
        
            if (!Gcep.ResidentGCEPTranscriptBtn.GetAttribute("class").Contains("disabled")) 
            {
                ///  4.Verifying transcript and certificate buttons are displayed and due date,progress and duration buttons are disabled..
                Assert.IsTrue(Gcep.ResidentGCEPTranscriptBtn.Displayed);
                Assert.IsTrue(Gcep.ResidentGCEPCertificatesDownloadBtn.Displayed);
                Assert.AreEqual("disabled", Gcep.ResidentGCEPSortBYDueDateBtn.GetAttribute("class"));
                Assert.AreEqual("disabled", Gcep.ResidentGCEPSortBYDurationBtn.GetAttribute("class"));
                Assert.AreEqual("disabled", Gcep.ResidentGCEPSortBYProgressBtn.GetAttribute("class"));

                ///  5. Scrolling down to get all information about completed courses.          
                Gcep.ScrolltoGetAllCourses();

                ///  6. 
                List<string> courseCompletionYears = new List<string>();
                List<string> CompletionDateTextRaw = new List<string>(Browser.FindElements(By.XPath("//div[@class='col-xs-12 col-md-2 activity-status-action']/button/../span[@class='completed-date ng-binding']")).Select(iw => iw.Text));
                for (int i = 0; i < CompletionDateTextRaw.Count; i++)
                {
                    courseCompletionYears.Add(CompletionDateTextRaw[i].Remove(0, 11)); }


                List<string> selectValueAcedemicYearDropdown = new List<string>(Browser.FindElements(Bys.GCEPPage.ResidentGCEPAcedimicYearSelElem).Select(IWebElement => IWebElement.Text));
                string selectValueAcedemicYearDropdownTextRaw = selectValueAcedemicYearDropdown[0].ToString();
                string[] selectedAcademicYear = null;
                if (BrowserName == BrowserNames.InternetExplorer)
                {
                    selectedAcademicYear = new string[] { "2018-2019", "2017-2018", "2016-2017" };
                }
                else
                {
                    string[] separatingChars = { "All Academic Years\r\n", " ", "\r\n" };
                    selectedAcademicYear = selectValueAcedemicYearDropdownTextRaw.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);
                }
                if ("2018-2019" == selectedAcademicYear[0])
                {
                    for (int k = 0; k < courseCompletionYears.Count; k++)
                    {
                        if (Convert.ToDateTime("07 / 01 / 2018") <= Convert.ToDateTime(courseCompletionYears[k]) & Convert.ToDateTime(courseCompletionYears[k]) <= Convert.ToDateTime("06 / 30 / 2019"))
                        {
                            countOfMatchingCoursesFor2018_2019++;
                        }
                    }

                }
                if ("2017-2018" == selectedAcademicYear[1])
                {
                    for (int l = 0; l < courseCompletionYears.Count; l++)
                    {
                        if (Convert.ToDateTime("07 / 01 / 2017") <= Convert.ToDateTime(courseCompletionYears[l]) & Convert.ToDateTime(courseCompletionYears[l]) <= Convert.ToDateTime("06 / 30 / 2018"))
                        {
                            countOfMatchingCoursesFor2017_2018++;
                        }
                    }

                }
                if ("2016-2017" == selectedAcademicYear[2])
                {
                    for (int n = 0; n < courseCompletionYears.Count; n++)
                    {
                        if (Convert.ToDateTime("07 / 01 / 2016") <= Convert.ToDateTime(courseCompletionYears[n]) & Convert.ToDateTime(courseCompletionYears[n]) <= Convert.ToDateTime("06 / 30 / 2017"))
                        {
                            countOfMatchingCoursesFor2016_2017++;
                        }
                    }
                }

                Gcep.ResidentGCEPAcedimicYearSelElem.SelectByIndex(1);
                Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);
                ElemSet.ScrollToElement(Browser, Gcep.FaceBookLnk);

                if (countOfMatchingCoursesFor2018_2019 == 0)
                { Assert.True(Gcep.ResidentNoCourseBeenCompltetLbl.Displayed); }
                else
                {
                    IList<IWebElement> CourseRows = Browser.FindElements(By.XPath("//div[@class='activity-listing-bottom ng-scope']"));
                    int countOfCoursesFirstIndex = CourseRows.Count;
                    Assert.True(countOfMatchingCoursesFor2018_2019 == countOfCoursesFirstIndex);
                }


                Gcep.ResidentGCEPAcedimicYearSelElem.SelectByIndex(2);
                Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);
                Gcep.ScrolltoGetAllCourses();


                if (countOfMatchingCoursesFor2017_2018 == 0)
                { Assert.True(Gcep.ResidentNoCourseBeenCompltetLbl.Displayed); }
                else
                {
                    IList<IWebElement> CourseRows = Browser.FindElements(By.XPath("//div[@class='activity-listing-bottom ng-scope']"));
                    int countOfCoursesSecondIndex = CourseRows.Count;
                    Assert.True(countOfMatchingCoursesFor2017_2018 == countOfCoursesSecondIndex);
                }

                Gcep.ResidentGCEPAcedimicYearSelElem.SelectByIndex(3);
                Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);
                Gcep.ScrolltoGetAllCourses();

                if (countOfMatchingCoursesFor2016_2017 == 0)
                { Assert.True(Gcep.ResidentNoCourseBeenCompltetLbl.Displayed); }
                else
                {
                    IList<IWebElement> CourseRows = Browser.FindElements(By.XPath("//div[@class='activity-listing-bottom ng-scope']"));
                    int countOfCoursesThirdIndex = CourseRows.Count;
                    Assert.True(countOfMatchingCoursesFor2016_2017 == countOfCoursesThirdIndex);
                }
            }
           
        }


        [Test]
        [Description("")]
        [Property("Status", "progress")]
        [Author("Azat Chariyev")]

        public void Resident_CourseListing_StartNow()
        {
            ///  1.Navigate to the login page login as
            LoginPage LP = Navigation.GoToLoginPage(browser);
            EducationCenterPage ED = LP.LoginAsUser("10030248", "password");//30248

            ///  2.click to GCEP link  navigate to Gcep page and waiting load icon disappear
            GCEPPage Gcep = ED.ClickToAdvance(ED.GcepLnk);

            ///  3. Waiting to course tracker to display and verifying all sort by buttons are displayed and enabled
            Browser.WaitForElement(Bys.GCEPPage.ResidentCourseTrackerLbl, TimeSpan.FromSeconds(120), ElementCriteria.IsVisible);
            Assert.True( Gcep.ResidentGCEPSortBYDueDateBtn.Displayed);
            Assert.True(Gcep.ResidentGCEPSortBYDurationBtn.Displayed);
            Assert.True(Gcep.ResidentGCEPSortBYProgressBtn.Displayed);


            ///  4.If Start Now button disabled then scrol down and click show elective courses
            if (Gcep.ResidentCouseStartNowBtn.GetAttribute("class").Contains("disabled"))
            {
                Gcep.ScrolltoGetAllCourses();
                Gcep.ResidentGcepShowElectiveCourseLnk.Click();
            }

            ///  5.Scrolling down and saving all courses headers.
            Gcep.ScrolltoGetAllCourses();
            List<string> courseTitlesByDueDate = new List<string>(Browser.FindElements(By.XPath("//div[@class='row activity-listing-row']//h4")).Select(iw => iw.Text));


            ///  6.Getting locked courses headers and comparing with all courses headers by due date if its matching removing locked courses headers
            List<string> lockedCourseTitlesByDueDate = new List<string>(Browser.FindElements(By.XPath(" //span[@class='locked']/../..//div[@class='activity-info-name']/h4")).Select(iw => iw.Text));
            if (lockedCourseTitlesByDueDate.Count > 0)
            {
                for (int i = 0; i < courseTitlesByDueDate.Count; i++)
                {
                    for (int j = 0; j < lockedCourseTitlesByDueDate.Count; j++)
                    {
                        if (courseTitlesByDueDate[i] == lockedCourseTitlesByDueDate[j])
                        {
                            courseTitlesByDueDate.Remove(courseTitlesByDueDate[i]);
                        }
                    }
                }
            }

            ///  7.Getting completed courses headers and comparing  with all courses headers by due date if its matching removing completed courses headers.
            List<string> completedCourseTitlesByDueDate = new List<string>(Browser.FindElements(By.XPath(" //span[@class='completed-date ng-binding']/../..//div[@class='activity-info-name']/h4")).Select(iw => iw.Text));
            if (completedCourseTitlesByDueDate.Count > 0)
            {
                for (int i = 0; i < courseTitlesByDueDate.Count; i++)
                {
                    for (int j = 0; j < completedCourseTitlesByDueDate.Count; j++)
                    {
                        if (courseTitlesByDueDate[i] == completedCourseTitlesByDueDate[j])
                        {
                            courseTitlesByDueDate.Remove(courseTitlesByDueDate[i]);
                        }
                    }
                }
            }

            ///  8.After removing locked courses and completed courses headers getting first course header and saving it.Then clicking start now button
            string firstCourseTitleByDueDate = courseTitlesByDueDate[0];
            ElemSet.ScrollToElement(Browser, Gcep.ResidentCourseTrackerLbl);
            CourseTestPage CTP = Gcep.ClickToAdvance(Gcep.ResidentCouseStartNowBtn);

            ///  9.getting course name header and comparing with our first course header what saved above and navigating back
            string startedCourseTitleByDueDate = CTP.CourseTitleLbl.Text;
            Assert.AreEqual(firstCourseTitleByDueDate, startedCourseTitleByDueDate);
            Browser.Navigate().Back();
            Gcep.WaitForInitialize();

            Gcep.ResidentGCEPSortBYDurationBtn.Click();
            Gcep.WaitForInitialize();
            if (Gcep.ResidentCouseStartNowBtn.GetAttribute("class").Contains("disabled"))
            {
                Gcep.ScrolltoGetAllCourses();
                Gcep.ResidentGcepShowElectiveCourseLnk.Click();
            }

            Gcep.ScrolltoGetAllCourses();

            List<string> courseTitlesByDuration = new List<string>(Browser.FindElements(By.XPath("//div[@class='row activity-listing-row']//h4")).Select(iw => iw.Text));

            List<string> lockedCourseTitlesByDuration = new List<string>(Browser.FindElements(By.XPath(" //span[@class='locked']/../..//div[@class='activity-info-name']/h4")).Select(iw => iw.Text));
            if (lockedCourseTitlesByDuration.Count > 0)
            {
                for(int i= 0;i< courseTitlesByDuration.Count; i++)
                {
                    for(int j =0;j<lockedCourseTitlesByDuration.Count; j++)
                    {
                        if (courseTitlesByDuration[i] == lockedCourseTitlesByDuration[j])
                        {
                            courseTitlesByDuration.Remove(courseTitlesByDuration[i]);
                        }
                    }
                }
            }
           
            List<string> completedCourseTitlesByDuration = new List<string>(Browser.FindElements(By.XPath(" //span[@class='completed-date ng-binding']/../..//div[@class='activity-info-name']/h4")).Select(iw => iw.Text));
            if (completedCourseTitlesByDuration.Count > 0)
            {
                for (int i = 0; i < courseTitlesByDuration.Count; i++)
                {
                    for (int j = 0; j < completedCourseTitlesByDuration.Count; j++)
                    {
                        if (courseTitlesByDuration[i] == completedCourseTitlesByDuration[j])
                        {
                            courseTitlesByDuration.Remove(courseTitlesByDuration[i]);
                        }
                    }
                }
            }

            string firstCourseTitleByDuration = courseTitlesByDuration[0];

            ElemSet.ScrollToElement(Browser, Gcep.ResidentCourseTrackerLbl);
            CTP = Gcep.ClickToAdvance(Gcep.ResidentCouseStartNowBtn);

            string startedCourseTitleByDuration = CTP.CourseTitleLbl.Text;

            Assert.AreEqual(firstCourseTitleByDuration, startedCourseTitleByDuration);

            Browser.Navigate().Back();
            Gcep.WaitForInitialize();

            Gcep.ResidentGCEPSortBYProgressBtn.Click();
            Gcep.WaitForInitialize();

            if (Gcep.ResidentCouseStartNowBtn.GetAttribute("class").Contains("disabled"))
            {
                Gcep.ScrolltoGetAllCourses();
                Gcep.ResidentGcepShowElectiveCourseLnk.Click();
            }

            Gcep.ScrolltoGetAllCourses();

            List<string> courseTitlesProgress = new List<string>(Browser.FindElements(By.XPath("//div[@class='row activity-listing-row']//h4")).Select(iw => iw.Text));

            List<string> lockedCourseTitlesByProgress = new List<string>(Browser.FindElements(By.XPath(" //span[@class='locked']/../..//div[@class='activity-info-name']/h4")).Select(iw => iw.Text));
            if (lockedCourseTitlesByProgress.Count > 0)
            {
                for (int i = 0; i < courseTitlesProgress.Count; i++)
                {
                    for (int j = 0; j < lockedCourseTitlesByProgress.Count; j++)
                    {
                        if (courseTitlesProgress[i] == lockedCourseTitlesByProgress[j])
                        {
                            courseTitlesProgress.Remove(courseTitlesProgress[i]);
                        }
                    }
                }
            }

            List<string> completedCourseTitlesByProgress = new List<string>(Browser.FindElements(By.XPath(" //span[@class='completed-date ng-binding']/../..//div[@class='activity-info-name']/h4")).Select(iw => iw.Text));
            if (completedCourseTitlesByProgress.Count > 0)
            {
                for (int i = 0; i < courseTitlesProgress.Count; i++)
                {
                    for (int j = 0; j < completedCourseTitlesByProgress.Count; j++)
                    {
                        if (courseTitlesProgress[i] == completedCourseTitlesByProgress[j])
                        {
                            courseTitlesProgress.Remove(courseTitlesProgress[i]);
                        }
                    }
                }
            }

            string firstCourseTitleByProgress = courseTitlesProgress[0];

            ElemSet.ScrollToElement(Browser, Gcep.ResidentCourseTrackerLbl);
            CTP = Gcep.ClickToAdvance(Gcep.ResidentCouseStartNowBtn);

            string startedCourseTitleByProgress = CTP.CourseTitleLbl.Text;

            Assert.AreEqual(firstCourseTitleByProgress, startedCourseTitleByProgress);

            Browser.Navigate().Back();
            Gcep.WaitForInitialize();


        }

        #endregion Tests
    }
}

