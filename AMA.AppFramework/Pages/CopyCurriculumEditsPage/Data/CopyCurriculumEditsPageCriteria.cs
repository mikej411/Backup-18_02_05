using Browser.Core.Framework;

namespace AMA.AppFramework
{
    public class CopyCurriculumEditsPageCriteria
    {
        public readonly ICriteria<CopyCurriculumEditsPage> TableVisisble = new Criteria<CopyCurriculumEditsPage>(p =>
        {
            return p.Exists(Bys.CopyCurriculumEditsPage.CopyEditProgramTbl, ElementCriteria.IsVisible,ElementCriteria.IsEnabled);

        }, "Copy Edit Table is not visible");

        public readonly ICriteria<CopyCurriculumEditsPage> CurriculumNameVisible = new Criteria<CopyCurriculumEditsPage>(p =>
        {
            return p.Exists(Bys.CopyCurriculumEditsPage.CurriculumNameLbl, ElementCriteria.IsVisible);

        }, "Curriculum name text is not visible");
        public readonly ICriteria<CopyCurriculumEditsPage> LoadIconNotVisible = new Criteria<CopyCurriculumEditsPage>(p =>
        {
            return p.Exists(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);

        }, "spalash is not visible");

        public readonly ICriteria<CopyCurriculumEditsPage> PageReady;

        public CopyCurriculumEditsPageCriteria()
        {
            PageReady = TableVisisble.AND(CurriculumNameVisible).AND(LoadIconNotVisible);
        }
    }
}
