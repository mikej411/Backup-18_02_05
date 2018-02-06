using Browser.Core.Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using AMA.AppFramework;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using System;
using OpenQA.Selenium.Firefox;
using AMA.AppFramework.Utils.User;
using static AMA.AppFramework.Utils.User.UserUtils;
using System.Linq;

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
    public class AMA_Curriculum_Tests : TestBase
    {
        #region Constructors
        public AMA_Curriculum_Tests(string browserName) : base(browserName) { }

        // Remote Selenium Grid Test
        public AMA_Curriculum_Tests(string browserName, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, version, platform, hubUri, extrasUri)
        { }
        #endregion

        /// <summary>
        /// These properties represent the curriculum names that we create in certain test methods below
        /// </summary>
        //public string newCurrName { get; set; }
        //public string editedCurrName { get; set; }
        //public bool newCurriculumCreated { get; set; }
        //public bool editedCurriculumCreated { get; set; }
        //public override void Setup()
        //{
        // newCurrName = DataUtils.GetRandomString(16);
        //    editedCurrName = DataUtils.GetRandomString(16);
        //}

        //public override void TestTearDown()
        //{
        //    //GCEPPage GP = new GCEPPage(browser);

        //    //if (newCurriculumCreated)
        //    //{
        //    //    Navigation.GoToEducationCenterPage(Browser);
        //    //    CurriculumMngPage CP = GP.ClickToAdvance(GP.CurriculumTemplatesLnk);
        //    //    CP.Search(newCurrName);
        //    //    CP.DeleteCurriculum(newCurrName);
        //    //    newCurriculumCreated = false;
        //    //}
        //    //if (editedCurriculumCreated)
        //    //{
        //    //    CurriculumMngPage CP1 = new CurriculumMngPage(browser);
        //    //    CP1.Search(editedCurrName);
        //    //    CP1.DeleteCurriculum(editedCurrName);
        //    //    editedCurriculumCreated = false;
        //    //}

        //    base.TestTearDown();
        //}

        #region Tests

        [Test]
        //[TestCase("L2!","L3!")]
        [Description("Workflow test navigating Gcep page chosing curriculum templates choose create curriculum, starting creating curriculum assigning them" +
        "PGY1,PGY2,PGY3 saving curriculum going back to curricculum template asserting curriculum creation editing and deleting cirruculum" +
        "veryfiying cirruculum deleted")]
        [Property("Status", "Complete")]
        [Author("Azat Chariyev")]


        public void EditCurriculumTemplate()
        {
            string curriculumName = "";
            string editedCurriculumName = "";

            if (BrowserName == BrowserNames.Chrome)
            {
                curriculumName = "ChromeCurrName";
                editedCurriculumName = "ChromeCurrNameEdited";
            }
            if (BrowserName == BrowserNames.Firefox)
            {
                curriculumName = "FFCurrName";
                editedCurriculumName = "FFCurrNameEdited";
            }
            if (BrowserName == BrowserNames.InternetExplorer)
            {
                curriculumName = "IECurrName";
                editedCurriculumName = "IECurrNameEdited";
            }

            // string curriculum,string newcurriculum
            //newCurrName = DataUtils.GetRandomString(16);
            //editedCurrName = DataUtils.GetRandomString(16);

            ///  1. Navigate to the login page and login as  AMA-staff("10031315","password")
            UserInfo role = UserUtils.GetUser(UserRole.Ama_Staff);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            EducationCenterPage ED = LP.LoginAsUser(role.Username, role.Password);
            if (BrowserName == BrowserNames.Firefox)
            {
                Browser.WaitForElement(Bys.EducationCenterPage.GcepLnk, ElementCriteria.IsEnabled);
            }

            ///  2.click to CGEP link and waiting load icon disappear and landing on curriculum management page
            GCEPPage GCEP = ED.ClickToAdvance(ED.GcepLnk);

            ///  3.on curriculum management page click curriculum templates link and landing  curriculum course page
            CurriculumMngPage CurTempPage = GCEP.ClickToAdvance(GCEP.CurriculumTemplatesLnk);

            ///  4. on curriculum courses page  clicking create and choosing courses
            CurTempPage.Search(curriculumName);
            CurTempPage.DeleteCurriculum(curriculumName);
            CurriculumCoursePage CurCoursPage = CurTempPage.ClickToAdvance(CurTempPage.CreateCurriculumTemplateBtn);
            CurCoursPage.AddOrRemoveCourses(CurCoursPage.AvailableCoursesTbl, CurCoursPage.AddSelectedBtn, 1, 7, 12);

            ///  5.giving name for curriculum from Test cases parametr ("L2!")
           // string randomCurrName = DataUtils.GetRandomString(16);
            CurCoursPage.CurriculumNameTxt.Clear();
            CurCoursPage.CurriculumNameTxt.SendKeys(curriculumName);

            ///  6. saving curriculum navigating pgy page 
            PGYAssignmentPage PGYpage = CurCoursPage.ClickToAdvance(CurCoursPage.NextBtn);


            ///  7.choosing pgy's and saving
          //  Assert.True(false);
            PGYpage.Grid_ClickElementWithoutTextInsideRow(PGYpage.CourseTbl, 1, 9);                                         // PGYpage.ClickOnCellsOfRow(1, 9);
            PGYpage.Grid_ClickElementWithoutTextInsideRow(PGYpage.CourseTbl, 2, 4);                                         //PGYpage.ClickOnCellsOfRow(2, 6, 7, 10);
            PGYpage.Grid_ClickElementWithoutTextInsideRow(PGYpage.CourseTbl, 3, 6);                                         //PGYpage.ClickOnCellsOfRow(3, 6, 7);

            ///  8.saving curruculum navigating curriculum MNG page
            CurTempPage = PGYpage.ClickToAdvance(PGYpage.SaveExitBtn);

            ///  9.curriculum management page searching for curriculum what we created and saving name and  row count choosing curriculum what we create and editing name and pgy years
            CurTempPage.Search(curriculumName);
            CurTempPage.CurriculumName.Click();
            var ActualCurriculumName = CurTempPage.CurriculumName.Text;
            int ActualCurriculumCount = CurTempPage.GetCountofRow(CurTempPage.SpecificCurriculumTbl);
            CurTempPage.CurriculumWinClose.Click();
            CurTempPage.EditCurriculum(curriculumName);

            ///  10. Adding new course        
            CurCoursPage.AddOrRemoveCourses(CurCoursPage.AvailableCoursesTbl, CurCoursPage.AddSelectedBtn, 10);

            ///  11. Renaming curriculum from ("L2!"), to ("L3!"),curriculum name passing from test case attribute 
            CurCoursPage.CurriculumNameTxt.Clear();
            CurCoursPage.CurriculumNameTxt.SendKeys(editedCurriculumName);
            PGYpage = CurCoursPage.ClickToAdvance(CurCoursPage.NextBtn);

            ///  12. Choosing student years for new course what was added
            PGYpage.Grid_ClickElementWithoutTextInsideRow(PGYpage.CourseTbl, 3, 8);

            ///  13.Saving curriculum and navigating to curriculum management page again
            CurTempPage = PGYpage.ClickToAdvance(PGYpage.SaveExitBtn);

            ///  14. searching new created curriculum and saving info about it
            CurTempPage.Search(editedCurriculumName);
            CurTempPage.CurriculumName.Click();
            var FinalCurriculumName = CurTempPage.CurriculumName.Text;
            int FinalCurriculumCount = CurTempPage.GetCountofRow(CurTempPage.SpecificCurriculumTbl);
            CurTempPage.CurriculumWinClose.Click();

            ///  15.comparing first curriculum and second curriculum after editing
            Assert.IsFalse(FinalCurriculumCount == ActualCurriculumCount && FinalCurriculumName == ActualCurriculumName);

            ///  16.deleting last curriculum what we created and edited           
            CurTempPage.DeleteCurriculum(editedCurriculumName);

        }



        [Test]                                 //[TestCase("new1111")]
        [Description("Workflow test navigating Gcep page chosing curriculum templates choose clicking action button then assign link" +
         " then choosing today date as start date then choosing 20 day later as a end date for curriculum and confirming date are changed and name ")]
        [Property("Status","Complete")]
        [Author("Azat Chariyev")]
        public void AssingCurriculum()    //public void AssingCurriculum(string curriculumName)
        {
            string curriculumName = "new1111";
            ///  1. Navigate to the login page and login as Manager ("10031315","password")
            UserInfo role = UserUtils.GetUser(UserRole.Manager);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            EducationCenterPage ED = LP.LoginAsUser(role.Username, role.Password);

            ///  2. click to CGEP link and waiting load icon disappear           
            GCEPPage GCEP = ED.ClickToAdvance(ED.GcepLnk);

            ///  3. From GCEP page click to curriculum template Link and Navigate curriculum management page and searching for curriculum created by AMA
            CurriculumMngPage CurTempPage = GCEP.ClickToAdvance(GCEP.CurriculumTemplatesLnk);
            CurTempPage.Search(curriculumName);

            ///  4.Clicking to Assign To Programm and redirecting Assign Program page
            CurTempPage.Actioncell.Click();

            ///  5. Choosing starting date and ending date for  program  and clicking next button
            AssignProgramPage Assign = CurTempPage.ClickToAdvance(CurTempPage.AssignToProgrammLnk);
            string StartingDate = Assign.ChoosingStartDate();
            string EndingDate = Assign.ChoosingEndDate(1,"M/d/yyyy");          
            AssignSummaryPage Summary = Assign.ClickToAdvance(Assign.NextBtn);

            ///  6. Verifying from Assign Summary page program is displayed
            Assert.IsTrue(Summary.CreatedProgramName.Displayed);
            Assert.AreEqual((Summary.CreatedProgramName.Text), curriculumName);
            Thread.Sleep(1000);

            ///  7.Verifying Assing confirmation test page curriculum name and starting date and ending dates are there which we choose.
            AssignConfirmationPage Confirmation = Summary.ClickToAdvance(Summary.NextBtn);
            Assert.IsTrue(Confirmation.Grid_CellTextFound(Confirmation.ProgramSummaryTbl, StartingDate + " - " + EndingDate));
            //Confirmation.ConfirmBtn.Click();

        }

        [Test]
        //[TestCase("L1!")]
        [Description( "Workflow test navigating Gcep page chosing curriculum templates choose create curriculum "+
         "starting creating curriculum assigning them PGY1,PGY2,PGY3 saving curriculum going back to curricculum template "+
         "verifying curriculum creation  deleting cirruculum veryfiying cirruculum is deleted")]
        [Property("Status", "Complete")]
        [Author("Azat Chariyev")]
        public void CreateCurriculumTemplate()
            
        {
            string curriculumName = "";
            if (BrowserName == BrowserNames.Chrome)
            {
                curriculumName = "Chrome1CurrName";
            }
            if (BrowserName == BrowserNames.Firefox)
            {
                curriculumName = "FireFox1CurrName";
            }
            if (BrowserName == BrowserNames.InternetExplorer)
            {
                curriculumName = "IE1CurrName";
            }
            ///  1. Navigate to the login page login as a Manager or AMA staff
            UserInfo role = UserUtils.GetUser(UserRole.Ama_Staff);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            EducationCenterPage ED = LP.LoginAsUser(role.Username, role.Password);

            ///  2.click to CGEP link to navigate Gcep page and waiting load icon disappear 
            GCEPPage GCEP = ED.ClickToAdvance(ED.GcepLnk);

            ///  3. From GCEP click to curriculum template Link and Navigate curriculum management page and choose create curriculum template button and redirect to curriculum course page.
            CurriculumMngPage CurTemp = GCEP.ClickToAdvance(GCEP.CurriculumTemplatesLnk);        
            CurriculumCoursePage CurCoursPage = CurTemp.ClickToAdvance(CurTemp.CreateCurriculumTemplateBtn);

            ///  4.Form course page choosing available courses from table by index
            CurCoursPage.AddOrRemoveCourses(CurCoursPage.AvailableCoursesTbl,CurCoursPage.AddSelectedBtn, 1, 7, 12);
           
            ///  5. Giving the name for curriculum passing parameter from TestCase as a string
            CurCoursPage.CurriculumNameTxt.Clear();
            CurCoursPage.CurriculumNameTxt.SendKeys(curriculumName);

            ///  6.Saving curriculum and navigating to the pgy pages to assigne course to students
            PGYAssignmentPage PGY = CurCoursPage.ClickToAdvance(CurCoursPage.NextBtn);

            ///  7.Choosing student years to assign course by index for each course
            PGY.Grid_ClickElementWithoutTextInsideRow(PGY.CourseTbl, 1, 8);                                   
            PGY.Grid_ClickElementWithoutTextInsideRow(PGY.CourseTbl, 2, 7);
            PGY.Grid_ClickElementWithoutTextInsideRow(PGY.CourseTbl, 3, 6);

            ///  8. Saving curriculum and navigating to curriculum management page again
            CurTemp = PGY.ClickToAdvance(PGY.SaveExitBtn);

            ///  9.Searching for curriculum what we created verifying its there and deleting.
            CurTemp.Search(curriculumName);
            CurTemp.DeleteCurriculum(curriculumName);          
            Assert.IsTrue(CurTemp.NoRecordLabel.Displayed);
            // Thread.Sleep(5000);
            //PGY.ClickOnCellsOfRow(1, 9);
            //PGY.ClickOnCellsOfRow(2, 6, 7, 10);
            //PGY.ClickOnCellsOfRow(3, 6, 7);   
        }
        
   
        [TestCase("Pan 456", "testing11111")]
        [Description("Workflow test navigating Gcep page chosing curriculum templates  searching for curriculim created by AMA clicking action button " +
         " verifying action button is eneadled repeating same action for second curriculum created by AMA")]
        [Property("Status", "Complete")]
        [Author("Azat Chariyev")]
        public void AssignmentAviability(string curriculumName, string curriculumName2)
        {
            ///  1.Navigate to the login page login as a Manager
            UserInfo role = UserUtils.GetUser(UserRole.Manager);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            EducationCenterPage ED = LP.LoginAsUser(role.Username, role.Password);

            ///  2.click to CGEP link  navigate to Gcep page and waiting load icon disappear
            GCEPPage GCEP = ED.ClickToAdvance(ED.GcepLnk);
            Thread.Sleep(9000); //until performance issue will be fix
            ///  3. From GCEP click to curriculum template Link and Navigate curriculum management page and searching for curriculum created by AMA
            CurriculumMngPage CurTempPage = GCEP.ClickToAdvance(GCEP.CurriculumTemplatesLnk);
            CurTempPage.Search(curriculumName);

            ///  4.Verifying Assign program is Enabled and Editing option is not there
            CurTempPage.Actioncell.Click();
            var Actualvalue = CurTempPage.AssignToProgrammLnk.Text;
            Assert.IsTrue(CurTempPage.AssignToProgrammLnk.Enabled);
            Assert.AreNotEqual("Edit", Actualvalue);

            ///  5. Searching for second curriculum created by AMA
            CurTempPage.Search(curriculumName2);

            ///  6.Verifying Assign program is Enabled and Editing option is not there 
            CurTempPage.Actioncell.Click();
            var ActualvalueSecond = CurTempPage.AssignToProgrammLnk.Text;
            Assert.IsTrue(CurTempPage.AssignToProgrammLnk.Enabled);
            Assert.AreNotEqual("Edit", ActualvalueSecond);

            // Assert.IsTrue(CurTempPage.AssignToProgrammLnk.Enabled);
            //  Assert.False(CurTempPage.Editcell.Displayed);
            //  Assert.False(CurTempPage.Deletecell.Displayed);

        }

      

        [TestCase("Albany Medical College")]
        [Description("On Program Managment Page date filter verification.Acceptance criteria :" +
       " Default date on datepicker today date minus 6 month, Assigned Curricula with an End Date AFTER the selected Date will display in the Assigned Curricula column' ")]
        [Property("Status", "Complete")]
        [Author("Azat Chariyev")]
        public void FilterEndedCurricula(string InstitutionName)
        {
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

            ///  5.Verifying date on calender set in default today date minus 6 month and seting calendar for today date
            Program.VeryfiyingDates();
            // Assert.IsFalse(Program.GetValueOfRow(Program.ProgramMngTbl, actualDateOnCalender));

            ///  6.Verifying from program management table ending date bigger then today date.
            Assert.IsTrue(Program.DateIsBiggerOrEqualsThenCurrentDate());

            //List<string> rowDates = new List<string>(Browser.FindElements(By.XPath("//*[@id='gridProgramManagement']//div[@class='ng-isolate-scope']/div[7]//div")).S‌​elect(iw => iw.Text));

            //foreach (string date in rowDates)
            //{
            //    DateTime currentDate = DateTime.Now;
            //    DateTime rowDate = Convert.ToDateTime(date);
            //    Assert.True(AssertUtils.DateGreaterThan(currentDate, rowDate));
            //}
        }

        //[Test]        
        [Description("")]
        [Property("Status", "In Progress")]
        [Author("Azat Chariyev")]
        public void MemberBenefits()

        {           
            ///  1. Navigate to the login page login as a Manager or AMA staff
            UserInfo role = UserUtils.GetUser(UserRole.Ama_Staff);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            EducationCenterPage ED = LP.LoginAsUser(role.Username, role.Password);

            ///  2.click to CGEP link to navigate Gcep page and waiting load icon disappear 
            GCEPPage GCEP = ED.ClickToAdvance(ED.GcepLnk);
            MemberBenefitPage MPG = GCEP.ClickToAdvance(GCEP.MemberBenefitsManagementLnk);

            ElemSet.ScrollToElement(browser, MPG.FaceBookLnk);

            List<string> benefitTitlesAMAlevel_Published = new List<string>(Browser.FindElements(By.XPath("//div[@class='col-lg-3 col-md-3 col-sm-3 col-xs-6 ng-scope']/span")).Select(ow => ow.Text));

            GCEP = MPG.ClickToAdvance(MPG.GMECompetencyEducationProgramLnk);
            GCEP.AdminSwitchBtn.Click();
            GCEP.WaitForInitialize();
            GCEP.ScrolltoGetAllCourses();
            ElemSet.ScrollToElement(Browser, GCEP.FaceBookLnk);

            List<string> benefitTitlesResidentlevel = new List<string>(Browser.FindElements(By.XPath("//div[@class='col-lg-3 col-md-3 col-sm-3 col-xs-6 ng-scope']/span")).Select(ow => ow.Text));

            Assert.AreEqual(benefitTitlesAMAlevel_Published, benefitTitlesResidentlevel);

            

            GCEP.GMECompetencyEducationProgramLnk.Click();
            GCEP.WaitForInitialize();
            if (BrowserName == BrowserNames.InternetExplorer)
            {
                Browser.WaitForElement(Bys.GCEPPage.MemberBenefitsManagementLnk,TimeSpan.FromSeconds(90), ElementCriteria.IsEnabled);
            }

            MPG = GCEP.ClickToAdvance(GCEP.MemberBenefitsManagementLnk);
            List<string> benefitTitlesAMAlevel_Not_Published = new List<string>(Browser.FindElements(By.XPath("//div[@class='col-lg-3 col-md-3 col-sm-3 col-xs-6 cross-sell-list-item ng-scope']/div[@class='margin-top-bottom-20 margin-bottom-15 ng-binding']")).Select(ow => ow.Text));

            if(benefitTitlesAMAlevel_Published.Count == benefitTitlesAMAlevel_Not_Published.Count)
            {
                MPG.TitleTxt.SendKeys("newBenefit");
                MPG.URLTxt.SendKeys("www.google.com");
                FileUtils.UploadFileUsingSendKeys(Browser, MPG.MembershipFormBrowseHiddenBtn, Bys.MemberBenefitPage.MembershipFormBrowseHiddenBtn, @"C:\upload\physician_save_0.jpg");
                MPG.SaveBtn.Click();
                MPG.WaitForInitialize();
            }
            ElemSet.ScrollToElement(Browser, MPG.PublishBtn);
            Thread.Sleep(500);
            MPG.PublishBtn.Click();
            MPG.WaitForInitialize();
            List<string> benefitTitlesAMAlevel_Published_firstTime = new List<string>(Browser.FindElements(By.XPath("//div[@class='col-lg-3 col-md-3 col-sm-3 col-xs-6 ng-scope']/span")).Select(ow => ow.Text));

            GCEP = MPG.ClickToAdvance(MPG.GMECompetencyEducationProgramLnk);
            GCEP.ClickToAdvance(GCEP.SignOutLnk);

            LP = Navigation.GoToLoginPage(browser);
            ED = LP.LoginAsUser("10031198","password");
            GCEP = ED.ClickToAdvance(ED.GcepLnk);

            GCEP.ScrolltoGetAllCourses();
            ElemSet.ScrollToElement(Browser, GCEP.FaceBookLnk);

            List<string> benefitTitlesResidentlevel_firstTime = new List<string>(Browser.FindElements(By.XPath("//div[@class='col-lg-3 col-md-3 col-sm-3 col-xs-6 ng-scope']/span")).Select(ow => ow.Text));

            Assert.AreEqual(benefitTitlesAMAlevel_Published_firstTime,benefitTitlesResidentlevel_firstTime);
            // FileUtils.UploadFileUsingSendKeys(Browser, MPG.MembershipFormBrowseHiddenBtn, Bys.MemberBenefitPage.MembershipFormBrowseHiddenBtn, @"C:\upload\physician_save_0.jpg");
            // MPG.ChooseFileBtn.Click();

            GCEP.ClickToAdvance(GCEP.SignOutLnk);

            LP = Navigation.GoToLoginPage(browser);
            ED = LP.LoginAsUser(role.Username, role.Password);
            GCEP = ED.ClickToAdvance(ED.GcepLnk);
            MPG = GCEP.ClickToAdvance(GCEP.MemberBenefitsManagementLnk);

            IWebElement benefitToDelete = Browser.FindElement(By.XPath("//div[@class = 'margin-top-bottom-20 margin-bottom-15 ng-binding' and contains (text(),'newBenefit')]/..//span[@class='glyphicon glyphicon-remove cursor-pointer']"));
            benefitToDelete.Click();
            Thread.Sleep(500);
            MPG.AcceptBtn.Click();
            MPG.WaitForInitialize();

            MPG.PublishBtn.Click();
            MPG.WaitForInitialize();
         

            List<string> benefitTitlesAMAlevel_Published_secondTime = new List<string>(Browser.FindElements(By.XPath("//div[@class='col-lg-3 col-md-3 col-sm-3 col-xs-6 ng-scope']/span")).Select(ow => ow.Text));
            GCEP = MPG.ClickToAdvance(MPG.GMECompetencyEducationProgramLnk);
            GCEP.ClickToAdvance(GCEP.SignOutLnk);

            LP = Navigation.GoToLoginPage(browser);
            ED = LP.LoginAsUser("10031198", "password");
            GCEP = ED.ClickToAdvance(ED.GcepLnk);

            GCEP.ScrolltoGetAllCourses();
            ElemSet.ScrollToElement(Browser, GCEP.FaceBookLnk);

            List<string> benefitTitlesResidentlevel_secondTime = new List<string>(Browser.FindElements(By.XPath("//div[@class='col-lg-3 col-md-3 col-sm-3 col-xs-6 ng-scope']/span")).Select(ow => ow.Text));

            Assert.AreEqual(benefitTitlesAMAlevel_Published_secondTime, benefitTitlesResidentlevel_secondTime);


            Thread.Sleep(15000);
         }


        #endregion Tests
    }
    }

