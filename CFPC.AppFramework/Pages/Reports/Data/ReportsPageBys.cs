﻿using OpenQA.Selenium;

namespace CFPC.AppFramework
{
    /// <summary>
    /// Elements that will exist on the login page
    /// </summary>
    public class ReportsPageBys
    {

        // Buttons
        public readonly By LoginBtn = By.Id("ctl00_ContentPlaceHolder1_cpLoginAspx_ctl00_LoginControl1_LTLogin_Login");

        // Charts

        // Check boxes
        public readonly By RememberMeChk = By.Id("ctl00_ContentPlaceHolder1_cpLoginAspx_ctl00_LoginControl1_LTLogin_RememberMe");

        // Labels
        public readonly By LoginUnsuccessfullWarningLbl = By.Id("ctl00_ContentPlaceHolder1_cpLoginAspx_ctl00_LoginControl1_LTLogin_FailureText");
        public readonly By PasswordWarningLbl = By.Id("ctl00_ContentPlaceHolder1_cpLoginAspx_ctl00_LoginControl1_LTLogin_PasswordRequired");
        public readonly By UserNameWarningLbl = By.Id("ctl00_ContentPlaceHolder1_cpLoginAspx_ctl00_LoginControl1_LTLogin_UserNameRequired");

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