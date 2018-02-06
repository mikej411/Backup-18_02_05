using Browser.Core.Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using AMA.AppFramework;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using AMA.AppFramework.Utils.User;
using static AMA.AppFramework.Utils.User.UserUtils;
using System;

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
    public class AMA_GCEPUserMng_Tests : TestBase
    {
        #region Constructors
        public AMA_GCEPUserMng_Tests(string browserName) : base(browserName) { }

        // Remote Selenium Grid Test
        public AMA_GCEPUserMng_Tests(string browserName, string version, string platform, string hubUri, string extrasUri)
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
        [Description("Workflow test making sure nothing goes wrong when a AMA_Staff can see all inactive and active users")]
        [Property("Status", "Complete")]
        [Author("Azat Chariyev")]
        public void UserManagement()
        {
            ///  1.Navigate to the login page with valid credentials below
            UserInfo role = UserUtils.GetUser(UserRole.Ama_Staff);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            EducationCenterPage ED = LP.LoginAsUser(role.Username, role.Password);

            if (BrowserName == BrowserNames.Firefox)
            {
                Browser.WaitForElement(Bys.EducationCenterPage.GcepLnk, ElementCriteria.IsEnabled);
            }
            ///  2.click to CGEP link and waiting load icon disappear
            GCEPPage GCEP = ED.ClickToAdvance(ED.GcepLnk);

            ///  3.From CGEPPage clicking UsermngLnk
            if (BrowserName == BrowserNames.Chrome)
            {
                Browser.WaitForElement(Bys.GCEPPage.UserManageLnk, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
            }
            GCEPUserMngPage UserManagePage = GCEP.ClickToAdvance(GCEP.UserManageLnk);


            ///  4.verifying Inactive user with below credentials 
            UserManagePage.SearchForUserByStatusAndName("Inactive", "Emily");
            if (BrowserName == BrowserNames.Chrome)
            {
                Thread.Sleep(5000);
            }
            Assert.True(UserManagePage.Grid_CellTextFoundAMATest(UserManagePage.UsersManagementTbl, "Inactive"),"text on the grid is not what we expected");

            ///  5.verifying Active user with below credentials 
            UserManagePage.SearchForUserByStatusAndName("Active", "Hamza");
            Assert.True(UserManagePage.Grid_CellTextFoundAMATest(UserManagePage.UsersManagementTbl, "Active"));

            /// 6.Verifying from User Management table that "Active" User ("Hamza") and with "Admin" Role Displayed
            UserManagePage.SearchForUserByRoleandStatusAndName("Admin", "Active","Hamza");
            Assert.True(UserManagePage.Grid_CellTextFoundAMATest(UserManagePage.UsersManagementTbl, "Active"));

            ///  7.Verifying from User Management table that "Inactive" User ("Hamza") and with "Admin" Role is not Displayed
            UserManagePage.SearchForUserByRoleandStatusAndName("Admin", "Inactive","Hamza");
            Assert.True(UserManagePage.NoRecorMatchLabel.Displayed,"No record Label is not displayed");

        }



       
        [Test]
        [Description("Workflow test making sure  AMA_Staff can see filterBox on their url which provided below and on education center page filter box is not visible" )]
        [Property("Status", "Complete")]
        [Author("Azat Chariyev")]
        public void Transcript()
        {
            ///  1.Navigate to the login page
            UserInfo role = UserUtils.GetUser(UserRole.Ama_Staff);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            EducationCenterPage ED = LP.LoginAsUser(role.Username, role.Password);

            ///  2.After navigating educationcenterpage clcicking transcript link and moving to educationcentertranscript 
            // page and updating new to the new url.
            Browser.Navigate().GoToUrl("https://ama.releasecandidate-community360.net/transcript.aspx?ActivityGUID=650acb7c-f163-4315-aa71-3df61acacb05");
            EducationCenterTransciptPage EDT = new EducationCenterTransciptPage(this.browser);

            ///  3.verifying filter box is displayed on new page
            Assert.IsTrue(EDT.FilterByBox.Displayed);

            ///  4.Go to education center
            if (BrowserName == BrowserNames.Firefox)
            {
                EDT.FilterByBoxIcon.SendKeys(Keys.Tab);
            }
            EDT.FilterByBoxIcon.Click();
            
            new WebDriverWait(Browser, TimeSpan.FromSeconds(55)).Until(ExpectedConditions.UrlContains("transcript.aspx"));

            ///  5. Verifying filter box is not there without any course displayed
            Assert.False(Browser.FindElements(Bys.EducationCenterTransciptPage.FilterByBoxIcon).Count > 0,"Filter Box Icon still displayed");
         
        }


        [Test]
        [Description("Workflow test making sure nothing goes wrong when a AMA_Staff can see all inactive and active users")]
        [Property("Status", "Complete")]
        [Author("Azat Chariyev")]
        public void DashboardNotification()
        {
            string notificationName = "";
            if (BrowserName == BrowserNames.Chrome)
            {
                notificationName = "ChromeNoteName";
            }
            if (BrowserName == BrowserNames.Firefox)
            {
                notificationName = "FireFoxNoteName";
            }
            if (BrowserName == BrowserNames.InternetExplorer)
            {
                notificationName = "IENoteName";
            }
            
            ///  1.Navigate to the login page with valid credentials below
            UserInfo role = UserUtils.GetUser(UserRole.Ama_Staff);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            EducationCenterPage ED = LP.LoginAsUser(role.Username, role.Password);

            if (BrowserName == BrowserNames.Firefox)
            {
                Browser.WaitForElement(Bys.EducationCenterPage.GcepLnk, ElementCriteria.IsEnabled);
            }
            ///  2.click to CGEP link and waiting load icon disappear
            GCEPPage GCEP = ED.ClickToAdvance(ED.GcepLnk);
           // Assert.False(GCEP.DashBoardDirectiveLbl.Displayed);
           // string NotificationText = GCEP.DashBoardDirectiveLbl.Text;


            ///  3.From CGEPPage clicking Dashboard Notification Management link
            if (BrowserName == BrowserNames.Chrome)
            {
                Browser.WaitForElement(Bys.GCEPPage.UserManageLnk, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
            }
            DashboardNotificationsPage DNP = GCEP.ClickToAdvance(GCEP.DashboardNotificationManageLnk);

            ///  4.On Dashboard notification page searching notification with the same name what we are planning to create if its exist delete and after that,
            ///  clicking create notification button and navigating NotificationCreate page
            DNP.Search(notificationName);          
            DNP.DeleteNotification();              
            NotificationCreatorPage NCP = DNP.ClickToAdvance(DNP.CreateNotificationBtn);

            ///  5.Creating notification each browser will create unique notification and for 3 diffeerent role
            NCP.NotificationNameTxt.SendKeys(notificationName);
            AssignProgramPage PP = new AssignProgramPage(Browser);
            PP.ChoosingStartDate();
            PP.ChoosingEndDate(1, "MM/dd/yyyy");
            if (BrowserName == BrowserNames.Chrome)
            {
                NCP.AdminsRdo.Click();
            }
            else if(BrowserName == BrowserNames.InternetExplorer)
            {
                NCP.ResidentsRdo.Click();                                   // NCP.ManagersRdo.Click();
            }
            else
            {
                NCP.ManagersRdo.Click();                                    // NCP.ResidentsRdo.Click();
            }
            NCP.NotificatioTitleTxt.SendKeys(NCP.CreateRandomString(12));
            NCP.NotificationBodyTxt.SendKeys(NCP.CreateRandomString(38));
            DNP = NCP.ClickToAdvance(NCP.SaveExitBtn);

            ///  6.After creating notification signing out. 
            DNP.ClickToAdvance(DNP.SignOutLnk);

            ///  7.Naviagting to login page signing in depends on the browser and since each browser create different notification each browser login with different role.
            LP = Navigation.GoToLoginPage(browser);
            if (BrowserName == BrowserNames.Chrome)
            {
                ED = LP.LoginAsUser("10031301", role.Password);//all notifications are here
            }
            else if (BrowserName == BrowserNames.InternetExplorer)
            {
                ED = LP.LoginAsUser("10021387", role.Password);       //10020462  10031037
            }
            else
            {
                ED = LP.LoginAsUser("10031037", role.Password);        //10021387
            }


            ///  8.Navigatin to GCEP page and verifying notification dashboard is displayed or notification container not empty.
            GCEP = ED.ClickToAdvance(ED.GcepLnk);
           // GCEP.WaitForInitialize();
            if (BrowserName == BrowserNames.Chrome)
            {
                Browser.WaitForElement(Bys.GCEPPage.UserManageLnk, TimeSpan.FromSeconds(60), ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
                Thread.Sleep(1500);
            }           
            GCEP.GCEPNotificationsBtn.Click();            
            Assert.True(GCEP.DashBoardDirectiveLbl.Displayed, "Dashboard Notification not displayed");
            IList<IWebElement> DashBoardNotificationContainer = GCEP.DashBoardDirectiveLbl.FindElements(By.XPath(".//li"));
            int ResidentDashboardNotificationCount = DashBoardNotificationContainer.Count;
            Assert.True(DashBoardNotificationContainer.Count >= 1);           

            ///  9.Sign out and login as a AMA_Staff navigating to GCEP page 
            GCEP.ClickToAdvance(GCEP.SignOutLnk);
            LP = Navigation.GoToLoginPage(browser);
            ED = LP.LoginAsUser(role.Username, role.Password);
            GCEP = ED.ClickToAdvance(ED.GcepLnk);

            ///  10.On GCEP Page clicking  Dashboard Notification Management link and navigating DashboardNotificationPage
            DNP = GCEP.ClickToAdvance(GCEP.DashboardNotificationManageLnk);

            ///  11.On DashboardNotificationPage searching for notification what we created and deleting notification and signing out
            DNP.Search(notificationName);
            DNP.DeleteNotification();
            DNP.ClickToAdvance(DNP.SignOutLnk);

            ///  12.Navigating login page and logging in depends on browser with different Role
            LP = Navigation.GoToLoginPage(browser);
            if (BrowserName == BrowserNames.Chrome)
            {
                ED = LP.LoginAsUser("10031301", role.Password);
            }
            else if (BrowserName == BrowserNames.InternetExplorer)
            {
                ED = LP.LoginAsUser("10021387", role.Password);                 //10031037
            }
            else
            {
                ED = LP.LoginAsUser("10031037", role.Password);                //10021387
            }


            ///  13.Navigating to GCEP page 
            GCEP = ED.ClickToAdvance(ED.GcepLnk);
            //GCEP.WaitForInitialize();
            
            if (BrowserName == BrowserNames.Chrome)
            {
                Browser.WaitForElement(Bys.GCEPPage.UserManageLnk, TimeSpan.FromSeconds(60), ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
                Thread.Sleep(1500);
            }
           // Thread.Sleep(2000);
            ///  14.Verifying that notification is no more exist.   
            if (GCEP.GCEPNotificationsBtn.Displayed)
            {
                if (!GCEP.NotificationTitlesLbl.Displayed)
                { GCEP.GCEPNotificationsBtn.Click(); }
               
                IList<IWebElement> DashBoardNotificationContainerFinal = GCEP.DashBoardDirectiveLbl.FindElements(By.XPath(".//li"));
                int ResidentDashboardNotificationCountFinal = DashBoardNotificationContainerFinal.Count;

                Assert.True(ResidentDashboardNotificationCount > ResidentDashboardNotificationCountFinal);
            }
            else { Assert.True(true, "Dashboard notitfication was deleted not displaying any notofications"); }           
        }


        [Test]
        [Description("Help Page elements changing depends on role verifying elements loging with different roles" +
          "Log in as a AMA staff then as a Admin Manager and a Resident")]
        [Property("Status", "Progress")]
        [Author("Azat Chariyev")]
    
        public void HelpPageVerification()
        {
            ///  1.Navigate to the login page login as a AMA staff
            UserInfo role = UserUtils.GetUser(UserRole.Ama_Staff);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            EducationCenterPage ED = LP.LoginAsUser(role.Username, role.Password);

            if (BrowserName == BrowserNames.Firefox)
            {
                Browser.WaitForElement(Bys.EducationCenterPage.GcepLnk, ElementCriteria.IsEnabled);
            }

            ///  2.click to GCEP link  navigate to Gcep page and waiting load icon disappear
            GCEPPage Gcep = ED.ClickToAdvance(ED.GcepLnk);

            ///  3.Navigate Institutions page choose Institutions get the contact email for it
            InstitutionsPage IP = Gcep.ClickToAdvance(Gcep.InstitutionManagLnk);
            IP.Search("Medical College of Wisconsin");
            IP.ActionGearBtn.Click();
            Thread.Sleep(0500);
            EditInstitutionPage EIP = IP.ClickToAdvance(IP.EditInstitutionLnk);

            string InstitutionContactEmailAdress = EIP.InstitutionPrimaryContactEmailTxt.GetAttribute("value");

            ///  4.Go to Help Page
            HelpPage HP = EIP.ClickToAdvance(EIP.HelpfromYourInstitutionLnk);   //(EIP.HelpLnk);

            ///  5.Verifying all Links are there for all roles
            Assert.IsTrue(HP.ResidentComingSoonLnk.Displayed);
            Assert.IsTrue(HP.ResidentLaunchResourceLnk.Displayed);
            Assert.IsTrue(HP.ManagerLaunchResourceLnk.Displayed);
            Assert.IsTrue(HP.AdminLaunchResourceLnk.Displayed);
            Assert.IsTrue(HP.AdminWatchVideoLnk.Displayed);
            Assert.IsTrue(HP.AMAMemberLaunchResourceLnk.Displayed);
            Assert.IsTrue(HP.AMAResidentLaunchResourceLnk.Displayed);
            Assert.IsTrue(HP.ContactUsLbl.Displayed);

            ///  6.Sign out        
            HP.HeaderMenuDropDown.Click();
            HP.SignOutLnk.Click();
            Thread.Sleep(2000);

            ///  7.Log in as Manager for Institution we choose above
            LP = Navigation.GoToLoginPage(Browser);
            ED = LP.LoginAsUser("10031047", "password");
            Gcep = ED.ClickToAdvance(ED.GcepLnk);

            ///  8.Navigating to a Help Page
            HP = Gcep.ClickToAdvance(EIP.HelpfromYourInstitutionLnk);   //ClickToAdvance(Gcep.HelpLnk);

            ///  9.Verifying  help links for Manager displayed and contact email are there and sign out
            Assert.IsTrue(HP.ManagerLaunchResourceLnk.Displayed);
            string InstitutionContactEmailAdressManager = HP.ContactInvolvedInstitutionEmailLnk.Text;
            Assert.AreEqual(InstitutionContactEmailAdressManager, InstitutionContactEmailAdress);
            HP.HeaderMenuDropDown.Click();
            HP.SignOutLnk.Click();
            Thread.Sleep(2000);

            ///  10.Log in as a Admin navigating to GCEP
            LP = Navigation.GoToLoginPage(browser);
            ED = LP.LoginAsUser("10031312", "password");
            Gcep = ED.ClickToAdvance(ED.GcepLnk);

            ///  11.Navigating to Help pag again
            HP = Gcep.ClickToAdvance(EIP.HelpfromYourInstitutionLnk);            //ClickToAdvance(Gcep.HelpLnk);

            ///  12.Verifying  help links for Admin displayed and contact email are there and sign out
            Assert.IsTrue(HP.AdminLaunchResourceLnk.Displayed);
            Assert.IsTrue(HP.AdminWatchVideoLnk.Displayed);
            string InstitutionContactEmailAdressAdmin = HP.ContactInvolvedInstitutionEmailLnk.Text;
            Assert.AreEqual(InstitutionContactEmailAdressAdmin, InstitutionContactEmailAdress);
            HP.HeaderMenuDropDown.Click();
            HP.SignOutLnk.Click();
            Thread.Sleep(2000);

            ///  13.Log in as a Resident navigating to GCEP
            LP = Navigation.GoToLoginPage(browser);
            ED = LP.LoginAsUser("10031201", "password");
            Gcep = ED.ClickToAdvance(ED.GcepLnk);

            ///  14.Navigating to Help page again
            HP = Gcep.ClickToAdvance(EIP.HelpfromYourInstitutionLnk);     //ClickToAdvance(Gcep.HelpLnk);

            ///  15.Verifying  help links for Resident displayed and contact email are there.
            Assert.IsTrue(HP.ResidentComingSoonLnk.Displayed);
            Assert.IsTrue(HP.ResidentLaunchResourceLnk.Displayed);
            string InstitutionContactEmailAdressResident = HP.ContactInvolvedInstitutionEmailLnk.Text;
            Assert.AreEqual(InstitutionContactEmailAdressResident, InstitutionContactEmailAdress);
        }


        [Test]
        [Description("Workflow AMA_Staff creating dashnboard notification for admin and a manager, then choosing any institution entering institution admin GCEP page"
           + "verifying notification is there and navigating to program management page choosing any program and navigating program management GCEP page verying notification is there.")]
        [Property("Status", "Complete")]
        [Author("Azat Chariyev")]
        public void DashboardNotification_AdminView_ManagerView()
        {
            string notificationNameforAdmin = "";
            if (BrowserName == BrowserNames.Chrome)
            {
                notificationNameforAdmin = "ChromeNotification";
            }
            if (BrowserName == BrowserNames.Firefox)
            {
                notificationNameforAdmin = "FireFoxNotificaton";
            }
            if (BrowserName == BrowserNames.InternetExplorer)
            {
                notificationNameforAdmin = "IENotification";
            }

            ///  1.Navigate to the login page with valid credentials below
            UserInfo role = UserUtils.GetUser(UserRole.Ama_Staff);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            EducationCenterPage ED = LP.LoginAsUser(role.Username, role.Password);

            if (BrowserName == BrowserNames.Firefox)
            {
                Browser.WaitForElement(Bys.EducationCenterPage.GcepLnk, ElementCriteria.IsEnabled);
            }

            ///  2.click to CGEP link and waiting load icon disappear and initializing page.
            GCEPPage GCEP = ED.ClickToAdvance(ED.GcepLnk);
            
            ///  3.From CGEPPage clicking Dashboard Notification Management link
            if (BrowserName == BrowserNames.Chrome)
            {
                Browser.WaitForElement(Bys.GCEPPage.UserManageLnk, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
            }
            DashboardNotificationsPage DNP = GCEP.ClickToAdvance(GCEP.DashboardNotificationManageLnk);

            ///  4.On Dashboard notification page searching notification with the same name what we are planning to create if its exist delete and after that,
            ///  clicking create notification button and navigating NotificationCreate page
            DNP.Search(notificationNameforAdmin);
            DNP.DeleteNotification();
            NotificationCreatorPage NCP = DNP.ClickToAdvance(DNP.CreateNotificationBtn);

            ///  5.Creating notification for Admin and Manager with the different name based on browser.
            NCP.NotificationNameTxt.SendKeys(notificationNameforAdmin);
            AssignProgramPage PP = new AssignProgramPage(Browser);
            PP.ChoosingStartDate();
            PP.ChoosingEndDate(1, "MM/dd/yyyy"); 
            
            NCP.AdminsRdo.Click();

            string notificationTitleAdmin = NCP.CreateRandomString(12);
            NCP.NotificatioTitleTxt.SendKeys(notificationTitleAdmin);
            
            NCP.NotificationBodyTxt.SendKeys(NCP.CreateRandomString(38));
            DNP = NCP.ClickToAdvance(NCP.SaveExitBtn);

            string notificationNameforManager = "";
            if (BrowserName == BrowserNames.Chrome)
            {
                notificationNameforManager = "ChromeNotificationManager";
            }
            if (BrowserName == BrowserNames.Firefox)
            {
                notificationNameforManager = "FireFoxNotificatonManager";
            }
            if (BrowserName == BrowserNames.InternetExplorer)
            {
                notificationNameforManager = "IENotificationManager";
            }


            GCEP = DNP.ClickToAdvance(DNP.GMECompetencyEducationProgramLnk);

            DNP = GCEP.ClickToAdvance(GCEP.DashboardNotificationManageLnk);

            DNP.Search(notificationNameforManager);
            DNP.DeleteNotification();

            NCP = DNP.ClickToAdvance(DNP.CreateNotificationBtn);

            NCP.NotificationNameTxt.SendKeys(notificationNameforManager);

            PP = new AssignProgramPage(Browser);
            PP.ChoosingStartDate();
            PP.ChoosingEndDate(1, "MM/dd/yyyy");

            NCP.ManagersRdo.Click();

            string notificationTitleManager = NCP.CreateRandomString(12);
            NCP.NotificatioTitleTxt.SendKeys(notificationTitleAdmin);

            NCP.NotificationBodyTxt.SendKeys(NCP.CreateRandomString(38));
            DNP = NCP.ClickToAdvance(NCP.SaveExitBtn);

           ///  6.Navigating Institutions Page
            GCEP = DNP.ClickToAdvance(DNP.GMECompetencyEducationProgramLnk);
            InstitutionsPage INS = GCEP.ClickToAdvance(GCEP.InstitutionManagLnk);

            ///  7.Searching for institution and navigating Institution GCEP.
            InstitutionsGCEPPage INSGCEP = INS.SearchforInstitutions("Arrowhead Regional Medical Center");
            if (BrowserName == BrowserNames.InternetExplorer)
            {
                Browser.WaitForElement(Bys.AMAPage.GCEPNotificationsBtn, TimeSpan.FromSeconds(90), ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
                //Thread.Sleep(5000);
            }

            ///  8.If notofications are displayed verify our last created is displayed with (title name is verification) if not displayed click notifications chevron and verify notification is there.
            if (!INSGCEP.NotificationTitlesLbl.Displayed)
            { INSGCEP.GCEPNotificationsBtn.Click(); }
            Assert.True(INSGCEP.VerifyNotificationTitle(INSGCEP.NotificationTitlesLbl, notificationTitleAdmin));
            
            ///  9.From Institution GCEP Page navigating Program management page searching for program and navigating program manager GCEP page.
            ProgramsPage ProgP = INSGCEP.ClickToAdvance(INSGCEP.InstitutionProgramManagmentLnk);
            ProgP.SearchforProgram("ARMC Faculty");

            ///  10.If notofications are displayed verify our last created is displayed with (title name is verification) if not displayed click notifications chevron and verify notification is there
            if (!GCEP.NotificationTitlesLbl.Displayed)
            { GCEP.GCEPNotificationsBtn.Click(); }
            Assert.True(INSGCEP.VerifyNotificationTitle(INSGCEP.NotificationTitlesLbl, notificationTitleAdmin));

            ///  11.navigating GCEP page Clicking dashboard notification links.
            GCEP.GMECompetencyEducationProgramLnk.Click();
            GCEP.WaitForInitialize();
            if (BrowserName == BrowserNames.InternetExplorer)
            {
                Browser.WaitForElement(Bys.GCEPPage.DashboardNotificationManageLnk,TimeSpan.FromSeconds(90), ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
               
            }
            ///  12.Entering dashboard notification page searching for notifications created above and deleting them.
            DNP = GCEP.ClickToAdvance(GCEP.DashboardNotificationManageLnk);
            DNP.Search(notificationNameforAdmin);
            DNP.DeleteNotification();
        }


        #endregion Tests
    }
}

