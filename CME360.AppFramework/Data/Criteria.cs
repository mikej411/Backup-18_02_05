namespace CME.AppFramework
{
    /// <summary>
    /// Provides access to all the known "Criteria" for the the application.
    /// Criteria are typically used when waiting for elements. I often wait until some
    /// "Criteria" is met before continuing with a test.
    /// </summary>
    public static class Criteria
    {
        public static readonly LoginPageCriteria LoginPage = new LoginPageCriteria();
        public static readonly MyDashboardPageCriteria MyDashboardPage = new MyDashboardPageCriteria();
        public static readonly ProjectsPageCriteria ProjectsPage = new ProjectsPageCriteria();
        public static readonly ActivityMainPageCriteria ActivityMainPage = new ActivityMainPageCriteria();
        public static readonly DistributionPageCriteria DistributionPage = new DistributionPageCriteria();
        public static readonly SearchResultsPageCriteria SearchResultsPage = new SearchResultsPageCriteria();
        public static readonly PortalPageCriteria PortalPage = new PortalPageCriteria();

        public static readonly AddCatalogPageCriteria AddCatalogPage = new AddCatalogPageCriteria();
        public static readonly CatalogsPageCriteria CatalogsPage = new CatalogsPageCriteria();

    }
}