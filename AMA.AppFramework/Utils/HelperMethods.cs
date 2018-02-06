using Browser.Core.Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Threading;

namespace AMA.AppFramework
{
	public static class HelperMethods
	{
        /// <summary>
        /// this specific method adding courses to curriculum for specific program
        /// </summary>
        /// <param name="browser"></param>
        public static void AddCourseToCurriculum(IWebDriver browser)
        {
            ProgramsPage PP = new ProgramsPage(browser);
            CurriculumCoursePage CoursePage = PP.EditProgramm();
            CoursePage.AddOrRemoveCourses(CoursePage.AvailableCoursesTbl, CoursePage.AddSelectedBtn, 2, 3);
            PGYAssignmentPage Pgy = CoursePage.ClickToAdvance(CoursePage.NextBtn);
            Pgy.Grid_ClickElementWithoutTextInsideRow(Pgy.EditCoursePgyTbl, 2, 3);
            Pgy.Grid_ClickElementWithoutTextInsideRow(Pgy.EditCoursePgyTbl, 3, 3);
            AssignSummaryPage Summary = Pgy.ClickToAdvance(Pgy.NextBtn);
            AssignConfirmationPage Confirm = Summary.ClickToAdvance(Summary.NextBtn);
            Confirm.ClickToAdvance(Confirm.EditConfirmBtn);
            PP.Search("Anesthesiology");
        }

        /// <summary>
        /// on this method we are assigning curriculum to the program
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="CurriculumName">curriculum name what we want to assign</param>
        /// <returns></returns>
        public  static string CurriculumAssignmentFlow(IWebDriver browser, string CurriculumName)
        {
            CurriculumMngPage CMP = new CurriculumMngPage(browser);
            CMP.Search(CurriculumName);
            CMP.Actioncell.Click();
            AssignProgramPage Assign = CMP.ClickToAdvance(CMP.AssignToProgrammLnk);
            string StartingDate = Assign.ChoosingStartDate();
            string EndingDate = Assign.ChoosingEndDate(1, "MM/dd/yyyy");
            AssignSummaryPage Summary = Assign.ClickToAdvance(Assign.NextBtn);
            AssignConfirmationPage Confirmation = Summary.ClickToAdvance(Summary.NextBtn);
            string breadCrumpafterASsignment = Confirmation.GetBreadCrumbContainerText();
            return breadCrumpafterASsignment;
        }

        /// <summary>
        /// basically creating curriculum all flow
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="curriculumName">  curriculum name </param>
        public static void CurriculumCreationFlow(IWebDriver browser, string curriculumName)
        {
            CurriculumCoursePage CurCoursPage = new CurriculumCoursePage(browser);
            ///  4.Form course page choosing available courses from table by index
            CurCoursPage.AddOrRemoveCourses(CurCoursPage.AvailableCoursesTbl, CurCoursPage.AddSelectedBtn, 1, 7, 12);

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
            PGY.ClickToAdvance(PGY.SaveExitBtn);
            PGY.Search(curriculumName);
        }


       /// <summary>
       /// searching for a specific Institutions and getting contact email adress and saving them on the List of strings
       /// </summary>
       /// <param name="browser"></param>
       /// <param name="Institutions">the List of Institutions for which you are looking email adress </param>
       /// <returns></returns>
        public static List<string> GetTheInstutionsEmail(IWebDriver browser,List<string> Institutions)
        {
            EditInstitutionPage EIP = new EditInstitutionPage(browser);
            InstitutionsPage IP = new InstitutionsPage(browser);
            List<string> InstitutionContactEmails = new List<string>();
            // Starting the for loop on the 2nd row (index = 1) on the for loop here because when we extracted the values from the dropdown
            //  above, the first value was not an institution, it was a static "Select your item" text
            for (var i=1; i < Institutions.Count; i++)
            {
                IP.Search(Institutions[i]);
                IP.ActionGearBtn.Click();
                Thread.Sleep(0500);
                EIP = IP.ClickToAdvance(IP.EditInstitutionLnk);
                InstitutionContactEmails.Add(EIP.InstitutionPrimaryContactEmailTxt.GetAttribute("value"));
                IP = EIP.ClickToAdvance(EIP.InstitutionCancelBtn);
            }

            return InstitutionContactEmails;
        }


        /// <summary>
        /// This method compare Institution contact email adress with contact email adress from UI
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="Institutions">List of Institution names</param>
        /// <param name="InstitutionContactEmails">List Of Institutions contact email adresses</param>
        /// <returns></returns>
        public static Boolean ComparingInstitutionEmailsWithHelpContactEmails(IWebDriver browser, List<string> Institutions, List<string> InstitutionContactEmails)
        {
          GCEPPage GP = new GCEPPage(browser);
          HelpPage HP = new HelpPage(browser);            
            if (Institutions.Count-1 == InstitutionContactEmails.Count)
            {
                // Starting the for loop on the 2nd row (index = 1) on the for loop here because when we extracted the values from the dropdown
                //  above, the first value was not an institution, it was a static "Select your item" text
                for (var i = 1; i < Institutions.Count; i++)
                {
                    GP = HP.ClickToAdvance(HP.GMECompetencyEducationProgramLnk); //(HP.AdministrationLnk)
                    GP.InstitutionSelElem.SelectByText(Institutions[i].ToString());
                    GP.WaitForInitialize();
                    HP = GP.ClickToAdvance(GP.HelpfromYourInstitutionLnk); //GP.ClickToAdvance(GP.HelpLnk);                  
                    Thread.Sleep(0500);                   
                    Assert.True(InstitutionContactEmails[i - 1].Equals( HP.ContactInvolvedInstitutionEmailLnk.Text));  //==firefox failure
                    Assert.True(HP.GMECompetencyEducationProgramLnk.Displayed); //HP.AdministrationLnk.Displayed
                    Assert.True(HP.AdminWatchVideoLnk.Displayed);
                }
                return true;
            }
            else { return false; }
        }
    }
}
