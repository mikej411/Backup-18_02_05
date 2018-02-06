namespace AMA.AppFramework
{
    /// <summary>
    /// Provides access to all known selenium "By"s
    /// "By"s provide a way to find a particular element on a page.
    /// </summary>
    public static class Bys
    {
        /// <summary>
        /// Locators to find elements on the skeleton of the AMA page. i.e. The menu items, header items and footer items.
        /// </summary>
        public static readonly AMAPageBys AMAPage = new AMAPageBys();

        /// <summary>
        /// Locators to find elements on the login page
        /// </summary>
        public static readonly LoginPageBys LoginPage = new LoginPageBys();

        /// <summary>
        /// Locators to find elements on the My CPD Activities List page
        /// </summary>
        public static readonly LibraryPageBys LibraryPage = new LibraryPageBys();

        /// <summary>
        /// Locators to find elements on the gcep page
        /// </summary>
        public static readonly GCEPPageBys GCEPPage = new GCEPPageBys();

        /// <summary>
        /// Locators to find elements on the educationcenter page
        /// </summary>
        public static readonly EducationCenterPageBys EducationCenterPage = new EducationCenterPageBys();

        /// <summary>
        /// Locators to find elements on the gcepusermanage page
        /// </summary>
        public static readonly GCEPUserMngPageBys GCEPUserMngPage = new GCEPUserMngPageBys();
        
        /// <summary>
        /// Locators to find elements on the educationcentertranscript page
        /// </summary>
        public static readonly EducationCenterTransciptPageBys EducationCenterTransciptPage = new EducationCenterTransciptPageBys();


        
    }
}