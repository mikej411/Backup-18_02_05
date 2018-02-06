using Browser.Core.Framework;

namespace TP.AppFramework
{
    public class ActivityDetailsPageCriteria
    {
        public readonly ICriteria<ActivityDetailsPage> ResumeSelectBtnVisible = new Criteria<ActivityDetailsPage>(p =>
        {
            return p.Exists(Bys.ActivityDetailsPage.ResumeSelectBtn, ElementCriteria.IsVisible);

        }, "Resume/Select buton visible");

        public readonly ICriteria<ActivityDetailsPage> PageReady;

        public ActivityDetailsPageCriteria()
        {
            PageReady = ResumeSelectBtnVisible;
        }
    }
}
