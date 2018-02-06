using OpenQA.Selenium;

namespace AMA.AppFramework
{
    public class CopyCurriculumEditsPageBys
    {
        // Main page

        //Label
        public readonly By ProgramNameLbl = By.XPath("//span[.='Anesthesiology']");       
        public readonly By CurriculumNameLbl = By.XPath("//*[text()='Created from the template:']/span");
        public readonly By TimeFrameLbl = By.XPath("//*[text()='Time Frame:']/span");

        //Table will change
        public readonly By CopyEditProgramTbl = By.Id("gridCopyCurriculum");

        //Buttons
        public readonly By NextBtn = By.XPath("//Button[.='Next']");
        
    
    }
}