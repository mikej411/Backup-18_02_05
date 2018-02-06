using OpenQA.Selenium;

namespace AMA.AppFramework
{
    public class DashboardNotificationsPageBys
    {
        // Main page

        // Table 
        public readonly By NotifacionsMngTbl = By.Id("gridNotificationsManagement");
       
        //Labels
        public readonly By DashboardLbl = By.LinkText("Dashboard Notifications Management");
      
        // Dropdown Select
        public readonly By SearchBySelElem = By.Name("singleSelect");

        //Buttons
        public readonly By CreateNotificationBtn = By.XPath("//button[.='Create Notification']");
        public readonly By GearActionBtn = By.XPath("//button[@class='btn btn-default ins-admin-btn-cp']");
        public readonly By AcceptFormBtn = By.XPath("//button[.='OK']");
        public readonly By DismissFormBtn = By.XPath("//button[.='Cancel']");

        //Links
        public readonly By EditLnk = By.XPath("//a/span[.='Edit']");
        public readonly By RemoveLnk = By.XPath("//a/span[.='Remove']");

    
    }
}