using OpenQA.Selenium;

namespace AMA.AppFramework
{
    public class AMAPageBys
    {
        // Header

        //Menu Items     
        public readonly By Menu_MyLibrary = By.XPath("//span[./text()='My Library']");
        public readonly By Menu_MyCPDActivitiesList = By.XPath("//span[./text()='My CPD Activities List']");

        //Icon
        public readonly By LoadIcon = By.XPath("//div[@class='cube']/../../../..");

        //Link       
        public readonly By SignOutLnk = By.XPath("//a[@target='_self'and contains(text(), 'Sign Out')]"); //LinkText("Sign Out")
        public readonly By BreadCrumbLnksContainer = By.XPath("//ol[@class='breadcrumb ng-isolate-scope']");// this container dynamic links starting appear from gcep and will keep update as much as you dig in too app
        public readonly By AdministrationLnk = By.LinkText("Administration");
        public readonly By TranscriptLnk = By.Id("transcriptTabGCE");
        public readonly By LibraryLnk = By.Id("libraryTabGCE");
        public readonly By HelpLnk = By.Id("helpTabGCE"); //a[@href='/gme-competency/help' and contains (text(),'Help from Your Institution')]
        public readonly By GMECompetencyEducationProgramLnk = By.Id("gce-link");                                      //a[@href='/gme-competency']);
        public readonly By HelpfromYourInstitutionLnk = By.XPath("//a[@href='/gme-competency/help' and contains (text(),'Help from Your Institution')]");
        public readonly By FaceBookLnk = By.XPath("//a[@title='Facebook']");

        //Button
        public readonly By SearchBtn = By.XPath("//*[contains(@class,'glyphicon glyphicon-search')]");
        public readonly By GCEPNotificationsBtn = By.XPath("//strong[@class='ng-binding' and contains(text(),'Notifications')]");

        //input Box
        public readonly By SearchTxt = By.XPath("//input[contains(@placeholder,'Search')]");

        //Label
        public readonly By CountTableItemLbl = By.XPath("//span[@class='ng-binding']");//this xpath shows actuall counts of items on  grid.
        public readonly By NotificationTitlesLbl = By.XPath("//li[@class='notification-list ng-scope']");

        //Chart
        public readonly By ResidentsChrt = By.Id("chartTrainees");

        // Ask Mike
        public readonly By AMALogoLnk = By.XPath("//a[@title='Home']"); //this xpath using for return to the GCEP page 
        public readonly By HeaderMenuDropDown = By.XPath("//*[@id='gmeHeaderRightRibbonUserMenu']/a/span");

        // Locator examples
        //public readonly By Menu_About = By.XPath("//li[@id='menu-item-1155']/a");

        //public readonly By Menu_FunctionalTesting = By.XPath("//li[@id='menu-item-1150']/a");
        //public readonly By Menu_FunctionalTesting_BDDSpecFlow = By.XPath("//li[@id='menu-item-1154']/a");

        // This XPath line selects the first TD element with the exact text
        //string xPathVariable = "//td[./text()='yourtext']";
        //string xPathVariable = "//td[contains(text(),'yourtext')]";
        //string xPathVariable = string.Format("//div[contains(.,'{0}')]", textOfCell);
        //IWebElement TDCell = gridElem.FindElement(By.XPath(xPathVariable));

        // Mulitple elements or multiple attributes
        //string xpathString = string.Format("//span[text()='{0}' and @class=\"ui-iggrid-headertext\"]", textOfHeaderCell);

        // Attribute does not equal
        //IWebElement lists = Browser.FindElement(By.CssSelector("li:not([class=hidden])"));

        //div[contains (text(),'Rubey, Kathryn')]/../../..//a---on alerts page users name;

    }
}