using Browser.Core.Framework;

namespace TP.AppFramework
{
    public class HomePageCriteria
    {
        public readonly ICriteria<HomePage> ActivityListingLblVisible = new Criteria<HomePage>(p =>
        {
            return p.Exists(Bys.HomePage.ActivityListingLbl, ElementCriteria.IsVisible);

        }, "Activity Listing label is visible");

        public readonly ICriteria<HomePage> PageReady;

        public HomePageCriteria()
        {
            PageReady = ActivityListingLblVisible;
        }
    }
}
