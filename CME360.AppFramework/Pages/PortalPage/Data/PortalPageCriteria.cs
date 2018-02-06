using Browser.Core.Framework;

namespace CME.AppFramework
{
    public class PortalPageCriteria
    {
        public readonly ICriteria<PortalPage> TCLnkVisible = new Criteria<PortalPage>(p =>
        {
            return p.Exists(Bys.PortalPage.TermsAndConditionsLnk, ElementCriteria.IsVisible);

        }, "Catalog Link visible");

        public readonly ICriteria<PortalPage> CatAndActTabSelCatalogTblVisible = new Criteria<PortalPage>(p =>
        {
            return p.Exists(Bys.PortalPage.CatAndActTabSelCatalogTbl, ElementCriteria.IsVisible);

        }, "Catalogs And Activities tab, Selected Catalogs table visible");

        public readonly ICriteria<PortalPage> PageReady;
        
        public PortalPageCriteria()
        {
            PageReady = TCLnkVisible;
        }
    }
}
