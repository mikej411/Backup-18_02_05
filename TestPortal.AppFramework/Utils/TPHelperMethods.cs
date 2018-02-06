using Browser.Core.Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Data;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using Browser.Core.Framework.Utils;
using System.Diagnostics;
using OpenQA.Selenium.Firefox;
using TP.AppFramework.Constants;
using TP.AppFramework;
using System.Configuration;

namespace TP.AppFramework.HelperMethods
{
    /// <summary>
    /// A class that consists of methods which combine custom page methods to accomplish various tasks for this application. This is mainly
    /// called/used when a tester is automating another application, and needs to also access this application to setup data or verify functionality
    /// </summary>
    public class TPHelperMethods
    {
        #region properties



        #endregion properties

        #region methods

        #region methods: CME360

        public bool ActivityAppearingWithAID(IWebDriver browser, string activityAID)
        {
            string url = string.Format("{0}/activity/{1}/activity.aspx", ConfigurationManager.AppSettings["TestPortalURL"].ToString(), activityAID);
            browser.Navigate().GoToUrl(url);

            try
            {
                ActivityDetailsPage page = new ActivityDetailsPage(browser);
                page.WaitForInitialize();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ActivityNotAppearingWithAID(IWebDriver browser, string activityAID)
        {
            string url = string.Format("{0}/activity/{1}/activity.aspx", ConfigurationManager.AppSettings["TestPortalURL"].ToString(), activityAID);
            browser.Navigate().GoToUrl(url);

            try
            {
                ActivityNotAvailablePage page = new ActivityNotAvailablePage(browser);
                page.WaitForInitialize();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion methods: CME360

        #region methods: general

        /// <summary>
        /// Logs in. This Login method should only be called/used from within this class (mainly when coding tests on another application).
        /// Otherwise, the Login page class's Login method should be used
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public void Login(IWebDriver browser, string username, string password)
        {
            string url = string.Format("{0}{1}", ConfigurationManager.AppSettings["TestPortalURL"].ToString(), "login.aspx");
            browser.Navigate().GoToUrl(url);

            LoginPage LP = new LoginPage(browser);
            LP.WaitForInitialize();

            LP.UserNameTxt.Clear();
            LP.PasswordTxt.Clear();
            LP.UserNameTxt.SendKeys(username);
            LP.PasswordTxt.SendKeys(password);
            LP.PasswordTxt.SendKeys(Keys.Tab);
            LP.ClickAndWait(LP.LoginBtn);
        }

        public string ActivityFee(IWebDriver browser, string activityAID)
        {
            string url = string.Format("{0}/activity/{1}/activity.aspx", ConfigurationManager.AppSettings["TestPortalURL"].ToString(), activityAID);
            browser.Navigate().GoToUrl(url);
            ActivityDetailsPage ADP = new ActivityDetailsPage(browser);
            ADP.WaitForInitialize();

            ADP.ResumeSelectBtn.Click();
            PaymentPage PP = new PaymentPage(browser);
            PP.WaitForInitialize();

            return PP.FeeAmountValueLbl.Text;
        }

        #endregion methods: general 

        #endregion methods

    }
}
