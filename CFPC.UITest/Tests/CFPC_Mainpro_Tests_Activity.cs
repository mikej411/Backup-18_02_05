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
    [LocalSeleniumTestFixture(BrowserNames.Firefox)]
    [LocalSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Firefox)]
    [RemoteSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [TestFixture]
    public class CFPCActivityCreationMainproTests : TestBase
    {
        #region Constructors
        public CFPCActivityCreationMainproTests(string browserName) : base(browserName) { }

        // Remote Selenium Grid Test
        public CFPCActivityCreationMainproTests(string browserName, string version, string platform, string hubUri, string extrasUri)
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




        [Test]
        [Description("This test checks to see that all of the elements are selectable for the updated boxes and that the " +
            "correct text appears")]
        [Property("Status", "Complete")]
        [Author("Daniel Nestor")]
        [Category("Activity")]
        public void CreateAnAssessmentActivityTest()
        {
             
            /// 1. Navigate to the login page
            LoginPage LP = Navigation.GoToLoginPageMainpro(browser);

            //create the dashboard page, but do not initalize it yet
            DashboardPage DP = null;

            /// 2. Login as a different user depending on which browser it is
            if (BrowserName == BrowserNames.InternetExplorer)
            {
                DP = LP.LoginAsUser("Automation Explorer User Assessment", "test");
            }
            else if (BrowserName == BrowserNames.Chrome)
            {
                DP = LP.LoginAsUser("Automation Chrome User Assessment", "test");
            }
            else if (BrowserName == BrowserNames.Firefox)
            {
                DP = LP.LoginAsUser("Automation Firefox User Assessment", "test");
            }
            else
            {
                //if an invalid browser is used
                Assert.Fail();
            }
                                                                    // MIKE: Removed a bunch of end lines here.
            double initialCreditValue = DP.GetTotalCredits();        

            EnterACPDActivityPage EP = DP.ClickToAdvance(DP.EnterCPDActBtn);

            /// 3. create an activity that is a Certified Assessment, Other Activity                  MIKE: Added an end line above here
            EP.FillEnterACPDActivityForm("Assessment", "Certified", "Other Certified Assessment Activities");
            EP.LiveInPersonRdoBtn.Click();                                             
            EP.ClickToAdvance(EP.LiveInPersonRdoBtn);

            
            /// 4. Click continue after all of the options have been selected               
            EP.ContinueBtn.Click();
              //Daniel: Wait Criteria Added
           
            /// 5. Fill out the details 
            EP.FillOutAndSubmitAssessmentForm();   // MIKE: See comments inside method

            DP.DashboardTab.Click();
            Thread.Sleep(1000);  // MIKE: Add wait criteria

            double newCreditValue = DP.GetTotalCredits();

            //loop over until the credits update
            do
            {
                Thread.Sleep(5000);
                browser.Navigate().Refresh();
                newCreditValue = DP.GetTotalCredits();
            } while (newCreditValue == initialCreditValue);

            Assert.AreEqual(initialCreditValue + 1, newCreditValue);



        }

      
        [Test]
        [Description("This test checks to see if A group activity is able to be created")]
        [Property("Status", "Complete")]
        [Author("Daniel Nestor")]
        [Category("Activity")]
        public void CreateAGroupLearningActivityTest()
        {
            /// 1. Navigate to the login page and Log In
            LoginPage LP = Navigation.GoToLoginPageMainpro(browser);

            //create the dashboard page, but do not initalize it yet
            DashboardPage DP = null;

            /// 2. Login as a different user depending on which browser the test is running on
            if (BrowserName == BrowserNames.InternetExplorer)
            {
                DP = LP.LoginAsUser("Automation Explorer User Group Learning", "test");
            }
            else if (BrowserName == BrowserNames.Chrome)
            {
                DP = LP.LoginAsUser("Automation Chrome User Group Learning", "test");
            }
            else if (BrowserName == BrowserNames.Firefox)
            {
                DP = LP.LoginAsUser("Automation Firefox User Group Learning", "test");
            }
            else
            {
                //if an invalid browser is used
                Assert.Fail();
            }


            //improving the test by comparing the original value in the Dashboard Page's 
            //Credit summary with a new updated value
            double initialCreditValue = DP.GetTotalCredits();

            /// 3. Click on the Enter a CPD Activity button
            EnterACPDActivityPage EP = DP.ClickToAdvance(DP.EnterCPDActBtn);

            /// 4. Fill out the CPD Page with Group Learning Certified, Other Certified Group Learning Activities and select Live & In person
            EP.FillEnterACPDActivityForm("Group Learning", "Certified", "Other Certified Group Learning Activities");
            EP.LiveInPersonRdoBtn.Click();
            Thread.Sleep(2000);

            EP.ContinueBtn.Click();


            /// 5. Fill out the Self Learning Form
            Thread.Sleep(2000);
            EP.FillOutGroupLearningForm();

            Thread.Sleep(1000);
          
            Browser.ExecuteScript("arguments[0].click();", EP.PopupSubmitBtn);
       


            //finally click on the Dashboard page's tab
            DP.DashboardTab.Click();

            Thread.Sleep(4000);

            double newCreditValue = DP.GetTotalCredits();

            //loop over until the credits update
            do
            {
                Thread.Sleep(5000);
                browser.Navigate().Refresh();
                newCreditValue = DP.GetTotalCredits();
            } while (newCreditValue == initialCreditValue);

            /// 6. Check to see that the new Credit Value is equal to the initial value + 1
            Assert.AreEqual(initialCreditValue + 1, newCreditValue);

        }


        [Test]
        [Description("This test checks to see if A Self Learning is able to be created")]
        [Property("Status", "Complete")]
        [Author("Daniel Nestor")]
        [Category("Activity")]
        public void CreateASelfLearningActivityTest()
        {
            /// 1. Navigate to the login page and Log In
            LoginPage LP = Navigation.GoToLoginPageMainpro(browser);

        

            //create the dashboard page, but do not initalize it yet
            DashboardPage DP = null;

            /// 2. Login as a different user depending on which browser it is
            if (BrowserName == BrowserNames.InternetExplorer)
            {
                DP = LP.LoginAsUser("Automation Explorer User Self Learning", "test");
            }
            else if (BrowserName == BrowserNames.Chrome)
            {
                DP = LP.LoginAsUser("Automation Chrome User Self Learning", "test");
            }
            else if (BrowserName == BrowserNames.Firefox)
            {
                DP = LP.LoginAsUser("Automation Firefox User Self Learning", "test");
            }
            else
            {
                //if an invalid browser is used
                Assert.Fail();
            }




            //get the initial credit value
            double initialCreditValue = DP.GetTotalCredits();

            /// 2. Click on the Enter a CPD Activity button
            EnterACPDActivityPage EP = DP.ClickToAdvance(DP.EnterCPDActBtn);

            /// 3. Fill out the CPD Page with Group Learning Certified, Other Certified Group Learning Activities and select Live & In person
            EP.FillEnterACPDActivityForm("Self-Learning", "Certified", "Other Certified Self Learning Activities");
            EP.LiveInPersonRdoBtn.Click();
            Thread.Sleep(2000);

            EP.ContinueBtn.Click();

            /// 4. Fill out the Self Learning Form
            Thread.Sleep(2000);
            EP.FillOutSelfLearningForm();
            Thread.Sleep(1000);

            

            //click on the popup submit button
            Browser.ExecuteScript("arguments[0].click();", EP.PopupSubmitBtn);

            //wait for the credits to be applied
            Thread.Sleep(40000);

            //click on the dashboard tab
            DP.DashboardTab.Click();
            Thread.Sleep(5000);

            double newCreditValue = -999;

            //loop over until the credits update
            do
            {
                Thread.Sleep(5000);
                browser.Navigate().Refresh();
                newCreditValue = DP.GetTotalCredits();
            } while (newCreditValue == initialCreditValue);

            /// 5. Verify that the new credits are equal to the Initial Credits + 1
            Assert.AreEqual(initialCreditValue + 1, newCreditValue);


          
        }

        [Test]
        [Description("This test checks to see that all of the elements are selectable for the updated boxes and that the " +
        "correct text appears")]
        [Property("Status", "Complete")]
        [Author("Daniel Nestor")]
        [Category("Activity")]
        public void CreateAnAMAPRASelfLearningActivityTest()
        {
            //creating a random user with api calls
            UserInfo NewUser1 = UserUtils.CreateUser("AMA-SL");

            /// 1. Navigate to the login page
            LoginPage LP = Navigation.GoToLoginPageMainpro(browser);



            //create the dashboard page
            //Login to the Automation Test User, However another user should be selected in the case that 
            
            DashboardPage DP = LP.LoginAsUser(NewUser1.Username, "test"); ;

            //deal with the eula
            DP.EULAButton.Click();


            double initialCreditValue = DP.GetTotalCredits();

            EnterACPDActivityPage EP = DP.ClickToAdvance(DP.EnterCPDActBtn);

            /// 3. create an activity that is a Certified Assessment, Other Activity                  MIKE: Added an end line above here
            EP.FillEnterACPDActivityForm("Self-Learning", "Certified", "American Medical Association (AMA) PRA Category 1");

            //if the popup appears, click the okay button and then
            //click on the popup button appears
            if (EP.AMAPopupSubmitBtn.Displayed) {
                EP.AMAPopupSubmitBtn.Click();
            }
           

            EP.LiveInPersonRdoBtn.Click();
            EP.ClickToAdvance(EP.LiveInPersonRdoBtn);


            /// 4. Click continue after all of the options have been selected               
            EP.ContinueBtn.Click();
            Thread.Sleep(2000);            // MIKE: Add wait criteria for this click, then use ClickToAdvance and place the wait criteria in there, instead of sleeping. Can wait for an element to appear on the next instance of this page

            /// 5. Fill out the details 
            EP.FillOutAMAActivityForm1(1);   // MIKE: See comments inside method

            Browser.ExecuteScript("arguments[0].click();", EP.PopupSubmitBtn);
            Thread.Sleep(8000);      // MIKE: Add wait criteria. Can wait for an element to be NOT visible

            DP.DashboardTab.Click();
            Thread.Sleep(1000);  // MIKE: Add wait criteria


            double newCreditValue = DP.GetTotalCredits();

            //loop over until the credits update
            do {
                Thread.Sleep(5000);
                browser.Navigate().Refresh();
                newCreditValue = DP.GetTotalCredits();
            } while (newCreditValue == initialCreditValue);

            Assert.AreEqual(initialCreditValue + 1, newCreditValue);

        }

        [Test]
        [Description("This test creates an APA PRA Group Learning Activity")]
        [Property("Status", "Complete")]
        [Author("Daniel Nestor")]
        [Category("ActivityBlah")]
        public void CreateAnAMAPRAGroupLearningActivityTest()
        {
            //creating a random new user with api calls
            UserInfo NewUser1 = UserUtils.CreateUser("AMA-SL");


            /// 1. Navigate to the login page
            LoginPage LP = Navigation.GoToLoginPageMainpro(browser);

            //create the dashboard page
            //Login to the Automation Test User, However another user should be selected in the case that 
            DashboardPage DP = LP.LoginAsUser(NewUser1.Username, "test"); ;

            //deal with the eula page
            DP.EULAButton.Click();

            double initialCreditValue = DP.GetTotalCredits(); //get the inital credit value so it can be compared in the end against a new credit value

            EnterACPDActivityPage EP = DP.ClickToAdvance(DP.EnterCPDActBtn);

            /// 3. create an activity that is a Certified Assessment, Other Activity                  MIKE: Added an end line above here
            EP.FillEnterACPDActivityForm("Group Learning", "Certified", "American Medical Association (AMA) PRA Category 1");

            //if the popup appears, click the okay button and then
            //click on the popup button appears
            if (EP.AMAPopupSubmitBtn.Displayed)
            {
                EP.AMAPopupSubmitBtn.Click();
            }


            EP.LiveInPersonRdoBtn.Click();
            EP.ClickToAdvance(EP.LiveInPersonRdoBtn);


            /// 4. Click continue after all of the options have been selected               
            EP.ContinueBtn.Click();
            Thread.Sleep(2000);            // MIKE: Add wait criteria for this click, then use ClickToAdvance and place the wait criteria in there, instead of sleeping. Can wait for an element to appear on the next instance of this page

            /// 5. Fill out the details 
            EP.FillOutAndSubmitAMAGLForm();   


            DP.DashboardTab.Click();
            Browser.WaitForElement(Bys.DashboardPage.TotalCreditsValueLbl, ElementCriteria.IsVisible);


            double newCreditValue = DP.GetTotalCredits();
            //loop over until the credits update
            do
            {
                Thread.Sleep(5000);
                browser.Navigate().Refresh();
                newCreditValue = DP.GetTotalCredits();
            } while (newCreditValue == initialCreditValue);

            Assert.AreEqual(initialCreditValue + 1, newCreditValue); // is what is expected if 1 credit is added



        }

        [Test]
        [Description("This test check to see that only 50 credits are applied to the user when a user attempst to add more than 50 credits fo an AmA activity")]
        [Property("Status", "Complete")]
        [Author("Daniel Nestor")]
        [Category("Activity")]
        public void AMAPRASelfMaxCreditTest() {

            //creating a random user with api calls
            UserInfo NewUser1 = UserUtils.CreateUser("AMA-SL");

            /// 1. Navigate to the login page
            LoginPage LP = Navigation.GoToLoginPageMainpro(browser);



            //create the dashboard page
            //Login to the Automation Test User, However another user should be selected in the case that 
            //
            DashboardPage DP = LP.LoginAsUser(NewUser1.Username, "test"); ;

            //deal with the eula
            DP.EULAButton.Click();

            EnterACPDActivityPage EP = DP.ClickToAdvance(DP.EnterCPDActBtn);

            /// 3. create an activity that is a Certified Assessment, Other Activity                  MIKE: Added an end line above here
            EP.FillEnterACPDActivityForm("Self-Learning", "Certified", "American Medical Association (AMA) PRA Category 1");

            //if the popup appears, click the okay button and then
            //click on the popup button appears
            if (EP.AMAPopupSubmitBtn.Displayed)
            {
                EP.AMAPopupSubmitBtn.Click();
            }


            EP.LiveInPersonRdoBtn.Click();
            EP.ClickToAdvance(EP.LiveInPersonRdoBtn);


            /// 4. Click continue after all of the options have been selected               
            EP.ContinueBtn.Click();
            Thread.Sleep(2000);            // MIKE: Add wait criteria for this click, then use ClickToAdvance and place the wait criteria in there, instead of sleeping. Can wait for an element to appear on the next instance of this page

            /// 5. Fill out the details 
            EP.FillOutAMAActivityForm1(90);   // MIKE: See comments inside method

            //next go on and check to see that only 50 credits are applied to the Certification
            Browser.ExecuteScript("arguments[0].click();", EP.PopupSubmitBtn);
            Thread.Sleep(8000);      // MIKE: Add wait criteria. Can wait for an element to be NOT visible

            DP.DashboardTab.Click();
            Thread.Sleep(1000);  // MIKE: Add wait criteria





            double newCreditValue = DP.GetTotalCredits();

            //loop over until the credits update
            do
            {
                Thread.Sleep(5000);
                browser.Navigate().Refresh();
                newCreditValue = DP.GetTotalCredits();
            } while (newCreditValue == 0);

            //once the new credits appear, click on the link to open up the popup
            int x = 0;

            //now check to see if the applied credits
            DP.TotalCreditsLinkLnk.Click();
            Thread.Sleep(5000);

            String creditValue = DP.TotalCreditsValueLbl.Text;

            //just putting the wait criteria here incase a breakpoint is needed
            Thread.Sleep(5000);
            Assert.AreEqual(creditValue, "50");

        }




        [Test]
        [Description("This Test checks to see that a CFP Activity is able to be created ")]
        [Property("Status", "Complete")]
        [Author("Daniel Nestor")]
        [Category("Activity")]
        public void EnterAnArticleTest()
        {

           
       
            //creating a random user with api calls
            UserInfo NewUser1 = UserUtils.CreateUser("Article");
            /// 1. Navigate to the login page and Log In
            LoginPage LP = Navigation.GoToLoginPageMainpro(browser);

            // Wrapper to login
           DashboardPage DP = LP.LoginAsUser(NewUser1.Username,"test");

            
            /// 2. Click on the Enter a CPD Activity Button
            DP.EULAButton.Click();

           

            /// 3. Fill out the Enter a CPD Activity 1st Page
            EnterACPDActivityPage EP = DP.ClickToAdvance(DP.EnterCPDActBtn);
            EP.FillEnterACPDActivityForm("Self-Learning", "Certified", "CFP Mainpro+ Articles");

            EP.ArticleDrpDn.Click();
            Thread.Sleep(1000);

            EP.AntibioticArticle.Click();
            Thread.Sleep(1000);

         
           
            EP.ContinueBtn.Click();
            Thread.Sleep(4000);
            /// 3. Fill out the Article Details for the article

            //scroll to the radio button
            ElemSet.ScrollToElement(browser, EP.ArticleDescriptionRdo);
            //EP.ArticleDescriptionRdo.Click();
           
           
            
            //Generate start and end dates for the article Page
            DateTime dt = DateTime.Now.AddDays(-1);
            String startDate =  dt.Month + "/" +  dt.Day + "/" +  dt.Year;
            String completionDate = startDate;

            ElemSet.ScrollToElement(browser, EP.ActivityStartDateArticleTxt);
            EP.ActivityStartDateArticleTxt.SendKeys(startDate);
            EP.ActivityStartDateArticleTxt.SendKeys(Keys.Tab);
            ElemSet.ScrollToElement(browser, EP.ActivityCompletionDateArticleTxt);
            EP.ActivityCompletionDateArticleTxt.SendKeys(completionDate);
            EP.ActivityCompletionDateArticleTxt.SendKeys(Keys.Tab);
            
            ElemSet.ScrollToElement(browser, EP.SubmitButton);
            EP.SubmitButton.SendKeys(Keys.Tab);
            //perform the Selenium Click
            Browser.ExecuteScript("arguments[0].click();", EP.SubmitButton);

            //wait until the popup submit button appears
            Browser.WaitForElement(Bys.EnterACPDActivityPage.PopupSubmitBtn,ElementCriteria.IsVisible);

   

            Browser.ExecuteScript("arguments[0].click();", EP.PopupSubmitBtn);
            /// 4. return to the dashboard

           
           

            String TotalCreditsString = DP.CheckForCreditUpdate();

            /// 5. check to see that the Total credits are now equal to the 0.5 added by the article
            Assert.AreEqual(TotalCreditsString, "0.5");

        }




            #endregion Tests
        }



}












