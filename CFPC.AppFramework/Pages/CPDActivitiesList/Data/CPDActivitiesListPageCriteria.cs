using Browser.Core.Framework;

namespace CFPC.AppFramework
{
    public class CPDActivitiesPageCriteria
    {
        public readonly ICriteria<LoginPage> UsernameVisible = new Criteria<LoginPage>(p =>
        {
            return p.Exists(Bys.LoginPage.UserNameTxt, ElementCriteria.IsVisible);

        }, "Username text box  visible");

        public readonly ICriteria<LoginPage> PasswordEnabled = new Criteria<LoginPage>(p =>
        {
            return p.Exists(Bys.LoginPage.UserNameTxt, ElementCriteria.IsEnabled);

        }, "Password is enabled");

        public readonly ICriteria<LoginPage> PageReady;

        public CPDActivitiesPageCriteria()
        {
            PageReady = UsernameVisible.AND(PasswordEnabled);
        }
    }
}
