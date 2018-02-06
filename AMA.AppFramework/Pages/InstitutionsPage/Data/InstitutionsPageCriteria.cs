using Browser.Core.Framework;

namespace AMA.AppFramework
{
    public class InstitutionsPageCriteria
    {
        public readonly ICriteria<InstitutionsPage> InstitutionTableVisible = new Criteria<InstitutionsPage>(p =>
        {
            return p.Exists(Bys.InstitutionsPage.InstitutionsTbl, ElementCriteria.IsVisible);

        }, "Instution table is not visible");

        public readonly ICriteria<InstitutionsPage> LoadIconAppear = new Criteria<InstitutionsPage>(p =>
        {
            return p.Exists(Bys.AMAPage.LoadIcon, ElementCriteria.AttributeValue("class", "page-splash dissolve-animation ng-hide"));
        }, "Load icon disappeared");

        public readonly ICriteria<InstitutionsPage> PageReady;

        public InstitutionsPageCriteria()
        {
            PageReady = InstitutionTableVisible.AND(LoadIconAppear);
        }
    }
}
