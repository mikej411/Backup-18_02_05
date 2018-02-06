using Browser.Core.Framework;

namespace AMA.AppFramework
{
    public class AssignSummaryPageCriteria
    {
        public readonly ICriteria<AssignSummaryPage> ProgramSummaryTableVisible = new Criteria<AssignSummaryPage>(p =>
        {
            return p.Exists(Bys.AssignSummaryPage.ProgramSummaryTbl, ElementCriteria.IsVisible,ElementCriteria.IsEnabled);

        }, "Program summary table not visible");

        public readonly ICriteria<AssignSummaryPage> LoadIconNotVisible = new Criteria<AssignSummaryPage>(p =>
        {
            return p.Exists(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);

        }, "Load Icon Not visible");

        public readonly ICriteria<AssignSummaryPage> PageReady;

        public AssignSummaryPageCriteria()
        {
            PageReady = ProgramSummaryTableVisible.AND(LoadIconNotVisible);
        }
    }
}
