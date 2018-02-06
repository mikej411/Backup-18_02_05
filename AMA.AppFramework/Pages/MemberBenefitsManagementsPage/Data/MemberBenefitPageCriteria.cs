using Browser.Core.Framework;

namespace AMA.AppFramework
{
    public class MemberBenefitPageCriteria
    {
        public readonly ICriteria<MemberBenefitPage> PublishButton = new Criteria<MemberBenefitPage>(p =>
        {
            return p.Exists(Bys.MemberBenefitPage.PublishBtn, ElementCriteria.IsVisible,ElementCriteria.IsEnabled);

        }, "Publish button is visible and enabled");

        public readonly ICriteria<MemberBenefitPage> LoadIcon = new Criteria<MemberBenefitPage>(p =>
        {
            return p.Exists(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);

        }, "spalash is not visible");

        public readonly ICriteria<MemberBenefitPage> PageReady;

        public MemberBenefitPageCriteria()
        {
            PageReady = PublishButton.AND(LoadIcon);
        }
    }
}
