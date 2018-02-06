using AMA.AppFramework;
using AMA.AppFramework.Utils.User;
using Browser.Core.Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AMA.UITest.Tests.AMA_Staff
{
    class AMA_PracticeForFireFox_Tests
    {
        //[Test]
        public void workingwithFF()
        {
            var service = FirefoxDriverService.CreateDefaultService(@"C:\SeleniumDrivers", "geckodriver.exe");           
            
            var driver = new FirefoxDriver(service);
            driver.Navigate().GoToUrl("http://ama.releasecandidate-community360.net/login.aspx");
            driver.Manage().Window.Maximize();
            Thread.Sleep(2000);

            IWebElement UserNameTxt = driver.FindElement(By.Id("ctl00_ctl00_ContentPlaceHolderBase1_ContentPlaceHolder1_cpLoginAspx_ctl00_LoginControl1_LTLogin_UserName"));
            IWebElement PasswordTxt = driver.FindElement(By.Id("ctl00_ctl00_ContentPlaceHolderBase1_ContentPlaceHolder1_cpLoginAspx_ctl00_LoginControl1_LTLogin_Password"));
            IWebElement LoginBtn = driver.FindElement(By.Id("ctl00_ctl00_ContentPlaceHolderBase1_ContentPlaceHolder1_cpLoginAspx_ctl00_LoginControl1_LTLogin_Login"));
           

            UserNameTxt.Clear();
            UserNameTxt.SendKeys("10031314");
            PasswordTxt.Clear();
            PasswordTxt.SendKeys("password");
            LoginBtn.Click();

            Thread.Sleep(2000);

            IWebElement GCEPLnk = driver.FindElement(By.Id("lnkGCE"));

            GCEPLnk.SendKeys(Keys.Tab);
            GCEPLnk.Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("User Management")));

            IWebElement UserMngLnk = driver.FindElement(By.LinkText("User Management"));

            UserMngLnk.Click();
            Thread.Sleep(8000);
            IWebElement AMALnk = driver.FindElement(By.XPath("//a[.='AMA']"));

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[.='AMA']")));

            AMALnk.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("User Management")));

            IWebElement SignOutLnk = driver.FindElement(By.LinkText("Sign Out"));
            Thread.Sleep(5000);
            SignOutLnk.SendKeys(Keys.Tab);
            SignOutLnk.Click();

            Thread.Sleep(2000);
            Assert.IsTrue(driver.Url.Equals("https://logintest.ama-assn.org/account/logout"));

            driver.Close();
            
           
           

        }
        // [Test]
        //public void AzatsPractice()
        //{
        //    ///1.Navigate to the login page login
        //   //UserInfo role = UserUtils.GetUser(UserRole.Ama_Staff);
        //    LoginPage LP = Navigation.GoToLoginPage(Browser);
        //    EducationCenterPage ED = LP.LoginAsUser("10016185", "password");

        //    //if (BrowserName == BrowserNames.Firefox)
        //    //{
        //    //    Browser.WaitForElement(Bys.EducationCenterPage.GcepLnk, ElementCriteria.IsEnabled);
        //    //}

        //    ///  2.click to GCEP link  navigate to Gcep page and waiting load icon disappear
        //    GCEPPage Gcep = ED.ClickToAdvance(ED.GcepLnk);

        //    //DataTable institutions = ElemGet.SelElem_ListTextToDataTable(Gcep.InstitutionSelElem);
        //    List<string> institutions = ElemGet.SelElem_ListTextToListString(Gcep.InstitutionSelElem);

        //    Gcep.ClickToAdvance(Gcep.SignOutLnk);
        //    //LP = Navigation.GoToLoginPage(browser);
        //    //ED = LP.LoginAsUser(role.Username, role.Password);
        //    Gcep = ED.ClickToAdvance(ED.GcepLnk);
        //    InstitutionsPage IP = Gcep.ClickToAdvance(Gcep.InstitutionManagLnk);

        //    // Starting the for loop on the 2nd row (index = 1) on the for loop here because when we extracted the values from the dropdown
        //    //  above, the first value was not an institution, it was a static "Select your item" text
        //    // New
        //    EditInstitutionPage EIP = new EditInstitutionPage(Browser);
        //    List<string> institutionContactEmailAdress = new List<string>();

        //    //for (var i = 1; i < firstExampleDropdownList.Rows.Count; i++)
        //    //{
        //    //    IP.Search(firstExampleDropdownList.Rows[i][0].ToString());
        //    //    IP.ActionGearBtn.Click();
        //    //    Thread.Sleep(0500);
        //    //    EIP = IP.ClickToAdvance(IP.EditInstitutionLnk);
        //    //    institutionContactEmailAdress.Add(EIP.InstitutionPrimaryContactEmailTxt.GetAttribute("value"));
        //    //    IP = EIP.ClickToAdvance(EIP.InstitutionCancelBtn);
        //    //}

        //    for (var i = 1; i < institutions.Count; i++)
        //    {
        //        string institution = institutions[i];
        //        IP.Search(institution);
        //        IP.ActionGearBtn.Click();
        //        Thread.Sleep(0500);
        //        EIP = IP.ClickToAdvance(IP.EditInstitutionLnk);
        //        institutionContactEmailAdress.Add(EIP.InstitutionPrimaryContactEmailTxt.GetAttribute("value"));
        //        IP = EIP.ClickToAdvance(EIP.InstitutionCancelBtn);
        //    }

        //    IP.SignOutLnk.Click();
        //    LP = Navigation.GoToLoginPage(browser);
        //    ED = LP.LoginAsUser("10016185", "password");
        //    Gcep = ED.ClickToAdvance(ED.GcepLnk);


        //    for (var i = 1; i < institutions.Count; i++)
        //    {
        //        string institution = institutions[i].ToString();
        //        Gcep.InstitutionSelElem.SelectByText(institutions[i].ToString());
        //        Gcep.WaitForInitialize();
        //        HelpPage HP2 = Gcep.ClickToAdvance(Gcep.HelpLnk);
        //        Assert.AreEqual(institutionContactEmailAdress[i - 1], HP2.ContactInvolvedInstitutionEmailLnk.Text);
        //        Gcep = HP2.ClickToAdvance(HP2.AdministrationLnk);

        //    }
        // }

        // <summary>
        /// Workflow  Test making sure Resident taking a  course and passing since its assigned test it will show in GCEP Transcript page </summary>
        /// Author: Azat Chariyev
        /// Status: Can not run more than 1 time
        /// </summary>
        /// TODO: Follow the rules for POM
        // [TestCase("AUTOMATION_001")]
        // public void ResidentPassTest(string TestName)
        // {
        //     // Navigate to the login page with valid credentials below          
        //     UserInfo role = UserUtils.GetUser(UserRole.Resident);
        //     LoginPage LP = Navigation.GoToLoginPage(browser);
        //     EducationCenterPage ED = LP.LoginAsUser("10031060", "password");//60-1-3

        //     //click to CGEP link and waiting load icon disappear and landing on curriculum management page
        //     GCEPPage GCEP = ED.ClickToAdvance(ED.GcepLnk);
        //     GCEPTranscriptPage Transcript = GCEP.ClickToAdvance(GCEP.TranscriptLnk);
        //     GCEPLibraryPage Library = GCEP.ClickToAdvance(GCEP.LibraryLnk);
        //     Library.Search(TestName);
        //     CourseTestPage Course = Library.ClickToAdvance(Library.BeginCourseBtn);

        //     Library = Course.TestPass();

        //     Library.ClickToAdvance(Library.TranscriptLnk);
        //     Transcript.CompletionDateSelElem.SelectByValue("newtoold");
        //     Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);
        //     Assert.IsTrue(Transcript.Grid_CellTextFound(Transcript.CompletedTestTbl, TestName));

        //     //int FinalCountsofTests=Transcript.GridRowCount(Transcript.CompletedTestTbl);
        //     //Assert.IsFalse(StartCountsofTests == FinalCountsofTests);
        // }



        // /// <summary>
        // /// Workflow Workflow  Test making sure Resident taking a test and failing since its assigned test it will not show in GCEP Transcript page</summary>
        // /// Author: Azat Chariyev
        // /// Status: Working on it
        // /// </summary>
        // /// TODO: Follow the rules for POM
        // //[TestCase("AUTOMATION_007", UserRole.Manager)]
        // //[TestCase("AUTOMATION_007", UserRole.Resident)]
        // //[Test]
        // public void ResidentFailTest()//(string TestName, UserRole userRole)

        // {
        //     ///  1. Login as a Resident   
        //     UserInfo role = UserUtils.GetUser(UserRole.Resident); //UserInfo role = UserUtils.GetUser( userRole);
        //     LoginPage LP = Navigation.GoToLoginPage(browser);
        //     EducationCenterPage ED = new EducationCenterPage(browser);
        //     if (BrowserName == BrowserNames.InternetExplorer)
        //     {
        //         ED = LP.LoginAsUser("10031052", role.Password);
        //     }
        //     else { ED = LP.LoginAsUser("10031051", role.Password); }//60-8,5-7

        //     ///  2. Navigate to GCEP page from Left corner drop down options 
        //     GCEPPage GCEP = ED.ClickToAdvance(ED.GcepLnk);

        //     ///  3.Click on Transcript link
        //     GCEPTranscriptPage Transcript = GCEP.ClickToAdvance(GCEP.TranscriptLnk);

        //     ///  4.From Transcript page getting count of completed courses
        //     int StartCountsofTests = ElemGet.Grid_GetRowCount(Transcript.CompletedTestTbl);

        //     ///  5.Click to Library Link 
        //     GCEPLibraryPage Library = GCEP.ClickToAdvance(GCEP.LibraryLnk);

        //     ///  6.Search for Assigned course with name provided on test case"AUTOMATION_002"
        //     Library.Search("AUTOMATION_002");

        //     ///  7.Finding course and begin the course and changing existing page to CourseTestPage
        //     CourseTestPage Course = Library.ClickToAdvance(Library.BeginCourseBtn);

        //     ///  8.Fail the course using TestFail method go back to Transcript page  getting count of completed courses
        //     Course.TestFail().TranscriptLnk.Click();
        //     Browser.WaitForElement(Bys.GCEPTranscriptPage.CompletedTestTbl, TimeSpan.FromSeconds(60), ElementCriteria.IsVisible);
        //     int FinalCountsofTests = ElemGet.Grid_GetRowCount(Transcript.CompletedTestTbl);

        //     ///  9.Compare count of courses before failing test and after.
        //     Assert.IsTrue(StartCountsofTests == FinalCountsofTests);

        // }


        // /// <summary>
        // ///Workflow  Test making sure Resident taking a test and passing  its not assigned test it will show in GCEP Transcript page</summary>
        // /// Author: Azat Chariyev
        // /// Status: Complete
        // /// </summary>
        // /// TODO: Follow the rules for POM
        // //[TestCase("AUTOMATION_008",UserRole.Admin)]
        // //[TestCase("AUTOMATION_008", UserRole.Manager)]
        // //[TestCase("AUTOMATION_008", UserRole.Resident)]
        // // [TestCase("AUTOMATION_002")]
        // public void ResidentNotRequiredTest(string TestName)  //string TestName,UserRole userRole)
        // {
        //     // Navigate to the login page with valid credentials below          
        //     UserInfo role = UserUtils.GetUser(UserRole.Resident); //UserInfo role = UserUtils.GetUser(userRole);
        //     LoginPage LP = Navigation.GoToLoginPage(browser);
        //     EducationCenterPage ED = LP.LoginAsUser(role.Username, role.Password);

        //     //click to CGEP link and waiting load icon disappear and landing on curriculum management page
        //     GCEPPage GCEP = ED.ClickToAdvance(ED.GcepLnk);
        //     GCEPTranscriptPage Transcript = GCEP.ClickToAdvance(GCEP.TranscriptLnk);
        //     int StartCountsofTests = ElemGet.Grid_GetRowCount(Transcript.CompletedTestTbl);
        //     GCEPLibraryPage Library = GCEP.ClickToAdvance(GCEP.LibraryLnk);
        //     Library.SearchTxt.Clear();
        //     Library.SearchTxt.SendKeys(TestName);
        //     Library.SearchTxt.Click();
        //     CourseTestPage Course = Library.ClickToAdvance(Library.BeginCourseBtn);
        //     Course.TestPassNotReqiured().TranscriptLnk.Click();
        //     // Browser.WaitForElement(Bys.GCEPTranscriptPage.CompletedTestTbl, TimeSpan.FromSeconds(60), ElementCriteria.IsVisible);
        //     Transcript.CompletionDateSelElem.SelectByValue("newtoold");
        //     Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);
        //     Assert.IsTrue(Transcript.Grid_CellTextFound(Transcript.CompletedTestTbl, TestName));
        //     Thread.Sleep(1000);
        //     int FinalCountsofTests = ElemGet.Grid_GetRowCount(Transcript.CompletedTestTbl);
        //     Thread.Sleep(1000);
        //     Assert.IsTrue(StartCountsofTests + 1 == FinalCountsofTests);


        // }


        // /// <summary>
        // ///Workflow  Test making sure Learner taking a test and failing and going to transcript page verifiying its not there then navigating GCEP Transript it will not show in GCEP Transcript page</summary>
        // /// Author: Azat Chariyev
        // /// Status: Complete
        // /// </summary>
        //// [TestCase("AUTOMATION_EC_002")]
        // public void LearnerFailTest(string TestName)
        // {
        //     // Navigate to the login page with valid credentials below          
        //     UserInfo role = UserUtils.GetUser(UserRole.Resident);
        //     LoginPage LP = Navigation.GoToLoginPage(browser);
        //     EducationCenterPage ED = LP.LoginAsUser(role.Username, role.Password);
        //     EducationCenterTransciptPage Transcript = ED.ClickToAdvance(ED.TranscriptLnk);
        //     int startNumber = ElemGet.Grid_GetRowCount(Transcript.TranscriptcontrolTbl);
        //     Transcript.MyCoursesLnk.Click();
        //     Thread.Sleep(2000);
        //     CourseTestPage Course = ED.FindCourse(ED.CourseTbl, TestName);
        //     Course.LearnerTestFail().TranscriptLnk.Click();
        //     int finalNumber = ElemGet.Grid_GetRowCount(Transcript.TranscriptcontrolTbl);
        //     Assert.IsTrue(startNumber == finalNumber);
        //     GCEPPage GCEP = ED.ClickToAdvance(ED.GcepLnk);
        //     GCEPTranscriptPage TranscriptGcep = GCEP.ClickToAdvance(GCEP.TranscriptLnk);
        //     Assert.IsFalse(TranscriptGcep.Grid_CellTextFound(TranscriptGcep.CompletedTestTbl, TestName));
        // }


        // /// <summary>
        // ///Workflow  Test making sure Learner taking a test and passing and going to transcript page verifiying its  there then navigating GCEP Transript it will not show in GCEP Transcript page</summary>
        // /// Author: Azat Chariyev
        // /// Status: Complete
        // /// </summary>
        // //[TestCase("AUTOMATION_EC_002")]
        // public void LearnerPassTest(string TestName)
        // {
        //     // Navigate to the login page with valid credentials below          
        //     UserInfo role = UserUtils.GetUser(UserRole.Resident);
        //     LoginPage LP = Navigation.GoToLoginPage(browser);
        //     EducationCenterPage ED = LP.LoginAsUser(role.Username, role.Password);
        //     EducationCenterTransciptPage Transcript = ED.ClickToAdvance(ED.TranscriptLnk);
        //     int startNumber = ElemGet.Grid_GetRowCount(Transcript.TranscriptcontrolTbl);
        //     Transcript.MyCoursesLnk.Click();
        //     Thread.Sleep(2000);
        //     CourseTestPage Course = ED.FindCourse(ED.CourseTbl, TestName);
        //     Course.LearnerTestPass().TranscriptLnk.Click();
        //     Thread.Sleep(5000);
        //     int finalNumber = ElemGet.Grid_GetRowCount(Transcript.TranscriptcontrolTbl);
        //     Assert.IsFalse(startNumber == finalNumber);
        //     GCEPPage GCEP = ED.ClickToAdvance(ED.GcepLnk);
        //     GCEPTranscriptPage TranscriptGcep = GCEP.ClickToAdvance(GCEP.TranscriptLnk);
        //     Assert.IsFalse(TranscriptGcep.Grid_CellTextFound(TranscriptGcep.CompletedTestTbl, TestName));

    
     }
}
