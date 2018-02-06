using Browser.Core.Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using CFPC.AppFramework;
using System.Data;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace CFPC.UITest
{
    //[BrowserMode(BrowserMode.New)]
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
   // [LocalSeleniumTestFixture(BrowserNames.Firefox)]
    [LocalSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Firefox)]
    [RemoteSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [TestFixture]
    public class CFPCMainproOtherTests : TestBase
    {
        #region Constructors
        public CFPCMainproOtherTests(string browserName) : base(browserName) { }

        // Remote Selenium Grid Test
        public CFPCMainproOtherTests(string browserName, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, version, platform, hubUri, extrasUri)
        { }
        #endregion

        /// <summary>
        /// Example of how to override the teardown at the test class level
        /// </summary>
        //public override void TearDown() 
        //{
        //    Browser.Manage().Window.Size = new System.Drawing.Size(1040, 784);
        //    CleanupBrowser();
        //}

        #region Tests


     
       // [Test]
        [Description("Verifying that the appropriate message appears whenever an advanced search is done with no return values")]
        [Property("Status", "Complete")]
        [Author("Daniel Nestor")]
        [Category("Nestor")]
        public void AdvancedSearchNoValuesReturnTest()
        {
            // Mike: You have 2 method inside Navigation that do the same thing. Remove one of these methods, and 
            // then utilize the config files instead if you need to use seperate URLs. We can talk about this.
            /// 1. Navigate to the login page
            LoginPage LP = Navigation.GoToLoginPageMainpro(browser);

            // Mike: Can remove this comment. I had this added in the early stages, its just clutter now. You can remove it in every test
            // Wrapper to login
            DashboardPage DP = LP.LoginAsUser("AutomationTestUser-DanielNestor", "test");

            /// 2. Click on the Enter a CPD activity button
            EnterACPDActivityPage EP = DP.ClickToAdvance(DP.EnterCPDActBtn);


            /// 3.  Select Assessment, Certified, CFPC Certified Mainpro+ Activites
            EP.FillEnterACPDActivityForm("Assessment", "Certified", "CFPC Certified Mainpro+ Activities");     // MIKE: See my comment inside this method
       
            EP.ClickToAdvance(EP.LiveInPersonRdoBtn);      // MIKE: See my comment inside this method

            // Mike: I added an end line above the below line. We want all step comments to have end lines above them to make the steps easier to read.
            // You had the above line EP.ClickToAdvance(EP.LiveInPersonRdoBtn) and the below line EP.ProgramActivityTitleTxt.SendKeys without an end line between them
            /// 4.  Fill out the search box with "This is a test sending keys" 
            EP.ProgramActivityTitleTxt.SendKeys("This is a test sending keys");
            //add wait critera
            Thread.Sleep(2000);

            /// 5. Click on the search button 
            EP.AdvancedSearchBtn.Click();

            Thread.Sleep(2000);

            /// 6. A message should appear indicating that there is no returned elements
            browser.WaitForElement(Bys.EnterACPDActivityPage.NoResultsLbl, ElementCriteria.IsVisible);
            Assert.True(EP.NoResultsLbl.Displayed);

        }


      
      //  [Test]
        [Description("A test for verifying that that the appropriate message appears for a search that returns more than 50 results")]     
        [Property("Status", "Complete")]
        [Author("Daniel Nestor")]
        [Category("Nestor")]
        public void AdvancedSearchTooManyResultsTest()
        {
            /// 1. Navigate to the login page
            LoginPage LP = Navigation.GoToLoginPageMainpro(browser);

           
            /// 2. Login As AutomationTestUser-DanielNestor
            DashboardPage DP = LP.LoginAsUser("AutomationTestUser-DanielNestor", "test");

            /// 3. Click on the Enter a CPD Activity Button
            EnterACPDActivityPage EP = DP.ClickToAdvance(DP.EnterCPDActBtn);
            
            /// 4. Select Group Learning, Certified, CFPC Certified Mainpro+ Activities         // MIKE: Added an end line above this line
            EP.FillEnterACPDActivityForm("Group Learning", "Certified", "CFPC Certified Mainpro+ Activities");

            /// 5. Click on the button to advance
            EP.ClickToAdvance(EP.LiveInPersonRdoBtn);

            /// 6. Enter "Heart" into the advanced search box
            EP.ProgramActivityTitleTxt.SendKeys("Heart");

            Thread.Sleep(2000);
             /// 7. Click on the search
            EP.AdvancedSearchBtn.Click();

            Thread.Sleep(2000);
            /// 8. verify that a message appears indicating that too many results appeared
            browser.WaitForElement(Bys.EnterACPDActivityPage.TooManyResultsLbl, ElementCriteria.IsVisible);
            Assert.True(EP.TooManyResultsLbl.Displayed);



        }

     
        [Test]
        [Description("Clicking through the tabs on the Mainpro+ page")]
        [Property("Status", "Complete")]
        [Author("Daniel Nestor")]
        [Category("Nestor")]
        public void TabClickThroughAutomation()
        {
            // Navigate to the login page
            LoginPage LP = Navigation.GoToLoginPageMainpro(browser);

            //Login and go to the Dashboard Page
            DashboardPage DP = LP.LoginAsUser("AutomationTestUser-DanielNestor", "test");

            //Go to the Credit Summary Page
            CreditSummaryPage CP = DP.ClickToAdvance(DP.CreditSummaryTab);

            //Go to the Holding Area Page
            HoldingAreaPage HP = CP.ClickToAdvance(CP.HoldingAreaTab);

            //Go to the CPD Activities List Page
            CPDActivitiesListPage ALP = HP.ClickToAdvance(HP.CPDActivitiesTab);

            //Go to the Planning Page
            CPDPlanningPage PP = ALP.ClickToAdvance(ALP.CPDPlanningTab);

            //Go to the Reports Page
            ReportsPage RP = PP.ClickToAdvance(PP.ReportsTab);

            //Once The Reports Page is hit, The test Passes
           

        }



        [Test]
        [Description("This Test checks to see that a the first page that appears for a new user is the correct page ")]
        [Property("Status", "Complete")]
        [Author("Daniel Nestor")]
        [Category("Nestor")]
        public void NewUserFrontPageTest()
        {

            //creating a random user with api calls          // Mike: If you are calling a method that has a Summary, you most likely can leave this comment out and let the Summary do the explaining. NOTE: Need to go to this method and remove parameters and update description   
            UserInfo NewUser1 = UserUtils.CreateUser("FrontPage");
            
            /// 1. Navigate to the login page and Log In               
            LoginPage LP = Navigation.GoToLoginPageMainpro(browser);      

            // Wrapper to login                                             
            DashboardPage DP = LP.LoginAsUser(NewUser1.Username, "test");

            //because the user is new, a eula will always appear on the page
            DP.EULAButton.Click();                                                // Put this at a lower level, inside the LoginAsUser method. Once you have a lot of tests, you dont want to have to add this line at the test level every time. See how we do it in RCP. We have LoginAsNewUser and LogInAsExistingUser. 
           

            // MIKE: The above If statement is not needed. Remove it and simply do this:
            Assert.True(DP.EnterCPDActBtn.Displayed);

        }


        [Test]
        [Description("This Test checks to see that an activity can be selected from the automation")]
        [Property("Status", "Complete")]
        [Author("Daniel Nestor")]
        [Category("Nestor")]
        public void MainproCertifiedSelectAnActivityTest()
        {

            /// 1. Navigate to the login page and Log In
            LoginPage LP = Navigation.GoToLoginPageMainpro(browser);

            // Wrapper to login
            DashboardPage DP = LP.LoginAsUser("AutomationTestUser-DanielNestor", "test");


        }

            #endregion Tests
        }



}












