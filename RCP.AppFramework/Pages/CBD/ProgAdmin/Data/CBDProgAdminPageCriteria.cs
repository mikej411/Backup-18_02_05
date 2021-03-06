﻿using Browser.Core.Framework;

namespace RCP.AppFramework
{
    /// <summary>
    /// A set of criteria that we define that needs to be met for the test to proceed. For example on this page, when we login to the
    /// Learner role page, we need to wait for the circular load icon to disappear and for the table below the tabs to appear.
    /// </summary>
    public class CBDProgAdminPageCriteria
    {
        /// <summary>
        /// This is the criteria that needs to be met for the main prog admin page, when the learners table is present. <see cref="PageReady"/>
        /// at the bottom of this class. That is where these specific criteria are combined
        /// </summary>
        public readonly ICriteria<CBDProgAdminPage> LoadElementClassAttributeSetToHide = new Criteria<CBDProgAdminPage>(p =>
        {
            return p.Exists(Bys.RCPPage.LoadIcon, ElementCriteria.AttributeValue("class", "page-splash dissolve-animation ng-hide")
                .OR(ElementCriteria.AttributeValue("class", "page-splash dissolve-animation ng-animate ng-hide")));
        }, "Load icon class attribute value set to \"ng-hide\"");

        public readonly ICriteria<CBDProgAdminPage> LoadElementDisappeared = new Criteria<CBDProgAdminPage>(p =>
        {
            return p.Exists(Bys.RCPPage.SplashPage, ElementCriteria.IsNotVisible);
        }, "Load icon disappeared");

        public readonly ICriteria<CBDProgAdminPage> LearnersTblVisibleAndEnabled = new Criteria<CBDProgAdminPage>(p =>
        {
            return p.Exists(Bys.CBDProgAdminPage.LearnersTbl, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
        }, "Learners table visible and enabled");

        public readonly ICriteria<CBDProgAdminPage> LearnersTblRowBodyVisibleAndEnabled = new Criteria<CBDProgAdminPage>(p =>
        {
            return p.Exists(Bys.CBDProgAdminPage.LearnersTblRowBody, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
        }, "Learners table body row 1 visible and enabled");

        /// <summary>
        /// The following criteria does not need to be met for the page load. But we can define more criteria below to use
        /// throughout our framework for specific instances where one of them needs to be met
        /// </summary>
        public readonly ICriteria<CBDProgAdminPage> CreateNewAgendaFormProgramSelElemHasItemsAndIsEnabled = new Criteria<CBDProgAdminPage>(p =>
        {
            return p.Exists(Bys.CBDProgAdminPage.CreateNewAgendaFormProgramSelElem, ElementCriteria.SelectElementHasItems, ElementCriteria.IsEnabled);
        }, "Program select element on Create New Agenda form has items and is enabled");

        // Note that the if this is a new environment, then this will fail because the Agenda table will not have any rows. You have to log in manually and create
        // an agenda so that the agenda table gets rows
        public readonly ICriteria<CBDProgAdminPage> AgendaTblRowBodyVisibleAndEnabled = new Criteria<CBDProgAdminPage>(p =>
        {
            return p.Exists(Bys.CBDProgAdminPage.AgendaTblRowBody, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
        }, "Agenda table body row 1 visible and enabled");

        public readonly ICriteria<CBDProgAdminPage> SetStatusFormLearnStatusSelElemHasItemsAndIsEnabled = new Criteria<CBDProgAdminPage>(p =>
        {
            return p.Exists(Bys.CBDProgAdminPage.SetStatusFormLearnerStatusSelElem, ElementCriteria.SelectElementHasItems, ElementCriteria.IsEnabled);
        }, "Learner Status select element on Set Status form has items and is enabled");

        public readonly ICriteria<CBDProgAdminPage> FinalizeAgendaFormFinalizeBtnIsEnabled = new Criteria<CBDProgAdminPage>(p =>
        {
            return p.Exists(Bys.CBDProgAdminPage.FinalizeAgendaFormFinalizeBtn, ElementCriteria.IsEnabled);
        }, "Finalize button on Finalize Agenda form enabled");

        public readonly ICriteria<CBDProgAdminPage> CBDTabVisibleAndEnabled = new Criteria<CBDProgAdminPage>(p =>
        {
            return p.Exists(Bys.RCPPage.CBDTab, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);

        }, "CBD tab visible and enabled");

        /// <summary>
        /// The criteria that should be used for this constructor are only elements that are contained within every page instance
        /// (There are only 2 instances of 8/4/17) of this Learner role login page. Instances, meaning the main page, and then the
        /// EPA Page. This does not include popups. 
        /// </summary>
        public readonly ICriteria<CBDProgAdminPage> PageReady;
        public readonly ICriteria<CBDProgAdminPage> LoadElementDoneLoading;
        public CBDProgAdminPageCriteria()
        {
            LoadElementDoneLoading = LoadElementClassAttributeSetToHide.AND(LoadElementDisappeared);
            PageReady = LoadElementClassAttributeSetToHide.AND(LoadElementDisappeared).AND(LearnersTblVisibleAndEnabled).
                AND(LearnersTblRowBodyVisibleAndEnabled);
        }
    }
}
