using OpenQA.Selenium;

namespace AMA.AppFramework
{
    public class EditInstitutionBys
    {
       //Main page

        // Text boxes  
        public readonly By InstitutionIdTxt = By.Id("instituionCode");
        public readonly By InstitutionNameTxt = By.Id("instituionName");
        public readonly By InstitutionPrimaryContactNameTxt = By.Id("primaryContactName");
        public readonly By InstitutionPrimaryContactPhoneTxt = By.Id("primaryContactPhone");
        public readonly By InstitutionPrimaryContactEmailTxt = By.Id("primaryContactEmail");

        //Labels
        public readonly By CreateInstitutionLbl = By.Id("//h3");
        public readonly By InstitutionDetailsLbl = By.XPath("//h4[.='Institution Details']");       
        public readonly By InstitutionPrimaryContactLbl = By.XPath("//h4[.='Primary Contact']");
        public readonly By InstitutionCertificateSignatureLbl = By.XPath("//h4[.='Certificate Signature']");
       
      
        //Buttons
        public readonly By InstitutionDetailsChooseFileBtn = By.XPath("//label[@for='logo1']");
        public readonly By InstitutionCertificateSignatureChooseFileBtn = By.XPath("//label[@for='logo2']");
        public readonly By InstitutionCancelBtn = By.XPath("//button[text()='Cancel']");
        public readonly By InstitutionSaveBtn = By.XPath("//button[text()='Save']");
       
        


    
    }
}