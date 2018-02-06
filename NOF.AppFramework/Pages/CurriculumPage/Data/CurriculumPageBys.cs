using OpenQA.Selenium;

namespace NOF.AppFramework
{
    /// <summary>
    /// Elements that will exist on the login page
    /// </summary>
    public class CurriculumPageBys
    {     

        // Buttons
        public readonly By LogoutBtn = By.LinkText("Logout");
        public readonly By iAcceptBtn = By.Id("");

        // Charts

        // Check boxes
        public readonly By RememberMeChk = By.Id("");

        // Labels                                              
        public readonly By LoginUnsuccessfullWarningLbl = By.Id("");
        //public readonly By PasswordWarningLbl = By.Id("");
        //public readonly By UserNameWarningLbl = By.Id("");
        public readonly By ParticipantLbl = By.Id("ctl00_ContentPlaceHolder1_cntlCurriculum_lblNoResults");
        public readonly By CurriculumLbl = By.XPath("//h2[text()='Curriculum']");
       

        // Links
        public readonly By MyTranscriptLink = By.LinkText("My Transcript");
        public readonly By HomeLnk = By.LinkText("Home");
        // Menu Items    

        // Radio buttons

        // Tables       

        // Tabs
        public readonly By CurriculumTab = By.XPath("//*[@id=\"mainhead\"]/div[2]/ul/li[3]/a");
        public readonly By TranscriptTab = By.XPath("//*[@id=\"mainhead\"]/div[2]/ul/li[4]/a");

        // Text boxes
        public readonly By UserNameTxt = By.Id("");
        public readonly By PasswordTxt = By.Id("");



    }
}