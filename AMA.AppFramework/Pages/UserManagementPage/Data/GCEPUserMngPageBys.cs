using OpenQA.Selenium;

namespace AMA.AppFramework
{
    public class GCEPUserMngPageBys
    {
        // Main page

        //Labels
        public readonly By UserManagementLbl = By.XPath("//h4");
        public readonly By NoRecorMatchLbl = By.XPath("//div[@class='no-data-watermark ng-scope']");
        public readonly By StatusLbl = By.XPath("//label[.='Status']");

        //Dropdown Select
        public readonly By StatusSelElem = By.XPath("//div[@class='usr-admin-grid-header-item']/label[.='Status']/../select");
        public readonly By FilterBySelElem = By.XPath("//div[@class='usr-admin-grid-header-item']/label[.='Filter By']/../select");

        //Input Box
        public readonly By SearchTxt = By.XPath("//input[contains(@placeholder,'Search')]");  
        
        //Table
        public readonly By UsersManagementTbl = By.Id("gridUserManagement");

        //Button
        public readonly By CancelBtn = By.XPath("//button[.='CANCEL']");
        public readonly By CancelSendBtn = By.XPath("//button[.='Cancel']");
        public readonly By AcceptBtn = By.XPath("//button[.='OK']");
        public readonly By DismissBtn = By.XPath("//button[.='Cancel']");
        public readonly By ActionGearBtn = By.XPath("//button[@class='btn btn-default ins-admin-btn-cp']");
        public readonly By SearchBtn = By.XPath("//*[contains(@class,'glyphicon glyphicon-search')]");

        //Links     
       // static string xpathVariable =string.Format( "//a[@class='btn primary ng-binding' and contains (text(),'{0}')]", Username);
        public readonly By UserLnk = By.XPath("//a[@class='btn primary ng-binding']");

       
    }
}//div[@class='ui-grid-contents-wrapper']
 //*[@class='ui-grid-render-container ng-isolate-scope ui-grid-render-container-body']