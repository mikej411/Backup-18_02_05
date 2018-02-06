using OpenQA.Selenium;

namespace AMA.AppFramework
{
    public class AssignConfirmationPageBys
    {
        // Main page
        //button
        public readonly By ConfirmBtn = By.XPath("//button[.='Confirm']");
        public readonly By BackBtn = By.LinkText("Back");
        public readonly By EditConfirmBtn = By.XPath("//button[.='Confirm Curriculum']");

        //tables
        public readonly By ProgramSummaryTbl = By.XPath("//table");

        //text
        public readonly By TimeFrameLbl = By.XPath("//*[text()='Time Frame:']/../span[@class='col-xs-9 copy-edit-val ng-binding']");
        //span[@class='col-xs-9 copy-edit-val']/span
    }
}