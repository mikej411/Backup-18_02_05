using OpenQA.Selenium;

namespace AMA.AppFramework
{
    public class ProgramsPageBys
    {    
        //Main page

        //Table
        public readonly By ProgramMngTbl = By.XPath("//*[@id='gridProgramManagement']");
        public readonly By CurriculumTempDetailsFormTbl = By.XPath("//table[@class='table table-striped']");
       
        //Links
        public readonly By UnassignCurriculumLnk = By.XPath("//a[.='Unassign Curriculum']");
        public readonly By EditCurriculumLnk = By.XPath("//a[.='Edit Curriculum']");
        public readonly By CopyEditsLnk = By.XPath("//a[.='Copy Edits']");

        //Buttons
        public readonly By AcceptBtn = By.XPath("//button[.='OK']");
        public readonly By ActionGearBtn = By.XPath("//button[@ng-click='$parent.grid.appScope.fnToggleActionMenu1(2);$event.stopPropagation();']");
        public readonly By CreateProgramBtn = By.LinkText("Create Program");
        public readonly By ActionBtn = By.XPath("//button[@ng-click='$parent.grid.appScope.fnToggleActionMenu2(0);$event.stopPropagation();']");
        public readonly By FormCloseBtn = By.XPath("//button[.='Close']");

        //Calendar Input
        public readonly By FromDateControlText = By.XPath("//input[@type='text' and @name='fromDate']");
        
        //*[@id='gridProgramManagement']//div[@class='ng-isolate-scope']//a[@style='text-decoration: none;']


    }
}