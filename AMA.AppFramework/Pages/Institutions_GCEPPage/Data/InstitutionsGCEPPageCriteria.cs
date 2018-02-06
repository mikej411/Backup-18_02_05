using Browser.Core.Framework;

namespace AMA.AppFramework
{
    public class InstitutionsGCEPPageCriteria
    {
        public readonly ICriteria<InstitutionsGCEPPage> AllDashBoard = new Criteria<InstitutionsGCEPPage>(p =>
        {
            return p.Exists(Bys.InstitutionsGCEPPage.DashboardDirectiveLnk, ElementCriteria.IsVisible);

        }, "All Dashboard is  visible");


        public readonly ICriteria<InstitutionsGCEPPage> LoadIconAppear = new Criteria<InstitutionsGCEPPage>(p =>
        {
            return p.Exists(Bys.AMAPage.LoadIcon, ElementCriteria.AttributeValue("class", "page-splash dissolve-animation ng-hide"));
        }, "Load icon disappeared");


        public readonly ICriteria<InstitutionsGCEPPage> PageReady;

        public InstitutionsGCEPPageCriteria()
        {
            PageReady = AllDashBoard.AND(LoadIconAppear);
        }
    }
}
