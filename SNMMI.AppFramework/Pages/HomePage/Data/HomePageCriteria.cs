using Browser.Core.Framework;

namespace SNMMI.AppFramework
{
    public class HomePageCriteria
    {
        public readonly ICriteria<HomePage> LogoutLinkVisible = new Criteria<HomePage>(p =>
        {
            return p.Exists(Bys.SNMMIPage.LogoutLnk, ElementCriteria.IsVisible);

        }, "Logout Link visible");

        public readonly ICriteria<HomePage> EducationLinkVisible = new Criteria<HomePage>(p =>
        {
            return p.Exists(Bys.HomePage.EducationLnk, ElementCriteria.IsVisible);

        }, "Search Button is visible");


        public readonly ICriteria<HomePage> PageReady;

        public HomePageCriteria()
        {
            PageReady = LogoutLinkVisible.AND(EducationLinkVisible);
        }
    }
}
