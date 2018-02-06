using Browser.Core.Framework;
using OpenQA.Selenium;
using System;

namespace AMA.AppFramework
{
	public static class Navigation
	{
        // Responsible for basic page navigation and specific-page initialization
        public static LoginPage GoToLoginPage(this IWebDriver driver, bool waitForInitialize = true)
        {
            var page = Navigate(p => new LoginPage(p), driver, waitForInitialize);
            return new LoginPage(driver);
        }

        public static EducationCenterPage GoToEducationCenterPage(this IWebDriver driver, bool waitForInitialize = true)
        {
            var page = Navigate(p => new EducationCenterPage(p), driver, waitForInitialize);
            return new EducationCenterPage(driver);
        }

        public static CurriculumMngPage GoToCurriculumMngmntPage(this IWebDriver driver, bool waitForInitialize = true)
        {
            var page = Navigate(p => new CurriculumMngPage(p), driver, waitForInitialize);
            return new CurriculumMngPage(driver);
        }

        private static T Navigate<T>(Func<IWebDriver, T> createPage, IWebDriver driver, bool waitForInitialize) where T : Page
        {
            var page = createPage(driver);
            page.GoToPage(waitForInitialize);
            return page;
        }

    }
}
