using Browser.Core.Framework;


namespace CFPC.AppFramework
{
    public class DashboardPageCriteria
    {
        public readonly ICriteria<DashboardPage> EnterACPDActivityBtnEnabled = new Criteria<DashboardPage>(p =>
        {
            return p.Exists(Bys.DashboardPage.EnterCPDActBtn, ElementCriteria.IsEnabled);

        }, "Enter A CPD Activity Button enabled");

        public readonly ICriteria<DashboardPage> TableEnabled = new Criteria<DashboardPage>(p =>
        {
            return p.Exists(Bys.DashboardPage.EnterCPDActBtn, ElementCriteria.IsEnabled);

        }, "My Table enabled");


        public readonly ICriteria<DashboardPage> PageReady;

        public DashboardPageCriteria()
        {
            PageReady = EnterACPDActivityBtnEnabled;//.AND(TableEnabled);
        }

        //adding additional wait criteria for the dashboard page tabs

        //credit summary tab wait criteria
        public readonly ICriteria<DashboardPage> CreditSummaryLblEnabled= new Criteria<DashboardPage>(p =>
        {
            return p.Exists(Bys.DashboardPage.MyCreditSummarySpan, ElementCriteria.IsEnabled);

        }, "Credit Summary Tab Label Enabled");

    }
}
