using Browser.Core.Framework;

namespace AMA.AppFramework
{
    public class CurriculumMngPageCriteria
    {
        public readonly ICriteria<CurriculumMngPage> StatusLabelVisible = new Criteria<CurriculumMngPage>(p =>
        {
            return p.Exists(Bys.CurriculumMngPage.CurrulumTemplatesHeader, ElementCriteria.IsVisible);

        }, "Reset Filter Button enabled");
        
        public readonly ICriteria<CurriculumMngPage> LoadIconAppear = new Criteria<CurriculumMngPage>(p =>
        {
            return p.Exists(Bys.AMAPage.LoadIcon, ElementCriteria.AttributeValue("class", "page-splash dissolve-animation ng-hide")
                .OR(ElementCriteria.AttributeValue("class", "page-splash dissolve-animation ng-animate ng-hide")));
        }, "Load icon disappeared");


        public readonly ICriteria<CurriculumMngPage> PageReady;

        public CurriculumMngPageCriteria()
        {
            PageReady = StatusLabelVisible.AND( LoadIconAppear);
            
        }
    }
}
