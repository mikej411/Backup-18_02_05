using OpenQA.Selenium;

namespace AMA.AppFramework
{
    public class GCEPPageBys
    {     
        //Main page

        //Links
        public readonly By SendEmailNotificationLnk = By.LinkText("Send email notifications");
        public readonly By CreateCurriculumTemplatesLnk = By.XPath("//a[@href='/gme-competency/admin/createcurriculumtemplate']");
        public readonly By RunReportLnk = By.LinkText("Run reports");
        public readonly By InstitutionManagLnk = By.LinkText("Institution Management");
        public readonly By UserManageLnk = By.LinkText("User Management");
        public readonly By CurriculumTemplatesLnk = By.XPath("//a[.='Curriculum Templates']");
        public readonly By DashboardNotificationManageLnk = By.XPath("//a[@href='/gme-competency/admin/dashboardcontent']");
        public readonly By InstitutionCountLnk = By.XPath("//a[@href='/gme-competency/admin/institutions']");
        public readonly By TotalCurriculumTemplateCountLnk = By.XPath("//a[@href='/gme-competency/admin/curriculumtemplates']");
        public readonly By TotalUsersCountLnk = By.XPath("//a[@href='/gme-competency/admin//users']");
        public readonly By MyRequiredCourseLnk = By.LinkText("My Required Courses");
        public readonly By TranscriptLnk = By.XPath("//a//span[.='Transcript']");
        public readonly By LibraryLnk = By.XPath("//a//span[.='Library']");         
        public readonly By BreadCrumpLnkContainer = By.XPath("//ol[@class='breadcrumb ng-isolate-scope']");
        public readonly By PromotePGYLnk = By.LinkText("Promote PGY");
        public readonly By ResidentGcepShowElectiveCourseLnk = By.XPath("//span[.='Explore Elective Courses']");
        public readonly By MemberBenefitsManagementLnk = By.XPath("//a[@href='/gme-competency/admin/gme-competency/crosssells']");

        //Header
        public readonly By InstitutionLogoImg = By.XPath("//*[@id='institution-logo']");

        //Container
        public readonly By ContainerToWait = By.XPath("//div[@class='container body-contianer']");

        //Select
        public readonly By ProgramSelElem = By.Name("programSelect");
        public readonly By InstitutionSelElem = By.Name("institutionSelect");
        public readonly By ResidentGCEPAcedimicYearSelElem = By.Name("academic-year-dropdown");

        //Label
        public readonly By DashBoardDirectiveLbl = By.XPath("//div[@class='panel-collapse collapse acc-panel-body-resident in']");//ol[@class='breadcrumb ng-isolate-scope']//li/span[@class='ng-binding ng-scope']
        public readonly By ResidentDashboardDirectiveLbl = By.XPath("//div[@class='panel-collapse collapse acc-panel-body-resident in']");
        //div[@class='container ng-scope']/div
        public readonly By ResidentCourseTrackerLbl = By.Id("course-tracker");
        public readonly By ResidentCourseStatusLockedLbl = By.XPath("//div[@class='col-xs-12 col-md-2 activity-status-action']/button/../span[@class='locked']");
        public readonly By ResidentCoutseStatusFailedLbl = By.XPath("//div[@class='col-xs-12 col-md-2 activity-status-action']/button/../span[@class='failed-attempt ng-binding']");
        public readonly By ResidentCourseCompletionDatesLbl = By.XPath("//div[@class='col-xs-12 col-md-2 activity-status-action']/button/../span[@class='completed-date ng-binding']/br");
        public readonly By ResidentNoCourseBeenCompltetLbl = By.Id("none-completed-message");

        //Table
        public readonly By MyRegiuredCourseTbl = By.XPath("//div[@id='stateViewDiv']");
        public readonly By ResidentCourseTbl = By.XPath("//div[@class='row activity-listing-top']");//div[@class='col-xs-12']//div[@class='row']");//this element for waiting criteria on GCEp page when login as a Resident.

        //Button      
        public readonly By ResidentGCEPSortBYDueDateBtn = By.XPath("//label[.='Date Due']");
        public readonly By ResidentGCEPSortBYDurationBtn = By.XPath("//label[.='Duration']");                                  //input[@id='duration']");
        public readonly By ResidentGCEPSortBYProgressBtn = By.XPath("//label[.='Progress']");
        public readonly By ResidentGCEPCompletedSwitchBtn = By.XPath("//div[@class='toggle']/div/label");
        public readonly By ResidentGCEPProgramAdminSwitchBtn = By.Id("completed-switch");      
        public readonly By ResidentGCEPTranscriptBtn = By.XPath("//button[@class='ama-btn secondary' or @class='ama-btn secondary disabled']");
        public readonly By ResidentGCEPCertificatesDownloadBtn = By.XPath("//button[@class='ama-btn secondary hidden-xs']");
        public readonly By ResidentCouseStartNowBtn = By.XPath("//button[contains(text(),'Start Now')]");
        public readonly By AdminSwitchBtn = By.XPath("//label[@class='toggle-switch-background']");

    }
}