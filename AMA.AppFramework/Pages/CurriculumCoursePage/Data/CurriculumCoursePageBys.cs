using OpenQA.Selenium;

namespace AMA.AppFramework
{
    public class CurriculumCoursePageBys
    {
        // Main page

        //Label
        public readonly By CurrulumCourseHeaderLbl = By.XPath("//h4[@class='ng-binding']");
        public readonly By CurrentDateWarningLbl = By.XPath("//div[@ng-message='invalidCurrent']");
        public readonly By GreaterDateWarningLbl = By.XPath("//div[@class='errorMessage ng-binding']");

        //InputBox
        public readonly By CurriculumNameTxt = By.XPath("//input[@name='curriculumTemplateName']");        
       
        //Tables
        public readonly By ChosenCoursesTbl = By.Id("gridCurriculumTemplateChosenCourses");       
        public readonly By AvailableCoursesTbl = By.Id("gridCurriculumTemplateAvailableCourses");
        
        //Buttons
        public readonly By RemoveSelectedBtn = By.XPath("//button[contains(text(),'Remove Selected')]");
        public readonly By AddSelectedBtn = By.XPath("//button[contains(text(),'Add Selected')]");
        public readonly By CancelBtn = By.XPath("//button[contains(text(),'Cancel')]");
        public readonly By SaveFinishLaterBtn = By.XPath("//button[contains(text(),'Save & Finish Later')]");
        public readonly By NextBtn = By.XPath("//button[contains(text(),'Next')]");
        
       
    }
}