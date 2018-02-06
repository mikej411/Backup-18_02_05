﻿using Browser.Core.Framework;
using Browser.Core.Framework.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace RCP.AppFramework
{
    public class CBDProgDirectorPage : RCPPage, IDisposable
    {
        #region constructors
        public CBDProgDirectorPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that we start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "/cbd#/programadministrator"; } }

        #endregion properties

        #region elements   
        public IWebElement CBDTab { get { return this.FindElement(Bys.RCPPage.CBDTab); } }
        public IWebElement AddSupportingDocumentationFormBrowseBtn { get { return this.FindElement(Bys.CBDProgDirectorPage.AddSupportingDocumentationFormBrowseBtn); } }      
        public IWebElement AddRemoveFlagFormRemoveFlagBtn { get { return this.FindElement(Bys.CBDProgDirectorPage.AddRemoveFlagFormRemoveFlagBtn); } }
        public IWebElement AddRemoveFlagFormSaveFlagBtn { get { return this.FindElement(Bys.CBDProgDirectorPage.AddRemoveFlagFormSaveFlagBtn); } }
        public IWebElement AddRemoveFlagFormReasonTxt { get { return this.FindElement(Bys.CBDProgDirectorPage.AddRemoveFlagFormReasonTxt); } }
        public IWebElement SchedProgMeetFormRecurChk { get { return this.FindElement(Bys.CBDProgDirectorPage.SchedProgMeetFormRecurChk); } }
        public SelectElement SchedProgMeetFormRecurringSelElem { get { return new SelectElement(this.WaitForElement(Bys.CBDProgDirectorPage.SchedProgMeetFormRecurringSelElem)); } }
        public IWebElement SchedProgMeetFormSubjectTxt { get { return this.FindElement(Bys.CBDProgDirectorPage.SchedProgMeetFormSubjectTxt); } }
        public IWebElement SchedProgMeetFormScheduleBtn { get { return this.FindElement(Bys.CBDProgDirectorPage.SchedProgMeetFormScheduleBtn); } }
        public IWebElement AddNotesFormSubjectTxt { get { return this.FindElement(Bys.CBDProgDirectorPage.AddNotesFormSubjectTxt); } }
        public IWebElement AddNotesFormNotesTxt { get { return this.FindElement(Bys.CBDProgDirectorPage.AddNotesFormNotesTxt); } }
        public IWebElement AddNotesFormSubmitBtn { get { return this.FindElement(Bys.CBDProgDirectorPage.AddNotesFormSubmitBtn); } }
        public SelectElement SetStatusFormActionsSelElem { get { return new SelectElement(this.WaitForElement(Bys.CBDProgDirectorPage.SetStatusFormActionsSelElem)); } }
        public SelectElement SetStatusFormLearnerStatusSelElem { get { return new SelectElement(this.WaitForElement(Bys.CBDProgDirectorPage.SetStatusFormLearnerStatusSelElem)); } }
        public IWebElement SetStatusFormConfirmBtn { get { return this.FindElement(Bys.CBDProgDirectorPage.SetStatusFormConfirmBtn); } }
        public SelectElement ProgramSelElem { get { return new SelectElement(this.WaitForElement(Bys.CBDProgDirectorPage.ProgramSelElem)); } }
        public SelectElement FacultyOfMedSelElem { get { return new SelectElement(this.WaitForElement(Bys.CBDProgDirectorPage.FacultyOfMedSelElem)); } }
        public SelectElement MeetingAgendaSelElem { get { return new SelectElement(this.WaitForElement(Bys.CBDProgDirectorPage.MeetingAgendaSelElem)); } }
        public IWebElement CBDHomeTab { get { return this.FindElement(Bys.CBDProgDirectorPage.CBDHomeTab); } }
        public IWebElement CompCommiteeTab { get { return this.FindElement(Bys.CBDProgDirectorPage.CompCommiteeTab); } }
        public IWebElement LearnersTab { get { return this.FindElement(Bys.CBDProgDirectorPage.LearnersTab); } }
        public IWebElement ProgramOffiliatedObsTab { get { return this.FindElement(Bys.CBDProgDirectorPage.ProgramOffiliatedObsTab); } }
        public IWebElement MSFAssignmentsTab { get { return this.FindElement(Bys.CBDProgDirectorPage.MSFAssignmentsTab); } }
        public IWebElement LearnersTbl { get { return this.FindElement(Bys.CBDProgDirectorPage.LearnersTbl); } }
        public IWebElement LearnLearnersTblRowBodyersTbl { get { return this.FindElement(Bys.CBDProgDirectorPage.LearnersTblRowBody); } }
        public IWebElement AssignObsFormNextBtn { get { return this.FindElement(Bys.CBDProgDirectorPage.AssignObsFormNextBtn); } }
        public IWebElement AssignObsFormSearchBtn { get { return this.FindElement(Bys.CBDProgDirectorPage.AssignObsFormSearchBtn); } }
        public IWebElement AssignObsFormAddBtn { get { return this.FindElement(Bys.CBDProgDirectorPage.AssignObsFormAddBtn); } }
        public IWebElement AssignObsFormAssignBtn { get { return this.FindElement(Bys.CBDProgDirectorPage.AssignObsFormAssignBtn); } }
        public IWebElement AssignObsFormBackBtn { get { return this.FindElement(Bys.CBDProgDirectorPage.AssignObsFormBackBtn); } }
        public SelectElement AssignObsFormStageSelElem { get { return new SelectElement(this.WaitForElement(Bys.CBDProgDirectorPage.AssignObsFormStageSelElem)); } }
        public IWebElement AgendaTab { get { return this.FindElement(Bys.CBDProgDirectorPage.AgendaTab); } }
        public IWebElement PresentersTab { get { return this.FindElement(Bys.CBDProgDirectorPage.PresentersTab); } }
        public IWebElement CreateNewAgendaLnk { get { return this.FindElement(Bys.CBDProgDirectorPage.CreateNewAgendaLnk); } }
        public IWebElement ExportAgendaLnk { get { return this.FindElement(Bys.CBDProgDirectorPage.ExportAgendaLnk); } }
        public IWebElement ActionsBtn { get { return this.FindElement(Bys.CBDProgDirectorPage.ActionsBtn); } }
        public IWebElement FinalizeAgendaBtn { get { return this.FindElement(Bys.CBDProgDirectorPage.FinalizeAgendaBtn); } }
        public IWebElement AgendaTbl { get { return this.FindElement(Bys.CBDProgDirectorPage.AgendaTbl); } }
        public IWebElement CreateNewAgendaFormCreateBtn { get { return this.FindElement(Bys.CBDProgDirectorPage.CreateNewAgendaFormCreateBtn); } }
        public IWebElement FinalizeAgendaFormFinalizeBtn { get { return this.FindElement(Bys.CBDProgDirectorPage.FinalizeAgendaFormFinalizeBtn); } }
        public SelectElement CreateNewAgendaFormFaculMedSelElem { get { return new SelectElement(this.WaitForElement(Bys.CBDProgDirectorPage.CreateNewAgendaFormFaculMedSelElem)); } }
        public SelectElement CreateNewAgendaFormProgramSelElem { get { return new SelectElement(this.WaitForElement(Bys.CBDProgDirectorPage.CreateNewAgendaFormProgramSelElem)); } }
        public IWebElement AssignObsFormObserverNameTxt { get { return this.FindElement(Bys.CBDProgDirectorPage.AssignObsFormObserverNameTxt); } }
        public IWebElement AgendaTblRowBody { get { return this.FindElement(Bys.CBDProgDirectorPage.AgendaTblRowBody); } }
        public IWebElement AddSupportingDocumentationFormFilelocationTxt { get { return this.FindElement(Bys.CBDProgDirectorPage.AddSupportingDocumentationFormFilelocationTxt); } }
        public IWebElement AddSupportingDocumentationFormSubmitBtn { get { return this.FindElement(Bys.CBDProgDirectorPage.AddSupportingDocumentationFormSubmitBtn); } }


        #endregion elements

        #region methods: repeated per page
        public override void WaitForInitialize()
        {
            // Sometimes the full page takes forever to load (and in some cases I dont think it will ever load), so we will put a try/catch here.
            // Try for waiting for the full page load. If the full page doesnt load in time, then refresh the page and try again
            try
            {
                this.WaitUntil(TimeSpan.FromSeconds(180), Criteria.CBDProgDirectorPage.PageReady);
            }
            catch
            {
                RefreshPage();
            }
        }

        /// Refreshes the page and then uses the wait criteria that is found within WaitForInitialize to wait for the page to load.
        /// This is used as a catch block inside WaitForInitialize, in case the page doesnt load initially. Can also be used to 
        /// randomly refresh the page
        /// </summary>
        public void RefreshPage()
        {
            // Whenever I used the regular Refresh method running 6 parallel tests, this works fine. However, today (1/7/17) I have increased 
            // max parallel runs to 16 because I found a solution for Grid problem, and since I have done this, the regular Refresh method
            // causes CBD application to sometimes not refresh correctly (Some weird page gets shown in the screenshot, with a bunch of
            // text like "vm.portfolio.activityname"). So now I am going to click on the CBD tab and see if this refreshes the page without
            // that issue
            //Browser.Navigate().Refresh();
            Browser.WaitForElement(Bys.RCPPage.CBDTab, ElementCriteria.IsVisible);
            ClickAndWaitBasePage(CBDTab);
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.CBDProgDirectorPage.PageReady);
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

        #endregion methods: repeated per page

        #region methods: page specific

        #region methods: page specific: Competence Committee tab

        /// <summary>
        /// 
        /// </summary>
        /// <param name="yearsToAdd">However many years you want to add to the current date. If none, enter "0"</param>
        /// <param name="monthsToAdd">However many months you want to add to the current date. If none, enter "0"</param>
        /// <param name="daysToAdd">However many days you want to add to the current date. If none, enter "0"</param>
        public void FinalizeAllUpcomingAgendasOnSpecificDate(int yearsToAdd, int monthsToAdd, int daysToAdd)
        {
            // If we are not on the Competence Committee tab, then click on it to go there
            if (!Browser.Exists(Bys.CBDProgDirectorPage.FinalizeAgendaBtn, ElementCriteria.IsEnabled))
            {
                ClickAndWait(CompCommiteeTab);
            }

            string date = DateTime.Now.AddYears(yearsToAdd).AddMonths(monthsToAdd).AddDays(daysToAdd).ToString("MM/dd/yyyy");
            String upcomingAgendaInstance = string.Format("{0} Upcoming", date);
            int countOfUpcomingAgendasOnSpecificDate = ElemGet.SelElem_GetCountOfItemsContainingText(MeetingAgendaSelElem, "Upcoming");
            for (int i = 0; i < countOfUpcomingAgendasOnSpecificDate; i++)
            {
                ElemSet.SelElem_SelectItemContainingText(MeetingAgendaSelElem, upcomingAgendaInstance);
                ClickAndWait(FinalizeAgendaBtn);
                ClickAndWait(FinalizeAgendaFormFinalizeBtn);
            }
        }

        /// <summary>
        /// If an upcoming agenda is not created (e.g. No items in the Meeting Agenda dropdown have an agenda that says "Upcoming"), then
        /// this creates one with a user-specified date. First it will click on the Competenece Committee tab if we are not on this page, 
        /// then it opens the Create New Agenda window, assigns the date and clicks Create to create the new agenda
        /// </summary>
        /// <param name="yearsToAdd">However many years you want to add to the current date. If you want a random date, enter "-1"</param>
        /// <param name="monthsToAdd">However many months you want to add to the current date. If none, enter "0"</param>
        /// <param name="daysToAdd">However many days you want to add to the current date. If none, enter "0"</param>
        public void CreateUpcomingAgendaWithSpecificDateIfNoneExist(int yearsToAdd, int monthsToAdd, int daysToAdd)
        {
            // If we are not on the Competence Committee tab, then click on it to go there
            if (!Browser.Exists(Bys.CBDProgDirectorPage.FinalizeAgendaBtn, ElementCriteria.IsEnabled))
            {
                ClickAndWait(CompCommiteeTab);
            }

            // If none of the list<string> items contain the text "Upcoming", then create an upcoming agenda
            if (!ElemGet.SelElem_ContainsText(MeetingAgendaSelElem, "Upcoming"))
            {
                CreateUpcomingAgendaWithSpecificDate(yearsToAdd, monthsToAdd, daysToAdd);
            }
        }

        /// <summary>
        /// Creates a new agenda. First it will click on the Competenece Committee tab if we are not on this page, then it opens the Create 
        /// New Agenda window, then assigns a user-specified date and clicks Create to create the new agenda
        /// </summary>
        /// <param name="yearsToAdd">However many years you want to add to the current date. If none, enter "0"</param>
        /// <param name="monthsToAdd">However many months you want to add to the current date. If none, enter "0"</param>
        /// <param name="daysToAdd">However many days you want to add to the current date. If none, enter "0"</param>
        public void CreateUpcomingAgendaWithSpecificDate(int yearsToAdd, int monthsToAdd, int daysToAdd)
        {
            // If we are not on the Competence Committee tab, then click on it to go there
            if (!Browser.Exists(Bys.CBDProgDirectorPage.FinalizeAgendaBtn, ElementCriteria.IsEnabled))
            {
                ClickAndWait(CompCommiteeTab);
            }

            ActionsBtn.Click();
            ClickAndWait(CreateNewAgendaLnk);

            Dictionary<string, string> dates = DataUtils.GetDateForCalendarElem(DateTime.Now.AddYears(yearsToAdd).AddMonths(monthsToAdd).AddDays(daysToAdd));
            ElemSet.DatePicker_ChooseDate(Browser, dates["year"], dates["month"], dates["day"]);

            ClickAndWait(CreateNewAgendaFormCreateBtn);
        }

        /// <summary>
        /// Clicks on the Competence Committee tab if that page is not in view, then If an upcoming agenda is not created 
        /// (e.g. No items in the Meeting Agenda dropdown have an agenda that says "Upcoming"), then this creates one with
        /// a random date. It opens the Create New Agenda window, assigns the date and clicks Create to create the new agenda.
        /// It will return the string representation of the agenda in the Meeting Agenda dropdown, so you can then select it.
        /// </summary>
        public string CreateUpcomingAgendaWithRandomDateIfNoneExist()
        {
            string agendaName = "";

            // If we are not on the Competence Committee tab, then click on it to go there
            if (!Browser.Exists(Bys.CBDProgDirectorPage.FinalizeAgendaBtn, ElementCriteria.IsEnabled))
            {
                ClickAndWait(CompCommiteeTab);
            }

            // If none of the list<string> items contain the text "Upcoming", then create an upcoming agenda
            if (!ElemGet.SelElem_ContainsText(MeetingAgendaSelElem, "Upcoming"))
            {
                agendaName = CreateUpcomingAgendaWithRandomDate();
            }
            else // else return the string value of an existing one
            {
                return ElemGet.SelElem_GetFirstItemContainingText(MeetingAgendaSelElem, "Upcoming").Text;
            }

            return agendaName;
        }

        /// <summary>
        /// Creates a new agenda. First it will click on the Competenece Committee tab if we are not on this page, then it opens the Create 
        /// New Agenda window, then assigns a random date and clicks Create to create the new agenda. Returns the agenda name that was created
        /// </summary>
        public string CreateUpcomingAgendaWithRandomDate()
        {
            // If we are not on the Competence Committee tab, then click on it to go there
            if (!Browser.Exists(Bys.CBDProgDirectorPage.FinalizeAgendaBtn, ElementCriteria.IsEnabled))
            {
                ClickAndWait(CompCommiteeTab);
            }

            // First get a list of all existing agendas, so that we can filter these out when we return the new agenda that we create below
            // Note that DEV may redesign creating agendas, and if they do, this wont be needed, because we may then be able to see the
            // agenda name at creation. See JIRA bug RCPSC-547
            List<string> existingAgendas = ElemGet.SelElem_ListTextToListString(MeetingAgendaSelElem);

            // Open the form
            ActionsBtn.Click();
            ClickAndWait(CreateNewAgendaLnk);

            // The calendar control limits a user to have a maximum of 9 agendas on a certain day. So we have to generate a random day,
            // then use it to create an upcoming agenda. We will also handle the situation where we hit the max per day by looping
            // and IF statement

            // First get the amount of days between tomorrow and 3 years from now because this is the furthest the date control will
            // allow you to select. Then use that amount to do the loop
            int daysCount = (DateTime.Now.AddYears(2).AddMonths(11).AddDays(27) - DateTime.Now.AddDays(1)).Days;
            for (int i = 0; i < daysCount; i++)
            {
                // Choose the date and click on Create
                Dictionary<string, string> dates = DataUtils.GetDateForCalendarElem(GetRandomDateForCreateNewAgendaFormDateControl());
                ElemSet.DatePicker_ChooseDate(Browser, dates["year"], dates["month"], dates["day"]);

                ClickAndWait(CreateNewAgendaFormCreateBtn);

                // If the warning message appears, continue in the loop to choose another date, else break
                if (Browser.Exists(Bys.CBDProgDirectorPage.CreateNewAgendaFormMaxNumOfAgendasLbl, ElementCriteria.IsVisible))
                {
                    continue;
                }
                else
                {
                    break;
                }
            }

            // Compare the existing agewnda list with the list at this moment and extract the new item that was inserted after we created it above
            List<string> existingAgendasPlusNewAgenda = ElemGet.SelElem_ListTextToListString(MeetingAgendaSelElem);
            List<string> differences = existingAgendasPlusNewAgenda.Except(existingAgendas).ToList();

            return differences[0];
        }

        /// <summary>
        /// Returns a random date between tomorrows date and 3 years from now. The Create New Agenda form Date control
        /// only allows for tomorrows date plus 3 years to be chosen
        /// </summary>
        /// <returns></returns>
        public DateTime GetRandomDateForCreateNewAgendaFormDateControl()
        {
            DateTime startDate = DateTime.Now.AddDays(1);
            DateTime endDate = DateTime.Now.AddYears(2).AddMonths(11).AddDays(27);

            return DataUtils.GetRandomDateBetween(startDate, endDate);
        }

        /// <summary>
        /// Clicks on the Competence Committee tab if that page is not in view, then creates an upcoming agenda with a random date.
        /// It opens the Create New Agenda window, assigns the date and clicks Create to create the new agenda. It will then pick this
        /// new agenda in the Meeting Agenda dropdown
        /// </summary>
        public void CreateUpcomingAgendaThenSelectIt()
        {
            string agendaToSelect = "";

            // If we are not on the Competence Committee tab, then click on it to go there
            if (!Browser.Exists(Bys.CBDProgDirectorPage.FinalizeAgendaBtn, ElementCriteria.IsEnabled))
            {
                ClickAndWait(CompCommiteeTab);
            }

            agendaToSelect = CreateUpcomingAgendaWithRandomDate();

            ElemSet.SelElem_SelectItemContainingText(MeetingAgendaSelElem, agendaToSelect);
            Browser.WaitForElement(Bys.RCPPage.LoadIcon, TimeSpan.FromMinutes(3), ElementCriteria.AttributeValue("class", "page-splash dissolve-animation ng-hide")
                .OR(ElementCriteria.AttributeValue("class", "page-splash dissolve-animation ng-animate ng-hide")));
        }

        /// <summary>
        /// Clicks on the Competence Committee tab if that page is not in view, then If an upcoming agenda is not created 
        /// (e.g. No items in the Meeting Agenda dropdown have an agenda that say "Upcoming"), then this creates one with
        /// a random date. It opens the Create New Agenda window, assigns the date and clicks Create to create the new agenda.
        /// Then it chooses the agenda in the Meeting Agenda dropdown, then it will change the value of any one of the columns 
        /// for the user-specified learner
        /// </summary>
        /// <param name="learnerFullName">The exact text from the cell that your learner exists within</param>
        /// <param name="reviewed">The exact text from one of the items in the select element from the Review column in the Agenda table. Enter null if you dont want this to be changed</param>
        /// <param name="agendaPriority">The exact text from one of the items in the select element from the Agenda Priority column in the Agenda table. Enter null if you dont want this to be changed</param>
        /// <param name="presenter">The exact text from one of the items in the select element from the Presenter column in the Agenda table. Enter null if you dont want this to be changed</param>
        /// <param name="learnerStatus">The exact text from one of the items in the Learner Status select element from the Set Status window. Enter null if you dont want this to be changed</param>
        /// <param name="actions">The exact text from one of the items in the Actions select element from the Set Status window. Enter null if you dont want this to be changed</param>
        public void CreateChangeAndFinalizeAgendaForLearner(string learnerFullName, string reviewed, string agendaPriority, string presenter, string learnerStatus, string actions)
        {
            CreateUpcomingAgendaThenSelectIt();

            ModifyLearnerForAgenda(learnerFullName, reviewed, agendaPriority, presenter, learnerStatus, actions);

            ClickAndWait(FinalizeAgendaBtn);
            ClickAndWait(FinalizeAgendaFormFinalizeBtn);
        }

        /// <summary>
        /// Changes any of the cells inside the Agenda table for a user-specified learner
        /// </summary>
        /// <param name="learnerFullName">The exact text from the cell that your learner exists within</param>
        /// <param name="reviewed">The exact text from one of the items in the select element from the Review column in the Agenda table. Enter null if you dont want this to be changed</param>
        /// <param name="agendaPriority">The exact text from one of the items in the select element from the Agenda Priority column in the Agenda table. Enter null if you dont want this to be changed</param>
        /// <param name="presenter">The exact text from one of the items in the select element from the Presenter column in the Agenda table. Enter null if you dont want this to be changed</param>
        /// <param name="learnerStatus">The exact text from one of the items in the Learner Status select element from the Set Status window. Enter null if you dont want this to be changed</param>
        /// <param name="actions">The exact text from one of the items in the Actions select element from the Set Status window. Enter null if you dont want this to be changed</param>
        public void ModifyLearnerForAgenda(string learnerFullName, string reviewed, string agendaPriority, string presenter, string learnerStatus, string actions)
        {
            if (reviewed != null)
            {                
                ElemSet.Grid_ClickButtonOrLinkWithinRow(Browser, AgendaTbl, Bys.CBDProgDirectorPage.AgendaTbl, learnerFullName, "a", "Not Reviewed", "span");
                Thread.Sleep(0300);
                ElemSet.Grid_SelectItemWithinSelElem(AgendaTbl, Bys.CBDProgAdminPage.AgendaTbl, learnerFullName, "a", "ReviewStatus", reviewed);

                this.WaitUntilAll(Criteria.CBDProgDirectorPage.LoadElementClassAttributeSetToHide, Criteria.CBDProgDirectorPage.LoadElementDisappeared);
            }

            if (agendaPriority != null)
            {                
                ElemSet.Grid_SelectItemWithinSelElem(AgendaTbl, Bys.CBDProgDirectorPage.AgendaTbl, learnerFullName, "a", "Priority", agendaPriority);
                this.WaitUntilAll(Criteria.CBDProgDirectorPage.LoadElementClassAttributeSetToHide, Criteria.CBDProgDirectorPage.LoadElementDisappeared);
            }

            if (presenter != null)
            {                
                ElemSet.Grid_SelectItemWithinSelElem(AgendaTbl, Bys.CBDProgDirectorPage.AgendaTbl, learnerFullName, "a", "Presenter", presenter);
                this.WaitUntilAll(Criteria.CBDProgDirectorPage.LoadElementClassAttributeSetToHide, Criteria.CBDProgDirectorPage.LoadElementDisappeared);
            }

            if (learnerStatus != null)
            {                
                SetStatusOfLearner(learnerFullName, learnerStatus, actions);
            }

        }

        /// <summary>
        /// Opens the Set Status form for a given learner on the Agenda table if it is not already opens, then chooses a user-specified
        /// Learner Status and Actions, then clicks the Confirm Status button and waits for the window to close
        /// </summary>
        /// <param name="learnerFullName">The exact text from the cell that your learner exists within</param>
        /// <param name="learnerStatus">The exact text from one of the items in the Learner Status select element</param>
        /// <param name="actions">The exact text from one of the items in the Actions select element</param>
        public void SetStatusOfLearner(string learnerFullName, string learnerStatus, string actions)
        {
            // If the Set Status window is not open, open it
            if (!Browser.Exists(Bys.CBDProgDirectorPage.SetStatusFormLearnerStatusSelElem, ElementCriteria.IsVisible))
            {                
                ElemSet.Grid_ClickButtonOrLinkWithinRow(Browser, AgendaTbl, Bys.CBDProgDirectorPage.AgendaTbl, learnerFullName, "a", "Set Status", "span");
                this.WaitUntilAll(Criteria.CBDProgDirectorPage.SetStatusFormLearnStatusSelElemHasItemsAndIsEnabled,
                    Criteria.CBDProgDirectorPage.LoadElementClassAttributeSetToHide, Criteria.CBDProgDirectorPage.LoadElementDisappeared);
            }

            SetStatusFormLearnerStatusSelElem.SelectByText(learnerStatus);
            Thread.Sleep(0200);
            this.WaitUntilAll(Criteria.CBDProgDirectorPage.LoadElementDisappeared, Criteria.CBDProgDirectorPage.LoadElementClassAttributeSetToHide);

            SetStatusFormActionsSelElem.SelectByText(actions);

            ClickAndWait(SetStatusFormConfirmBtn);
        }

        #endregion methods: page specific: Competence Committee tab

        #region methods: page specific: CBD Home tab

        /// <summary>
        /// Chooses User specified Learner, clicks the Action button, selects Add Supporting Documentation,
        /// fills in the File Name text box, then clicks Submit 
        /// </summary>
        /// <param name="learnerName">First and last name of the learner</param>
        /// <param name="fileLocation">File location</param>
        public void AddSupportDocumentation(string learnerFullName, string fileLocation)
        {
            IWebElement btn = ElemSet.Grid_ClickButtonOrLinkWithinRow(Browser, LearnersTbl, 
                Bys.CBDProgDirectorPage.LearnersTblRowBody, learnerFullName, "a", "Actions", "span");
            Thread.Sleep(0500);

            IWebElement btnParent = XpathUtils.GetNthParentElem(btn, 3);

            ElemSet.Grid_ClickMenuItemInsideDropdown(Browser, btnParent, "Add Supporting Documentation");
            Browser.WaitForElement(Bys.CBDProgDirectorPage.AddSupportingDocumentationFormFilelocationTxt, ElementCriteria.IsVisible);
            this.WaitUntilAll(Criteria.CBDProgDirectorPage.LoadElementClassAttributeSetToHide, Criteria.CBDProgDirectorPage.LoadElementDisappeared);

            AddSupportingDocumentationFormFilelocationTxt.SendKeys(fileLocation);

            // FileUtils.Upload(AddSupportingDocumentationFormBrowseBtn, @"C:\SeleniumAutoIt\FileUpload.exe", browserName, "C:\\SeleniumAutoIt\\test.txt");

            ClickAndWait(AddSupportingDocumentationFormSubmitBtn);
        }

        /// <summary>
        /// Chooses a learner in the learners table, clicks on the Actions button, clicks on Schedule Progress Meeting, fill in 
        /// all of the fields with random data and clicks Submit
        /// </summary>
        /// <param name="learnerFullName"></param>
        public void ScheduleProgressMeeting(string learnerFullName)
        {
            IWebElement btn = ElemSet.Grid_ClickButtonOrLinkWithinRow(Browser, LearnersTbl, Bys.CBDProgDirectorPage.LearnersTblRowBody, 
                learnerFullName, "a", "Actions", "span");
            Thread.Sleep(0500);

            IWebElement btnParent = XpathUtils.GetNthParentElem(btn, 3);

            ElemSet.Grid_ClickMenuItemInsideDropdown(Browser, btnParent, "Schedule Progress Meeting");
            Browser.WaitForElement(Bys.CBDProgDirectorPage.SchedProgMeetFormSubjectTxt, ElementCriteria.IsVisible);
            this.WaitUntilAll(Criteria.CBDProgDirectorPage.LoadElementClassAttributeSetToHide, Criteria.CBDProgDirectorPage.LoadElementDisappeared);

            ElemSet.ChkBx_ChooseRandom(Browser, UserUtils.ProgAdmin1FullName);

            ElemSet.DatePicker_ChooseDate(Browser, "19", "December", "01");

            SchedProgMeetFormSubjectTxt.SendKeys(DataUtils.GetRandomSentence(10));

            // Generate a random boolean, then use it to randomly check or uncheck the check box and select a recurring meeting or not 
            Random gen = new Random();
            bool reccuring = gen.Next(100) <= 20 ? true : false;

            if (reccuring)
            {
                SchedProgMeetFormRecurChk.Click();
                Thread.Sleep(0300);
                ElemSet.SelElem_Single_SelectRandomItem(SchedProgMeetFormRecurringSelElem);
            }

            ClickAndWait(SchedProgMeetFormScheduleBtn);
        }

        /// <summary>
        /// Chooses a learner in the learners table, clicks on the Actions button, clicks on Add/Remove Flag, fill in 
        /// all of the fields with random data and clicks Save Flag
        /// </summary>
        /// <param name="learnerFullName"></param>
        public void AddFlag(string learnerFullName)
        {
            IWebElement btn = ElemSet.Grid_ClickButtonOrLinkWithinRow(Browser, LearnersTbl, Bys.CBDProgDirectorPage.LearnersTblRowBody, 
                learnerFullName, "a", "Actions", "span");
            Thread.Sleep(0500);

            IWebElement btnParent = XpathUtils.GetNthParentElem(btn, 3);

            ElemSet.Grid_ClickMenuItemInsideDropdown(Browser, btnParent, "Add/Remove Flag");
            Browser.WaitForElement(Bys.CBDProgDirectorPage.AddRemoveFlagFormReasonTxt, ElementCriteria.IsVisible);
            this.WaitUntilAll(Criteria.CBDProgDirectorPage.LoadElementClassAttributeSetToHide, Criteria.CBDProgDirectorPage.LoadElementDisappeared);

            AddRemoveFlagFormReasonTxt.SendKeys(DataUtils.GetRandomSentence(10));
            ClickAndWait(AddRemoveFlagFormSaveFlagBtn);
        }

        /// <summary>
        /// Chooses a learner in the learners table, clicks on the Actions button, clicks on Add/Remove Flag, fill in 
        /// all of the fields with random data and clicks Remove Flag if a flag is present
        /// </summary>
        /// <param name="learnerFullName"></param>
        public void RemoveFlag(string learnerFullName)
        {
            IWebElement btn = ElemSet.Grid_ClickButtonOrLinkWithinRow(Browser, LearnersTbl, Bys.CBDProgDirectorPage.LearnersTblRowBody, 
                learnerFullName, "a", "Actions", "span");
            Thread.Sleep(0500);

            IWebElement btnParent = XpathUtils.GetNthParentElem(btn, 3);

            ElemSet.Grid_ClickMenuItemInsideDropdown(Browser, btnParent, "Add/Remove Flag");
            Browser.WaitForElement(Bys.CBDProgDirectorPage.AddRemoveFlagFormReasonTxt, ElementCriteria.IsVisible);
            this.WaitUntilAll(Criteria.CBDProgDirectorPage.LoadElementClassAttributeSetToHide, Criteria.CBDProgDirectorPage.LoadElementDisappeared);

            // If the flag is there, remove it
            if (AddRemoveFlagFormRemoveFlagBtn.Enabled)
            {
                ClickAndWait(AddRemoveFlagFormRemoveFlagBtn);
            }
        }

        /// <summary>
        /// Chooses User specified Learner, clicks the Action button, selects Add Notes, fills in the Subject and Notes text box with
        /// a random string, chooses the user-specified radio button  then clicks Submit 
        /// </summary>
        /// <param name="learnerFullName">First and last name of the learner</param>
        /// <param name="sharingOption">The exact text of either radio button on the Add Notes form</param>
        public void AddNotes(string learnerFullName, string sharingOption)
        {
            IWebElement btn = ElemSet.Grid_ClickButtonOrLinkWithinRow(Browser, LearnersTbl, Bys.CBDProgDirectorPage.LearnersTblRowBody, 
                learnerFullName, "a", "Actions", "span");
            Thread.Sleep(0500);

            IWebElement btnParent = XpathUtils.GetNthParentElem(btn, 3);

            ElemSet.Grid_ClickMenuItemInsideDropdown(Browser, btnParent, "Add Notes");
            Browser.WaitForElement(Bys.CBDProgDirectorPage.AddNotesFormSubjectTxt, ElementCriteria.IsVisible);
            this.WaitUntilAll(Criteria.CBDProgDirectorPage.LoadElementClassAttributeSetToHide, Criteria.CBDProgDirectorPage.LoadElementDisappeared);

            AddNotesFormNotesTxt.SendKeys(DataUtils.GetRandomSentence(12));
            ElemSet.RdoBtn_ClickRandom(Browser, "Share This Note");
            AddNotesFormSubjectTxt.SendKeys(DataUtils.GetRandomSentence(12));

            ClickAndWait(AddNotesFormSubmitBtn);
        }

        #endregion methods: page specific: learners table

        #region methods: page specific: General

        /// <summary>
        /// Clicks the user-specified button, link, tab, etc. and then waits for a window to close or open, or a page to load,
        /// depending on the element that was clicked
        /// </summary>
        /// <param name="buttonOrLinkElem">The element to click on</param>
        public void ClickAndWait(IWebElement buttonOrLinkElem)
        {
            if (Browser.Exists(Bys.CBDProgDirectorPage.AddRemoveFlagFormRemoveFlagBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AddRemoveFlagFormRemoveFlagBtn.GetAttribute("outerHTML"))
                {
                    AddRemoveFlagFormRemoveFlagBtn.Click();
                    this.WaitUntilAll(TimeSpan.FromSeconds(60), Criteria.CBDProgDirectorPage.LearnersTblRowBodyVisibleAndEnabled,
                        Criteria.CBDProgDirectorPage.LoadElementClassAttributeSetToHide,
                        Criteria.CBDProgDirectorPage.LoadElementDisappeared);
                    return;
                }
            }

            if (Browser.Exists(Bys.RCPPage.CBDTab))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == Browser.FindElement(Bys.RCPPage.CBDTab).GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntil(TimeSpan.FromSeconds(180), Criteria.CBDProgDirectorPage.PageReady);
                    return;
                }
            }

            if (Browser.Exists(Bys.CBDProgDirectorPage.AddRemoveFlagFormSaveFlagBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AddRemoveFlagFormSaveFlagBtn.GetAttribute("outerHTML"))
                {
                    AddRemoveFlagFormSaveFlagBtn.Click();
                    this.WaitUntilAll(TimeSpan.FromSeconds(60), Criteria.CBDProgDirectorPage.LearnersTblRowBodyVisibleAndEnabled,
                        Criteria.CBDProgDirectorPage.LoadElementClassAttributeSetToHide,
                        Criteria.CBDProgDirectorPage.LoadElementDisappeared);
                    return;
                }
            }

            if (Browser.Exists(Bys.CBDProgDirectorPage.SchedProgMeetFormScheduleBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == SchedProgMeetFormScheduleBtn.GetAttribute("outerHTML"))
                {
                    SchedProgMeetFormScheduleBtn.Click();
                    this.WaitUntilAll(TimeSpan.FromSeconds(60), Criteria.CBDProgDirectorPage.LearnersTblRowBodyVisibleAndEnabled,
                        Criteria.CBDProgDirectorPage.LoadElementClassAttributeSetToHide,
                        Criteria.CBDProgDirectorPage.LoadElementDisappeared);
                    this.WaitForInitialize();
                    return;
                }
            }

            if (Browser.Exists(Bys.CBDProgDirectorPage.AddNotesFormSubmitBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AddNotesFormSubmitBtn.GetAttribute("outerHTML"))
                {
                    AddNotesFormSubmitBtn.Click();
                    this.WaitUntilAll(TimeSpan.FromSeconds(60), Criteria.CBDProgDirectorPage.LearnersTblRowBodyVisibleAndEnabled,
                        Criteria.CBDProgDirectorPage.LoadElementClassAttributeSetToHide,
                        Criteria.CBDProgDirectorPage.LoadElementDisappeared);
                    this.WaitForInitialize();
                    return;
                }
            }

            if (Browser.Exists(Bys.CBDProgDirectorPage.AssignObsFormNextBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AssignObsFormNextBtn.GetAttribute("outerHTML"))
                {
                    AssignObsFormNextBtn.Click();
                    Browser.WaitForElement(Bys.CBDProgDirectorPage.AssignObsFormBackBtn, ElementCriteria.IsEnabled);
                    Browser.WaitForElement(Bys.CBDProgDirectorPage.AssignObsFormBackBtn, ElementCriteria.IsVisible);
                    Browser.WaitForElement(Bys.CBDProgDirectorPage.AssignObsFormBackBtn, ElementCriteria.HasText);
                    this.WaitForInitialize();
                    return;
                }
            }

            if (Browser.Exists(Bys.CBDProgDirectorPage.AssignObsFormSearchBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AssignObsFormSearchBtn.GetAttribute("outerHTML"))
                {
                    AssignObsFormSearchBtn.Click();
                    Browser.WaitForElement(Bys.CBDProgDirectorPage.AssignObsFormObserverNameTxt, ElementCriteria.IsEnabled);
                    this.WaitForInitialize();
                    Thread.Sleep(1000);
                    this.WaitForInitialize();
                    return;
                }
            }

            if (Browser.Exists(Bys.CBDProgDirectorPage.AssignObsFormAssignBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AssignObsFormAssignBtn.GetAttribute("outerHTML"))
                {
                    AssignObsFormAssignBtn.Click();
                    this.WaitForInitialize();
                    return;
                }
            }

            if (Browser.Exists(Bys.CBDProgDirectorPage.CompCommiteeTab))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == CompCommiteeTab.GetAttribute("outerHTML"))
                {
                    CompCommiteeTab.Click();
                    // Note that the if this is a new environment, then this will fail because the Agenda table will not have any rows. You have to log in manually and create
                    // an agenda so that the agenda table gets rows
                    this.WaitUntilAll(TimeSpan.FromSeconds(60), Criteria.CBDProgDirectorPage.AgendaTblRowBodyVisibleAndEnabled,
                        Criteria.CBDProgDirectorPage.LoadElementClassAttributeSetToHide, Criteria.CBDProgDirectorPage.LoadElementDisappeared);
                    return;
                }
            }

            if (Browser.Exists(Bys.CBDProgDirectorPage.CreateNewAgendaLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == CreateNewAgendaLnk.GetAttribute("outerHTML"))
                {
                    CreateNewAgendaLnk.Click();
                    this.WaitUntilAll(TimeSpan.FromSeconds(120), Criteria.CBDProgDirectorPage.CreateNewAgendaFormProgramSelElemHasItemsAndIsEnabled,
                        Criteria.CBDProgDirectorPage.LoadElementClassAttributeSetToHide,
                        Criteria.CBDProgDirectorPage.LoadElementDisappeared);
                    return;
                }
            }
            
            if (Browser.Exists(Bys.CBDProgDirectorPage.CreateNewAgendaFormCreateBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == CreateNewAgendaFormCreateBtn.GetAttribute("outerHTML"))
                {
                    CreateNewAgendaFormCreateBtn.Click();
                    this.WaitUntilAll(TimeSpan.FromSeconds(120), Criteria.CBDProgDirectorPage.LoadElementClassAttributeSetToHide,
                        Criteria.CBDProgDirectorPage.LoadElementDisappeared);
                    
                    // This failed once and the screenshot showed that the load element was still there. So the above wait criteria didnt work,
                    // maybe there is a second load icon that appears. So lets condition this so that if the form did not close yet, then wait 
                    // again for the load icon to disappear.
                    if (Browser.FindElements(Bys.CBDProgDirectorPage.CreateNewAgendaFormCreateBtn).Count > 0)
                    {
                        this.WaitUntilAll(TimeSpan.FromSeconds(120), Criteria.CBDProgDirectorPage.LoadElementClassAttributeSetToHide,
                            Criteria.CBDProgDirectorPage.LoadElementDisappeared);
                    }
                        
                    return;
                }
            }

            if (Browser.Exists(Bys.CBDProgDirectorPage.SetStatusFormConfirmBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == SetStatusFormConfirmBtn.GetAttribute("outerHTML"))
                {
                    SetStatusFormConfirmBtn.Click();
                    this.WaitUntilAll(TimeSpan.FromSeconds(60), Criteria.CBDProgDirectorPage.LoadElementClassAttributeSetToHide,
                        Criteria.CBDProgDirectorPage.LoadElementDisappeared);
                    return;
                }
            }

            if (Browser.Exists(Bys.CBDProgDirectorPage.FinalizeAgendaBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == FinalizeAgendaBtn.GetAttribute("outerHTML"))
                {
                    FinalizeAgendaBtn.Click();
                    this.WaitUntilAll(TimeSpan.FromSeconds(60), Criteria.CBDProgDirectorPage.FinalizeAgendaFormFinalizeBtnIsEnabled,
                        Criteria.CBDProgDirectorPage.LoadElementClassAttributeSetToHide, Criteria.CBDProgDirectorPage.LoadElementDisappeared);
                    return;
                }
            }

            if (Browser.Exists(Bys.CBDProgDirectorPage.FinalizeAgendaFormFinalizeBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == FinalizeAgendaFormFinalizeBtn.GetAttribute("outerHTML"))
                {
                    FinalizeAgendaFormFinalizeBtn.Click();
                    this.WaitUntilAll(TimeSpan.FromSeconds(60), Criteria.CBDProgDirectorPage.LoadElementClassAttributeSetToHide,
                        Criteria.CBDProgDirectorPage.LoadElementDisappeared, Criteria.CBDProgDirectorPage.AgendaTblRowBodyVisibleAndEnabled);
                    return;
                }
            }

            if (Browser.Exists(Bys.CBDProgDirectorPage.AddSupportingDocumentationFormSubmitBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AddSupportingDocumentationFormSubmitBtn.GetAttribute("outerHTML"))
                {
                    AddSupportingDocumentationFormSubmitBtn.Click();
                    this.WaitUntilAll(Criteria.CBDProgDirectorPage.LearnersTblRowBodyVisibleAndEnabled,
                        Criteria.CBDProgDirectorPage.LoadElementClassAttributeSetToHide,
                        Criteria.CBDProgDirectorPage.LoadElementDisappeared);
                    return;
                }
            }

            else
            {
                throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, " +
                    "or if the button is already added, then the page you were on did not contain the button.");
            }
        }

        #endregion methods: page specific: General

        #endregion methods: page specific
    }

}
