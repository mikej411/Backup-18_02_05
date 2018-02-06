using Browser.Core.Framework;

namespace AMA.AppFramework
{
    public class GCEPPageCriteria
    {
        public readonly ICriteria<GCEPPage> WaitContainer = new Criteria<GCEPPage>(p =>
        {
            return p.Exists(Bys.GCEPPage.ContainerToWait, ElementCriteria.IsVisible,ElementCriteria.IsEnabled,ElementCriteria.IsVisible);

        }, "Institution link text visible");

        public readonly ICriteria<GCEPPage> LoadIconAppear = new Criteria<GCEPPage>(p =>
        {
            return p.Exists(Bys.AMAPage.LoadIcon, ElementCriteria.AttributeValue("class", "page-splash dissolve-animation ng-hide"));
        }, "Load icon disappeared");

        public readonly ICriteria<GCEPPage> SendEmailNotificationLnkEnabledVisible = new Criteria<GCEPPage>(p =>
         {
             return p.Exists(Bys.GCEPPage.SendEmailNotificationLnk, ElementCriteria.IsEnabled, ElementCriteria.IsVisible);

         }, "Send Email Notification link enabled and visible");

        public readonly ICriteria<GCEPPage> ResidentCourseTableVisible = new Criteria<GCEPPage>(p =>
        {
            return p.Exists(Bys.GCEPPage.ResidentCourseTbl, ElementCriteria.IsVisible);

        }, "Resident course table visible");


        public readonly ICriteria<GCEPPage> LoadingComplete;

        public GCEPPageCriteria()
        {
            LoadingComplete = LoadIconAppear;
            //.OR(SendEmailNotificationEnabled);

        }
    }
}
