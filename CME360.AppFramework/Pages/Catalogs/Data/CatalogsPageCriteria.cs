using Browser.Core.Framework;

namespace CME.AppFramework
{
    public class CatalogsPageCriteria
    {
      

        public readonly ICriteria<CatalogsPage> AddNewCatalogLnkVisible = new Criteria<CatalogsPage>(p =>
        {
            return p.Exists(Bys.CatalogsPage.AddNewCatalogLnk, ElementCriteria.IsVisible);

        }, "AddnewCatalog Link visible");

        
        
        public readonly ICriteria<CatalogsPage> PageReady;
        
        public CatalogsPageCriteria()
        {
            PageReady = AddNewCatalogLnkVisible;
        }
    }
}
