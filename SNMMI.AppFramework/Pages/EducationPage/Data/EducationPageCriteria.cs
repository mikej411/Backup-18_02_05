using Browser.Core.Framework;

namespace SNMMI.AppFramework
{
    public class EducationPageCriteria
    {
        public readonly ICriteria<EducationPage> MyActivitiesLinkVisible = new Criteria<EducationPage>(p =>
        {
            return p.Exists(Bys.EducationPage.MyActivitiesLnk, ElementCriteria.IsVisible);

        }, "MyActivities Link visible");

        public readonly ICriteria<EducationPage> MyTranscriptLinkVisible = new Criteria<EducationPage>(p =>
        {
            return p.Exists(Bys.EducationPage.MyTranscriptLnk, ElementCriteria.IsVisible);

        }, "MyTranscript Link is visible");


        public readonly ICriteria<EducationPage> PageReady;

        public EducationPageCriteria()
        {
            PageReady = MyActivitiesLinkVisible.AND(MyTranscriptLinkVisible);
        }
    }
}
