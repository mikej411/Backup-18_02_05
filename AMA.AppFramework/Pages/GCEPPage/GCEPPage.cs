using AMA.AppFramework.Utils.User;
using Browser.Core.Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace AMA.AppFramework
{
    

    public class GCEPPage : AMAPage, IDisposable
    {
        #region constructors
        public GCEPPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "gme-competency/admin"; } }

        #endregion properties

        #region elements

        public IWebElement SendEmailNotificationLnk { get { return this.FindElement(Bys.GCEPPage.SendEmailNotificationLnk); } }
        public IWebElement CreateCurriculumTemplatesLnk { get { return this.FindElement(Bys.GCEPPage.CreateCurriculumTemplatesLnk); } }
        public IWebElement RunReportLnk { get { return this.FindElement(Bys.GCEPPage.RunReportLnk); } }
        public IWebElement InstitutionManagLnk { get { return this.FindElement(Bys.GCEPPage.InstitutionManagLnk); } }
        public IWebElement UserManageLnk { get { return this.FindElement(Bys.GCEPPage.UserManageLnk); } }
        public IWebElement CurriculumTemplatesLnk { get { return this.FindElement(Bys.GCEPPage.CurriculumTemplatesLnk); } }
        public IWebElement DashboardNotificationManageLnk { get { return this.FindElement(Bys.GCEPPage.DashboardNotificationManageLnk); } }
        public IWebElement InstitutionsCountLnk { get { return this.FindElement(Bys.GCEPPage.InstitutionCountLnk); } }
        public IWebElement TotalCurriculumTmtsCountLnk { get { return this.FindElement(Bys.GCEPPage.TotalCurriculumTemplateCountLnk); } }
        public IWebElement TotalUsersCountLnk { get { return this.FindElement(Bys.GCEPPage.TotalUsersCountLnk); } }
        public IWebElement MyRequiredCourseLnk { get { return this.FindElement(Bys.GCEPPage.MyRequiredCourseLnk); } }
        public IWebElement LibraryLnk { get { return this.FindElement(Bys.GCEPPage.LibraryLnk); } }
        public IWebElement TranscriptLnk { get { return this.FindElement(Bys.GCEPPage.TranscriptLnk); } }
        public IWebElement MyRegiuredCourseTbl { get { return this.FindElement(Bys.GCEPPage.MyRegiuredCourseTbl); } }
        public IWebElement InstitutionLogoImg { get { return this.FindElement(Bys.GCEPPage.InstitutionLogoImg); } }
        public IWebElement ContainerToWait { get { return this.FindElement(Bys.GCEPPage.ContainerToWait); } }
        public SelectElement ProgramSelElem { get { return new SelectElement(this.FindElement(Bys.GCEPPage.ProgramSelElem)); } }
        public SelectElement InstitutionSelElem { get { return new SelectElement(this.FindElement(Bys.GCEPPage.InstitutionSelElem)); } }
        public IWebElement DashBoardDirectiveLbl { get { return this.FindElement(Bys.GCEPPage.DashBoardDirectiveLbl); } }   
        public IWebElement PromotePGYLnk { get { return this.FindElement(Bys.GCEPPage.PromotePGYLnk); } }
        public IWebElement ResidentCourseTbl { get { return this.FindElement(Bys.GCEPPage.ResidentCourseTbl); } }    
        public IWebElement ResidentCourseTrackerLbl { get { return this.FindElement(Bys.GCEPPage.ResidentCourseTrackerLbl); } }
        public IWebElement ResidentGcepShowElectiveCourseLnk { get { return this.FindElement(Bys.GCEPPage.ResidentGcepShowElectiveCourseLnk); } }
        public IWebElement ResidentGCEPSortBYDueDateBtn { get { return this.FindElement(Bys.GCEPPage.ResidentGCEPSortBYDueDateBtn); } }
        public IWebElement ResidentGCEPSortBYDurationBtn { get { return this.FindElement(Bys.GCEPPage.ResidentGCEPSortBYDurationBtn); } }
        public IWebElement ResidentGCEPSortBYProgressBtn { get { return this.FindElement(Bys.GCEPPage.ResidentGCEPSortBYProgressBtn); } }
        public IWebElement ResidentCoutseStatusFailedLbl { get { return this.FindElement(Bys.GCEPPage.ResidentCoutseStatusFailedLbl); } }
        public IWebElement ResidentCourseStatusLockedLbl { get { return this.FindElement(Bys.GCEPPage.ResidentCourseStatusLockedLbl); } }
        public IWebElement ResidentGCEPCompletedSwitchBtn { get { return this.FindElement(Bys.GCEPPage.ResidentGCEPCompletedSwitchBtn); } }
        public IWebElement ResidentGCEPTranscriptBtn { get { return this.FindElement(Bys.GCEPPage.ResidentGCEPTranscriptBtn); } }
        public IWebElement ResidentGCEPCertificatesDownloadBtn { get { return this.FindElement(Bys.GCEPPage.ResidentGCEPCertificatesDownloadBtn); } }
        public SelectElement ResidentGCEPAcedimicYearSelElem { get { return new SelectElement( this.FindElement(Bys.GCEPPage.ResidentGCEPAcedimicYearSelElem)); } }
        public IWebElement ResidentCourseCompletionDatesLbl { get { return this.FindElement(Bys.GCEPPage.ResidentCourseCompletionDatesLbl); } }
        public IWebElement ResidentNoCourseBeenCompltetLbl { get { return this.FindElement(Bys.GCEPPage.ResidentNoCourseBeenCompltetLbl); } }
        public IWebElement ResidentCouseStartNowBtn { get { return this.FindElement(Bys.GCEPPage.ResidentCouseStartNowBtn); } }
        public IWebElement MemberBenefitsManagementLnk { get { return this.FindElement(Bys.GCEPPage.MemberBenefitsManagementLnk); } }
        public IWebElement AdminSwitchBtn { get { return this.FindElement(Bys.GCEPPage.AdminSwitchBtn); } }

        #endregion elements

        #region methods: per page

        public override void WaitForInitialize()
        {
            // There are 2 instances of the GCEP page. One instance is when we login as a resident, and the other is when we login as any other user
            // For this reason, we have to wait on 2 conditions. The first condition will apply for both page instances...
            this.WaitUntilAll(TimeSpan.FromSeconds(300), Criteria.GCEPPage.LoadingComplete, Criteria.GCEPPage.WaitContainer);//change UserNameTxt
            //The second condition is that we either wait for the Send Email Notification link to appear (which happens when we login as any user except
            // a resident), OR we wait for the resident directive label to appear (This occurs when we login as the resident). If one of the below
            // 2 conditions is met, then the test will proceed
            this.WaitUntilAny(Criteria.GCEPPage.SendEmailNotificationLnkEnabledVisible, Criteria.GCEPPage.ResidentCourseTableVisible);//SendEmailNotificationEnabled ChAHGE;
         
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            try { activeRequests.Clear(); }
            catch (Exception ex) { _log.ErrorFormat("Failed to dispose LoginPage", activeRequests.Count, ex); }
        }

        #endregion methods: per page

        #region methods: page specific

        /// <summary>
        /// Clicks the user-specified button or link and then waits for a window to close or open, or a page to load,
        /// depending on the button that was clicked
        /// </summary>
        /// <param name="buttonOrLinkElem">The element to click on</param>
        public dynamic ClickToAdvance(IWebElement buttonOrLinkElem)
        {
            if (Browser.Exists(Bys.GCEPPage.UserManageLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == UserManageLnk.GetAttribute("outerHTML"))
                {
                    UserManageLnk.Click();                   
                    GCEPUserMngPage GPU = new GCEPUserMngPage(Browser);
                    GPU.WaitForInitialize();
                    return GPU;
                }
            }
            if (Browser.Exists(Bys.GCEPPage.CurriculumTemplatesLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == CurriculumTemplatesLnk.GetAttribute("outerHTML"))
                {
                    WaitForInitialize();                  
                    CurriculumTemplatesLnk.Click();                  
                    CurriculumMngPage CMP = new CurriculumMngPage(Browser);
                    CMP.WaitForInitialize();
                    return CMP;
                    //new WebDriverWait(Browser, TimeSpan.FromSeconds(25)).Until(ExpectedConditions.UrlContains("gme-competency/admin/programcurriculumtemplates"));
                }
            }
            if (Browser.Exists(Bys.GCEPPage.LibraryLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == LibraryLnk.GetAttribute("outerHTML"))
                {
                    Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);
                    LibraryLnk.Click();                  
                    GCEPLibraryPage GPL = new GCEPLibraryPage(Browser);
                    GPL.WaitForInitialize();
                    return GPL;
                    // return  new CurriculumMngPage(Browser);
                }
            }
            if (Browser.Exists(Bys.GCEPPage.TranscriptLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == TranscriptLnk.GetAttribute("outerHTML"))
                {
                    Browser.WaitForElement(Bys.AMAPage.LoadIcon, TimeSpan.FromSeconds(90), ElementCriteria.IsNotVisible);
                    TranscriptLnk.Click();                    
                    GCEPTranscriptPage GCTP = new GCEPTranscriptPage(Browser);
                    GCTP.WaitForInitialize();
                    return  GCTP;
                }
            }
            if (Browser.Exists(Bys.GCEPPage.InstitutionManagLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == InstitutionManagLnk.GetAttribute("outerHTML"))
                {
                    WaitForInitialize();                   
                    InstitutionManagLnk.Click();                  
                    InstitutionsPage IP = new InstitutionsPage(Browser);
                    IP.WaitForInitialize();
                    return IP;
                }
            }
            if (Browser.Exists(Bys.GCEPPage.DashboardNotificationManageLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == DashboardNotificationManageLnk.GetAttribute("outerHTML"))
                {
                    WaitForInitialize();                   
                    DashboardNotificationManageLnk.Click();                   
                    DashboardNotificationsPage DNP = new DashboardNotificationsPage(Browser);
                    DNP.WaitForInitialize();
                    return DNP;
                }
            }
            if (Browser.Exists(Bys.AMAPage.SignOutLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == SignOutLnk.GetAttribute("outerHTML"))
                {
                    Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);                    
                    HeaderMenuDropDown.Click();                  
                    SignOutLnk.SendKeys(Keys.Tab);
                    SignOutLnk.Click();
                    Thread.Sleep(0500);
                    return this.Browser;
                    // new WebDriverWait(Browser, TimeSpan.FromSeconds(45)).Until(ExpectedConditions.UrlMatches("https://logintest.ama-assn.org/account/logout"));

                }
            }
            if (Browser.Exists(Bys.GCEPPage.PromotePGYLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == PromotePGYLnk.GetAttribute("outerHTML"))
                {                   
                    WaitForInitialize();
                    PromotePGYLnk.Click();                   
                    Browser.WaitForElement(Bys.PromotePGYPage.AvailableResidentsPromotePGYTblFirstRowChk, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
                    PromotePGYPage PPGYP = new PromotePGYPage(Browser);
                    PPGYP.WaitForInitialize();
                    return PPGYP;
                }
            }
            if (Browser.Exists(Bys.AMAPage.HelpLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == HelpLnk.GetAttribute("outerHTML"))
                {
                    Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);
                    HelpLnk.SendKeys(Keys.Tab);
                    HelpLnk.Click();
                    HelpPage HP = new HelpPage(Browser);
                    HP.WaitForInitialize();
                    return HP;
                }
            }
            if (Browser.Exists(Bys.AMAPage.HelpfromYourInstitutionLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == HelpfromYourInstitutionLnk.GetAttribute("outerHTML"))
                {
                    HeaderMenuDropDown.Click();                 
                    Thread.Sleep(0500);                   
                    HelpfromYourInstitutionLnk.Click();
                    Browser.SwitchTo().Window(Browser.WindowHandles.Last());
                    Thread.Sleep(0500);
                    Browser.Manage().Window.Maximize();
                    Thread.Sleep(0500);
                    Browser.SwitchTo().Window(Browser.WindowHandles.First()).Close();
                    if ( Browser.WindowHandles.Count>1) { Browser.SwitchTo().Window(Browser.WindowHandles.First()).Close(); }
                    Thread.Sleep(0500);
                    Browser.SwitchTo().Window(Browser.WindowHandles.Last());
                    Browser.Manage().Window.Maximize();
                    Thread.Sleep(0500);
                    HelpPage HP = new HelpPage(Browser);
                    HP.WaitForInitialize();                  
                    return HP;                 
                }
            }
            if (Browser.Exists(Bys.GCEPPage.ResidentCouseStartNowBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == ResidentCouseStartNowBtn.GetAttribute("outerHTML"))
                {
                    Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);
                    ResidentCouseStartNowBtn.Click();
                    CourseTestPage CTP = new CourseTestPage(Browser);
                    CTP.WaitForInitialize();
                    return CTP;
                }

            }
            if (Browser.Exists(Bys.GCEPPage.MemberBenefitsManagementLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == MemberBenefitsManagementLnk.GetAttribute("outerHTML"))
                {
                    Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);
                    MemberBenefitsManagementLnk.Click();
                    MemberBenefitPage MBP = new MemberBenefitPage(Browser);
                    MBP.WaitForInitialize();
                    return MBP;
                }

            }
            else
            {
                throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, or if the button is already added, then the page you were on did not contain the button.");
            }

            return null;
        }
       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gridElem"></param>
        /// <returns></returns>
        public int Grid_GetRowCount(IWebElement gridElem)
        {
            Thread.Sleep(0500);
            try
            {
                if (gridElem.FindElements(By.XPath("./div/div")).Count == 0)
                {
                    return 0;
                }
            }
            catch (StaleElementReferenceException)
            {
                Browser.WaitForElement(Bys.GCEPPage.MyRegiuredCourseTbl, ElementCriteria.IsVisible);

            }
            try
            {
                int rowCount = gridElem.FindElements(By.XPath(".//div[@class='row activity-listing-row']")).Count;
                return rowCount;
            }
            catch 
            {
                string CourseTracker = ResidentCourseTrackerLbl.Text;
                Assert.True(!CourseTracker.Contains("0"));
                return 0;
                //int rowCount = Browser.FindElements(By.XPath("//div[@class='col-xs-12']/div[@class='row']/div/div/div/div/div")).Count;
                //return rowCount;
            }          
        }

        /// <summary>
        /// cliking to link and if their is table getting count of rows 
        /// </summary>
        /// <param name="gridElem">table where tables located</param>
        /// <param name="Linktoclick"> </param>
        /// <returns></returns>
        public int GetCountOfCourses(IWebElement gridElem, IWebElement Linktoclick)
        {
            Linktoclick.SendKeys(Keys.Tab);
            Linktoclick.Click();
            Thread.Sleep(0500);
            int CountOfCourses = Grid_GetRowCount(gridElem);
            return CountOfCourses;
        }


      

        /// <summary>
        /// Explanation
        /// </summary>
        /// <param name="SelElem"></param>
        /// <returns></returns>
        public Boolean SelectProgramAndVerifyBreadcrump(SelectElement SelElem)
        {
            WaitForInitialize();
               // Thread.Sleep(1000);
                string BT = "";
                string P1N = "";
                IWebElement BreadCrump = Browser.FindElement(Bys.GCEPPage.BreadCrumpLnkContainer);
                //Thread.Sleep(1000);
                P1N = SelElem.SelectedOption.Text;
                if (BreadCrump.Displayed)
                {
                    BT = BreadCrump.Text;
                    Assert.True(string.Equals(P1N, BT, StringComparison.OrdinalIgnoreCase));
                    return true;
                }
                return false;
         
        }


        /// <summary>
        /// Explain
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string ReturnValueAfterSelectingProgram(int index)
        {
            string ProgramName = "";
            ProgramSelElem.SelectByIndex(index);
            WaitForInitialize();
            ProgramName = ProgramSelElem.SelectedOption.Text;
            return ProgramName;
        }

        /// <summary>
        /// Explain
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string ReturnValueAfterSelectingInstitution(int index)
        {
                string InstitutionName = "";               
                InstitutionSelElem.SelectByIndex(index);
                WaitForInitialize();
                Browser.WaitForElement(Bys.GCEPPage.InstitutionSelElem, ElementCriteria.IsVisible,ElementCriteria.IsEnabled);            
                InstitutionName = InstitutionSelElem.SelectedOption.Text;          
                return InstitutionName;
        }

        public dynamic GetAllInstitutionNamesFromDropdown()
        {
            //IList<IWebElement> Institutions = InstitutionSelElem.AllSelectedOptions;
            // List<string> InstitutionName = Institutions.Select(IWebElement => IWebElement.Text);
            List<string> SelectValue = new List<string>(Browser.FindElements(By.Name("institutionSelect")).Select(IWebElement => IWebElement.Text));
            string InstitutionNames = SelectValue[0].ToString();
            string[] separatingChars = { "---", "\r\n" };
            string[] Institution = InstitutionNames.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);

            return Institution;
        }

       public dynamic ResidentStartCourseOrContinue(IWebDriver browser,string Coursname)
        {
            string xPathVariable = string.Format("//div[@class='activity-info-name']/h4[.='{0}']/../../..//button", Coursname);
            Thread.Sleep(0500);
            IWebElement button = browser.FindElement(By.XPath(xPathVariable));
            Thread.Sleep(0500);
            button.Click();
            CourseTestPage CTP = new CourseTestPage(browser);
            return CTP;
        }

        /// <summary>
        /// Explain
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="Coursname"></param>
        /// <param name="buttonTextExpected"></param>
        /// <returns></returns>
        public Boolean VerificationCourseCompletion (IWebDriver browser, string Coursname, string buttonTextExpected)
        {
            try
            {
                string xPathVariable = string.Format("//div[@class='activity-info-name']/h4[.='{0}']/../../..//button", Coursname);
                string buttonTextActual = browser.FindElement(By.XPath(xPathVariable)).Text;
                //buttonTextExpected = "View Certificate";
                if (buttonTextActual.Equals(buttonTextExpected))
                { return true; }
            }
            catch 
            {
                string xPathVariable = string.Format("//div[@class='margin-top-bottom-10']/h3[.='{0}']/../..//div[@class='ng-scope']", Coursname);
                string buttonTextActual = browser.FindElement(By.XPath(xPathVariable)).Text;
                //buttonTextExpected = "View Certificate";
                if (buttonTextActual.Equals(buttonTextExpected))
                { return true; }
            }
            return false;
        }

        /// <summary>
        /// Explain
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="CourseNames"></param>
        /// <returns></returns>
        public Boolean VerificationOfChoosenCoursesAssignedForResident(IWebDriver browser,List<string>CourseNames)
        {
            List<string> CourseNamesFromResintGcep = new List<string>(browser.FindElements(By.XPath("//div[@class='row course-listing-top']//div[@class='margin-top-bottom-10']/h3")).Select(iw => iw.Text));
            if (CourseNames.All(item=> CourseNamesFromResintGcep.Contains(item)) && CourseNamesFromResintGcep.All(item => CourseNames.Contains(item))) { return true; }
            return false;
        }

        /// <summary>
        /// This method scrool to the bottom of the page to load all courses.
        /// </summary>
        public void ScrolltoGetAllCourses()
        {
            int countofRowNoScroll;
            int countofRowFirstScroll;
            do
            {
                List<IWebElement> EnterRow = new List<IWebElement>(Browser.FindElements(By.XPath("//div[@class='activity-listing-bottom ng-scope']")));
                countofRowNoScroll = EnterRow.Count;
                //Thread.Sleep(1000);
                ElemSet.ScrollToElement(Browser, FaceBookLnk);

                Thread.Sleep(1000);
                List<IWebElement> EnterRowFirstScroll1 = new List<IWebElement>(Browser.FindElements(By.XPath("//div[@class='activity-listing-bottom ng-scope']")));
                countofRowFirstScroll = EnterRowFirstScroll1.Count;
                Thread.Sleep(1000);
            }
            while (countofRowFirstScroll > countofRowNoScroll);
        }

    }

         #endregion methods: page specific



}