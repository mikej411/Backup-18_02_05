using OpenQA.Selenium;

namespace AMA.AppFramework
{
    public class MemberBenefitPageBys
    {     
        // Main page

         //Labels
        public readonly By MemberBenefitsManagementLbl = By.XPath("//h4[.='Membership benefits management']");

        //Input Box
        public readonly By TitleTxt = By.Id("crossSellTitle");
        public readonly By URLTxt = By.Id("crossSellURL");

        //Buttons
        public readonly By MembershipFormBrowseHiddenBtn = By.Name("logoFile");
        public readonly By ChooseFileBtn = By.XPath("//label[@for='logo']");    
        public readonly By ClearBtn = By.XPath("//button[@class='btn btn-default'and contains(text(),'Clear')]");
        public readonly By SaveBtn = By.XPath("//button[@class='btn btn-primary'and contains(text(),'Save')]");
        public readonly By PublishBtn = By.XPath("//button[@class='btn btn-primary' and contains(text(),'Publish')]");
        public readonly By AcceptBtn = By.XPath("//button[@class='btn btn-primary'and contains(text(),'OK')]");
      
    }
}