using Browser.Core.Framework;

namespace CME.AppFramework
{
    public class ActivityMainPageCriteria
    {
        public readonly ICriteria<ActivityMainPage> PubDetailsTabAvailCatTblVisible = new Criteria<ActivityMainPage>(p =>
        {
            return p.Exists(Bys.ActivityMainPage.PubDetailsTabAvailCatTbl, ElementCriteria.IsVisible);

        }, "Available Catalogs table visible");

        public readonly ICriteria<ActivityMainPage> PubDetailsTabVisible = new Criteria<ActivityMainPage>(p =>
        {
            return p.Exists(Bys.ActivityMainPage.PubDetailsTab, ElementCriteria.IsVisible);

        }, "Publishing Details tab visible");

        public readonly ICriteria<ActivityMainPage> PubDetailsTabAvailCatTblSearchCatLoadElemVisible = new Criteria<ActivityMainPage>(p =>
        {
            return p.Exists(Bys.ActivityMainPage.PubDetailsTabAvailCatTblSearchCatLoadElem, ElementCriteria.AttributeValue("aria-hidden", "false"));

        }, "Publishing Details tab, search available catalogs, load element visible");

        public readonly ICriteria<ActivityMainPage> PubDetailsTabAvailCatTblSearchCatLoadElemNotVisible = new Criteria<ActivityMainPage>(p =>
        {
            return p.Exists(Bys.ActivityMainPage.PubDetailsTabAvailCatTblSearchCatLoadElem, ElementCriteria.AttributeValue("aria-hidden", "true"));

        }, "Publishing Details tab, search available catalogs, load element not visible");

        public readonly ICriteria<ActivityMainPage> PubDetailsTabAvailCatTblAddCatLoadElemVisible = new Criteria<ActivityMainPage>(p =>
        {
            return p.Exists(Bys.ActivityMainPage.PubDetailsTabAvailCatTblAddCatLoadElem, ElementCriteria.AttributeValue("aria-hidden", "false"));

        }, "Publishing Details tab, add catalog, load element visible");

        public readonly ICriteria<ActivityMainPage> PubDetailsTabAvailCatTblAddCatLoadElemNotVisible = new Criteria<ActivityMainPage>(p =>
        {
            return p.Exists(Bys.ActivityMainPage.PubDetailsTabAvailCatTblAddCatLoadElem, ElementCriteria.AttributeValue("aria-hidden", "true"));

        }, "Publishing Details tab, add catalog, load element not visible");

        public readonly ICriteria<ActivityMainPage> PubDetailsTabSelectedCatTblRemoveCatLoadElemVisible = new Criteria<ActivityMainPage>(p =>
        {
            return p.Exists(Bys.ActivityMainPage.PubDetailsTabSelectedCatTblRemoveCatLoadElem, ElementCriteria.AttributeValue("aria-hidden", "false"));

        }, "Publishing Details tab, remove catalog, load element visible");

        public readonly ICriteria<ActivityMainPage> PubDetailsTabSelectedCatTblRemoveCatLoadElemNotVisible = new Criteria<ActivityMainPage>(p =>
        {
            return p.Exists(Bys.ActivityMainPage.PubDetailsTabSelectedCatTblRemoveCatLoadElem, ElementCriteria.AttributeValue("aria-hidden", "true"));

        }, "Publishing Details tab, remove catalog, load element not visible");

        public readonly ICriteria<ActivityMainPage> EditPortalFormCustomFeeTxtVisible = new Criteria<ActivityMainPage>(p =>
        {
            return p.Exists(Bys.ActivityMainPage.EditPortalFormCustomFeeTxt, ElementCriteria.IsVisible);

        }, "Edit Portal form, Custom Fee text box visible");

        public readonly ICriteria<ActivityMainPage> EditPortalFormCustomFeeTxtNotVisible = new Criteria<ActivityMainPage>(p =>
        {
            return p.Exists(Bys.ActivityMainPage.EditPortalFormCustomFeeTxt, ElementCriteria.IsNotVisible);

        }, "Edit Portal form, Custom Fee text box not visible");

        public readonly ICriteria<ActivityMainPage> PageReady;
        public ActivityMainPageCriteria()
        {
            PageReady = PubDetailsTabVisible;
        }
    }
}
