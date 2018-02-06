using OpenQA.Selenium;

namespace AMA.AppFramework
{
    public class CurriculumMngPageBys
    {
        // Main page

        //Labels
        public readonly By CurrulumTemplatesHeader = By.XPath("//h4[@class='ng-binding']");
        public readonly By CurriculumNameLbl = By.XPath("//a[@style='text-decoration: none;']");
        public readonly By NoRecordLbl = By.XPath("//div[@class='no-data-watermark ng-scope']");
        public readonly By CountTableItemLbl = By.XPath("//span[@class='ng-binding']");

        //input box
        public readonly By SearchTxt = By.XPath("//input[contains(@placeholder,'Search')]");     
       
        //Table
        public readonly By CurriculumTemplateTbl = By.Id("gridCurriculumTemplates");
        public readonly By SpecificCurriculumTbl = By.XPath("//table");
        public readonly By Actioncell = By.XPath("//button[@ng-click='grid.appScope.fnToggleActionMenu(row.entity);$event.stopPropagation();']");
        public readonly By Deletecell = By.LinkText("Remove");
        public readonly By Editcell = By.LinkText("Edit");       

        //button
        public readonly By CreateCurriculumTemplateBtn = By.XPath("//button[.='Create a Curriculum Template']");
        public readonly By CurriculumWinCloseBtn = By.XPath("//button[.='Close']");
        public readonly By SearchBtn = By.XPath("//*[contains(@class,'glyphicon glyphicon-search')]");
        public readonly By AcceptBtn = By.XPath("/html/body/div[1]/div/div/div[3]/button[1]");

        //Link
        public readonly By AssignToProgrammLnk = By.LinkText("Assign to Program");

        

       

        //*[@id='gridCurriculumTemplates']/div/div/div[2]/div/div-row
        //*[@id='gridCurriculumTemplates']/div/div/div[2]/div/div/div/div/div-cell value
        //div[@class='ui-grid-contents-wrapper']
        //*[@class='ui-grid-render-container ng-isolate-scope ui-grid-render-container-body']

    }
}