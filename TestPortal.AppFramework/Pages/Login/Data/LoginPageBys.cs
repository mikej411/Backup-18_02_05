using OpenQA.Selenium;

namespace TP.AppFramework
{
    /// <summary>
    /// Elements that will exist on the login page
    /// </summary>
    public class LoginPageBys
    {     

        // Buttons
        public readonly By LoginBtn = By.Id("ctl00_ContentPlaceHolder1_cpLoginAspx_ctl00_LoginControl1_LTLogin_Login");

        // Charts

        // Check boxes

        // Labels                                              
        public readonly By UserNameWarningLbl = By.Id("ctl00_ContentPlaceHolder1_cpLoginAspx_ctl00_LoginControl1_LTLogin_UserNameRequired");
        public readonly By LoginUnsuccessfullWarningLbl = By.Id("ctl00_ContentPlaceHolder1_cpLoginAspx_ctl00_LoginControl1_LTLogin_FailureText");

        // Links

        // Menu Items    

        // Radio buttons

        // Tables       

        // Tabs

        // Text boxes
        public readonly By UserNameTxt = By.Id("ctl00_ContentPlaceHolder1_cpLoginAspx_ctl00_LoginControl1_LTLogin_UserName");
        public readonly By PasswordTxt = By.Id("ctl00_ContentPlaceHolder1_cpLoginAspx_ctl00_LoginControl1_LTLogin_Password");
    }
}