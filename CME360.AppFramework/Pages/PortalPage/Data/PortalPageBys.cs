using OpenQA.Selenium;

namespace CME.AppFramework
{
    /// <summary>
    /// Elements that will exist on the login page
    /// </summary>
    public class PortalPageBys
    {

        // Buttons
        //public readonly By SaveBtn = By.XPath("//input[@name='ctl00$btnSave']");

        // Charts

        // Check boxes

        // Labels                                              


        // Links
        public readonly By TermsAndConditionsLnk = By.XPath("//a[@class='Normal']");

        // Menu Items    

        // Radio buttons

        // Tables   
        public readonly By CatAndActTabSelCatalogTbl = By.XPath("//a[@class='TabLinkColor']");
        public readonly By PortalTbl = By.XPath("//table[@cellpadding='3']");
        public readonly By PortalTblBody = By.XPath("//table[@cellpadding='3']/tbody");
        public readonly By PortalTblBodyRow = By.XPath("//table[@cellpadding='3']/tbody/tr[3]");

        public readonly By PortalTbl2 = By.XPath("//td[@id='ContentPane']/table[1]/tbody[1]/tr[2]/td[1]/table[1]/tbody/tr/td/table/tbody/tr[2]/td/table/tbody/tr/td/table[1]");
        public readonly By PortalTblBody2 = By.XPath("//td[@id='ContentPane']/table[1]/tbody[1]/tr[2]/td[1]/table[1]/tbody/tr/td/table/tbody/tr[2]/td/table/tbody/tr/td/table[1]/tbody");
        public readonly By PortalTblBodyRow2 = By.XPath("//td[@id='ContentPane']/table[1]/tbody[1]/tr[2]/td[1]/table[1]/tbody/tr/td/table/tbody/tr[2]/td/table/tbody/tr/td/table[1]/tbody/tr[4]");

        // Tabs
        public readonly By CatAndActTab = By.XPath("//a[@class='TabLinkColor']");


        // Text boxes




    }
}