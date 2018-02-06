using OpenQA.Selenium;

namespace AMA.AppFramework
{
    public class AssignProgramPageBys
    {
        // Main page

        //Label
        public readonly By CurriculumProgramAndTimeFrameLbl = By.LinkText("Curriculum Program and Time Frame");

        //tables
        public readonly By CurriculumNameTbl = By.XPath("//table");
        public readonly By CurriculumTimeFrameTbl = By.XPath("//form[@ name='formCurriculumProgramTimeFrame']/table");

        //buttons
        public readonly By CancelBtn = By.LinkText("Cancel");
        public readonly By NextBtn = By.XPath("//button[.='Next']");
        public readonly By GetProgramsBtn = By.XPath("//button[.='Get Programs']");
        public readonly By AddSelectedBtn = By.XPath("//button[.='Add Selected']");
        public readonly By RemoveSelectedBtn = By.XPath("//button[.='Remove Selected']");

        //Calenders
       public readonly By StartDateCalenderBox = By.XPath("//input[@id='startDate']");
        // public readonly By EndingTableNameBox = By.XPath("//input[@type='text' and @name=" + "CalendarTableName" + "']");

        //table
        public readonly By AssignAvailableProgramTbl = By.Id("gridCurriculumTemplateAvailablePrograms");
        public readonly By AssignChosenProgramTbl = By.Id("gridCurriculumTemplateChosenPrograms");

        
    }
}