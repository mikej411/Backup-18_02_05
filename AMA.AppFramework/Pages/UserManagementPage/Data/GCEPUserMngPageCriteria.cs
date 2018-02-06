using Browser.Core.Framework;

namespace AMA.AppFramework
{
    public class GCEPUserMngPageCriteria
    {
        public readonly ICriteria<GCEPUserMngPage> UserManagementTableVisible = new Criteria<GCEPUserMngPage>(p =>
        {
            return p.Exists(Bys.GCEPUserMngPage.UsersManagementTbl, ElementCriteria.IsEnabled,ElementCriteria.IsVisible);

        }, "User Management table is visible and enabled");

        public readonly ICriteria<GCEPUserMngPage> LoadIconAppear = new Criteria<GCEPUserMngPage>(p =>
        {
            return p.Exists(Bys.AMAPage.LoadIcon, ElementCriteria.AttributeValue("class", "page-splash dissolve-animation ng-hide")
                .OR(ElementCriteria.AttributeValue("class", "page-splash dissolve-animation ng-animate ng-hide")));
        }, "Load icon disappeared");

        public readonly ICriteria<GCEPUserMngPage> Action = new Criteria<GCEPUserMngPage>(p =>
        {
            return p.Exists(Bys.GCEPUserMngPage.ActionGearBtn, ElementCriteria.IsEnabled, ElementCriteria.IsVisible);

        }, "Reset Filter Button enabled");


        public readonly ICriteria<GCEPUserMngPage> PageReady;

        public GCEPUserMngPageCriteria()
        {
            PageReady = UserManagementTableVisible.AND(LoadIconAppear).OR(Action);
            
        }
    }
}
