using OpenQA.Selenium;

namespace TP.AppFramework
{
    public class PageBys
    {
        // Banners


        // Buttons
        public readonly By SearchBtn = By.Id("ctl00_txtSearch");

        // Charts

        // Check boxes

        // Frames



        // Labels    

        // Links


        // public readonly By LogoutLnk = By.LinkText("Log Out");
        public readonly By LogoutLnk = By.XPath("//a[@title='Logout']");

        //Menu Items    


        // Radio buttons

        // Tables       

        // Tabs
        public readonly By HomeTab = By.XPath("//span[text()='home']");


        // Text boxes
        public readonly By SearchTxt = By.Id("ctl00_txtSearch");

        
            











    }
}