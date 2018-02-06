using OpenQA.Selenium;

namespace AMA.AppFramework
{
    public class GCEPTranscriptPageBys
    {     
        // Main Page

          //Label
        public readonly By LibraryLbl = By.XPath("//h4[.='Transcript']");
       
        //Table
        public readonly By CompletedTestTbl = By.XPath("//div[@class=' row transcript-item']");
        public readonly By TranscriptHeaderTbl = By.XPath("//div[@class='row transcript-header']");

        //DropdownSelect
        public readonly By CompletionDateSelElem = By.Name("singleSelect");
         
    }
}