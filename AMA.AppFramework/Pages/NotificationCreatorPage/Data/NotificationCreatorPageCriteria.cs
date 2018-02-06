using Browser.Core.Framework;

namespace AMA.AppFramework
{
    public class NotificationCreatorPageCriteria
    {
        public readonly ICriteria<NotificationCreatorPage> SaveExitbuttonEnabled = new Criteria<NotificationCreatorPage>(p =>
        {
            return p.Exists(Bys.NotificationCreatorPage.SaveExitBtn, ElementCriteria.IsVisible,ElementCriteria.IsEnabled);

        }, "Save button not enabled");

        public readonly ICriteria<NotificationCreatorPage> LoadIconAppear = new Criteria<NotificationCreatorPage>(p =>
        {
            return p.Exists(Bys.AMAPage.LoadIcon, ElementCriteria.AttributeValue("class", "page-splash dissolve-animation ng-hide")
                .OR(ElementCriteria.AttributeValue("class", "page-splash dissolve-animation ng-animate ng-hide")));
        }, "Load icon disappeared");

        public readonly ICriteria<NotificationCreatorPage> PageReady;

        public NotificationCreatorPageCriteria()
        {
            PageReady = SaveExitbuttonEnabled.AND(LoadIconAppear);
        }
    }
}
