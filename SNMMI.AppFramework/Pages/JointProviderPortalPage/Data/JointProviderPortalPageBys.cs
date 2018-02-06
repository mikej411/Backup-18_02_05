using OpenQA.Selenium;

namespace SNMMI.AppFramework
{
    /// <summary>
    /// Elements that will exist on the login page
    /// </summary>
    public class JointProviderPortalPageBys
    {     

        // Buttons


        // Charts

        // Check boxes
        public readonly By RememberMeChk = By.Id("");

        // Labels                                              

        // Links

        // Menu Items    

        // Radio buttons

        // Tables       

        // Tabs
        public readonly By FacultyTab = By.XPath("//span[@data-bind='text: name']");
       

        // Text boxes
        public readonly By SearchBoxTxt = By.XPath("//input[@placeholder='Enter search criteria']");

    }
}