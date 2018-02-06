using OpenQA.Selenium;

namespace AMA.AppFramework
{
    public class GCEPLibraryPageBys
    {     
        // Main page

            //Labels
        public readonly By LibraryLbl = By.XPath("//h4[.='Library']");

        //Input Box
        public readonly By SearchTxt = By.XPath("//input[contains(@placeholder,'Search')]");

        //Buttons
        public readonly By SearchBtn = By.XPath("//*[contains(@class,'glyphicon glyphicon-search')]");    
        public readonly By BeginCourseBtn = By.XPath("//div/button[@type='button'][1]");
        // public readonly By GridTbl = By.XPath("//div[@class='row']"); 

        //Links
        public readonly By TranscriptLnk = By.Id("transcriptTabGCE");     
      
    }
}