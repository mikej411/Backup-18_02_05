using Browser.Core.Framework;

namespace AMA.AppFramework
{
    public class AssignConfirmationPageCriteria
    {
        public readonly ICriteria<AssignConfirmationPage> ProgramSummaryTableVisible = new Criteria<AssignConfirmationPage>(p =>
        {
            return p.Exists(Bys.AssignConfirmationPage.ProgramSummaryTbl, ElementCriteria.IsVisible,ElementCriteria.IsEnabled);

        }, "Program summary table is not visible ");

        public readonly ICriteria<AssignConfirmationPage> LoadIconNotVisible = new Criteria<AssignConfirmationPage>(p =>
        {
            return p.Exists(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);

        }, "Load Icon Not visible");

        public readonly ICriteria<AssignConfirmationPage> PageReady;

        public AssignConfirmationPageCriteria()
        {
            PageReady = ProgramSummaryTableVisible.AND(LoadIconNotVisible);
        }
    }
}
