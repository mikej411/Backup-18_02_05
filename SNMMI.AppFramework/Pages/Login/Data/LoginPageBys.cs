using OpenQA.Selenium;

namespace SNMMI.AppFramework
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
      

        // Links
        public readonly By ForgotPasswordLnk = By.Id("ctl00_ContentPlaceHolder1_cpLoginAspx_ctl00_LoginControl1_LTLogin_forgotPasswordbutton");

        // Menu Items    

        // Radio buttons

        // Tables       

        // Tabs

        // Text boxes
        public readonly By UserNameTxt = By.Id("ctl00_ContentPlaceHolder1_cpLoginAspx_ctl00_LoginControl1_LTLogin_UserName");
        public readonly By PasswordTxt = By.Id("ctl00_ContentPlaceHolder1_cpLoginAspx_ctl00_LoginControl1_LTLogin_Password");



    }
}