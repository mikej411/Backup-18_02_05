using OpenQA.Selenium;

namespace AMA.AppFramework
{
    public class InstitutionsPageBys
    {
        //Main page

        //Input Box
        public readonly By SearchTxt = By.XPath("//input[contains(@placeholder,'Search')]"); 

        //Links
        public readonly By CreateInstitutionsLnk = By.LinkText("Create Institution");
        public readonly By EditInstitutionLnk = By.LinkText("Edit Institution");
        public readonly By MarkInactiveLnk = By.LinkText("Mark Inactive");
        public readonly By CreateProgramLnk = By.LinkText("Create Program");

        //Table 
        public readonly By InstitutionsTbl = By.Id("gridInstitutions");

        //Button
        public readonly By SearchBtn = By.XPath("//*[contains(@class,'glyphicon glyphicon-search')]");
        public readonly By CancelBtn = By.XPath("//button[.='Cancel']");
        public readonly By AcceptBtn = By.XPath("//button[.='OK']");
        public readonly By DismissBtn = By.XPath("//button[.='Cancel']");
        public readonly By ActionGearBtn = By.XPath("//button[@class='btn btn-default ins-admin-btn-cp']");
    }
}