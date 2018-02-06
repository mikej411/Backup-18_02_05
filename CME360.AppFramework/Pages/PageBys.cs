using OpenQA.Selenium;

namespace CME.AppFramework
{
    public class PageBys
    {
        // Banners


        // Buttons
        public readonly By SearchBtn = By.Id("Search1_SearchSubmit");

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
        public readonly By RecentItemsTbl = By.XPath("RecentItems1_RecentItemPanel");

        

        // Tabs
        public readonly By HomeTab = By.Id("PortalTabb_PortalTabs_ParentImageTabRptr_ctl00_ParentImageTab");
        public readonly By StakeholdersTab = By.Id("PortalTabb_PortalTabs_ParentImageTabRptr_ctl01_ParentImageTab");
        public readonly By PlanningTab = By.Id("PortalTabb_PortalTabs_ParentImageTabRptr_ctl02_ParentImageTab");
        public readonly By ProjectsTab = By.Id("PortalTabb_PortalTabs_ParentImageTabRptr_ctl03_ParentImageTab");
        public readonly By DistributionTab = By.Id("PortalTabb_PortalTabs_ParentImageTabRptr_ctl04_ParentImageTab");
        public readonly By ParticpantsTab = By.Id("PortalTabb_PortalTabs_ParentImageTabRptr_ctl05_ParentImageTab");
        public readonly By ProcessingTab = By.Id("PortalTabb_PortalTabs_ParentImageTabRptr_ctl06_ParentImageTab");
        public readonly By Outcomes = By.Id("PortalTabb_PortalTabs_ParentImageTabRptr_ctl07_ParentImageTab");




        // Text boxes
        public readonly By SearchTxt = By.Id("Search1_TextBox_SearchField");


        












    }
}