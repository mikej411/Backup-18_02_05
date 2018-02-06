using Browser.Core.Framework;

namespace AMA.AppFramework
{
    public class EducationCenterPageCriteria
    {
        public readonly ICriteria<EducationCenterPage> MyCoursesTitle = new Criteria<EducationCenterPage>(p =>
        {
            return p.Exists(Bys.EducationCenterPage.AmaDropdownMenuLnk, ElementCriteria.IsEnabled);

        }, "My Courses Title visible");

        public readonly ICriteria<EducationCenterPage> LoadIconAppear = new Criteria<EducationCenterPage>(p =>
        {
            return p.Exists(Bys.AMAPage.LoadIcon, ElementCriteria.AttributeValue("class", "page-splash dissolve-animation ng-hide")
                .OR(ElementCriteria.AttributeValue("class", "page-splash dissolve-animation ng-animate ng-hide")));
        }, "Load icon disappeared");


        public readonly ICriteria<EducationCenterPage> PageReady;

        public EducationCenterPageCriteria()
        {
            PageReady = MyCoursesTitle.AND(LoadIconAppear);
        }
    }
}
