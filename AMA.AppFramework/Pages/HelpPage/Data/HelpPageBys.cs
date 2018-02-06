using OpenQA.Selenium;

namespace AMA.AppFramework
{
    public class HelpPageBys
    {
        //Main page

        //Label
        public readonly By ContactUsLbl = By.XPath("//h4[.='Contact Us']");
        public readonly By HelpLbl = By.XPath("//h4[.='Help']");

        //Link
        public readonly By ResidentLaunchResourceLnk = By.XPath("//div[contains(text(),'Resident and Fellow User Manual')]/a");
        public readonly By ResidentComingSoonLnk = By.XPath("//div[contains(text(),'Resident and Fellow FAQ')]/span");
        public readonly By ManagerLaunchResourceLnk = By.XPath("//div[contains(text(),'Manager User Manual')]/a");
        public readonly By AdminLaunchResourceLnk = By.XPath("//div[contains(text(),'Administrator User Manual')]/a");
        public readonly By AdminWatchVideoLnk = By.XPath("//div[contains(text(),'Administrator Training')]/a");
        public readonly By AdminFAQLaunchResourceLnk = By.XPath("//div[contains(text(),'Administrator FAQ')]/a");
        public readonly By AMAMemberLaunchResourceLnk = By.XPath("//div[contains(text(),'AMA Member Benefits for Residents')]/a");
        public readonly By AMAResidentLaunchResourceLnk = By.XPath("//div[contains(text(),'AMA Resident & Fellow Section (RFS)')]/a");
        public readonly By JAMALaunchResourceLnk = By.XPath("//div[contains(text(),'The JAMA Network')]/a");
        public readonly By ContactGCEPatAMALnk = By.XPath("//a[.='gcep@ama-assn.org']");
        public readonly By ContactInvolvedInstitutionEmailLnk = By.XPath("//a[.='gcep@ama-assn.org']/../span/a");
    
    }
}