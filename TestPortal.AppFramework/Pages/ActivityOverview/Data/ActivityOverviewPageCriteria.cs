using Browser.Core.Framework;

namespace TP.AppFramework
{
    public class ActivityOverviewPageCriteria
    {
        public readonly ICriteria<ActivityOverviewPage> CreditInfoLblVisible = new Criteria<ActivityOverviewPage>(p =>
        {
            return p.Exists(Bys.ActivityOverviewPage.CreditInfoLbl, ElementCriteria.IsVisible);

        }, "Credit Information label visible");

        public readonly ICriteria<ActivityOverviewPage> PageReady;

        public ActivityOverviewPageCriteria()
        {
            PageReady = CreditInfoLblVisible;
        }
    }
}
