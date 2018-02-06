using Browser.Core.Framework;

namespace AMA.AppFramework
{
    public class EducationCenterTransciptPageCriteria
    {
        public readonly ICriteria<EducationCenterTransciptPage> ReportedActivityVisible = new Criteria<EducationCenterTransciptPage>(p =>
        {
            return p.Exists(Bys.EducationCenterTransciptPage.FilterByTxt, ElementCriteria.IsVisible);

        }, "Add Self-Reported Activity visible");

     /*   public readonly ICriteria<LoginPage> PasswordEnabled = new Criteria<LoginPage>(p =>
        {
            return p.Exists(Bys.LoginPage.PasswordTxt, ElementCriteria.IsEnabled);

        }, "Password is enabled");*/

        public readonly ICriteria<EducationCenterTransciptPage> PageReady;

        public EducationCenterTransciptPageCriteria()
        {
           PageReady = ReportedActivityVisible;
        }
    }
}
