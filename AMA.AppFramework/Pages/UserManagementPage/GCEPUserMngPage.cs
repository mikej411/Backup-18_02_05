using Browser.Core.Framework;
using Browser.Core.Framework.Data;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace AMA.AppFramework
{
    public class GCEPUserMngPage : AMAPage, IDisposable
    {
        #region constructors
        public GCEPUserMngPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that I start so I can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "gme-competency/admin/d26fda86-7d2a-4ea4-91ac-dbb0e6ecc2a2/users"; } } 
    

        #endregion properties

        #region elements

        // Main page
        
        
        public IWebElement StatusLbl { get { return this.FindElement(Bys.GCEPUserMngPage.StatusLbl); } }    

        public SelectElement StatusSelElem { get { return new SelectElement (this.FindElement(Bys.GCEPUserMngPage.StatusSelElem)); } }

        public SelectElement FilterBySelElem { get { return new SelectElement(this.FindElement(Bys.GCEPUserMngPage.FilterBySelElem)); } }

        public IWebElement NoRecorMatchLabel { get { return this.FindElement(Bys.GCEPUserMngPage.NoRecorMatchLbl); } }

        public IWebElement UsersManagementTbl { get { return this.FindElement(Bys.GCEPUserMngPage.UsersManagementTbl); } }

        public IWebElement CancelBtn { get { return this.FindElement(Bys.GCEPUserMngPage.CancelBtn); } }

        public IWebElement CancelSendBtn { get { return this.FindElement(Bys.GCEPUserMngPage.CancelSendBtn); } }

        public IWebElement AcceptBtn { get { return this.FindElement(Bys.GCEPUserMngPage.AcceptBtn); } }

        public IWebElement DismissBtn { get { return this.FindElement(Bys.GCEPUserMngPage.DismissBtn); } }

        public IWebElement ActionGearBtn { get { return this.FindElement(Bys.GCEPUserMngPage.ActionGearBtn); } }

        public IWebElement UserManagementLbl { get { return this.FindElement(Bys.GCEPUserMngPage.UserManagementLbl); } }

        public IWebElement UserLnk { get { return this.FindElement(Bys.GCEPUserMngPage.UserLnk); } }



        #endregion elements

        #region methods: per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(120), Criteria.GCEPUserMngPage.PageReady);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            try { activeRequests.Clear(); }
            catch (Exception ex) { _log.ErrorFormat("Failed to dispose GCEEPUserMngPage", activeRequests.Count, ex); }
        }

        #endregion methods: per page

        #region methods: page specific

        ///// <summary>
        ///// Clicks the user-specified button or link and then waits for a window to close or open, or a page to load,
        ///// depending on the button that was clicked
        ///// </summary>
        ///// <param name="buttonOrLinkElem">The element to click on</param>
        public dynamic ClickToAdvance(IWebElement buttonOrLinkElem)
        {
            if (Browser.Exists(Bys.GCEPUserMngPage.UserLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == UserLnk .GetAttribute("outerHTML"))
                {
                    UserLnk.Click();
                    Browser.WaitForElement(Bys.UserManagementPage.SaveBtn, TimeSpan.FromSeconds(120), ElementCriteria.IsVisible,ElementCriteria.IsEnabled);
                    new WebDriverWait(Browser, TimeSpan.FromSeconds(90)).Until(ExpectedConditions.UrlContains("users/manageuser"));
                    // Browser.SwitchTo().Frame(G.EnterACPDFrame);
                    return new UserManagementPage(Browser);
                }
            }
            if (Browser.Exists(Bys.AMAPage.GMECompetencyEducationProgramLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == GMECompetencyEducationProgramLnk.GetAttribute("outerHTML"))   // GMECompetencyEducationProgramLnk.GetAttribute("outerHTML"))  
                {
                    GMECompetencyEducationProgramLnk.SendKeys(Keys.Tab); // AdministrationLnk
                    GMECompetencyEducationProgramLnk.Click(); 
                   // AdministrationLnk.Click();
                    GCEPPage GP = new GCEPPage(Browser);
                    GP.WaitForInitialize();
                    return GP;
                }
            }
            else
            {
                throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, or if the button is already added, then the page you were on did not contain the button.");
            }

            return null;
        }


        /// <summary>
        /// Selecting a value from the Status dropdown, Entering text in the name text box and clicking search
        /// </summary>
        /// <param name="UserStatus">Select user status from dropdown</param>
        /// <param name="UserName">passing username</param>
        public  void SearchForUserByStatusAndName(String UserStatus, String UserName)
        {
            Browser.WaitForElement(Bys.AMAPage.LoadIcon,TimeSpan.FromSeconds(180), ElementCriteria.IsNotVisible);
            StatusSelElem.SelectByValue(UserStatus);
            SearchTxt.Clear();
            SearchTxt.SendKeys(UserName);
            Browser.WaitForElement(Bys.AMAPage.SearchBtn, TimeSpan.FromSeconds(180), ElementCriteria.IsEnabled,ElementCriteria.IsVisible);
            try
            {
                Thread.Sleep(0500);
                SearchBtn.Click();
            }
            catch (InvalidOperationException)
            {
                SearchTxt.SendKeys(Keys.Enter);
                SearchTxt.SendKeys(Keys.Enter);
            }
           
           // StatusSelElem.SelectByValue(UserStatus);
            Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);         
        }


        /// <summary>
        /// Manupulating user management table
        /// </summary>
        /// <param name="Userrole"></param>
        /// <param name="UserStatus"></param>
        /// <param name="UserName"></param>
        public void SearchForUserByRoleandStatusAndName(string UserRole,string UserStatus, string UserName)
        {
            FilterBySelElem.SelectByText(UserRole);
            SearchForUserByStatusAndName(UserStatus, UserName);
        }

        /// <summary>
        /// verifiying  text inside of table and asserting returns boolean
        /// </summary>
        /// <param name="tableBodyElem">table weblelemnt where to find text to verify</param>
        /// <param name="expectedText"> text what you expecting to get</param>
        /// <returns></returns>  
        public bool Grid_CellTextFoundAMATest(IWebElement tableBodyElem, string expectedText)
        {
            Thread.Sleep(0500);
            IList<IWebElement> allRows = tableBodyElem.FindElements(By.XPath("./div/div/div[2]/div/div")); // Store all TR (rows) from the table into a variable
            foreach (var row in allRows)  // Loop through each row
            {
               IWebElement cell = row.FindElements(By.XPath("./div/div"))[3]; // Get the cell under the first column
                    
                    if (cell.Text.Contains(expectedText))
                    {
                            return true;                         
                    }
                    else
                    {
                       if (cell.Equals( expectedText))
                       {
                            return true;
                       }
                    }              
            }

            return false;
        }

        /// <summary>
        /// Choosing form Select dropdown
        /// </summary>
        /// <param name="topLevelMenuId"></param>
        /// <param name="subMenuLinkText"></param>
        public void Select(string topLevelMenuId, string subMenuLinkText)
        {
            try
            {
                
                WaitForInitialize();            
                var toplevelMenuList = Browser.FindElements(By.XPath("//div[contains(@class, " + topLevelMenuId + ")]//button[contains(@class, 'ins-admin-btn-cp')]")).ToList();
                if (toplevelMenuList.Any())
                {
                    toplevelMenuList.First().Click();
                }

                Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);

                List<IWebElement> subLevelMenuLinks = Browser.FindElements(By.XPath("//a/span[.='" + subMenuLinkText + "']/..")).Where(g => g.Displayed && g.Enabled).ToList();
                if (subLevelMenuLinks.Any())
                {
                    subLevelMenuLinks.First().Click();
                    Browser.WaitForElement(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);                 

                }
            }
            catch (NoSuchElementException)
            {
                List<IWebElement> subLevelMenuLinks = Browser.FindElements(By.XPath("//a/span[.='" + subMenuLinkText + "']/..")).Where(g => g.Displayed && g.Enabled).ToList();
                subLevelMenuLinks.First().Click();
            }

        }

        /// <summary>
        /// Getting header from user managment page spliting text and getting second part whic contains number and casting to integer
        /// </summary>
        /// <returns>count of courses </returns>
        public int GetCountOfUsersFromUserManagementLabel()
        {
            WaitForInitialize();
            Browser.WaitForElement(Bys.GCEPUserMngPage.ActionGearBtn, ElementCriteria.IsVisible,ElementCriteria.IsEnabled);
            string CountOfUsersOnUserMngPage = UserManagementLbl.Text;
            string[] separatingChars = { "(", ")" };
            string[] s = CountOfUsersOnUserMngPage.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);
            int CountOfUsers = Convert.ToInt32(s[1]);
            return CountOfUsers;
        }

       
        /// <summary>
        /// Choosing user by Linktext GCEP User management page
        /// </summary>
        /// <param name="UserFullname"> User name the way how its appear on the table or DOM</param>
        /// <returns></returns>
        public dynamic chooseUser(string UserFullname)
        {
             Browser.FindElement(By.LinkText(UserFullname)).Click();
             Browser.WaitForElement(Bys.UserManagementPage.SaveBtn, TimeSpan.FromSeconds(120), ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
             UserManagementPage UMP = new UserManagementPage(Browser);
             UMP.WaitForInitialize();
             return UMP;
        }

        #endregion methods: page specific
    }
}