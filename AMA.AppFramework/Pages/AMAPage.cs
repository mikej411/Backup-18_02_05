using Browser.Core.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using Microsoft.CSharp;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using System;
using OpenQA.Selenium.Interactions;
using System.Linq;
using NUnit.Framework;

namespace AMA.AppFramework
{
    public abstract class AMAPage : Page
    {
        #region Constructors

        public AMAPage(IWebDriver driver): base(driver){}

        #endregion

        #region Elements


        // Menu Items

        public IWebElement Menu_About { get { return this.FindElement(Bys.AMAPage.Menu_MyLibrary); } }
        public IWebElement Menu_MyCPDActivitiesList { get { return this.FindElement(Bys.AMAPage.Menu_MyCPDActivitiesList); } }
        public IWebElement LoadIcon { get { return this.FindElement(Bys.AMAPage.LoadIcon); } }
        public IWebElement SignOutLnk { get { return this.FindElement(Bys.AMAPage.SignOutLnk); } }
        public IWebElement SearchTxt { get { return this.FindElement(Bys.AMAPage.SearchTxt); } }
        public IWebElement SearchBtn { get { return this.FindElement(Bys.AMAPage.SearchBtn); } }
        public IWebElement GMECompetencyEducationProgramLnk { get { return this.FindElement(Bys.AMAPage.GMECompetencyEducationProgramLnk); } }
        public IWebElement CountTableItemLbl { get { return this.FindElement(Bys.AMAPage.CountTableItemLbl); } }
        public IWebElement BreadCrumbLnksContainer { get { return this.FindElement(Bys.AMAPage.BreadCrumbLnksContainer); } }
        public IWebElement ResidentsChrt { get { return this.FindElement(Bys.AMAPage.ResidentsChrt); } }
        public IWebElement HelpLnk { get { return this.FindElement(Bys.AMAPage.HelpLnk); } }
        public IWebElement AMALogoLnk { get { return this.FindElement(Bys.AMAPage.AMALogoLnk); } }
        public IWebElement AdministrationLnk { get { return this.FindElement(Bys.AMAPage.AdministrationLnk); } }
        public IWebElement HelpfromYourInstitutionLnk { get { return this.FindElement(Bys.AMAPage.HelpfromYourInstitutionLnk); } }
        public IWebElement HeaderMenuDropDown { get { return this.FindElement(Bys.AMAPage.HeaderMenuDropDown); } }
        public IWebElement FaceBookLnk { get { return this.FindElement(Bys.AMAPage.FaceBookLnk); } }
        public IWebElement GCEPNotificationsBtn { get { return this.FindElement(Bys.AMAPage.GCEPNotificationsBtn); } }
        public IWebElement NotificationTitlesLbl { get { return this.FindElement(Bys.AMAPage.NotificationTitlesLbl); } }



        #endregion Elements

        #region Methods

        /// <summary>
        /// Navigates to a given page through a single or multi-layered menu system
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="menuItems">If it is a single-layered menu, then only 1 By type will be needed. If it is multi-layered, the tester should
        /// pass the By types in the order they would click them manually</param>
        /// <returns>The page object, which contains all elements of the page and any page related methods</returns>
        public static dynamic NavigateThroughMenuItems(IWebDriver browser, params By[] menuItems) //By menu1, By menu2 = null, By menu3 = null,
        {
            if(menuItems.Length == 1)
            {
                IWebElement elemToClick = browser.FindElement(menuItems[0]);
                elemToClick.Click();
            }

            
            else
            {
                for(int i = 0; i < menuItems.Length - 1; i++)
                {
                    WebDriverWait wait = new WebDriverWait(browser, TimeSpan.FromSeconds(1));
                    IWebElement elemToHover = wait.Until(ExpectedConditions.ElementIsVisible(menuItems[0]));
                    Actions action = new Actions(browser);
                    action.MoveToElement(elemToHover).Perform();
                }

                IWebElement elemtToClick = browser.FindElement(menuItems[menuItems.Length - 1]);
                elemtToClick.Click();
            }

            //if (browser.Url.Contains("CPDActivities.aspx"))
            //{
            //    var APPage = new ActivitiesListPage(browser);
            //    APPage.WaitForInitialize();
            //    return APPage;
            //}


            //if (browser.Url.Contains("About Me"))
            //{
            //    var aboutPage = new AboutPage(browser);
            //    aboutPage.WaitForInitialize();
            //    return aboutPage;
            //}


            return null;
        }
        /// <summary>
        /// Seaching from page
        /// </summary>
        /// <param name="Anything"> what you searching for</param>
        public void Search(string Anything)
        {
           
            SearchTxt.Clear();
            SearchTxt.SendKeys(Anything);
            Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);
            SearchBtn.Click();
            Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);

        }
        /// <summary>
        /// talk with mike
        /// </summary>
        /// <returns></returns>

        public string GetBreadCrumbContainerText()
        {
            string text = Browser.FindElement(Bys.AMAPage.BreadCrumbLnksContainer).Text.ToLower();
            return text;
        }
        /// <summary>
        /// again mike
        /// </summary>
        /// <param name="LinkText"></param>
        /// <returns></returns>
        public dynamic ClickToBreadCrumbContainerToReturnGcep(string LinkText)
        {           
                string xPathVariable = string.Format("//li/a[contains(.,'{0}')]", LinkText);
                IWebElement BreadCrumpWithName = Browser.FindElement(By.XPath(xPathVariable));
                BreadCrumpWithName.Click();
                Thread.Sleep(1000);
                GCEPPage GP = new GCEPPage(Browser);
                GP.WaitForInitialize();             
                return GP;            
        }

        public int Grid_GetCountOfItemsOnTable(string firsSeparator, string secondSeparator)
        {
            WaitForInitialize();
            Browser.WaitForElement(Bys.GCEPUserMngPage.ActionGearBtn, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
            string CountOfUsersOnUserMngPage = CountTableItemLbl.Text;
            string[] separatingChars = { firsSeparator, secondSeparator };
            string[] s = CountOfUsersOnUserMngPage.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);
            int CountOfUsers = Convert.ToInt32(s[1]);
            return CountOfUsers;
        }

        public dynamic ClickToBreadCrumbContainerToReturnInsGCEP(string LinkText)
        {
            string xPathVariable = string.Format("//li/a[contains(.,'{0}')]", LinkText);
            IWebElement BreadCrumpWithName = Browser.FindElement(By.XPath(xPathVariable));
            BreadCrumpWithName.Click();
            Thread.Sleep(1000);
            InstitutionsGCEPPage IGP = new InstitutionsGCEPPage(Browser);            
            return IGP;
        }


        public dynamic UseDropDownOnRightSide(IWebElement LinkToClick)
        {
            HeaderMenuDropDown.Click();
            Thread.Sleep(200);
            if (LinkToClick.GetAttribute("outerHTML") == SignOutLnk.GetAttribute("outerHTML"))
            {
                SignOutLnk.Click();
                new WebDriverWait(Browser, TimeSpan.FromSeconds(45)).Until(ExpectedConditions.UrlMatches("https://logintest.ama-assn.org/account/logout"));
                return this.Browser;
            }
            if (LinkToClick.GetAttribute("outerHTML") == HelpfromYourInstitutionLnk.GetAttribute("outerHTML"))
            {               
                HelpfromYourInstitutionLnk.Click();
                Browser.SwitchTo().Window(Browser.WindowHandles.Last());
                Thread.Sleep(0500);
                Browser.Manage().Window.Maximize();
                Thread.Sleep(0500);
                Browser.SwitchTo().Window(Browser.WindowHandles.First()).Close();
                if (Browser.WindowHandles.Count > 1) { Browser.SwitchTo().Window(Browser.WindowHandles.First()).Close(); }
                Thread.Sleep(0500);
                Browser.SwitchTo().Window(Browser.WindowHandles.Last());
                Browser.Manage().Window.Maximize();
                Thread.Sleep(0500);
                HelpPage HP = new HelpPage(Browser);
                HP.WaitForInitialize();
                return HP;
            }

            return null;
        }



        /// <summary>
        /// Comparing dates of courses and returning boolean.Collecting all dates for courses and verifying ascending order. 
        /// </summary>
        /// <returns>true,false</returns>
        public Boolean ResidentCourseCompareDate()
        {
            List<string> DateDueText = new List<string>(Browser.FindElements(By.XPath("//span[contains(text(),'Due')]")).Select(iw => iw.Text));
            List<string> Dates = new List<string>();
            for (int j = 0; j < DateDueText.Count(); j++)
            {
                Dates.Add(DateDueText[j].Substring(4, (DateDueText[j].Length) - 4));
            }
            if (Dates.Count > 0)
            {
                for (int i = 0; i < Dates.Count() - 1; i++)
                {
                    DateTime row = Convert.ToDateTime(Dates[i]);
                    DateTime rowNextDate = Convert.ToDateTime(Dates[i + 1]);
                    Assert.True(AssertUtils.DateGreaterThanOrEquals(row, rowNextDate));
                    Assert.True(row <= rowNextDate);
                }
                return true;
            }
            return false;
        }


        /// <summary>
        /// method collecting all duration for courses and comparing them verifying its ascending order
        /// </summary>
        /// <returns>true or false</returns>
        public Boolean ResidentCourseCompareDuration()
        {
            List<string> DurationText = new List<string>(Browser.FindElements(By.XPath("//span[contains(text(),'Mins')]")).Select(iw => iw.Text));
            List<string> Durations = new List<string>();
            for (int j = 0; j < DurationText.Count(); j++)
            {
                Durations.Add(DurationText[j].Substring(0, (DurationText[j].Length) - 5));
            }
            if (Durations.Count > 0)
            {
                for (int i = 0; i < Durations.Count() - 1; i++)
                {
                    int CourseDuration = Convert.ToInt32(Durations[i]);
                    int CourseDurationNext = Convert.ToInt32(Durations[i + 1]);
                    Assert.True(CourseDuration <= CourseDurationNext);
                }
                return true;
            }
            return false;
        }


        /// <summary>
        /// this method getting text form cousre and verifying its following order from AC
        /// </summary>
        /// <param name="FailedLbl">WebElements for failded course</param>
        /// <param name="LockedLbl">WebElements for locked course</param>
        /// <returns></returns>
        public Boolean ResidentCourseProgressVerification(IWebElement FailedLbl,IWebElement LockedLbl)
        {
            List<string> ProgressText = new List<string>(Browser.FindElements(By.XPath("//div[@class='col-xs-12 col-md-2 activity-status-action']//button")).Select(iw => iw.Text));
            if (ProgressText.Count >= 1)
            {
                for (int i = 0; i < ProgressText.Count() - 1; i++)
                {
                    string CourseStatusBtn = ProgressText[i];
                    string CourseStatusBtnNext = ProgressText[i + 1];
                    if (CourseStatusBtnNext == "Continue" && FailedLbl.Displayed)
                    {
                        Assert.True(CourseStatusBtn == "Continue" && (FailedLbl.Displayed | LockedLbl.Displayed));
                    }
                    if (CourseStatusBtnNext == "Start")
                    {
                        Assert.True(CourseStatusBtn == "Continue" | CourseStatusBtn == "Start");

                    }
                    if (CourseStatusBtnNext == "View Certificate")
                    {
                        Assert.True(CourseStatusBtn == "Continue" | CourseStatusBtn == "Start" | CourseStatusBtn == "View Certificate");

                    }
                }
                return true;
            }
            return false;
        }

        public Boolean VerifyNotificationTitle(IWebElement notificationsRows, string notificationTitle)
        {
            IList<IWebElement> notificationsRow = notificationsRows.FindElements(By.XPath("//div[@class='col-sm-3 margin-bottom-5']/strong"));
            List<string> NotificationTitleText = new List<string>(notificationsRow.Select(iw => iw.Text));
            for (int i = 0; i < NotificationTitleText.Count; i++)
            {
                if (NotificationTitleText[i].Contains(notificationTitle))
                {
                    return true;
                }
            }
            return false;
        }

        #endregion Methods
    }
}