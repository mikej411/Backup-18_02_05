using Browser.Core.Framework;

namespace CME.AppFramework
{
    public class AddCatalogPageCriteria
    {
        public readonly ICriteria<AddCatalogPage> DetailsTabCatalogNameTxtVisible = new Criteria<AddCatalogPage>(p =>
        {
            return p.Exists(Bys.AddCatalogPage.DetailsTabCatalogNameTxt, ElementCriteria.IsVisible);

        }, "Details tab, Catalog Name textbox visible");

        public readonly ICriteria<AddCatalogPage> PageReady;
        
        public AddCatalogPageCriteria()
        {
            PageReady = DetailsTabCatalogNameTxtVisible;
        }
    }
}
