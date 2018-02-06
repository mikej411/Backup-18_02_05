using Browser.Core.Framework;

namespace AMA.AppFramework
{
    public class GCEPLibraryPageCriteria
    {
        public readonly ICriteria<GCEPLibraryPage> LibraryTxt = new Criteria<GCEPLibraryPage>(p =>
        {
            return p.Exists(Bys.GCEPLibraryPage.LibraryLbl, ElementCriteria.IsVisible);

        }, "Username text box  visible");

        public readonly ICriteria<GCEPLibraryPage> LoadIcon = new Criteria<GCEPLibraryPage>(p =>
        {
            return p.Exists(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);

        }, "spalash is not visible");

        public readonly ICriteria<GCEPLibraryPage> PageReady;

        public GCEPLibraryPageCriteria()
        {
            PageReady = LibraryTxt.AND(LoadIcon);
        }
    }
}
