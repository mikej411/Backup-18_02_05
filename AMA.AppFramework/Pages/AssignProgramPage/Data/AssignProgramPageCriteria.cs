using Browser.Core.Framework;

namespace AMA.AppFramework
{
    public class AssignProgramPageCriteria
    {
        public readonly ICriteria<AssignProgramPage> CurriculumTableVisible = new Criteria<AssignProgramPage>(p =>
        {
            return p.Exists(Bys.AssignProgramPage.StartDateCalenderBox, ElementCriteria.IsVisible);

        }, "Curriculum name table visible");

        public readonly ICriteria<AssignProgramPage> LoadIconNotVisible = new Criteria<AssignProgramPage>(p =>
        {
            return p.Exists(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);

        }, "Load Icon Not visible");

        public readonly ICriteria<AssignProgramPage> PageReady;

        public AssignProgramPageCriteria()
        {
            PageReady = CurriculumTableVisible.AND(LoadIconNotVisible);
        }
    }
}
