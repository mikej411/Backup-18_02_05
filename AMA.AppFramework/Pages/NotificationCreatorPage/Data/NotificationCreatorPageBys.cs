using OpenQA.Selenium;

namespace AMA.AppFramework
{
    public class NotificationCreatorPageBys
    {
        //Main page

        // Text boxes  
        public readonly By NotificationNameTxt = By.Id("name");
        public readonly By NotificatioTitleTxt = By.Id("title");
        public readonly By NotificationBodyTxt = By.XPath("//textarea[@id='body']");

        //Labels
        public readonly By DashBoardNotificationLbl = By.XPath("h4");  
        
        // Check Box
        public readonly By AddHyperlinkChk = By.Id("enableUrl");

        //Buttons
        public readonly By SaveExitBtn = By.XPath("//button[.='Save & Exit>>']");
        public readonly By CancelBtn = By.XPath("//button[.='Cancel']");

        // Radio Buttons
        public readonly By AdminsRdo = By.XPath("//*[@id='contentType1']");
        public readonly By ManagersRdo = By.XPath("//*[@id='contentType2']");
        public readonly By ResidentsRdo = By.XPath("//*[@id='contentType3']");


    
    }
}