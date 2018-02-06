using Browser.Core.Framework;

namespace AMA.AppFramework
{
    public class PromotePGYPageCriteria
    {
        public readonly ICriteria<PromotePGYPage> TablewithResindentsVisible = new Criteria<PromotePGYPage>(p =>
        {
            return p.Exists(Bys.PromotePGYPage.AvailableResidentsPromotePGYTbl, ElementCriteria.IsVisible,ElementCriteria.IsEnabled);

        }, "residents table is not visible");

        public readonly ICriteria<PromotePGYPage> AddButtonEnabled = new Criteria<PromotePGYPage>(p =>
        {
            return p.Exists(Bys.PromotePGYPage.AddSelectedBtn, ElementCriteria.IsEnabled,ElementCriteria.IsVisible);

        }, "add selected button is enabled");
        public readonly ICriteria<PromotePGYPage> AvailableTableFirstRowEnabled = new Criteria<PromotePGYPage>(p =>
        {
            return p.Exists(Bys.PromotePGYPage.AvailableResidentsPromotePGYTblFirstRowChk, ElementCriteria.IsEnabled, ElementCriteria.IsVisible);

        }, "first row from table is enabled");

        public readonly ICriteria<PromotePGYPage> LoadIconNotVisible = new Criteria<PromotePGYPage>(p =>
        {
            return p.Exists(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);

        }, "Load Icon Not visible");
        public readonly ICriteria<PromotePGYPage> PageReady;

        public PromotePGYPageCriteria()
        {
            PageReady = LoadIconNotVisible.AND(TablewithResindentsVisible).AND(AddButtonEnabled).AND(AvailableTableFirstRowEnabled);
        }
    }
}
