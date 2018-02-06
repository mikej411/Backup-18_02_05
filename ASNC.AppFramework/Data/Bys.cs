namespace RCP.AppFramework
{
    /// <summary>
    /// Provides access to all known selenium "By"s
    /// "By"s provide a way to find a particular element on a page.
    /// </summary>
    public static class Bys
    {
        /// <summary>
        /// Locators to find elements on the skeleton of the RCP page. i.e. The menu items, header items and footer items.
        /// </summary>
        public static readonly RCPPageBys RCPPage = new RCPPageBys();

        /// <summary>
        /// Locators to find elements on the login page
        /// </summary>
        public static readonly LoginPageBys LoginPage = new LoginPageBys();


    }
}