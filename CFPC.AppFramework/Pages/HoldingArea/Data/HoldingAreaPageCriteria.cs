using Browser.Core.Framework;


namespace CFPC.AppFramework
{
    public class HoldingAreaPageCriteria
    {
        public readonly ICriteria<HoldingAreaPage> EnterACPDActivityBtnEnabled = new Criteria<HoldingAreaPage>(p =>
        {
            return p.Exists(Bys.HoldingAreaPage.EnterCPDActBtn, ElementCriteria.IsEnabled);

        }, "Enter A CPD Activity Button enabled");

        public readonly ICriteria<HoldingAreaPage> HoldingAreaLblEnabled = new Criteria<HoldingAreaPage>(p =>
        {
            return p.Exists(Bys.HoldingAreaPage.MyHoldingAreaLbl, ElementCriteria.IsVisible);

        }, "My Credit Summary Label enabled");


        public readonly ICriteria<HoldingAreaPage> PageReady;

        public HoldingAreaPageCriteria()
        {
            PageReady = HoldingAreaLblEnabled;//.AND(TableEnabled);
        }
    }
}
