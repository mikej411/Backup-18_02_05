namespace AAFPRS.AppFramework
{
    /// <summary>
    /// Provides access to all known selenium "By"s
    /// "By"s provide a way to find a particular element on a page.
    /// </summary>
    public static class Bys
    {

        /// <summary>
        /// Locators to find elements on the login page
        /// </summary>
        public static readonly LoginPageBys LoginPage = new LoginPageBys();

        public static readonly AAFPRSPageBys AAFPRSPage = new AAFPRSPageBys();

        


        /// <summary>
        /// Locators to find elements on the My CPD Activities List page
        /// </summary>
      //  public static readonly DashboardPageBys DashboardPage = new DashboardPageBys();
        
    }
}