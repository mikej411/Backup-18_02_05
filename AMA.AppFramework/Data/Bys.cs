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

        /// <summary>
        /// Locators to find elements on the curriculumcourses page
        /// </summary>
        public static readonly CurriculumCoursePageBys CurriculumCoursePage = new CurriculumCoursePageBys();

        /// <summary>
        /// Locators to find elements on the curriculum manage page
        /// </summary>
        public static readonly CurriculumMngPageBys CurriculumMngPage = new CurriculumMngPageBys();

        /// <summary>
        /// Locators to find elements on the PGY assignment page
        /// </summary>
        public static readonly PGYAssignmentPageBys PGYAssignmentPage = new PGYAssignmentPageBys();

        /// <summary>
        /// Locators to find elements on the programm assignment page
        /// </summary>
        public static readonly AssignProgramPageBys AssignProgramPage = new AssignProgramPageBys();

        /// <summary>
        /// Locators to find elements on the programm assignment page
        /// </summary>
        public static readonly AssignSummaryPageBys AssignSummaryPage = new AssignSummaryPageBys(); 

        /// <summary>
        /// Locators to find elements on the programm assignment page
        /// </summary>
        public static readonly AssignConfirmationPageBys AssignConfirmationPage = new AssignConfirmationPageBys();

        /// <summary>
        /// Locators to find elements on the programm assignment page
        /// </summary>
        public static readonly GCEPLibraryPageBys GCEPLibraryPage = new GCEPLibraryPageBys(); 

        /// <summary>
        /// Locators to find elements on the programm assignment page
        /// </summary>
        public static readonly CourseTestPageBys CourseTestPage = new CourseTestPageBys(); 

        /// <summary>
        /// Locators to find elements on the programm assignment page
        /// </summary>
        public static readonly GCEPTranscriptPageBys GCEPTranscriptPage = new GCEPTranscriptPageBys(); 

        /// <summary>
        /// Locators to find elements on the Institutions page
        /// </summary>
        public static readonly InstitutionsPageBys InstitutionsPage = new InstitutionsPageBys();

        /// <summary>
        /// Locators to find elements on the Institution Gcep page
        /// </summary>
        public static readonly InstitutionsGCEPPageBys InstitutionsGCEPPage = new InstitutionsGCEPPageBys();

        /// <summary>
        /// Locators to find elements on the Program Pages page
        /// </summary>
        public static readonly ProgramsPageBys ProgramsPage = new ProgramsPageBys();

        /// <summary>
        /// Locators to find elements on the CopyCurriculumEdits Pages page
        /// </summary>
        public static readonly CopyCurriculumEditsPageBys CopyCurriculumEditsPage = new CopyCurriculumEditsPageBys(); 
        
        /// <summary>
        /// Locators to find elements on the DashboardNotification Pages page
        /// </summary>
        public static readonly DashboardNotificationsPageBys DashboardNotificationsPage = new DashboardNotificationsPageBys(); 

        /// <summary>
        /// Locators to find elements on the NotificationCreator Pages page
        /// </summary>
        public static readonly NotificationCreatorPageBys NotificationCreatorPage = new NotificationCreatorPageBys(); 

        /// <summary>
        /// Locators to find elements on the PromotePGY Pages page
        /// </summary>
        public static readonly PromotePGYPageBys PromotePGYPage = new PromotePGYPageBys();

        /// <summary>
        /// Locators to find elements on the User Management  Pages page
        /// </summary>
        public static readonly UserManagementPageBys UserManagementPage = new UserManagementPageBys(); 

        /// <summary>
        /// Locators to find elements on the Help Pages page
        /// </summary>
        public static readonly HelpPageBys HelpPage = new HelpPageBys();

        /// <summary>
        /// Locators to find elements on the Edit Institution Pages page
        /// </summary>
        public static readonly EditInstitutionBys EditInstitutionPage = new EditInstitutionBys(); 

        /// <summary>
        /// Locators to find elements on the Member Benefit Pages page
        /// </summary>
        public static readonly MemberBenefitPageBys MemberBenefitPage = new MemberBenefitPageBys();

    }
}