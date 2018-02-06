using OpenQA.Selenium;

namespace SNMMI.AppFramework
{
    /// <summary>
    /// Elements that will exist on every single page of the SNMMI Application
    /// </summary>
    public class SNMMIPageBys
    {
        // The load icon. We are actually getting the parent of the load icon here. This is because the parent element
        // has a class attribute value that populates with "ng-hide" whenever a page loads fully

        
        // Charts

        // Check boxes

        // Labels

        // Links
        public readonly By LogoutLnk = By.LinkText("Log Out");


        //Menu Items    


        // Radio buttons

        // Tables       

        // Tabs
       


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