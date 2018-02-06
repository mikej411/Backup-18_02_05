using OpenQA.Selenium;

namespace NOF.AppFramework
{
    /// <summary>
    /// Elements that will exist on the login page
    /// </summary>
    public class TranscriptPageBys
    {     

        // Buttons
        public readonly By LogoutBtn = By.LinkText("Logout");
        public readonly By iAcceptBtn = By.Id("");

        // Charts

        // Check boxes
        public readonly By RememberMeChk = By.Id("");

        // Labels                                              
        public readonly By TranscriptLbl = By.XPath("//h2[text()='Transcript' and @class='pageHeader']");

        // Links

        // Menu Items    

        // Radio buttons

        // Tables       

        // Tabs

        // Text boxes
        public readonly By DateFieldTxt = By.Id("ctl00_ContentPlaceHolder1_userTranscript_DateMin_dateInput");
        public readonly By PasswordTxt = By.Id("");



    }
}