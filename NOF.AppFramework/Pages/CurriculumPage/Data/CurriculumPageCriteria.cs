using Browser.Core.Framework;

namespace NOF.AppFramework
{
    public class CurriculumPageCriteria
    {
        public readonly ICriteria<CurriculumPage> CurriculumLabelVisible = new Criteria<CurriculumPage>(p =>
        {
            return p.Exists(Bys.CurriculumPage.CurriculumLbl, ElementCriteria.IsVisible);

        }, "Curriculum Label visible");

        public readonly ICriteria<CurriculumPage> ListActivitesLabelVisible = new Criteria<CurriculumPage>(p =>
        {
            return p.Exists(Bys.CurriculumPage.CurriculumLbl, ElementCriteria.IsVisible);

        }, "Curriculum Label visible");


        public readonly ICriteria<CurriculumPage> PageReady;

        public CurriculumPageCriteria()
        {
            PageReady = CurriculumLabelVisible.AND(ListActivitesLabelVisible);
        }
    }
}
 
