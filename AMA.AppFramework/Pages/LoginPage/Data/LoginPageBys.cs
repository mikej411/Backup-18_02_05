using OpenQA.Selenium;

namespace AMA.AppFramework
{
    public class LoginPageBys
    {
       //Main page

        // Text boxes  
        public readonly By PasswordTxt = By.Id("ctl00_ctl00_ContentPlaceHolderBase1_ContentPlaceHolder1_cpLoginAspx_ctl00_LoginControl1_LTLogin_Password");
        public readonly By UserNameTxt = By.Id("ctl00_ctl00_ContentPlaceHolderBase1_ContentPlaceHolder1_cpLoginAspx_ctl00_LoginControl1_LTLogin_UserName");
        
        //Labels
        public readonly By LoginUnsuccessfullWarningLbl = By.Id("ctl00_ctl00_ContentPlaceHolderBase1_ContentPlaceHolder1_cpLoginAspx_ctl00_LoginControl1_LTLogin_FailureText");
        public readonly By UserNameWarningLbl = By.Id("ctl00_ctl00_ContentPlaceHolderBase1_ContentPlaceHolder1_cpLoginAspx_ctl00_LoginControl1_LTLogin_UserNameRequired");       
        public readonly By PasswordWarningLbl = By.Id("ctl00_ctl00_ContentPlaceHolderBase1_ContentPlaceHolder1_cpLoginAspx_ctl00_LoginControl1_LTLogin_PasswordRequired");
       
        // Check Box
        public readonly By RememberMeChk = By.Id("ctl00_ctl00_ContentPlaceHolderBase1_ContentPlaceHolder1_cpLoginAspx_ctl00_LoginControl1_LTLogin_RememberMe");
      
        //Buttons
        public readonly By LoginBtn = By.Id("ctl00_ctl00_ContentPlaceHolderBase1_ContentPlaceHolder1_cpLoginAspx_ctl00_LoginControl1_LTLogin_Login");
       
        //Link
        public readonly By ForgotPasswordLnk = By.Id("ctl00_ctl00_ContentPlaceHolderBase1_ContentPlaceHolder1_cpLoginAspx_ctl00_LoginControl1_LTLogin_forgotPasswordbutton");


    
    }
}