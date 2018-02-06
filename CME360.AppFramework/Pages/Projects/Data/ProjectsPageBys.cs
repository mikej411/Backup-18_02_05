using OpenQA.Selenium;

namespace CME.AppFramework
{
    /// <summary>
    /// 
    /// </summary>
    public class ProjectsPageBys
    {

        // Buttons
        public readonly By ActivitiesSearchBtn = By.Id("ctl00_btnSearchAct");

        // Charts

        // Check boxes

        // Labels                                              
        public readonly By ManageProjectsLnk = By.XPath("(//span[@id='CtlLink'])[1]/a");
        public readonly By ManageActivitiesLnk = By.XPath("(//span[@id='CtlLink'])[2]/a");


        // Links

        // Menu Items    

        // Radio buttons

        // Tables       
        public readonly By ManageActivitiesTbl = By.ClassName("ccTableAL");
        public readonly By ManageActivitiesTblBodyRow = By.XPath("//table[@class='ccTableAL']/descendant::tr/following-sibling::tr[6]");  // If one row exists, this will represent that row




        // Tabs

        // Text boxes
        public readonly By ActivitiesSearchTxt = By.Id("ctl00_txtSearchAct");

        

    }
}