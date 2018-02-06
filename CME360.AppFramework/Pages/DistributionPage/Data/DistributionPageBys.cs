using OpenQA.Selenium;

namespace CME.AppFramework
{
    /// <summary>
    /// Elements that will exist on the login page
    /// </summary>
    public class DistributionPageBys
    {

        // Buttons
        //public readonly By SaveBtn = By.XPath("//input[@name='ctl00$btnSave']");

        // Charts

        // Check boxes

        // Labels                                              
        public readonly By FilterByLbl = By.XPath("//strong[text()='Filter By: ']");
        public readonly By CatalogLibraryLbl = By.XPath("//div[@id='ctl00_pnlHeading']/descendant::span[1]");

        // Links
        public readonly By CatalogsLnk = By.XPath("//div[@id='ctl00_ctl01_pnlMenuControl']/descendant::a[2]");
       
        public readonly By PortalsLnk = By.XPath("//table[@cellspacing='4']//tr[2]//td[2]//span//a[1]");
        public readonly By AddNewPortalLnk = By.XPath("//strong[text()='Add New Portal']");
        public readonly By TCLnk = By.XPath("//a[@class='Normal']");

        

     
        // Menu Items    

        // Radio buttons

        // Tables   
        public readonly By PortalTbl = By.Id("ctl00_distributionSourceGrid");
        public readonly By PortalTblBody = By.Id("//table[@id='ctl00_distributionSourceGrid']/tbody");
        public readonly By PortalTblBodyRow = By.XPath("//table[@id='ctl00_distributionSourceGrid']/tbody/tr[2]");  // Represents the first row in the table, if there are any rows appearing




        // Tabs


        // Text boxes




    }
}