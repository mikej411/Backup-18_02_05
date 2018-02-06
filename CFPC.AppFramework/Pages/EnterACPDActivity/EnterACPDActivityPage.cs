using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using LOG4NET = log4net.ILog;


namespace CFPC.AppFramework
{
    public class EnterACPDActivityPage : CFPCPage, IDisposable
    {
        #region constructors
        public EnterACPDActivityPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that I start so I can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "Default.aspx"; } }

        #endregion properties

        #region elements

        // Main page
        public IWebElement CategoryDrpDn { get { return this.FindElement(Bys.EnterACPDActivityPage.CategoryDrpDn); } }


        //public SelectElement ActivityTypeSelElem { get { return new SelectElement(this.FindElement(Bys.EnterACPDActivityPage.ActivityTypeSelElem)); } }

        public IWebElement TooManyResultsLbl { get { return this.FindElement(Bys.EnterACPDActivityPage.TooManyResultsLbl); } }

        public IWebElement ProgramTitleCertifiedAssessmentTxt { get { return this.FindElement(Bys.EnterACPDActivityPage.ProgramTitleCertifiedAssessmentTxt); } }

        public IWebElement ArticleDrpDn { get { return this.FindElement(Bys.EnterACPDActivityPage.ArticleDrpDn); } }
        public IWebElement AntibioticArticle { get { return this.FindElement(Bys.EnterACPDActivityPage.AntibioticArticle); } }


        //for the Certified Group Learning Page
        public IWebElement ProgramTitleCertifiedGroupLearningTxt { get { return this.FindElement(Bys.EnterACPDActivityPage.ProgramTitleCertifiedGroupLearningTxt); } }
        public SelectElement ProvinceSelectorCertifiedGroupLearningDrpDn { get { return new SelectElement(this.FindElement(Bys.EnterACPDActivityPage.ProvinceSelectorCertifiedGroupLearningDrpDn)); } }
        public IWebElement CityTxtCertifiedGroupLearningTxt { get { return this.FindElement(Bys.EnterACPDActivityPage.CityTxtCertifiedGroupLearningTxt); } }
        public IWebElement PlanningOrganizationCertifiedGroupLearningTxt { get { return this.FindElement(Bys.EnterACPDActivityPage.PlanningOrganizationCertifiedGroupLearningTxt); } }
        public IWebElement ActivityStartDateCertifiedGroupLearningTxt { get { return this.FindElement(Bys.EnterACPDActivityPage.ActivityStartDateCertifiedGroupLearningTxt); } }
        public IWebElement ActivityCompletionDateCertifiedGroupLearningTxt { get { return this.FindElement(Bys.EnterACPDActivityPage.ActivityCompletionDateCertifiedGroupLearningTxt); } }
        public IWebElement CreditsClaimedDateCertifiedGroupLearningTxt { get { return this.FindElement(Bys.EnterACPDActivityPage.CreditsClaimedDateCertifiedGroupLearningTxt); } }
        public IWebElement ChangedImprovedCertifiedGroupLearningRdo { get { return this.FindElement(Bys.EnterACPDActivityPage.ChangedImprovedCertifiedGroupLearningRdo); } }
        public IWebElement LearnedNewCertifiedGroupLearningRdo { get { return this.FindElement(Bys.EnterACPDActivityPage.LearnedNewCertifiedGroupLearningRdo); } }

        public IWebElement LearnedMoreCertifiedGroupLearningRdo { get { return this.FindElement(Bys.EnterACPDActivityPage.LearnedMoreCertifiedGroupLearningRdo); } }
        public IWebElement  ConfirmedCertifiedGroupLearningRdo { get { return this.FindElement(Bys.EnterACPDActivityPage.ConfirmedCertifiedGroupLearningRdo); } }
        public IWebElement BiasedCertifiedGroupLearningRdo  { get { return this.FindElement(Bys.EnterACPDActivityPage.BiasedCertifiedGroupLearningRdo); } }

        public IWebElement DissatisfiedCertifiedGroupLearningRdo  { get { return this.FindElement(Bys.EnterACPDActivityPage.DissatisfiedCertifiedGroupLearningRdo); } }

        //for the Certified SelfLearning Page
        public IWebElement ProgramTitleCertifiedSelfLearningTxt { get { return this.FindElement(Bys.EnterACPDActivityPage.ProgramTitleCertifiedSelfLearningTxt); } }
        public SelectElement ProvinceSelectorCertifiedSelfLearningDrpDn { get { return new SelectElement(this.FindElement(Bys.EnterACPDActivityPage.ProvinceSelectorCertifiedSelfLearningDrpDn)); } }
        public IWebElement CityTxtCertifiedSelfLearningTxt { get { return this.FindElement(Bys.EnterACPDActivityPage.CityTxtCertifiedSelfLearningTxt); } }
        public IWebElement PlanningOrganizationCertifiedSelfLearningTxt { get { return this.FindElement(Bys.EnterACPDActivityPage.PlanningOrganizationCertifiedSelfLearningTxt); } }
        public IWebElement ActivityStartDateCertifiedSelfLearningTxt { get { return this.FindElement(Bys.EnterACPDActivityPage.ActivityStartDateCertifiedSelfLearningTxt); } }
        public IWebElement ActivityCompletionDateCertifiedSelfLearningTxt { get { return this.FindElement(Bys.EnterACPDActivityPage.ActivityCompletionDateCertifiedSelfLearningTxt); } }
        public IWebElement CreditsClaimedDateCertifiedSelfLearningTxt { get { return this.FindElement(Bys.EnterACPDActivityPage.CreditsClaimedDateCertifiedSelfLearningTxt); } }
        public IWebElement ChangedImprovedCertifiedGSelfLearningRdo { get { return this.FindElement(Bys.EnterACPDActivityPage.ChangedImprovedCertifiedSelfLearningRdo); } }
        public IWebElement LearnedNewCertifiedSelfLearningRdo { get { return this.FindElement(Bys.EnterACPDActivityPage.LearnedNewCertifiedSelfLearningRdo); } }

        public IWebElement LearnedMoreCertifiedSelfLearningRdo { get { return this.FindElement(Bys.EnterACPDActivityPage.LearnedMoreCertifiedSelfLearningRdo); } }
        public IWebElement ConfirmedCertifiedSelfLearningRdo { get { return this.FindElement(Bys.EnterACPDActivityPage.ConfirmedCertifiedSelfLearningRdo); } }
        public IWebElement BiasedCertifiedSelfLearningRdo { get { return this.FindElement(Bys.EnterACPDActivityPage.BiasedCertifiedSelfLearningRdo); } }

        public IWebElement DissatisfiedCertifiedSelfLearningRdo { get { return this.FindElement(Bys.EnterACPDActivityPage.DissatisfiedCertifiedSelfLearningRdo); } }


        public IWebElement ContinueBtn { get { return this.FindElement(Bys.EnterACPDActivityPage.ContinueBtn); } }

        public SelectElement ProvinceSelectorDrpDn { get { return new SelectElement(this.WaitForElement(Bys.EnterACPDActivityPage.ProvinceSelectorDrpDn)); } }

        public IWebElement CityTxt { get { return this.FindElement(Bys.EnterACPDActivityPage.CityTxt); } }
        public IWebElement PlanningOrganizationTxt { get { return this.FindElement(Bys.EnterACPDActivityPage.PlanningOrganizationTxt); } }
    

        public IWebElement ActivityStartDateTxt { get { return this.FindElement(Bys.EnterACPDActivityPage.ActivityStartDateTxt); } }
        public IWebElement ActivityCompletionDateTxt { get { return this.FindElement(Bys.EnterACPDActivityPage.ActivityCompletionDateTxt); } }
        //radio buttons on page
        public IWebElement ChangedImprovedRdo { get { return this.FindElement(Bys.EnterACPDActivityPage.ChangedImprovedRdo); } }
        public IWebElement LearnedNewRdo { get { return this.FindElement(Bys.EnterACPDActivityPage.LearnedNewRdo); } }
        public IWebElement LearnMoreRdo { get { return this.FindElement(Bys.EnterACPDActivityPage.LearnMoreRdo); } }
        public IWebElement ConfirmedRdo { get { return this.FindElement(Bys.EnterACPDActivityPage.ConfirmedRdo); } }
        public IWebElement BiasedRdo { get { return this.FindElement(Bys.EnterACPDActivityPage.BiasedRdo); } }
        public IWebElement DissatisfiedRdo { get { return this.FindElement(Bys.EnterACPDActivityPage.DissatisfiedRdo); } }

        public IWebElement SubmitButton { get { return this.FindElement(Bys.EnterACPDActivityPage.SubmitButton); } }

        //elements for the AMA Activity Page(Self Learning)
        public IWebElement ProgramTitleAMASLTxt { get { return this.FindElement(Bys.EnterACPDActivityPage.ProgramTitleAMASLTxt); } }
        public SelectElement ProvinceSelectorAMASLDrpDn { get { return new SelectElement (this.FindElement(Bys.EnterACPDActivityPage.ProvinceSelectorAMASLDrpDn)); } }
        public IWebElement CityAMASLTxt { get { return this.FindElement(Bys.EnterACPDActivityPage.CityAMASLTxt); } }
        public IWebElement PlanningOrganizationAMASLTxt { get { return this.FindElement(Bys.EnterACPDActivityPage.PlanningOrganizationAMASLTxt); } }

        public IWebElement ActivityStartDateAMASLTxt { get { return this.FindElement(Bys.EnterACPDActivityPage.ActivityStartDateAMASLTxt); } }
        public IWebElement ActivityCompletionDateAMASLTxt { get { return this.FindElement(Bys.EnterACPDActivityPage.ActivityCompletionDateAMASLTxt); } }
        public IWebElement CreditsClaimedAMASLTxt { get { return this.FindElement(Bys.EnterACPDActivityPage.CreditsClaimedAMASLTxt); } }
        public IWebElement LearnedNewAMASLRdo { get { return this.FindElement(Bys.EnterACPDActivityPage.LearnedNewAMASLRdo); } }

        public IWebElement LearnedMoreAMASLRdo { get { return this.FindElement(Bys.EnterACPDActivityPage.LearnedMoreAMASLRdo); } }
        public IWebElement ConfirmedAMASLRdo { get { return this.FindElement(Bys.EnterACPDActivityPage.ConfirmedAMASLRdo); } }
        public IWebElement BiasedAMASLRdo { get { return this.FindElement(Bys.EnterACPDActivityPage.BiasedAMASLRdo); } }
        public IWebElement DissatisfiedAMASLRdo { get { return this.FindElement(Bys.EnterACPDActivityPage.DissatisfiedAMASLRdo); } }
        public IWebElement ChangedImprovedAMASLRdo { get { return this.FindElement(Bys.EnterACPDActivityPage.ChangedImprovedAMASLRdo); } }

        //elements for the AMA Activity Page(Group Learning)
        public IWebElement ProgramTitleAMAGLTxt { get { return this.FindElement(Bys.EnterACPDActivityPage.ProgramTitleAMAGLTxt); } }
        public SelectElement ProvinceSelectorAMAGLDrpDn { get { return new SelectElement(this.FindElement(Bys.EnterACPDActivityPage.ProvinceSelectorAMAGLDrpDn)); } }
        public IWebElement CityAMAGLTxt { get { return this.FindElement(Bys.EnterACPDActivityPage.CityAMAGLTxt); } }
        public IWebElement PlanningOrganizationAMAGLTxt { get { return this.FindElement(Bys.EnterACPDActivityPage.PlanningOrganizationAMAGLTxt); } }

        public IWebElement ActivityStartDateAMAGLTxt { get { return this.FindElement(Bys.EnterACPDActivityPage.ActivityStartDateAMAGLTxt); } }
        public IWebElement ActivityCompletionDateAMAGLTxt { get { return this.FindElement(Bys.EnterACPDActivityPage.ActivityCompletionDateAMAGLTxt); } }
        public IWebElement CreditsClaimedAMAGLTxt { get { return this.FindElement(Bys.EnterACPDActivityPage.CreditsClaimedAMAGLTxt); } }
        public IWebElement LearnedNewAMAGLRdo { get { return this.FindElement(Bys.EnterACPDActivityPage.LearnedNewAMAGLRdo); } }

        public IWebElement LearnedMoreAMAGLRdo { get { return this.FindElement(Bys.EnterACPDActivityPage.LearnedMoreAMAGLRdo); } }
        public IWebElement ConfirmedAMAGLRdo { get { return this.FindElement(Bys.EnterACPDActivityPage.ConfirmedAMAGLRdo); } }
        public IWebElement BiasedAMAGLRdo { get { return this.FindElement(Bys.EnterACPDActivityPage.BiasedAMAGLRdo); } }
        public IWebElement DissatisfiedAMAGLRdo { get { return this.FindElement(Bys.EnterACPDActivityPage.DissatisfiedAMAGLRdo); } }
        public IWebElement ChangedImprovedAMAGLRdo { get { return this.FindElement(Bys.EnterACPDActivityPage.ChangedImprovedAMAGLRdo); } }

        //for the enter an article page
        public IWebElement ArticleDescriptionRdo { get { return this.FindElement(Bys.EnterACPDActivityPage.ArticleDescriptionRdo); } }
        public IWebElement ActivityCompletionDateArticleTxt { get { return this.FindElement(Bys.EnterACPDActivityPage.ActivityCompletionDateArticleTxt); } }
        public IWebElement ActivityStartDateArticleTxt { get { return this.FindElement(Bys.EnterACPDActivityPage.ActivityStartDateArticleTxt); } }

        public IWebElement ArticleCreditsRequestedTxt { get { return this.FindElement(Bys.EnterACPDActivityPage.ArticleCreditsRequestedTxt); } }



        public IWebElement CreditsClaimedTxt { get { return this.FindElement(Bys.EnterACPDActivityPage.CreditsClaimedTxt); } }

        public IWebElement NoResultsLbl { get { return this.FindElement(Bys.EnterACPDActivityPage.NoResultsLbl); } }

        public IWebElement ActivityTypeDrpDn { get { return this.FindElement(Bys.EnterACPDActivityPage.ActivityTypeDrpDn); } }
        public IWebElement LiveInPersonBtn { get { return this.FindElement(Bys.EnterACPDActivityPage.LiveInPersonBtn); } }

        public IWebElement LiveInPersonRdoBtn { get { return this.FindElement(Bys.EnterACPDActivityPage.LiveInPersonRdoBtn); } }

        public IWebElement ProgramActivityTitleTxt { get { return this.FindElement(Bys.EnterACPDActivityPage.ProgramActivityTitleTxt); } }

        public IWebElement AdvancedSearchBtn { get { return this.FindElement(Bys.EnterACPDActivityPage.AdvancedSearchBtn); } }

        public IWebElement AdvancedSearchNoResultsTxt { get { return this.FindElement(Bys.EnterACPDActivityPage.AdvancedSearchNoResultsTxt); } }


        //buttons for the popup
        public IWebElement PopupSubmitBtn { get { return this.FindElement(Bys.EnterACPDActivityPage.PopupSubmitBtn); } }

        //element for the AMA popup submit
        public IWebElement AMAPopupSubmitBtn { get { return this.FindElement(Bys.EnterACPDActivityPage.AMAPopupSubmitBtn); } }
        #endregion elements

        #region methods: per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(15), Criteria.EnterACPDActivityPage.PageReady);
        }
        public void Wait(int time)
        {
            System.Threading.Thread.Sleep(time);
        }
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            try { activeRequests.Clear(); }
            catch (Exception ex) { _log.ErrorFormat("Failed to dispose DashboardPge", activeRequests.Count, ex); }
        }

        #endregion methods: per page

        #region methods: page specific
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dropDownElem"> This is the dropdown element to click</param>
        /// <param name="textOfItem">The exact text of the item. If the item is located in a <b> tag, only send the bolded text of the item</b></param>
        public void SelectItemInCustomDropDown(IWebElement dropDownElem, string textOfItem)
        {
            IWebElement itemToSelectInDropdown = null;

        
            ElemSet.ScrollToElement(Browser, dropDownElem);

            dropDownElem.Click();
            Thread.Sleep(1500);
            string xpathStringForSpanTag = string.Format("//span[text()='{0}']", textOfItem);
            string xpathStringForBTag = string.Format("//b[text()='{0}']", textOfItem);

            if (Browser.FindElements(By.XPath(xpathStringForSpanTag)).Count > 0)
            {
                itemToSelectInDropdown = Browser.FindElement(By.XPath(xpathStringForSpanTag));

            }
            else if (Browser.FindElements(By.XPath(xpathStringForBTag)).Count > 0) {

                itemToSelectInDropdown = Browser.FindElement(By.XPath(xpathStringForBTag));
            }
            
            

            itemToSelectInDropdown.Click();

        }


        /// <summary>
        /// Clicks the user-specified element and then waits for a window to close or open, or a page to load,
        /// depending on the element that was clicked
        /// </summary>
        /// <param name="buttonOrLinkElem">The element to click on</param>
        public void ClickToAdvance(IWebElement elemToClick)
        {
            if (Browser.Exists(Bys.EnterACPDActivityPage.LiveInPersonRdoBtn))
            {
                if (elemToClick.GetAttribute("id") == LiveInPersonRdoBtn.GetAttribute("id"))
                {
                    LiveInPersonRdoBtn.Click();
                    this.WaitUntilAny(Criteria.EnterACPDActivityPage.ProgramActivityTitleTextEnabled, Criteria.EnterACPDActivityPage.ContinueBtnExists);  
                    return;
                }
            }

            if (Browser.Exists(Bys.EnterACPDActivityPage.PopupSubmitBtn))
            {
                if (elemToClick.GetAttribute("id") == PopupSubmitBtn.GetAttribute("id"))
                {
                    Browser.ExecuteScript("arguments[0].click();", PopupSubmitBtn);

                    int x = 0;
                    Browser.WaitForElement(Bys.CPDActivitiesListPage.MyCPDActivitieslbl, ElementCriteria.IsVisible); 
                    return;
                }
            }




            else
            {
                throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, or if the button is already added, then the page you were on did not contain the button.");
            }
        }

        /// <summary>
        /// Fills the required fields on the enter a CPD activity page
        /// </summary>
        /// <param name="category">The exact text that is bolded for each option</param>
        /// <param name="certType"></param>
        /// <param name="activityType">The exact text from the Actiivyt type dropdown</param>
        public void FillEnterACPDActivityForm(string category, string certType, string activityType)
        {
            // Click the Category drop, then locate the user-specified list item, then click it
            SelectItemInCustomDropDown(CategoryDrpDn,category);

            // MIKE: Since clicking on this radio button requires waiting, lets put this waiting at a lower level, so that we dont have to
            // add multiple thread.sleeps throughout our code everytime we click it. I created a method, comment your code, and called this method
            SelectCertTypeRadioButton(certType);                                                          
                                               
            SelectItemInCustomDropDown(ActivityTypeDrpDn, activityType);
            Thread.Sleep(2000);

            // MIKE: We can remove this right?
            //scroll down in selenium


        }

        /// <summary>
        /// Clicks on either Certification Type radio button, then waits a second
        /// </summary>
        /// <param name="certType"></param>
        public void SelectCertTypeRadioButton(string certType)
        {
            ElemSet.RdoBtn_ClickByText(Browser, certType);
            Thread.Sleep(2000);
        }


            /// <summary>
            /// Fills the required fields on the enter a CPD activity page
            /// </summary>
            /// 
            public void FillOutAssessmentForm()
            {
            //generate the data to fill out the data
            DateTime dt = DateTime.Now;
            int currentDay = dt.Day;
            int currentMonth = dt.Month;
            int currentYear = dt.Year;
            int currentHour = dt.Hour;
            int currentMinute = dt.Minute;
            int currentSecond = dt.Second;
                                                                
            //create a string for the program title
            String ProgramTitle = "TestRun_" + currentMonth + "_" + currentDay + "_" + currentYear + "-" + currentHour + ":" + currentMinute + ":" + currentSecond;

            Thread.Sleep(2000);
            ProgramTitleCertifiedAssessmentTxt.SendKeys(ProgramTitle);
                                                                               
            //move down to the province selection screen
            IWebElement element = Browser.FindElement(By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl17_CEComboBox3449685"));   // Put this element in the page bys class
            ElemSet.ScrollToElement(Browser,element);
                                                                    // MIKE: Removed a bunch of end lines
            //select alberta
            ProvinceSelectorDrpDn.SelectByIndex(1);
            ProvinceSelectorDrpDn.SelectByText("Alberta (AB)");       // MIKE: I added this line and commented the one above it. Why use index when you know which item you are going to choose? The index might change in the future if more items are added to the select list

            //Fill out the test city
            CityTxt.SendKeys("Test City");
            //fill out the
            PlanningOrganizationTxt.SendKeys("Test");

            //generate a date for these Activity Test
            string date = DateTime.Today.AddDays(-1).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);  // MIKE: I added this line and commented out the below lines. Reduces lines and having seperate varaibles for the start and end doesnt make sense in this case
            //DateTime dt2 = DateTime.Today.AddDays(-1);                                            
            //String startDateText = dt2.Month + "/"  + dt2.Day + "/" + dt2.Year;
            //String completionDateText = dt2.Month + "/" + dt2.Day + "/" + dt2.Year;

            //send the text to the box
            ActivityStartDateTxt.SendKeys(date);
            ActivityCompletionDateTxt.SendKeys(date);

            CreditsClaimedTxt.SendKeys("1");

            ChangedImprovedRdo.Click();
            LearnedNewRdo.Click();
            LearnMoreRdo.Click();

            //scroll to the dissatisfied radio button
           // ElemSet.ScrollToElement(Browser,DissatisfiedRdo);
            ElemSet.ClickAfterScroll(Browser, DissatisfiedRdo);    // MIKE: I added this line and commented the above one. We can talk at meeting

            BiasedRdo.Click();

            //scroll to element
            ElemSet.ClickAfterScroll(Browser, ConfirmedRdo);   // MIKE: I would name these elements with a little more detail. It took me, a new person, a little bit of time to figure out what radio button was for each variable
                                                               // MIKE: Always start with the beginning text when naming elements. For example for this radio button, name it "ThisExperienceConfirmedRdo" instead of just "Confirmed"
                                                               
            // For some reason, whenever we use Selenium's built in click method here, it triggers the application to add more than
            // 1 credit for the activity/user (We entered "1" into the CreditsClaimed text box above, so only 1 credit should get
            // added for the user). I added a workaround to use the javascript version of a click, and this 
            // works (only adds the specified amount of credits) For more info, 
            // see https://stackoverflow.com/questions/24571048/selenium-webelement-click-vs-javascript-click-event
            JavascriptUtils.Click(Browser, SubmitButton);
            SubmitButton.SendKeys(Keys.Tab);


            Thread.Sleep(20000);     // MIKE: Definitely add wait criteria here. I see that a popup appears, we can wait on an element in this popup
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public void FillOutAndSubmitAssessmentForm()
        {
            FillOutAssessmentForm();

            ClickToAdvance(PopupSubmitBtn);

        }
        /// <summary>
        /// 
        /// </summary>
        /// 
        public void FillOutAndSubmitGroupLearningForm()
        {
            FillOutGroupLearningForm();

            ClickToAdvance(PopupSubmitBtn);

        }
        /// <summary>
        /// 
        /// </summary>
        /// 
        public void FillOutAndSubmitSelfLearningForm()
        {
            FillOutSelfLearningForm();

            ClickToAdvance(PopupSubmitBtn);

        }

        /// <summary>
        /// Fills the required fields on the AMA Self Learning Page
        /// </summary>
        /// 
        public void FillOutAMAActivityForm1(int creditValue)
        {
            //generate the data to fill out the data
            DateTime dt = DateTime.Now;
            int currentDay = dt.Day;
            int currentMonth = dt.Month;
            int currentYear = dt.Year;
            int currentHour = dt.Hour;
            int currentMinute = dt.Minute;
            int currentSecond = dt.Second;

            //create a string for the program title
            String ProgramTitle = "TestRun_" + currentMonth + "_" + currentDay + "_" + currentYear + "-" + currentHour + ":" + currentMinute + ":" + currentSecond;

           
            //These lines fill out the
            //text fields in the AMA Self Learning Activity Form

            ProgramTitleAMASLTxt.SendKeys(ProgramTitle);
            ProvinceSelectorAMASLDrpDn.SelectByIndex(1);
            ProvinceSelectorAMASLDrpDn.SelectByText("Alberta (AB)");  
            CityAMASLTxt.SendKeys("Test City");
            PlanningOrganizationAMASLTxt.SendKeys("Test");
            string date = DateTime.Today.AddDays(-1).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);  //Generate a date to use later in the form
            ElemSet.ScrollToElement(Browser,ActivityStartDateAMASLTxt);
            ActivityStartDateAMASLTxt.SendKeys(date);
            ActivityCompletionDateAMASLTxt.SendKeys(date);
            CreditsClaimedAMASLTxt.SendKeys("" + creditValue);



            //clicking on the radio buttons
            //at the end of the form
            ElemSet.ClickAfterScroll(Browser, ChangedImprovedAMASLRdo);
            ElemSet.ClickAfterScroll(Browser, LearnedNewAMASLRdo);
            ElemSet.ClickAfterScroll(Browser, LearnedMoreAMASLRdo);
            ElemSet.ClickAfterScroll(Browser, DissatisfiedAMASLRdo);
            ElemSet.ClickAfterScroll(Browser, BiasedAMASLRdo);
            ElemSet.ClickAfterScroll(Browser, ConfirmedAMASLRdo);   

            // For some reason, whenever we use Selenium's built in click method here, it triggers the application to add more than
            // 1 credit for the activity/user (We entered "1" into the CreditsClaimed text box above, so only 1 credit should get
            // added for the user). I added a workaround to use the javascript version of a click, and this 
            // works (only adds the specified amount of credits) For more info, 
            // see https://stackoverflow.com/questions/24571048/selenium-webelement-click-vs-javascript-click-event
            JavascriptUtils.Click(Browser, SubmitButton);
            SubmitButton.SendKeys(Keys.Tab);

            Thread.Sleep(20000);     // MIKE: Definitely add wait criteria here. I see that a popup appears, we can wait on an element in this popup

        }

        public void FillOutAndSubmitAMAGLForm()
        {
            FillOutAMAActivityForm2();

            int x = 0;
            Browser.WaitForElement(Bys.EnterACPDActivityPage.PopupSubmitBtn, ElementCriteria.IsVisible); ;
            ClickToAdvance(PopupSubmitBtn);

        }


        /// <summary>
        /// Fills the required fields on the AMA Group Learning Page
        /// </summary>
        /// 
        public void FillOutAMAActivityForm2()
        {
            //generate the data to fill out the data
            DateTime dt = DateTime.Now;
            int currentDay = dt.Day;
            int currentMonth = dt.Month;
            int currentYear = dt.Year;
            int currentHour = dt.Hour;
            int currentMinute = dt.Minute;
            int currentSecond = dt.Second;

            //create a string for the program title
            String ProgramTitle = "TestRun_" + currentMonth + "_" + currentDay + "_" + currentYear + "-" + currentHour + ":" + currentMinute + ":" + currentSecond;


            //These lines fill out the
            //text fields in the AMA Group Learning Activity Form

            ProgramTitleAMAGLTxt.SendKeys(ProgramTitle);
            ProvinceSelectorAMAGLDrpDn.SelectByIndex(1);
            ProvinceSelectorAMAGLDrpDn.SelectByText("Alberta (AB)");
            CityAMAGLTxt.SendKeys("Test City");
            PlanningOrganizationAMAGLTxt.SendKeys("Test");
            string date = DateTime.Today.AddDays(-1).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);  //generate a date value for the activity forms
            ElemSet.ScrollToElement(Browser, ActivityStartDateAMAGLTxt);
            ActivityStartDateAMAGLTxt.SendKeys(date);
            ActivityCompletionDateAMAGLTxt.SendKeys(date);
            CreditsClaimedAMAGLTxt.SendKeys("1");

            //Select out the group radio buttons
            ElemSet.ClickAfterScroll(Browser, ChangedImprovedAMAGLRdo);
            ElemSet.ClickAfterScroll(Browser, LearnedNewAMAGLRdo);
            ElemSet.ClickAfterScroll(Browser, LearnedMoreAMAGLRdo);
            ElemSet.ClickAfterScroll(Browser, DissatisfiedAMAGLRdo);
            ElemSet.ClickAfterScroll(Browser, BiasedAMAGLRdo);
            ElemSet.ClickAfterScroll(Browser, ConfirmedAMAGLRdo);



            // MIKE: I would name these elements with a little more detail. It took me, a new person, a little bit of time to figure out what radio button was for each variable
            // MIKE: Always start with the beginning text when naming elements. For example for this radio button, name it "ThisExperienceConfirmedRdo" instead of just "Confirmed"

            // For some reason, whenever we use Selenium's built in click method here, it triggers the application to add more than
            // 1 credit for the activity/user (We entered "1" into the CreditsClaimed text box above, so only 1 credit should get
            // added for the user). I added a workaround to use the javascript version of a click, and this 
            // works (only adds the specified amount of credits) For more info, 
            // see https://stackoverflow.com/questions/24571048/selenium-webelement-click-vs-javascript-click-event
            JavascriptUtils.Click(Browser, SubmitButton);
            SubmitButton.SendKeys(Keys.Tab);

            
        }



        /// <summary>
        /// Fills the required fields on the enter a CPD activity page
        /// </summary>
        /// 
        public void FillOutGroupLearningForm()
        {
            //generate the data to fill out the data

            DateTime dt = DateTime.Now;
            int currentDay = dt.Day;
            int currentMonth = dt.Month;
            int currentYear = dt.Year;
            int currentHour = dt.Hour;
            int currentMinute = dt.Minute;
            int currentSecond = dt.Second;


            //create a string for the program title
            String ProgramTitle = "TestRun_" + currentMonth + "_" + currentDay + "_" + currentYear + "-" + currentHour + ":" + currentMinute + ":" + currentSecond;
            Thread.Sleep(5000);//add wait criteria eventually
            ProgramTitleCertifiedGroupLearningTxt.SendKeys(ProgramTitle);




            //move down to the province selection screen
            IWebElement element = Browser.FindElement(By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl17_CEComboBox3449688"));
            ElemSet.ScrollToElement(Browser,element);



            //select alberta
            ProvinceSelectorCertifiedGroupLearningDrpDn.SelectByIndex(1);
            //Fill out the test city
            CityTxtCertifiedGroupLearningTxt.SendKeys("Test City");
            //fill out the
            PlanningOrganizationCertifiedGroupLearningTxt.SendKeys("Test");

            //generate a date for these Activity Test
            DateTime dt2 = DateTime.Today.AddDays(-1);
            String startDateText = dt2.Month + "/" + dt2.Day + "/" + dt2.Year;
            String completionDateText = dt2.Month + "/" + dt2.Day + "/" + dt2.Year;

            //send the text to the box
            ActivityStartDateCertifiedGroupLearningTxt.SendKeys(startDateText);
            ActivityCompletionDateCertifiedGroupLearningTxt.SendKeys(completionDateText);

            CreditsClaimedDateCertifiedGroupLearningTxt.SendKeys("1");

            ChangedImprovedCertifiedGroupLearningRdo.Click();
            LearnedNewCertifiedGroupLearningRdo.Click();
            LearnedMoreCertifiedGroupLearningRdo.Click();

            //scroll to the dissatisfied radio button
            ElemSet.ScrollToElement(Browser, ConfirmedCertifiedGroupLearningRdo);

            ConfirmedCertifiedGroupLearningRdo.Click();
            BiasedCertifiedGroupLearningRdo.Click();

            //scroll to element
            ElemSet.ScrollToElement(Browser, DissatisfiedCertifiedGroupLearningRdo);
            DissatisfiedCertifiedGroupLearningRdo.Click();


            // For some reason, whenever we use Selenium's built in click method here, it triggers the application to add more than
            // 1 credit for the activity/user (We entered "1" into the CreditsClaimed text box above, so only 1 credit should get
            // added for the user). I added a workaround to use the javascript version of a click, and this 
            // works (only adds the specified amount of credits) For more info, 
            // see https://stackoverflow.com/questions/24571048/selenium-webelement-click-vs-javascript-click-event         
            JavascriptUtils.Click(Browser, SubmitButton);


            Thread.Sleep(20000);
        }


        /// <summary>
        /// Fills out the Self page
        /// </summary>
        /// 

        public void FillOutSelfLearningForm()
        {
            //generate the data to fill out the data

            DateTime dt = DateTime.Now;
            int currentDay = dt.Day;
            int currentMonth = dt.Month;
            int currentYear = dt.Year;
            int currentHour = dt.Hour;
            int currentMinute = dt.Minute;
            int currentSecond = dt.Second;


            //create a string for the program title
            String ProgramTitle = "TestRun_" + currentMonth + "_" + currentDay + "_" + currentYear + "-" + currentHour + ":" + currentMinute + ":" + currentSecond;

            Thread.Sleep(3000);//add wait criteria later
            ElemSet.ScrollToElement(Browser,ProgramTitleCertifiedSelfLearningTxt);
            ProgramTitleCertifiedSelfLearningTxt.SendKeys(ProgramTitle);


            //move down to the province selection screen

            ElemSet.ScrollToElement(Browser, CityTxtCertifiedSelfLearningTxt);


            //select alberta
            ProvinceSelectorCertifiedSelfLearningDrpDn.SelectByIndex(1);
            //Fill out the test city
            CityTxtCertifiedSelfLearningTxt.SendKeys("Test City");
            //fill out the
            PlanningOrganizationCertifiedSelfLearningTxt.SendKeys("Test");

            //generate a date for these Activity Test
            DateTime dt2 = DateTime.Today.AddDays(-1);
            String startDateText = dt2.Month + "/" + dt2.Day + "/" + dt2.Year;
            String completionDateText = dt2.Month + "/" + dt2.Day + "/" + dt2.Year;


            ActivityStartDateCertifiedSelfLearningTxt.SendKeys(startDateText);
            ActivityCompletionDateCertifiedSelfLearningTxt.SendKeys(completionDateText);

            CreditsClaimedDateCertifiedSelfLearningTxt.SendKeys("1");



            ElemSet.ClickAfterScroll(Browser, ChangedImprovedCertifiedGSelfLearningRdo);
            ElemSet.ClickAfterScroll(Browser, LearnedNewCertifiedSelfLearningRdo);
            ElemSet.ClickAfterScroll(Browser,LearnedMoreCertifiedSelfLearningRdo);
            ElemSet.ClickAfterScroll(Browser,ConfirmedCertifiedSelfLearningRdo);
            ElemSet.ClickAfterScroll(Browser,BiasedCertifiedSelfLearningRdo);
            ElemSet.ClickAfterScroll(Browser, DissatisfiedCertifiedSelfLearningRdo);
         

            ElemSet.ScrollToElement(Browser,SubmitButton);

            // For some reason, whenever we use Selenium's built in click method here, it triggers the application to add more than
            // 1 credit for the activity/user (We entered "1" into the CreditsClaimed text box above, so only 1 credit should get
            // added for the user). I added a workaround to use the javascript version of a click, and this 
            // works (only adds the specified amount of credits) For more info, 
            // see https://stackoverflow.com/questions/24571048/selenium-webelement-click-vs-javascript-click-event
            JavascriptUtils.Click(Browser, SubmitButton);
            SubmitButton.SendKeys(Keys.Tab);

            Thread.Sleep(20000);
        }






    }

    /// <summary>
    /// Clicks the user-specified button or link and then waits for a window to close or open, or a page to load,
    /// depending on the button that was clicked
    /// </summary>
    /// <param name="buttonOrLinkElem">The element to click on</param>
    //public dynamic ClickToAdvance(IWebElement buttonOrLinkElem)
    //{
    //    if (Browser.Exists(Bys.DashboardPage.EnterCPDActBtn))
    //    {
    //        if (buttonOrLinkElem.GetAttribute("id") == EnterCPDActBtn.GetAttribute("id"))
    //        {
    //            EnterCPDActBtn.Click();

    //            return true;
    //        }
    //    }

    //    else
    //    {
    //        throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, or if the button is already added, then the page you were on did not contain the button.");
    //    }

    //    return null;
    //}

    #endregion methods: page specific


}


