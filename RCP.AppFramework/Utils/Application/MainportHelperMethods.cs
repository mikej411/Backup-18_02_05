using Browser.Core.Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using RCP.AppFramework;
using System.Data;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using Browser.Core.Framework.Utils;
using System.Diagnostics;
using OpenQA.Selenium.Firefox;
using LS.AppFramework.HelperMethods;
using LS.AppFramework.Constants;

namespace RCP.AppFramework
{
    public static class MainportHelperMethods
    {
        #region properties

        #endregion properties

        #region methods

        /// <summary>
        /// If you add an activity which gives you credits, this method can be called to wait for a user-specified label on your application
        /// to get updated with those credits after that activity was added. Once an activity is submitted, a record gets put into a windows service
        /// queue, and then waits for that service to push the activity through, because of this, we need to wait in our code. Note that there
        /// is not database flag to check instead of just randomly refreshing every couple of seconds. Right now, we will refresh every 4 seconds,
        /// 30 times. If your application ever takes longer than that, then increase the For loop below
        /// Note: This method is a workaround to be able to refresh this page. It can be removed when/if defect 
        /// https://code.premierinc.com/issues/browse/RCPSC-793 is fixed. If that gets fixed, then
        /// we will remove this method, and just use the regular <see cref="ApplicationUtils.WaitForCreditsToBeApplied(Page, By, string)"/>
        /// </summary>
        /// <param name="Page">The page to refresh</param>
        /// <param name="creditLabelBy">the label which stores the amount of credits that you are waiting to be refreshed</param>
        /// <param name="amountOfCredits">The amount of credits that will show when the windows service is complete</param>
        public static void WaitForCreditsToBeApplied(IWebDriver Browser, Page page, By creditLabelBy, string amountOfCredits)
        {
            MyMOCPage MP = new MyMOCPage(Browser);
            MyDashboardPage DP = new MyDashboardPage(Browser);

            DP.ClickAndWaitBasePage(DP.MyMOCTab);
            DP.ClickAndWaitBasePage(DP.MyDashboardTab);

            ApplicationUtils.WaitForCreditsToBeApplied(page, creditLabelBy, amountOfCredits);
        }

        /// <summary>
        /// Send as many activities as you want to this method, and this will determine if they need credit validation, and if so, they will validate them. Specificaly,
        /// for activities that need credit validation, this clicks on the the Self Reporting tab on the Program Page of Lifetime support, clicks the Actions>Validate 
        /// link for a user-specified activity, waits for the Credit Validation page to appear, clicks the Accept radio button, clicks the Submit button, and waits for
        /// the page to be done loading. 
        /// </summary>
        /// <param name="activities"><see cref="Activity"/></param>
        public static void ValidateCreditsIfApplicable(IWebDriver browser, UserInfo user, params Activity[] activities)
        {
            LSHelperMethods LSHelp = new LSHelperMethods();

            foreach (Activity act in activities)
            {
                if (act.RequiresValidation)
                {
                    LSHelp.ValidateCredit(browser, "Royal College of Physicians", user.FullName, "Maintenance of Certification", act.ActivityName);
                }
            }
        }


        #endregion methods
    }
}
