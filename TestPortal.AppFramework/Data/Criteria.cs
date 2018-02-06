namespace TP.AppFramework
{
    /// <summary>
    /// Provides access to all the known "Criteria" for the the application.
    /// Criteria are typically used when waiting for elements. I often wait until some
    /// "Criteria" is met before continuing with a test.
    /// </summary>
    public static class Criteria
    {
        public static readonly LoginPageCriteria LoginPage = new LoginPageCriteria();
        public static readonly ActivityNotAvailablePageCriteria ActivityNotAvailablePage = new ActivityNotAvailablePageCriteria();
        public static readonly HomePageCriteria HomePage = new HomePageCriteria();
        public static readonly ActivityOverviewPageCriteria ActivityOverviewPage = new ActivityOverviewPageCriteria();

        public static readonly ActivityDetailsPageCriteria ActivityDetailsPage = new ActivityDetailsPageCriteria();

        public static readonly PaymentPageCriteria PaymentPage = new PaymentPageCriteria();

    }
}