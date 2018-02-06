using Browser.Core.Framework;

namespace AMA.AppFramework
{
    public class PGYAssignmentPageCriteria
    {
        public readonly ICriteria<PGYAssignmentPage> AnyTableVisible = new Criteria<PGYAssignmentPage>(p =>
        {
            return p.Exists(Bys.PGYAssignmentPage.UltimateTbl, ElementCriteria.IsVisible,ElementCriteria.IsEnabled);

        }, "Institution table is visible");

        public readonly ICriteria<PGYAssignmentPage> LoadIconAppear = new Criteria<PGYAssignmentPage>(p =>
        {
            return p.Exists(Bys.AMAPage.LoadIcon, ElementCriteria.AttributeValue("class", "page-splash dissolve-animation ng-hide"));
        }, "Load icon disappeared");

        /*  public readonly ICriteria<GCEPPage> SendEmailNotificationEnabled = new Criteria<GCEPPage>(p =>
           {
               return p.Exists(Bys.GCEPPage.SendEmailNotificationLnk, ElementCriteria.IsEnabled);

           }, "Password is enabled");*/

        public readonly ICriteria<PGYAssignmentPage> PageReady;

        public PGYAssignmentPageCriteria()
        {
            PageReady = AnyTableVisible.AND(LoadIconAppear);
            //.OR(SendEmailNotificationEnabled);

        }
    }
}