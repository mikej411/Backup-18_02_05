using Browser.Core.Framework;

namespace AMA.AppFramework
{
    public class DashboardNotificationsPageCriteria
    {
        public readonly ICriteria<DashboardNotificationsPage> NotificationMngTableVisible = new Criteria<DashboardNotificationsPage>(p =>
        {
            return p.Exists(Bys.DashboardNotificationsPage.NotifacionsMngTbl, ElementCriteria.IsVisible);

        }, "Notications table not visible");

        public readonly ICriteria<DashboardNotificationsPage> LoadIconNotVisible = new Criteria<DashboardNotificationsPage>(p =>
        {
            return p.Exists(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);

        }, "Load Icon Not visible");

        public readonly ICriteria<DashboardNotificationsPage> PageReady;

        public DashboardNotificationsPageCriteria()
        {
            PageReady = NotificationMngTableVisible.AND(LoadIconNotVisible);
        }
    }
}
