using OpenQA.Selenium;

namespace AMA.AppFramework
{
    public class EducationCenterTransciptPageBys
    {   
        //Main Page
        
        //Link
        public readonly By AddSelfRepaortedActivityLnk = By.Id("ctl00_ctl00_ContentPlaceHolderBase1_ContentPlaceHolder1_TranscriptControlResponsive1_lnkAddActivity");
        public readonly By TranscriptLnk = By.XPath("//a[@href='/transcript.aspx']");
        public readonly By MyCoursesLnk = By.XPath("//a[@href='/courses.aspx']");

        public readonly By FilterByBoxIcon = By.XPath("//a[@onclick='removeFilter();']");

        //buttons
        public readonly By FilterByDateBtn = By.Id("ctl00_ctl00_ContentPlaceHolderBase1_ContentPlaceHolder1_TranscriptControlResponsive1_DateFilter");
        public readonly By TranscriptActivitiesBtn = By.Id("ctl00_ctl00_ContentPlaceHolderBase1_ContentPlaceHolder1_TranscriptControlResponsive1_lnkTranscriptActivities");
       
        //table
        public readonly By TranscriptcontrolTbl = By.XPath("//table[@id='ctl00_ctl00_ContentPlaceHolderBase1_ContentPlaceHolder1_TranscriptControlResponsive1_rgTranscript_ctl00']");

        //input Box
        public readonly By FilterByTxt = By.Id("ctl00_ctl00_ContentPlaceHolderBase1_ContentPlaceHolder1_TranscriptControlResponsive1_spnActivityName");  
       
       
    }
}