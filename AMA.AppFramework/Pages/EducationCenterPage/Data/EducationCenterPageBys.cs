using OpenQA.Selenium;

namespace AMA.AppFramework
{
    public class EducationCenterPageBys
    {
        // Main page
      
       //Links
        public readonly By AmaDropdownMenuLnk = By.Id("left-menu");
        public readonly By GcepLnk = By.Id("lnkGCE");
        //a[@href='/gme-competency']
        public readonly By TranscriptLnk = By.XPath("//a[@href='/transcript.aspx']");
        public readonly By LibraryLnk = By.Id("tabDefault");
        public readonly By MyCoursesLnk = By.Id("tabCourses");
        public readonly By HelpLnk = By.Id("tabContact");
        public readonly By ProfileLnk = By.Id("tabProfile");

        //Tables    
        public readonly By CourseTbl = By.XPath("//div[@id='CurriculumControlNew']");

        //Input Box
        public readonly By FilterByTxt = By.Id("ctl00_ctl00_ContentPlaceHolderBase1_ContentPlaceHolder1_TranscriptControlResponsive1_spnActivityName");

        //Title
        public readonly By MyCoursesTtl = By.Id("ctl00_ctl00_ctl00_ContentPlaceHolderBase1_HR1_LoginViewAMA1_lblLoginName");

    }
}