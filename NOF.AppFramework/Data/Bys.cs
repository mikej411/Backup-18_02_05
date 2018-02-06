namespace NOF.AppFramework
{
    /// <summary>
    /// Provides access to all known selenium "By"s
    /// "By"s provide a way to find a particular element on a page.
    /// </summary>
    public static class Bys
    {
        /// <summary>
        /// Locators to find elements on the skeleton of the NOF page. i.e. The menu items, header items and footer items.
        /// </summary>
        public static readonly NOFPageBys NOFPage = new NOFPageBys();

        /// <summary>
        /// Locators to find elements on the login page
        /// </summary>
        public static readonly LoginPageBys LoginPage = new LoginPageBys();

        /// <summary>
        /// Locators to find elements on the Home page
        /// </summary>
        public static readonly HomePageBys HomePage = new HomePageBys();

        /// <summary>
        /// Locators to find elements on the Curriculum page
        /// </summary>
        public static readonly CurriculumPageBys CurriculumPage = new CurriculumPageBys();

        /// <summary>
        /// Locators to find elements on the Transcript page
        /// </summary>
        public static readonly TranscriptPageBys TranscriptPage = new TranscriptPageBys();

    }
}