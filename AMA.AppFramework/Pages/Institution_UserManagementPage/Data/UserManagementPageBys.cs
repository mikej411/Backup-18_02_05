using OpenQA.Selenium;

namespace AMA.AppFramework
{
    public class UserManagementPageBys
    {
       //Main page

        // Text boxes  
        public readonly By UserNameTxt = By.Id("userName");
        public readonly By UserEmailTxt = By.Id("userEmail");
        
        //Labels
        public readonly By UserMangementLbl = By.XPath("//h3");
        public readonly By UserInstitutionLbl = By.XPath("//div[.='Institution(s)']/..//span");       
        public readonly By UserProgramLbl = By.XPath("//div[.='Program(s)']/..//span");
       
        //Buttons
        public readonly By SaveBtn = By.XPath("//button[.='Save']");
        public readonly By CancelBtn = By.XPath("//button[.='CANCEL']");
       
        //Dropdwon Select Element
        public readonly By UserRoleSelElem = By.Name("singleSelect");
        public readonly By UserPgySelElem = By.Id("pgyYear"); 
      
    }
}