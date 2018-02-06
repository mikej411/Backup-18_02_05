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
using System.Linq;

/// <summary>
/// This class was created because of the NUnit bug that causes browser sessions to hang when using the [NonParallelizable] attribute above test
/// methods. The latest version does not have a fix, so we have to create this separate class and use the [NonParallelizable] attribute 
/// at the Test Fixture level. When there is a fix, we can put these back in their respective classes
/// </summary>
namespace AMA.UITest
{
    //[BrowserMode(BrowserMode.New)]
    //[LocalSeleniumTestFixture(BrowserNames.Chrome)]
    //[LocalSeleniumTestFixture(BrowserNames.Firefox)]
    //[LocalSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    //[RemoteSeleniumTestFixture(BrowserNames.Firefox)]
    //[RemoteSeleniumTestFixture(BrowserNames.InternetExplorer)]
    // These tests cant be run in parallel for various different reasons. See the comment above each test method for an explanation for each one.
    [NonParallelizable]
    [TestFixture]
    public class AMA_Nonparallel_Runs_OnesDaily_Tests : TestBase

    {
        #region Constructors
        public AMA_Nonparallel_Runs_OnesDaily_Tests(string browserName) : base(browserName) { }

        // Remote Selenium Grid Test
        public AMA_Nonparallel_Runs_OnesDaily_Tests(string browserName, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region tests

       

        [Test]
        [Description("Help Page elements changing depends on role verifying elements loging with different roles" +
           "Log in as a AMA staff then as a Admin Manager and a Resident")]
        [Property("Status", "Progress")]
        [Author("Azat Chariyev")]
    
        public void CourseTracker_Resident_RequiredCoursePass()
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

         
            string CourseTracker = Gcep.ResidentCourseTrackerLbl.Text;
            if (!CourseTracker.Contains('/'))
            {
                if (!CourseTracker.Contains("0"))
                {
                    string[] CourseTrackernotNull = CourseTracker.Split(' ');                    
                    int regCoursCount = Convert.ToInt16(CourseTrackernotNull[3]);
                    
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
                InstitutionsPage Instute1 = Gcep.ClickToAdvance(Gcep.InstitutionManagLnk);
                InstitutionsGCEPPage InsGcep1 = Instute1.SearchforInstitutions("Ellis Hospital");

                ProgramsPage Program1 = InsGcep1.ClickToAdvance(InsGcep1.InstitutionProgramManagmentLnk);
                Program1.UnassignCurriculum();


                CurriculumMngPage Curriculum1 = InsGcep1.ClickToAdvance(InsGcep1.InstitutionCurriculumTmpLnk);
                Curriculum1.Search("Learning111!!!");
                Curriculum1.DeleteCurriculum("Learning111!!!");
                CurriculumCoursePage CurCoursPage = Curriculum1.ClickToAdvance(Curriculum1.CreateCurriculumTemplateBtn);

                ///  4.Form course page choosing available courses from table by index
                CurCoursPage.AddOrRemoveCourses(CurCoursPage.AvailableCoursesTbl, CurCoursPage.AddSelectedBtn, 8, 9, 10, 11, 12, 13, 14, 15, 16);

                // List<string> CourseNames = new List<string>();
                List<string> CourseNames = CurCoursPage.GetTheNamesChoosenCourses();


                int CountofCoursewasAssigned = CourseNames.Count;


                ///  5. Giving the name for curriculum passing parameter from TestCase as a string
                CurCoursPage.CurriculumNameTxt.Clear();
                CurCoursPage.CurriculumNameTxt.SendKeys("Learning111!!!");

                ///  6.Saving curriculum and navigating to the pgy pages to assigne course to students
                PGYAssignmentPage PGY = CurCoursPage.ClickToAdvance(CurCoursPage.NextBtn);

                ///  7.Choosing student years to assign course by index for each course                      
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
                Curriculum1.Search("Learning111!!!");
                Curriculum1.Actioncell.Click();
                AssignProgramPage Assign = Curriculum1.ClickToAdvance(Curriculum1.AssignToProgrammLnk);

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
                Curriculum1.ClickToAdvance(Curriculum1.SignOutLnk);

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
                int CountofcoursesOnResidentGcepaftercourseAssignment = Convert.ToInt16(courseword2[3]);

                Thread.Sleep(2000);
                int CountofcoursesOnResidentGcepaftercourseAssigmentCompleted = Convert.ToInt16(courseword2[2]);

                Thread.Sleep(2000);

                Assert.True(CountofcoursesOnResidentGcepaftercourseAssignment.Equals(CountofCoursewasAssigned));
            }
            do { ElemSet.ScrollToElement(browser, Gcep.FaceBookLnk); }
            while (!Gcep.ResidentGcepShowElectiveCourseLnk.Displayed);

            
            ElemSet.ScrollToElement(browser, Gcep.ResidentCourseTrackerLbl);

            CourseTestPage Course = Gcep.ResidentStartCourseOrContinue(browser, "AUTOMATION_002");


            Gcep = Course.TestPass();

            do { ElemSet.ScrollToElement(browser, Gcep.FaceBookLnk); }
            while (!Gcep.ResidentGcepShowElectiveCourseLnk.Displayed);

            Assert.IsTrue(Gcep.VerificationCourseCompletion(browser, "AUTOMATION_002", "View Certificate"), "View Certificet button not visible");

            string CourseTrackerAfterTestCompletion = Gcep.ResidentCourseTrackerLbl.Text;
            string[] courseword3 = CourseTrackerAfterTestCompletion.Split(' ');
            int CountOfCompletedRegCourseafterPassingTest = Convert.ToInt16(courseword3[2]);
            //int some = CountofcoursesOnResidentGcepaftercourseAssigmentCompleted + 1;
            //Assert.True(CountofcoursesOnResidentGcepaftercourseAssigmentCompleted + 1 == (CountOfCompletedRegCourseafterPassingTest));
            Gcep.ClickToAdvance(Gcep.SignOutLnk);

            Thread.Sleep(2500);
            LP = Navigation.GoToLoginPage(browser);
            ED = LP.LoginAsUser(role.Username, role.Password);//10021375,10021377,21387
            Gcep = ED.ClickToAdvance(ED.GcepLnk);

            InstitutionsPage Instute = Gcep.ClickToAdvance(Gcep.InstitutionManagLnk);
            InstitutionsGCEPPage InsGcep = Instute.SearchforInstitutions("Ellis Hospital");

            ProgramsPage Program = InsGcep.ClickToAdvance(InsGcep.InstitutionProgramManagmentLnk);
            Program.UnassignCurriculum();

            CurriculumMngPage Curriculum = InsGcep.ClickToAdvance(InsGcep.InstitutionCurriculumTmpLnk);
            Curriculum.Search("Learning111!!!");
            Curriculum.DeleteCurriculum("Learning111!!!");

        }

        //[Test]
        [Description("Help Page elements changing depends on role verifying elements loging with different roles" +
         "Log in as a AMA staff then as a Admin Manager and a Resident")]
        [Property("Status", "Progress")]
        [Author("Azat Chariyev")]

        public void CourseTracker_Resident_RequiredCourseFail()
        {
            ///  1.Navigate to the login page login as a AMA staff
            UserInfo role = UserUtils.GetUser(UserRole.Ama_Staff);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            EducationCenterPage ED = LP.LoginAsUser("10021373", "password");

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
                string[] CourseTrackernotNull = CourseTracker.Split(' ');
                //string[] countofcoursewasAssigned = CourseyrackernotNull[2].Split('/');
                //string[] courseword = CourseTracker.Split(' ');
                int regCoursCount = Convert.ToInt16(CourseTrackernotNull[2]);
                //Thread.Sleep(5000);
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
            CurCoursPage.AddOrRemoveCourses(CurCoursPage.AvailableCoursesTbl, CurCoursPage.AddSelectedBtn, 8, 9, 10, 11, 12, 13, 14, 15, 16);

        }



        [Test]
        [Description("")]
        [Property("Status", "Progress")]
        [Author("Azat Chariyev")]

        public void CourseTracker_Resident_ElectiveCoursePass()
        {
            ///  1.Navigate to the login page login as a AMA staff
            UserInfo role = UserUtils.GetUser(UserRole.Ama_Staff);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            EducationCenterPage ED = LP.LoginAsUser("10021378", "password");

            if (BrowserName == BrowserNames.Firefox)
            {
                Browser.WaitForElement(Bys.EducationCenterPage.GcepLnk, ElementCriteria.IsEnabled);
            }

            ///  2.click to GCEP link  navigate to Gcep page and waiting load icon disappear
            GCEPPage Gcep = ED.ClickToAdvance(ED.GcepLnk);           
            string CourseTracker = Gcep.ResidentCourseTrackerLbl.Text;
            string[] CourseyrackernotNull = CourseTracker.Split(' ');           
            int regCoursCount = Convert.ToInt16(CourseyrackernotNull[3]);
            Gcep.ScrolltoGetAllCourses();           
         
            string xPathVariable1 = string.Format("//div[@class='activity-info-name']/h4[.='{0}']/../../..//button", "Confidentiality");
            Thread.Sleep(0500);
            IWebElement button1 = browser.FindElement(By.XPath(xPathVariable1));
            ElemSet.ScrollToElement(browser, button1);

           // ElemSet.ScrollToElement(Browser, Browser.FindElement(By.LinkText("AMA TEST")));
            CourseTestPage Course = Gcep.ResidentStartCourseOrContinue(browser, "AUTOMATION_004");
            Gcep = Course.TestPass();
            Gcep.ScrolltoGetAllCourses();
          
           // Assert.IsTrue(Gcep.VerificationCourseCompletion(browser, "AUTOMATION_003", "View Certificate"), "View Certificet button not visible");

            string CourseTrackerAfterTestCompletion = Gcep.ResidentCourseTrackerLbl.Text;
            string[] courseword3 = CourseTrackerAfterTestCompletion.Split(' ');
            int CountOfCompletedRegCourseafterPassingTest = Convert.ToInt16(courseword3[3]);           
            Assert.True(regCoursCount + 1 == (CountOfCompletedRegCourseafterPassingTest));
          

        }

        //[Test]
        [Description("")]
        [Property("Status", "Progress")]
        [Author("Azat Chariyev")]

        public void CourseTracker_Resident_ElectiveCourseFail()
        {
            ///  1.Navigate to the login page login as a AMA staff
            UserInfo role = UserUtils.GetUser(UserRole.Ama_Staff);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            EducationCenterPage ED = LP.LoginAsUser("10021378", "password");

            if (BrowserName == BrowserNames.Firefox)
            {
                Browser.WaitForElement(Bys.EducationCenterPage.GcepLnk, ElementCriteria.IsEnabled);
            }

            ///  2.click to GCEP link  navigate to Gcep page and waiting load icon disappear
            GCEPPage Gcep = ED.ClickToAdvance(ED.GcepLnk);
            string CourseTracker = Gcep.ResidentCourseTrackerLbl.Text;
            string[] CourseyrackernotNull = CourseTracker.Split(' ');
            int regCoursCount = Convert.ToInt16(CourseyrackernotNull[2]);
            int countofRowNoScroll;
            int countofRowFirstScroll;
            do
            {
                List<IWebElement> EnterRow = new List<IWebElement>(Browser.FindElements(By.XPath("//div[@class='activity-listing-bottom ng-scope']")));
                countofRowNoScroll = EnterRow.Count;
                //Thread.Sleep(1000);
                ElemSet.ScrollToElement(Browser, Gcep.FaceBookLnk);

                Thread.Sleep(1000);
                List<IWebElement> EnterRowFirstScroll1 = new List<IWebElement>(Browser.FindElements(By.XPath("//div[@class='activity-listing-bottom ng-scope']")));
                countofRowFirstScroll = EnterRowFirstScroll1.Count;
                Thread.Sleep(1000);
            }
            while (countofRowFirstScroll > countofRowNoScroll);

            //Assert.True(Gcep.VerificationOfChoosenCoursesAssignedForResident(browser, CourseNames), "Course count are not equal");

            string xPathVariable1 = string.Format("//div[@class='activity-info-name']/h4[.='{0}']/../../..//button", "content-2jpg");
            Thread.Sleep(0500);
            IWebElement button1 = browser.FindElement(By.XPath(xPathVariable1));
            ElemSet.ScrollToElement(browser, button1);

            
        }

        #endregion Tests
    }
    }







