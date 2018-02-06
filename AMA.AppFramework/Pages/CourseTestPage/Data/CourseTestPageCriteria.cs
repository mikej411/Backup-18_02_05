using Browser.Core.Framework;

namespace AMA.AppFramework
{
    public class CourseTestPageCriteria
    {
        public readonly ICriteria<CourseTestPage> ContinueBtnVisible = new Criteria<CourseTestPage>(p =>
        {
            return p.Exists(Bys.CourseTestPage.ContinueBtn, ElementCriteria.IsVisible,ElementCriteria.IsEnabled);

        }, "  visible");

        public readonly ICriteria<CourseTestPage> LoadIconNotVisible = new Criteria<CourseTestPage>(p =>
        {
            return p.Exists(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);

        }, "load icon is visible");

        public readonly ICriteria<CourseTestPage> CountainerToWait = new Criteria<CourseTestPage>(p =>
        {
            return p.Exists(Bys.CourseTestPage.CourseWaitContainer, ElementCriteria.IsVisible,ElementCriteria.IsEnabled);

        }, "load icon is visible");

        public readonly ICriteria<CourseTestPage> CourseCreditInfoVisisble = new Criteria<CourseTestPage>(p =>
        {
            return p.Exists(Bys.CourseTestPage.CourseCreditInfoConatiner, ElementCriteria.IsVisible,ElementCriteria.IsEnabled);

        }, "load icon is visible");

        public readonly ICriteria<CourseTestPage> PageReady;

        public CourseTestPageCriteria()
        {
            PageReady = ContinueBtnVisible.AND(LoadIconNotVisible);
        }
    }
}
