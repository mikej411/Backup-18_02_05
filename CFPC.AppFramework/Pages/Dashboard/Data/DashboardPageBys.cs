using OpenQA.Selenium;


namespace CFPC.AppFramework
{
    public class DashboardPageBys
    {
        //buttons for page
        public readonly By EnterCPDActBtn = By.Id("ctl00_ContentPlaceHolder1_lnkAddActivity");
        public readonly By EULAButton = By.Id("ctl00_EULAControl1_btnEulaAgree");

        //tabs on page


        //Total Applied Credits Value in table
        public readonly By TotalCreditsValueLbl = By.Id("ctl00_ContentPlaceHolder1_CFPCPointTracker_ctrlPointTracker_lblCycleTotalApp");                                                      
        public readonly By MyCreditSummarySpan = By.Id("ctl00_ContentPlaceHolder1_lblPageTitle");


        //maximum credit standings link
        public readonly By TotalCreditsLinkLnk = By.XPath("//*[@id=\"ctl00_ContentPlaceHolder1_CFPCPointTracker_ctrlPointTracker_maxCreditLinkContainer\"]/a");
                                                      
        public readonly By AppliedAMACreditsLbl = By.Id("ctl00_ContentPlaceHolder1_CFPCPointTracker_ctrlPointTracker_RadWindow_ContentTemplate_C_lblAMAPRACreditsApplied");

        
    }
}
