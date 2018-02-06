using OpenQA.Selenium;


namespace CFPC.AppFramework
{
    public class CPDPlanningPageBys
    {
        //buttons
        public readonly By EnterCPDActBtn = By.XPath("//span[text()='ENTER A CPD ACTIVITY']");
        
        // tabs


        // labels
        public readonly By TotalCreditsValueLbl = By.Id("ctl00_ContentPlaceHolder1_CFPCPointTracker_ctrlPointTracker_lblCycleTotalApp");

        public readonly By MyCreditSummaryLbl = By.Id("ctl00_ContentPlaceHolder1_lblPageTitle");
    }
}
