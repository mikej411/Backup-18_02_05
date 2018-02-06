using Browser.Core.Framework;

namespace LS.AppFramework
{
    public class ProgramPageCriteria
    {
        public readonly ICriteria<ProgramPage> SelfReportActTabValidActivityTblBodyVisible = new Criteria<ProgramPage>(p =>
        {
            return p.Exists(Bys.ProgramPage.SelfReportActTabActivityTblBody, ElementCriteria.IsVisible);

        }, "Self Report Activities tab, Activity table body visible");

        public readonly ICriteria<ProgramPage> ProgramAdjustmentsActivityTblBodyRowVisible = new Criteria<ProgramPage>(p =>
        {
            return p.Exists(Bys.ProgramPage.ProgramAdjustmentsActivityTblBodyRow, ElementCriteria.IsVisible);

        }, "Program Adjustments tab table body row visible");

        public readonly ICriteria<ProgramPage> ProgAdjustTabAddAdjustFormAdjustCodeSelElemVisible = new Criteria<ProgramPage>(p =>
        {
            return p.Exists(Bys.ProgramPage.ProgAdjustTabAddAdjustFormAdjustCodeSelElem, ElementCriteria.IsVisible);

        }, "Program Adjustments tab, Add Adjustment form, Adjustment Code select element visible");

        public readonly ICriteria<ProgramPage> ProgAdjustTabAddAdjustFormNotExists = new Criteria<ProgramPage>(p =>
        {
            return !p.Exists(Bys.ProgramPage.ProgAdjustTabAddAdjustForm);

        }, "Program Adjustments tab, Add Adjustment form not exists");

        public readonly ICriteria<ProgramPage> SelfReportActTabVisible = new Criteria<ProgramPage>(p =>
        {
            return p.Exists(Bys.ProgramPage.SelfReportActTab, ElementCriteria.IsVisible);

        }, "Self Report Activities tab visible");

        public readonly ICriteria<ProgramPage> DetailsTabStatusValueLblVisible = new Criteria<ProgramPage>(p =>
        {
            return p.Exists(Bys.ProgramPage.DetailsTabStatusValueLbl, ElementCriteria.IsVisible);

        }, "Details tab, Status label visible");

        public readonly ICriteria<ProgramPage> ExternalActivityHasBeenAcceptedBannerNotExists = new Criteria<ProgramPage>(p =>
        {
            return !p.Exists(Bys.Page.GreenBanner);

        }, "Self Reported Activities tab, External Activity Has Been Accepted Banner not exists");

        public readonly ICriteria<ProgramPage> AdjustmentAddedBannerNotExists = new Criteria<ProgramPage>(p =>
        {
            return !p.Exists(Bys.Page.GreenBanner);

        }, "Add Adjustments tab, Adjustment Added Banner not exists");

        public readonly ICriteria<ProgramPage> PageReady;

        public ProgramPageCriteria()
        {
            PageReady = SelfReportActTabVisible;
        }
    }
}
