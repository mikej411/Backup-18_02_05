using OpenQA.Selenium;

namespace AMA.AppFramework
{
    public class InstitutionsGCEPPageBys
    {   
        //Main page

        //Links
        public readonly By InstitutionSendEmailNoticationLnk = By.LinkText("Send email notifications");
        public readonly By InstitutionRunReportLnk = By.LinkText("Run Reports");
        public readonly By InstitutionPromotePgyLnk = By.LinkText("Promote PGY");
        public readonly By InstitutionProgramManagmentLnk = By.LinkText("Program Management");
        public readonly By InstitutionUserManagementLnk = By.LinkText("User Management");
        public readonly By InstitutionCurriculumTmpLnk = By.LinkText("Curriculum Templates");
        public readonly By InstitutionDashboarNotificationManagementLnk = By.Id("Dashboard Notification Management");
        public readonly By DashboardDirectiveLnk = By.Id("InstitutionUserManagementLnk");
        public readonly By TotalProgramCountLnk = By.XPath("//a[contains (@href ,'/programs') and @class='ng-binding']");
        public readonly By TotalUserCountLnk = By.XPath("//a[contains (@href ,'/users') and @class='ng-binding']");
        public readonly By TotalCurriculumTemplatesCountLnk = By.XPath("//a[contains (@href ,'/curriculumtemplates') and @class='ng-binding']");
        // public readonly By TotalPastDueAlertsCountLnk = By.XPath("//a[@ui-sref='.alerts({institutionId:$parent.vm.institutionId, alertType:'PastDue'})']");//a[@href='/gme-competency/admin/institutions/d16b7b87-28ff-48bf-84ce-1625912d1fca/curriculumtemplates' and @class='ng-binding']

    }
}