using Browser.Core.Framework;

namespace CME.AppFramework
{
    public class SearchResultsPageCriteria
    {
        public readonly ICriteria<SearchResultsPage> ActivitiesTblBodyRowVisible = new Criteria<SearchResultsPage>(p =>
        {
            return p.Exists(Bys.SearchResultsPage.ActivitiesTblBodyRow, ElementCriteria.IsVisible);

        }, "Activities table body row visible");

        public readonly ICriteria<SearchResultsPage> PageReady;

        public SearchResultsPageCriteria()
        {
            PageReady = ActivitiesTblBodyRowVisible;
        }
    }
}
