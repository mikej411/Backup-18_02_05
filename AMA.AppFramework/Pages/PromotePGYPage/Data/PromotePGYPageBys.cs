using OpenQA.Selenium;

namespace AMA.AppFramework
{
    public class PromotePGYPageBys
    {
       //Main page

        // Label
        public readonly By PromotePGYLbl = By.XPath("//h4");       
        
        //Tables
        public readonly By ChoosenResidentsPromotePGYTbl = By.XPath("//*[@id='gridPromotePGY1']");
        public readonly By AvailableResidentsPromotePGYTbl = By.Id("gridPromotePGY2");
        public readonly By FormResidentsDescriptionTbl = By.XPath("//table[@class='table table-striped']");


        
        //Buttons
        public readonly By RemoveSelectedBtn = By.XPath("//button[contains(text() , 'Remove Selected')]");
        public readonly By RemoveAllBtn = By.XPath("//button[contains(text() , 'Remove All')]");
        public readonly By AddSelectedBtn = By.XPath("//button[contains(text() , 'Add Selected')]");
        public readonly By AddAllBtn = By.XPath("//button[contains(text() , 'Add All')]");
        public readonly By CancelBtn = By.XPath("//button[.='Cancel']");
        public readonly By PromoteBtn = By.XPath("//button[.='Promote']");
        public readonly By FormConfirmBtn = By.XPath("//button[.='Confirm']");
        public readonly By FormCloseBtn = By.XPath("//button[.='Close']");

        //Check Box
        public readonly By AvailableResidentsPromotePGYTblFirstRowChk = By.XPath("//div[@id='gridPromotePGY2']/descendant::div[@role='rowgroup']/descendant::input[2]"); // This is the first row's checkbox from PGY table. We are using this checkbox to wait for the page to load in the PageReady property 




    }
}