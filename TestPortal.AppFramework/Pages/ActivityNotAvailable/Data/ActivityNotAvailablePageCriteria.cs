using Browser.Core.Framework;

namespace TP.AppFramework
{
    public class ActivityNotAvailablePageCriteria
    {
        public readonly ICriteria<ActivityNotAvailablePage> ActivityNotAvailLblVisible = new Criteria<ActivityNotAvailablePage>(p =>
        {
            return p.Exists(Bys.ActivityNotAvailablePage.ActivityNotAvailLbl, ElementCriteria.IsVisible);

        }, "Activity Not Available label is visible");

        public readonly ICriteria<ActivityNotAvailablePage> PageReady;

        public ActivityNotAvailablePageCriteria()
        {
            PageReady = ActivityNotAvailLblVisible;
        }
    }
}
