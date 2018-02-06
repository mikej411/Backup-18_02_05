using Browser.Core.Framework;

namespace AMA.AppFramework
{
    public class HelpPageCriteria
    {
        public readonly ICriteria<HelpPage> HelpLabelVisible = new Criteria<HelpPage>(p =>
        {
            return p.Exists(Bys.HelpPage.HelpLbl, ElementCriteria.IsVisible);

        }, "Help Label is visible");

        public readonly ICriteria<HelpPage> LoadIconAppear = new Criteria<HelpPage>(p =>
        {
            return p.Exists(Bys.AMAPage.LoadIcon, ElementCriteria.AttributeValue("class", "page-splash dissolve-animation ng-hide"));
        }, "Load icon disappeared");

        public readonly ICriteria<HelpPage> PageReady;

        public HelpPageCriteria()
        {
            PageReady = HelpLabelVisible.AND(LoadIconAppear);
        }
    }
}
