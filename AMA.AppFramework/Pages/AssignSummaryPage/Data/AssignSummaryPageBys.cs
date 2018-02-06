using OpenQA.Selenium;

namespace AMA.AppFramework
{
    public class AssignSummaryPageBys
    {
        // Main page

        //button
        public readonly By NextBtn = By.XPath("//button[.='Next']");
        public readonly By BackBtn = By.LinkText("Back");

        //Label
        public readonly By CreatedProgramNameLbl = By.XPath("//h5[@class='ng-binding']");

        //tables
        public readonly By ProgramSummaryTbl = By.Id("gridCurriculumTemplateProgramSummary");
        public readonly By EditProgramSummarytbl = By.Id("gridEditCurriculumTemplateProgramSummary");
       
         

    
    }
}