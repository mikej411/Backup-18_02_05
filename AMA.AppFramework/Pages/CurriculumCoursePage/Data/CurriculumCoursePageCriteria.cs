using Browser.Core.Framework;

namespace AMA.AppFramework
{
    public class CurriculumCoursePageCriteria
    {
        public readonly ICriteria<CurriculumCoursePage> StatusLabelVisible = new Criteria<CurriculumCoursePage>(p =>
        {
            return p.Exists(Bys.CurriculumCoursePage.CurrulumCourseHeaderLbl, ElementCriteria.IsVisible);

        }, "Status label is not visible");
        
        public readonly ICriteria<CurriculumCoursePage> LoadIconAppear = new Criteria<CurriculumCoursePage>(p =>
        {
            return p.Exists(Bys.AMAPage.LoadIcon, ElementCriteria.AttributeValue("class", "page-splash dissolve-animation ng-hide")
                .OR(ElementCriteria.AttributeValue("class", "page-splash dissolve-animation ng-animate ng-hide")));
        }, "Load icon disappeared");

        public readonly ICriteria<CurriculumCoursePage> AvailableCourseTblEnabled = new Criteria<CurriculumCoursePage>(p =>
        {
            return p.Exists(Bys.CurriculumCoursePage.AvailableCoursesTbl, ElementCriteria.IsVisible,ElementCriteria.IsEnabled);

        }, "Avalaible course table Enabled and visible");

        public readonly ICriteria<CurriculumCoursePage> PageReady;

        public CurriculumCoursePageCriteria()
        {
            PageReady = StatusLabelVisible.AND( LoadIconAppear).AND(AvailableCourseTblEnabled);
            
        }
    }
}
