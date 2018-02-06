using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace AMA.AppFramework
{
    public class CourseTestPage : AMAPage, IDisposable
    {
        #region constructors
        public CourseTestPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "https://ama.releasecandidate-community360.net/activity3/4649682#/activity"; } }

        #endregion properties

        #region elements

        public IWebElement ContinueBtn { get { return this.FindElement(Bys.CourseTestPage.ContinueBtn); } }
        public IWebElement AssesmentTbl { get { return this.FindElement(Bys.CourseTestPage.AssessmentTbl); } }
        public IWebElement ExitActivitybtn { get { return this.FindElement(Bys.CourseTestPage.ExitActivitybtn); } }
        public IWebElement ContinuePostAssessmentBtn { get { return this.FindElement(Bys.CourseTestPage.ContinuePostAssessmentBtn); } }
        public IWebElement PreviousPostAssessmentBtn { get { return this.FindElement(Bys.CourseTestPage.PreviousPostAssessmentBtn); } }
        public IWebElement TestLaunchBtn { get { return this.FindElement(Bys.CourseTestPage.TestLaunchBtn); } }
        public IWebElement PostTestFrame { get { return this.FindElement(Bys.CourseTestPage.PostTestFrame); } }
        public IWebElement TestSubmitBtn { get { return this.FindElement(Bys.CourseTestPage.TestSubmitBtn); } }
        public IWebElement TestCancelBtn { get { return this.FindElement(Bys.CourseTestPage.TestCancelBtn); } }
        public IWebElement CertificateCloseBtn { get { return this.FindElement(Bys.CourseTestPage.CertificateCloseBtn); } }
        public IWebElement FrameCloseBtn { get { return this.FindElement(Bys.CourseTestPage.FrameCloseBtn); } }
        public IWebElement ActivityOverviewLnk { get { return this.FindElement(Bys.CourseTestPage.ActivityOverviewLnk); } }
        public IWebElement PreAssessmentLnk { get { return this.FindElement(Bys.CourseTestPage.PreAssessmentLnk); } }
        public IWebElement CourseTitleLbl { get { return this.FindElement(Bys.CourseTestPage.CourseTitleLbl); } }
        public IWebElement CourseWaitContainer { get { return this.FindElement(Bys.CourseTestPage.CourseWaitContainer); } }
        public IWebElement CourseCreditInfoConatiner { get { return this.FindElement(Bys.CourseTestPage.CourseCreditInfoConatiner); } }


        #endregion elements

        #region methods: per page

        public override void WaitForInitialize()
        { // There are 2 instances of the GCEP page. One instance is when we login as a resident, and the other is when we login as any other user
            // For this reason, we have to wait on 2 conditions. The first condition will apply for both page instances...
            this.WaitUntilAll(TimeSpan.FromSeconds(300), Criteria.CourseTestPage.LoadIconNotVisible, Criteria.CourseTestPage.CourseCreditInfoVisisble);//change UserNameTxt
            //The second condition is that we either wait for the Send Email Notification link to appear (which happens when we login as any user except
            // a resident), OR we wait for the resident directive label to appear (This occurs when we login as the resident). If one of the below
            // 2 conditions is met, then the test will proceed
            this.WaitUntilAny(Criteria.CourseTestPage.ContinueBtnVisible, Criteria.CourseTestPage.CourseCreditInfoVisisble,Criteria.CourseTestPage.ContinueBtnVisible);//SendEmailNotificationEnabled ChAHGE;

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
        public void ClickToAdvance(IWebElement buttonOrLinkElem)
        {

            if (Browser.Exists(Bys.CourseTestPage.ContinueBtn))
            {
                if (buttonOrLinkElem.GetAttribute("xpath") == ContinueBtn.GetAttribute("xpath"))
                {
                    ContinueBtn.Click();
                    Browser.WaitForElement(Bys.CourseTestPage.AssessmentTbl, TimeSpan.FromSeconds(60), ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
                    Browser.WaitForElement(Bys.AMAPage.LoadIcon, TimeSpan.FromSeconds(60), ElementCriteria.IsNotVisible);
                    //new WebDriverWait(Browser, TimeSpan.FromSeconds(15)).Until(ExpectedConditions.UrlContains("Courses.aspx"));
                    // return new EducationCenterPage(Browser);    
                }
                else
                {
                    throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, or if the button is already added, then the page you were on did not contain the button.");
                }
            }

            // return null;
        }

        /// <summary>
        /// TestFail method created specificly for the courses AUTOMATION_001 and _002, _003it will take a test and fail with credentials below
        /// It will take test 3 times and fails and returning GCEP Library Page
        /// </summary>
        ///  TODO : remove Threads sleep after flickiring isssue will be fixed


        public dynamic TestFail()
        {

            Browser.WaitForElement(Bys.AMAPage.LoadIcon, TimeSpan.FromSeconds(160), ElementCriteria.IsNotVisible);
            Browser.WaitForElement(Bys.CourseTestPage.ContinueBtn, TimeSpan.FromSeconds(420), ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
            Thread.Sleep(5000);
            ContinueBtn.Click();

            Browser.WaitForElement(Bys.AMAPage.LoadIcon, TimeSpan.FromSeconds(160), ElementCriteria.IsNotVisible);
            Browser.WaitForElement(Bys.CourseTestPage.ContinueBtn, TimeSpan.FromSeconds(420), ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
            Thread.Sleep(5000);
            ContinueBtn.Click();
            Browser.WaitForElement(Bys.CourseTestPage.ContinueBtn, TimeSpan.FromSeconds(125), ElementCriteria.IsEnabled);

            int i = 0;
            do
            {
                Thread.Sleep(1000);            
                TestLaunchBtn.Click();
                Thread.Sleep(0500);
                Browser.SwitchTo().Frame(PostTestFrame);
                ElemSet.RdoBtn_ClickMultipleByText(Browser, "B. False", "A. Harmful, abusive", "A. Low grade/poor class evaluation");
                Thread.Sleep(0500);
                TestSubmitBtn.Click();
                Thread.Sleep(0500);
                FrameCloseBtn.Click();
                i++;               
            } while (i < 3);
           
            Browser.WaitForElement(Bys.AMAPage.LoadIcon, TimeSpan.FromSeconds(160), ElementCriteria.IsNotVisible);
            Browser.WaitForElement(Bys.CourseTestPage.ContinueBtn, TimeSpan.FromSeconds(125), ElementCriteria.IsEnabled, ElementCriteria.IsVisible);
            Thread.Sleep(5000);
            ContinueBtn.Click();          

            Browser.WaitForElement(Bys.AMAPage.LoadIcon, TimeSpan.FromSeconds(160), ElementCriteria.IsNotVisible);
            Browser.WaitForElement(Bys.CourseTestPage.ContinueBtn, TimeSpan.FromSeconds(160), ElementCriteria.IsEnabled);
            Thread.Sleep(5000);
            ContinueBtn.Click();           

            Browser.WaitForElement(Bys.AMAPage.LoadIcon, TimeSpan.FromSeconds(160), ElementCriteria.IsNotVisible);
            Browser.WaitForElement(Bys.CourseTestPage.CertificateCloseBtn, TimeSpan.FromSeconds(135), ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
            Thread.Sleep(5000);
            CertificateCloseBtn.Click();          

            Browser.WaitForElement(Bys.AMAPage.LoadIcon, TimeSpan.FromSeconds(160), ElementCriteria.IsNotVisible);
            return new GCEPLibraryPage(Browser);           

        }
        #endregion methods: page specific


        /// <summary>
        /// TestPass method created specificly for the courses AUTOMATION_001 and _002, _003 it will take a test and faipass with credentials below
        /// GCEP Library Page
        /// </summary>
        /// TODO : remove Threads sleep after flickiring isssue will be fixed

        public dynamic TestPass()
        {
             WaitForInitialize();         
            Browser.WaitForElement(Bys.CourseTestPage.ContinueBtn, TimeSpan.FromSeconds(125), ElementCriteria.IsVisible, ElementCriteria.IsEnabled);         
            ContinueBtn.Click();

            Thread.Sleep(2000);
            WaitForInitialize();
            ContinueBtn.Click();

            //Thread.Sleep(2000);
            //WaitForInitialize();
            //ContinueBtn.Click();

            WaitForInitialize();                
            TestLaunchBtn.Click();

            Thread.Sleep(0500);
            Browser.SwitchTo().Frame(PostTestFrame);
            ElemSet.RdoBtn_ClickMultipleByText(Browser, "A. True", "B. Harmful, injurious", "D. All of the above");
            TestSubmitBtn.Click();
            Thread.Sleep(0500);
            FrameCloseBtn.Click();
            Thread.Sleep(0500);

            Browser.WaitForElement(Bys.AMAPage.LoadIcon, TimeSpan.FromSeconds(60), ElementCriteria.IsNotVisible);
            Browser.WaitForElement(Bys.CourseTestPage.ContinueBtn, TimeSpan.FromSeconds(125), ElementCriteria.IsEnabled);

            Thread.Sleep(1000);
            ContinueBtn.Click();
            WaitForInitialize();

            Thread.Sleep(1000);
            ContinueBtn.Click();
            Browser.WaitForElement(Bys.AMAPage.LoadIcon, TimeSpan.FromSeconds(90), ElementCriteria.IsNotVisible);
            Browser.WaitForElement(Bys.CourseTestPage.CertificateCloseBtn, TimeSpan.FromSeconds(95), ElementCriteria.IsEnabled);

            Thread.Sleep(4000);
            CertificateCloseBtn.Click();         
            Browser.WaitForElement(Bys.AMAPage.LoadIcon, TimeSpan.FromSeconds(90), ElementCriteria.IsNotVisible);

            return new GCEPPage(Browser);

        }


        public dynamic LearnerTestFail()
        {
            WaitForInitialize();
            Browser.WaitForElement(Bys.CourseTestPage.ContinueBtn, TimeSpan.FromSeconds(125), ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
            ContinueBtn.Click();

            Thread.Sleep(2000);
            WaitForInitialize();
            ContinueBtn.Click();          
            
            Browser.WaitForElement(Bys.CourseTestPage.ContinueBtn, TimeSpan.FromSeconds(25), ElementCriteria.IsEnabled);
            int i = 0;
            do
            {
                WaitForInitialize();
                Thread.Sleep(0500);
                TestLaunchBtn.Click();
                Thread.Sleep(0500);
                Browser.SwitchTo().Frame(PostTestFrame);
                ElemSet.RdoBtn_ClickMultipleByText(Browser, "B. False", "A. Harmful, abusive", "A. Low grade/poor class evaluation");
                Thread.Sleep(0500);
                TestSubmitBtn.Click();
                Thread.Sleep(0500);
                FrameCloseBtn.Click();
                i++;

            } while (i < 3);

            Thread.Sleep(1000);
            ContinueBtn.Click();
            WaitForInitialize();

            Browser.WaitForElement(Bys.AMAPage.LoadIcon, TimeSpan.FromSeconds(60), ElementCriteria.IsNotVisible);
            Browser.WaitForElement(Bys.CourseTestPage.ContinueBtn, TimeSpan.FromSeconds(25), ElementCriteria.IsEnabled);

            Thread.Sleep(1000);
            ContinueBtn.Click();           
            Browser.WaitForElement(Bys.AMAPage.LoadIcon, TimeSpan.FromSeconds(90), ElementCriteria.IsNotVisible);
            Browser.WaitForElement(Bys.CourseTestPage.CertificateCloseBtn, TimeSpan.FromSeconds(35), ElementCriteria.IsEnabled);

            Thread.Sleep(2000);
            CertificateCloseBtn.Click();         //  Browser.Navigate().Refresh();
            Browser.WaitForElement(Bys.AMAPage.LoadIcon, TimeSpan.FromSeconds(60), ElementCriteria.IsNotVisible);
            return new GCEPPage(Browser);
        }


        public dynamic TestPassNotReqiured()
        {
           
           // WaitForInitialize();
            Thread.Sleep(15000);
           // Browser.WaitForElement(Bys.AMAPage.LoadIcon, TimeSpan.FromSeconds(60), ElementCriteria.IsNotVisible);
            Browser.WaitForElement(Bys.CourseTestPage.ContinueBtn, TimeSpan.FromSeconds(25), ElementCriteria.IsEnabled);
            ContinueBtn.Click();
            Browser.WaitForElement(Bys.AMAPage.LoadIcon, TimeSpan.FromSeconds(60), ElementCriteria.IsNotVisible);
            Browser.WaitForElement(Bys.CourseTestPage.ContinueBtn, TimeSpan.FromSeconds(25), ElementCriteria.IsEnabled); 
            
            ContinueBtn.Click();
            Browser.WaitForElement(Bys.AMAPage.LoadIcon, TimeSpan.FromSeconds(60), ElementCriteria.IsNotVisible);          
            Browser.WaitForElement(Bys.CourseTestPage.AssessmentTbl, TimeSpan.FromSeconds(60), ElementCriteria.IsEnabled);  
            
            TestLaunchBtn.Click();
            Thread.Sleep(0500);
            Browser.SwitchTo().Frame(PostTestFrame);
            ElemSet.RdoBtn_ClickMultipleByText(Browser, "A. True", "B. Harmful, injurious", "D. All of the above");
            TestSubmitBtn.Click();
            Thread.Sleep(0500);
            FrameCloseBtn.Click();

            Browser.WaitForElement(Bys.CourseTestPage.ContinueBtn, TimeSpan.FromSeconds(25), ElementCriteria.IsEnabled);
            Browser.WaitForElement(Bys.AMAPage.LoadIcon, TimeSpan.FromSeconds(60), ElementCriteria.IsNotVisible);
            ContinueBtn.Click();

            Browser.WaitForElement(Bys.AMAPage.LoadIcon, TimeSpan.FromSeconds(60), ElementCriteria.IsNotVisible);
            Browser.WaitForElement(Bys.CourseTestPage.ContinueBtn, TimeSpan.FromSeconds(25), ElementCriteria.IsEnabled);
            ContinueBtn.Click();
            Thread.Sleep(1000);

            //Browser.WaitForElement(Bys.CourseTestPage.ContinueBtn, TimeSpan.FromSeconds(25), ElementCriteria.IsEnabled);
            Browser.WaitForElement(Bys.AMAPage.LoadIcon, TimeSpan.FromSeconds(90), ElementCriteria.IsNotVisible);
            Browser.WaitForElement(Bys.CourseTestPage.CertificateCloseBtn, TimeSpan.FromSeconds(35), ElementCriteria.IsEnabled);
            CertificateCloseBtn.Click();
            Browser.Navigate().Refresh(); 
            
            Browser.WaitForElement(Bys.AMAPage.LoadIcon, TimeSpan.FromSeconds(90), ElementCriteria.IsNotVisible);
            return new GCEPLibraryPage(Browser);
        }


        public dynamic LearnerTestPass()
        {
            Thread.Sleep(15000);
           // Browser.WaitForElement(Bys.AMAPage.LoadIcon, TimeSpan.FromSeconds(60), ElementCriteria.IsNotVisible);
            Browser.WaitForElement(Bys.CourseTestPage.ContinueBtn, TimeSpan.FromSeconds(25), ElementCriteria.IsEnabled);
            ContinueBtn.Click();
            Browser.WaitForElement(Bys.AMAPage.LoadIcon, TimeSpan.FromSeconds(60), ElementCriteria.IsNotVisible);
            Browser.WaitForElement(Bys.CourseTestPage.ContinueBtn, TimeSpan.FromSeconds(25), ElementCriteria.IsEnabled);
            ContinueBtn.Click();
            Browser.WaitForElement(Bys.AMAPage.LoadIcon, TimeSpan.FromSeconds(60), ElementCriteria.IsNotVisible);
           // Browser.WaitForElement(Bys.CourseTestPage.ContinueBtn, TimeSpan.FromSeconds(25), ElementCriteria.IsEnabled);
            Browser.WaitForElement(Bys.CourseTestPage.AssessmentTbl, TimeSpan.FromSeconds(60), ElementCriteria.IsEnabled);
             ContinueBtn.Click();
            TestLaunchBtn.Click();
            Thread.Sleep(0500);
            Browser.SwitchTo().Frame(PostTestFrame);
            ElemSet.RdoBtn_ClickMultipleByText(Browser, "A. True", "B. Harmful, injurious", "D. All of the above");
            TestSubmitBtn.Click();
            Thread.Sleep(0500);
            FrameCloseBtn.Click();
            Thread.Sleep(0500);

            Browser.WaitForElement(Bys.AMAPage.LoadIcon, TimeSpan.FromSeconds(60), ElementCriteria.IsNotVisible);
            Browser.WaitForElement(Bys.CourseTestPage.ContinueBtn, TimeSpan.FromSeconds(25), ElementCriteria.IsEnabled);
            ContinueBtn.Click();

           // Browser.WaitForElement(Bys.AMAPage.LoadIcon, TimeSpan.FromSeconds(60), ElementCriteria.IsNotVisible);
            ////Browser.WaitForElement(Bys.CourseTestPage.ContinueBtn, TimeSpan.FromSeconds(25), ElementCriteria.IsEnabled);          
            //ContinueBtn.Click();

            Browser.WaitForElement(Bys.AMAPage.LoadIcon, TimeSpan.FromSeconds(60), ElementCriteria.IsNotVisible);
            Browser.WaitForElement(Bys.CourseTestPage.CertificateCloseBtn, TimeSpan.FromSeconds(35), ElementCriteria.IsEnabled);
            CertificateCloseBtn.Click();
            Browser.Navigate().Refresh();
            Browser.WaitForElement(Bys.AMAPage.LoadIcon, TimeSpan.FromSeconds(60), ElementCriteria.IsNotVisible);
            return new EducationCenterPage(Browser);
        }
        public dynamic TakeTest(string role, string testStatus)
        {
            if(role=="Resident" && testStatus=="Pass" )
            {
                Thread.Sleep(5000);
                // WaitForInitialize();
                // Browser.WaitForElement(Bys.CourseTestPage.AssessmentTbl, TimeSpan.FromSeconds(60), ElementCriteria.IsEnabled);
                ContinueBtn.Click();
                Thread.Sleep(5000);
                // WaitForInitialize();
                Thread.Sleep(0500);
                Browser.SwitchTo().Frame(PostTestFrame);
                ElemSet.RdoBtn_ClickMultipleByText(Browser, "A. True", "B. Harmful, injurious", "D. All of the above");
                TestSubmitBtn.Click();
                Thread.Sleep(0500);
                FrameCloseBtn.Click();
                Thread.Sleep(0500);
                Thread.Sleep(5000);
                // WaitForInitialize();
                // Browser.WaitForElement(Bys.CourseTestPage.AssessmentTbl, TimeSpan.FromSeconds(60), ElementCriteria.IsEnabled);
                ContinueBtn.Click();
                Thread.Sleep(5000);
                //Browser.WaitForElement(Bys.AMAPage.LoadIcon, TimeSpan.FromSeconds(60), ElementCriteria.IsNotVisible);
                //Browser.WaitForElement(Bys.CourseTestPage.CertificateCloseBtn, TimeSpan.FromSeconds(35), ElementCriteria.IsEnabled);
                CertificateCloseBtn.Click();
                Browser.WaitForElement(Bys.AMAPage.LoadIcon, TimeSpan.FromSeconds(60), ElementCriteria.IsNotVisible);

                return new GCEPLibraryPage(Browser);
            }
            else if(role=="Resident" && testStatus=="Fail" )
            {
                Thread.Sleep(5000);
                // WaitForInitialize();
                // Browser.WaitForElement(Bys.CourseTestPage.AssessmentTbl, TimeSpan.FromSeconds(60), ElementCriteria.IsEnabled);
                ContinueBtn.Click();
                Thread.Sleep(5000);
                // WaitForInitialize();
                int i = 0;
                do
                {
                    Thread.Sleep(0500);
                    TestLaunchBtn.Click();
                    Thread.Sleep(0500);
                    Browser.SwitchTo().Frame(PostTestFrame);
                    ElemSet.RdoBtn_ClickMultipleByText(Browser, "B. False", "A. Harmful, abusive", "A. Low grade/poor class evaluation");
                    Thread.Sleep(0500);
                    TestSubmitBtn.Click();
                    Thread.Sleep(0500);
                    FrameCloseBtn.Click();
                    i++;
                } while (i < 3);
                Thread.Sleep(0500);
                // WaitForInitialize();
                // Browser.WaitForElement(Bys.CourseTestPage.AssessmentTbl, TimeSpan.FromSeconds(60), ElementCriteria.IsEnabled);
                ContinueBtn.Click();
                Thread.Sleep(5000);
                // WaitForInitialize();
                // Browser.WaitForElement(Bys.CourseTestPage.AssessmentTbl, TimeSpan.FromSeconds(60), ElementCriteria.IsEnabled);
                ContinueBtn.Click();
                Thread.Sleep(5000);
                //Browser.WaitForElement(Bys.AMAPage.LoadIcon, TimeSpan.FromSeconds(60), ElementCriteria.IsNotVisible);
                //Browser.WaitForElement(Bys.CourseTestPage.CertificateCloseBtn, TimeSpan.FromSeconds(35), ElementCriteria.IsEnabled);
                CertificateCloseBtn.Click();
                Browser.WaitForElement(Bys.AMAPage.LoadIcon, TimeSpan.FromSeconds(60), ElementCriteria.IsNotVisible);
                return new GCEPLibraryPage(Browser);
            }
            return null;
        }
    }
}


