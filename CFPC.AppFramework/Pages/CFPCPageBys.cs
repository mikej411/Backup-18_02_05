using OpenQA.Selenium;


namespace CFPC.AppFramework
{
    /// <summary>
    /// Elements that will exist on every single page of the RCP Application
    /// </summary>
    public class CFPCPageBys
    {
        // The load icon. We are actually getting the parent of the load icon here. This is because the parent element
        // has a class attribute value that populates with "ng-hide" whenever a page loads fully
        public readonly By LoadIcon = By.XPath("//div[@class='loader']/..");

        // Buttons

        // Charts

        // Check boxes

        // Labels

        // Links
        public readonly By LogoutLnk = By.LinkText("Log Out");

        //Menu Items    
        public readonly By Menu_MyDashboard = By.XPath("//span[./text()='My Dashboard']");
        public readonly By Menu_MyCPDActivitiesList = By.XPath("//span[./text()='My CPD Activities List']");

        // Radio buttons

        // Tables       

        // Tabs
        public readonly By DashboardTab = By.XPath("//*[@id=\"ctl00_MainTabs\"]/div/ul/li[1]/a/span/span/span");
        public readonly By CreditSummaryTab = By.XPath("//span[text()='Credit Summary']");
        public readonly By HoldingAreaTab = By.XPath("//span[text()='Holding Area']");
        public readonly By CPDActivitiesTab = By.XPath("//span[text()='CPD Activities List']");
        public readonly By CPDPlanningTab = By.XPath("//span[text()='CPD Planning']");
        public readonly By ReportsTab = By.XPath("//span[text()='Reports']");

        // Text boxes














        // Locator examples
        //public readonly By Menu_About = By.XPath("//li[@id='menu-item-1155']/a");

        //public readonly By Menu_FunctionalTesting = By.XPath("//li[@id='menu-item-1150']/a");
        //public readonly By Menu_FunctionalTesting_BDDSpecFlow = By.XPath("//li[@id='menu-item-1154']/a");

        // This XPath line selects the first TD element with the exact text
        //string xPathVariable = "//td[./text()='yourtext']";
        //string xPathVariable = "//td[contains(text(),'yourtext')]";
        //string xPathVariable = string.Format("//div[contains(.,'{0}')]", textOfCell);
        //IWebElement TDCell = gridElem.FindElement(By.XPath(xPathVariable));

        // Mulitple elements or multiple attributes
        //string xpathString = string.Format("//span[text()='{0}' and @class=\"ui-iggrid-headertext\"]", textOfHeaderCell);

        // Attribute does not equal
        //IWebElement lists = Browser.FindElement(By.CssSelector("li:not([class=hidden])"));



    }
}
