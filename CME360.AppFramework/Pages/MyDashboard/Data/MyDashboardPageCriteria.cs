using Browser.Core.Framework;

namespace CME.AppFramework
{
    public class MyDashboardPageCriteria
    {
        public readonly ICriteria<MyDashboardPage> MyDashboardsLblVisible = new Criteria<MyDashboardPage>(p =>
        {
            return p.Exists(Bys.MyDashboardPage.MyDashboardsLbl, ElementCriteria.IsVisible);

        }, "My Dashboards label visible");


        public readonly ICriteria<MyDashboardPage> PageReady;

        public MyDashboardPageCriteria()
        {
            PageReady = MyDashboardsLblVisible;
        }
    }
}
