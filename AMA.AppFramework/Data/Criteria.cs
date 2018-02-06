namespace AMA.AppFramework
{
    /// <summary>
    /// Provides access to all the known "Criteria" for the the application.
    /// Criteria are typically used when waiting for elements.  I often wait until some
    /// "Criteria" is met before continuing with a test.
    /// </summary>
    public static class Criteria
    {
        public static readonly LoginPageCriteria LoginPage = new LoginPageCriteria();

        public static readonly LibraryPageCriteria LibraryPage = new LibraryPageCriteria();

        public static readonly GCEPPageCriteria GCEPPage = new GCEPPageCriteria();

        public static readonly EducationCenterPageCriteria EducationCenterPage = new EducationCenterPageCriteria();

        public static readonly GCEPUserMngPageCriteria GCEPUserMngPage = new GCEPUserMngPageCriteria();

        public static readonly EducationCenterTransciptPageCriteria EducationCenterTransciptPage = new EducationCenterTransciptPageCriteria();

        public static readonly CurriculumMngPageCriteria CurriculumMngPage = new CurriculumMngPageCriteria();

        public static readonly CurriculumCoursePageCriteria CurriculumCoursePage = new CurriculumCoursePageCriteria();

        public static readonly PGYAssignmentPageCriteria PGYAssignmentPage = new PGYAssignmentPageCriteria();

        public static readonly AssignProgramPageCriteria AssignProgramPage = new AssignProgramPageCriteria();

        public static readonly AssignSummaryPageCriteria AssignSummaryPage = new AssignSummaryPageCriteria(); 

        public static readonly AssignConfirmationPageCriteria AssignConfirmationPage = new AssignConfirmationPageCriteria(); 

        public static readonly GCEPLibraryPageCriteria GCEPLibraryPage = new GCEPLibraryPageCriteria(); 

        public static readonly CourseTestPageCriteria CourseTestPage = new CourseTestPageCriteria(); 

        public static readonly GCEPTranscriptPageCriteria GCEPTranscriptPage = new GCEPTranscriptPageCriteria(); 

        public static readonly InstitutionsPageCriteria InstitutionsPage = new InstitutionsPageCriteria(); 

        public static readonly InstitutionsGCEPPageCriteria InstitutionsGCEPPage = new InstitutionsGCEPPageCriteria(); 

        public static readonly ProgramsPageCriteria ProgramsPage = new ProgramsPageCriteria(); 
        
        public static readonly CopyCurriculumEditsPageCriteria CopyCurriculumEditsPage = new CopyCurriculumEditsPageCriteria();
        
        public static readonly DashboardNotificationsPageCriteria DashboardNoticationsPage = new DashboardNotificationsPageCriteria(); 

        public static readonly NotificationCreatorPageCriteria NotificationCreatorPage = new NotificationCreatorPageCriteria(); 

        public static readonly PromotePGYPageCriteria PromotePGYPage = new PromotePGYPageCriteria();

        public static readonly UserManagementPageCriteria UserManagementPage = new UserManagementPageCriteria(); 

        public static readonly HelpPageCriteria HelpPage = new HelpPageCriteria();

        public static readonly EditInstitutionCriteria EditInstitutionPage = new EditInstitutionCriteria(); 

        public static readonly MemberBenefitPageCriteria MemberBenefitPage = new MemberBenefitPageCriteria();

    }
}