using Browser.Core.Framework;

namespace AMA.AppFramework
{
    public class UserManagementPageCriteria
    {
        public readonly ICriteria<UserManagementPage> SaveButtonVisible = new Criteria<UserManagementPage>(p =>
        {
            return p.Exists(Bys.UserManagementPage.SaveBtn, ElementCriteria.IsVisible,ElementCriteria.IsEnabled);

        }, "Save button not visible, and enabled");

        public readonly ICriteria<UserManagementPage> LoadIconAppear = new Criteria<UserManagementPage>(p =>
        {
            return p.Exists(Bys.AMAPage.LoadIcon, ElementCriteria.AttributeValue("class", "page-splash dissolve-animation ng-hide")
                .OR(ElementCriteria.AttributeValue("class", "page-splash dissolve-animation ng-animate ng-hide")));
        }, "Load icon disappeared");

        public readonly ICriteria<UserManagementPage> PageReady;

        public UserManagementPageCriteria()
        {
            PageReady = SaveButtonVisible.AND(LoadIconAppear);
        }
    }
}
