using OpenQA.Selenium;

namespace AMA.AppFramework
{
    public class PGYAssignmentPageBys
    {
        // Main page

          //Labels
        public readonly By CurriculumCoursePGYLbl = By.XPath("//h4[.='Curriculum Course PGY Assignment']");

        //table
        public readonly By CurriculumNameTbl = By.XPath("//table");
        public readonly By CourseTbl = By.Id("gridCurriculumTemplateCoursePGY");
        public readonly By EditCoursePgyTbl = By.Id("gridEditCurriculumCoursePGY");
        public readonly By UltimateTbl = By.XPath("//div[contains  (@ui-grid, 'vm.gridOptions')]");
        //div[@id='gridCurriculumTemplateCoursePGY']/div[2]/div/div[1]==header
        //div[@id='gridCurriculumTemplateCoursePGY']/div[2]/div/div[1]/div/div/div/div/div/div==header cells
        //div[@id='gridCurriculumTemplateCoursePGY']/div[2]/div/div[2]/div/div==rows
        //div[@id='gridCurriculumTemplateCoursePGY']/div[2]/div/div[2]/div/div/div/div==cellvalue
        //div[@id='gridCurriculumTemplateCoursePGY']/div[2]/div/div[2]/div/div[1]/div/div[8]==first row 8cell      

        //Buttons
        public readonly By CancelBtn = By.XPath("//button[.='Cancel']");
        public readonly By SaveExitBtn = By.XPath("//button[.='Save & Exit']");
        public readonly By NextBtn = By.XPath("//button[.='Next']");
        public readonly By SaveandContinue = By.XPath("//button[.='Save & Continue']"); 

    }
}