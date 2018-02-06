using Browser.Core.Framework;


namespace CFPC.AppFramework
{
    public class CreditSummaryPageCriteria
    {
        public readonly ICriteria<CreditSummaryPage> EnterACPDActivityBtnEnabled = new Criteria<CreditSummaryPage>(p =>
        {
            return p.Exists(Bys.CreditSummaryPage.EnterCPDActBtn, ElementCriteria.IsEnabled);

        }, "Enter A CPD Activity Button enabled");

        public readonly ICriteria<CreditSummaryPage> MyCreditSummaryLblEnabled = new Criteria<CreditSummaryPage>(p =>
        {
            return p.Exists(Bys.CreditSummaryPage.MyCreditSummaryLbl, ElementCriteria.IsVisible);

        }, "My Credit Summary Label enabled");


        public readonly ICriteria<CreditSummaryPage> PageReady;

        public CreditSummaryPageCriteria()
        {
            PageReady = MyCreditSummaryLblEnabled;//.AND(TableEnabled);
        }
    }
}
