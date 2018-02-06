using Browser.Core.Framework;

namespace NOF.AppFramework
{
    public class HomePageCriteria
    {
        public readonly ICriteria<HomePage> LogoutLinkVisible = new Criteria<HomePage>(p =>
        {
            return p.Exists(Bys.NOFPage.LogoutLnk, ElementCriteria.IsVisible);

        }, "Logout Link visible");

        public readonly ICriteria<HomePage> SearchButtonVisible = new Criteria<HomePage>(p =>
        {
            return p.Exists(Bys.HomePage.SearchBtn, ElementCriteria.IsVisible);

        }, "Search Button is visible");


        public readonly ICriteria<HomePage> PageReady;

        public HomePageCriteria()
        {
            PageReady = LogoutLinkVisible.AND(SearchButtonVisible);
        }
    }
}
