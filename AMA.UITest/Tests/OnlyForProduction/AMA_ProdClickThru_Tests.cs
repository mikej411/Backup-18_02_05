using Browser.Core.Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using AMA.AppFramework;
using System;
using AMA.AppFramework.Utils.User;
using static AMA.AppFramework.Utils.User.UserUtils;
using System.Threading;

namespace AMA.UITest
{
    //[BrowserMode(BrowserMode.New)]
    //[LocalSeleniumTestFixture(BrowserNames.Chrome)]
    //[LocalSeleniumTestFixture(BrowserNames.Firefox)]
    //[LocalSeleniumTestFixture(BrowserNames.InternetExplorer)]
    //[RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    //[RemoteSeleniumTestFixture(BrowserNames.Firefox)]
    //[RemoteSeleniumTestFixture(BrowserNames.InternetExplorer)]
    //[TestFixture]
    public class Production_ClickThru_Tests : TestBase
    {
        #region Constructors
        public Production_ClickThru_Tests(string browserName) : base(browserName) { }

        // Remote Selenium Grid Test
        public Production_ClickThru_Tests(string browserName, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests



       // [Test]
        [Description("")]
        [Property("Status", "Complete")]
        [Author("Azat Chariyev")]
       // [Category("AzatCat")]

        public void LoginTest()
        {
            ///  1.Navigate to the login page
            Browser.Navigate().GoToUrl("https://login.ama-assn.org/account/login");
            IWebElement ProdUserNameTxt = Browser.FindElement(By.Id("go_username"));
            IWebElement ProdPasswordTxt = Browser.FindElement(By.Id("go_password"));
            IWebElement ProdLoginBtn = Browser.FindElement(By.XPath("//input[@value='Sign In']"));
            ProdLoginBtn.Click();
            Thread.Sleep(2000);
            IWebElement ProdUserNameWarningLbl = Browser.FindElement(By.Id("wwerr_go_username"));
            IWebElement ProdPassWordWarningLbl = Browser.FindElement(By.Id("wwerr_go_password"));
            Assert.IsTrue(ProdPassWordWarningLbl.Displayed);
            Assert.IsTrue(ProdUserNameWarningLbl.Displayed);
           
        }


        //[Test]
        [Description("")]
        [Property("Status", "Complete")]
        [Author("Azat Chariyev")]
        public void TestCount()
        {
            string InstitutionName = "Temple University Hospital";
            Browser.Navigate().GoToUrl("https://cme.ama-assn.org");
            IWebElement ProdUserNameTxt = Browser.FindElement(By.Id("go_username"));
            IWebElement ProdPasswordTxt = Browser.FindElement(By.Id("go_password"));
            IWebElement ProdLoginBtn = Browser.FindElement(By.XPath("//input[@value='Sign In']"));           
            ProdUserNameTxt.Clear();
            ProdUserNameTxt.SendKeys("jpenderville");
            ProdPasswordTxt.Clear();
            ProdPasswordTxt.SendKeys("password1");   //div[@role='gridcell' and @ng-if='row.entity.Users > 0']/a
            ProdLoginBtn.Click();
            EducationCenterPage ED = new EducationCenterPage(Browser);
            Browser.WaitForElement(Bys.EducationCenterPage.CourseTbl, ElementCriteria.IsEnabled, ElementCriteria.IsVisible);           
            GCEPPage Gcep = ED.ClickToAdvance(ED.GcepLnk); 
            
            int countOfCurriculumsOnGcep = Convert.ToInt32(Gcep.TotalCurriculumTmtsCountLnk.Text);
            int countOfTotalUsers = Convert.ToInt32(Gcep.TotalUsersCountLnk.Text);
            GCEPUserMngPage GUMP = Gcep.ClickToAdvance(Gcep.UserManageLnk);
            int CountOfUsersOnUserMngPage = GUMP.GetCountOfUsersFromUserManagementLabel();
            Assert.AreEqual(countOfTotalUsers, CountOfUsersOnUserMngPage, "Counts of Users are not the same");

            Gcep = GUMP.ClickToAdvance(GUMP.AdministrationLnk);
            CurriculumMngPage CMP = Gcep.ClickToAdvance(Gcep.CurriculumTemplatesLnk);
            int countofCurriculumTemplatesFromCurriculumTemplatesPage = CMP.GetCountOfCurriculumonTable("of", "items");            
            Assert.AreEqual(countOfCurriculumsOnGcep, countofCurriculumTemplatesFromCurriculumTemplatesPage,"Count of Curriculum Templates are not equals on GCEP page with CurriculumMngPage");
            Gcep = CMP.ClickToAdvance(CMP.AdministrationLnk);          
            InstitutionsPage IP = Gcep.ClickToAdvance(Gcep.InstitutionManagLnk);
            IP.Search(InstitutionName);
            string xPathVariableForUserCount = string.Format("//*[text()='{0}']/../../../../div/div[3]", InstitutionName);       //div[@role='gridcell' and contains (text(),'')]/a[@ng-if='row.entity.Users > 0']
            string xPathVariableForProgramCount = string.Format("//*[text()='{0}']/../../../../div/div[4]", InstitutionName);    //div[@role='gridcell' and contains (text(),'')]/a[@ng-if='row.entity.Programs > 0']
            int usersCountFromInstitutionsPage = Convert.ToInt32(Browser.FindElement(By.XPath(xPathVariableForUserCount)).Text);
            int programsCountFromInstitutionsPage = Convert.ToInt32(Browser.FindElement(By.XPath(xPathVariableForProgramCount)).Text);

            InstitutionsGCEPPage IGP = IP.SearchforInstitutions(InstitutionName);
            int usersCountFromInstitutionsGCEPPage = Convert.ToInt32(IGP.TotalUserCountLnk.Text);
            int programsCountFromInstitutionsGCEPPage = Convert.ToInt32(IGP.TotalProgramCountLnk.Text);

           // Assert.AreEqual(usersCountFromInstitutionsPage, usersCountFromInstitutionsGCEPPage, "Count of user are not equals  on Institutions table with InstitutionGCEP page");
           // Assert.AreEqual(programsCountFromInstitutionsPage, programsCountFromInstitutionsGCEPPage, "Count of programs are not equals on Institutions table with InstitutionGCEP page");

            ProgramsPage PP = IGP.ClickToAdvance(IGP.InstitutionProgramManagmentLnk);
            int countsOfProgramsOnPP = PP.Grid_GetCountOfItemsOnTable("of", "items");

           // Assert.AreEqual(programsCountFromInstitutionsPage, countsOfProgramsOnPP);  
            
            IGP = PP.ClickToBreadCrumbContainerToReturnInsGCEP(InstitutionName);
            GUMP = IGP.ClickToAdvance(IGP.InstitutionUserManagementLnk);
         int CountOfUsersOnUserMngPageForIns = GUMP.GetCountOfUsersFromUserManagementLabel();

            //Assert.AreEqual(usersCountFromInstitutionsPage, CountOfUsersOnUserMngPageForIns);

            Assert.True(DataUtils.intsEqual(usersCountFromInstitutionsPage, usersCountFromInstitutionsGCEPPage, CountOfUsersOnUserMngPageForIns));
            Assert.True(DataUtils.intsEqual(countsOfProgramsOnPP, programsCountFromInstitutionsGCEPPage, programsCountFromInstitutionsPage));
            Thread.Sleep(5000);
        }

       // [Test]
        [Description(" ")]
        [Property("Status", "Complete")]
        [Author("Azat Chariyev")]
        public void TestHelp()
        {
            Browser.Navigate().GoToUrl("https://cme.ama-assn.org/gme-competency/");
            IWebElement ProdUserNameTxt = Browser.FindElement(By.Id("go_username"));
            IWebElement ProdPasswordTxt = Browser.FindElement(By.Id("go_password"));
            IWebElement ProdLoginBtn = Browser.FindElement(By.XPath("//input[@value='Sign In']"));
            ProdUserNameTxt.Clear();
            ProdUserNameTxt.SendKeys("jpenderville");
            ProdPasswordTxt.Clear();
            ProdPasswordTxt.SendKeys("password1");
            ProdLoginBtn.Click();
            GCEPPage GP = new GCEPPage(Browser);
            GP.WaitForInitialize();
            HelpPage HP = GP.ClickToAdvance(GP.HelpLnk);
            Assert.IsTrue(HP.JAMALaunchResourceLnk.Displayed);
            Assert.IsTrue(HP.AMAMemberLaunchResourceLnk.Displayed);            
            Assert.IsTrue(HP.ContactUsLbl.Displayed);
        }


       // [Test]
        [Description("")]
        [Property("Status", "Complete")]
        [Author("Azat Chariyev")]
        public void CheckGCEPElements()
        {          
            Browser.Navigate().GoToUrl("https://cme.ama-assn.org/gme-competency/");
            IWebElement ProdUserNameTxt = Browser.FindElement(By.Id("go_username"));
            IWebElement ProdPasswordTxt = Browser.FindElement(By.Id("go_password"));
            IWebElement ProdLoginBtn = Browser.FindElement(By.XPath("//input[@value='Sign In']"));
            ProdUserNameTxt.Clear();
            ProdUserNameTxt.SendKeys("jpenderville");
            ProdPasswordTxt.Clear();
            ProdPasswordTxt.SendKeys("password1");
            ProdLoginBtn.Click();
            GCEPPage GP = new GCEPPage(Browser);
            GP.WaitForInitialize();
            Assert.True(GP.AMALogoLnk.Enabled);
            Assert.True(GP.InstitutionsCountLnk.Displayed);
            Assert.True(GP.TotalCurriculumTmtsCountLnk.Displayed);
            Assert.True(GP.TotalUsersCountLnk.Displayed);
            Assert.True(GP.SendEmailNotificationLnk.Displayed);
            Assert.True(GP.CreateCurriculumTemplatesLnk.Displayed);
            Assert.True(GP.RunReportLnk.Displayed);
            Assert.True(GP.InstitutionManagLnk.Displayed);
            Assert.True(GP.UserManageLnk.Displayed);
            Assert.True(GP.CurriculumTemplatesLnk.Displayed);
            Assert.True(GP.DashboardNotificationManageLnk.Displayed);

        }


       // [Test]
        [Description("")]
        [Property("Status", "Complete")]
        [Author("Azat Chariyev")]
        public void CheckLoading()
        {
            Browser.Navigate().GoToUrl("https://cme.ama-assn.org/gme-competency/");
            IWebElement ProdUserNameTxt = Browser.FindElement(By.Id("go_username"));
            IWebElement ProdPasswordTxt = Browser.FindElement(By.Id("go_password"));
            IWebElement ProdLoginBtn = Browser.FindElement(By.XPath("//input[@value='Sign In']"));
            ProdUserNameTxt.Clear();
            ProdUserNameTxt.SendKeys("jpenderville");
            ProdPasswordTxt.Clear();
            ProdPasswordTxt.SendKeys("password1");
            ProdLoginBtn.Click();
            GCEPPage GP = new GCEPPage(Browser);
            GP.WaitForInitialize();
            InstitutionsPage IP = GP.ClickToAdvance(GP.InstitutionManagLnk);
            InstitutionsGCEPPage InsGcep = IP.SearchforInstitutions("Beaumont Health System (SEMCME)");
            PromotePGYPage ProPGY = InsGcep.ClickToAdvance(InsGcep.InstitutionPromotePgyLnk);
            Thread.Sleep(8000);
            Assert.True(ProPGY.AvailableResidentsPromotePGYTblFirstRowChk.Displayed);
            //Thread.Sleep(3000);
            //Assert.False(ProPGY.AvailableResidentsPromotePGYTblFirstRowChk.Displayed);
            //Thread.Sleep(3000);
            //Assert.True(ProPGY.AvailableResidentsPromotePGYTblFirstRowChk.Displayed);
        }


            #endregion Tests
        }
}

