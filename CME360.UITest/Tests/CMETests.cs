using Browser.Core.Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using CME.AppFramework;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using OpenQA.Selenium.Firefox;
using Browser.Core.Framework.Resources;
using OpenQA.Selenium.Remote;
using System.Reflection;
using System.IO;
using CME.AppFramework.Constants;
using TP.AppFramework.HelperMethods;

namespace CME.UITest
{
    //[BrowserMode(BrowserMode.New)]
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [LocalSeleniumTestFixture(BrowserNames.Firefox)]
    [LocalSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Firefox)]
    [RemoteSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [TestFixture]
    public class CMETests : TestBase
    {
        #region Constructors
        public CMETests(string browserName) : base(browserName) { }

        // Remote Selenium Grid Test
        public CMETests(string browserName, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region properties

        TPHelperMethods TestPortalHelp = new TPHelperMethods();

        #endregion properties

        #region Tests

        [Test]
        [Description("Tests the Available Catalogs table search function, tests that a user can add and remove catalogs to an activity, and tests that" +
            " the portal associated to a catalog appears/disappears in the Portal table depending on if you add or remove that catalog")]
        [Property("Prerequisites", "See manual steps and IDs 1, 2 and 3 here: https://code.premierinc.com/docs/x/BYnbAw")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void SearchAddRemoveCatalogAndPortalDependency()
        {
            string activityName = "";
            if (BrowserName == BrowserNames.Chrome)
            { activityName = "TestAuto Activity 1 Chrome"; }
            if (BrowserName == BrowserNames.InternetExplorer)
            { activityName = "TestAuto Activity 1 IE"; }
            if (BrowserName == BrowserNames.Firefox)
            { activityName = "TestAuto Activity 1 FF"; }

            string catalogName = "TestAuto Catalog 1";

            string portalName = "_Test Portal";

            /// 1. Login as TestAuto_TestPortal_User1
            LoginPage LP = Navigation.GoToLoginPage(browser);
            MyDashboardPage MDP = LP.Login("TestAuto_TestPortal_User1", "password");

            /// 2. Open the "TestAuto Activity 1" activity
            SearchResultsPage SP = MDP.Search(activityName);
            ActivityMainPage AMP = SP.GoToActivity(activityName);

            /// 3. Go to the Publishing Details and do a search for "TestAuto Catalog 1" and verify that it shows in the available table
            // First, we should check to make sure that this catalog is not in the Selected table (if it is, then the test failed at some point, and didnt reach 
            // the part of the test where it moves the catalog back to the available list). So if its not in the available list, then we need to put it back
            AMP.ClickAndWait(AMP.PubDetailsTab);
            if (ElemGet.Grid_ContainsRecord(browser, AMP.PubDetailsTabSelCatTbl, Bys.ActivityMainPage.PubDetailsTabSelCatTblBodyRow, 1,
                catalogName, "td"))
            {
                AMP.RemoveCatalogFromActivity(catalogName);
            }
            AMP.SearchForAvailableCatalog(catalogName);
            Assert.True(ElemGet.Grid_ContainsRecord(browser, AMP.PubDetailsTabAvailCatTbl, Bys.ActivityMainPage.PubDetailsTabAvailCatTblBodyRow, 1,
                "TestAuto Catalog 1", "td", Bys.ActivityMainPage.PubDetailsTabAvailCatTblFirstBtn, Bys.ActivityMainPage.PubDetailsTabAvailCatTblNextBtn));

            /// 4. Click the + icon in the catalog row of the available table and verify that it gets added to the selected table and removed from
            /// the available table
            AMP.AddCatalogToActivity(catalogName);
            Assert.False(ElemGet.Grid_ContainsRecord(browser, AMP.PubDetailsTabAvailCatTbl, Bys.ActivityMainPage.PubDetailsTabAvailCatTblBodyRow, 1,
                 catalogName, "td", Bys.ActivityMainPage.PubDetailsTabAvailCatTblFirstBtn, Bys.ActivityMainPage.PubDetailsTabAvailCatTblNextBtn));
            Assert.True(ElemGet.Grid_ContainsRecord(browser, AMP.PubDetailsTabSelCatTbl, Bys.ActivityMainPage.PubDetailsTabSelCatTblBodyRow, 1,
                catalogName, "td"));

            /// 5. Verify that the Portals table populates with the cooresponding portal that the Catalog is associated to
            Assert.True(ElemGet.Grid_ContainsRecord(browser, AMP.PubDetailsTabPortalsTbl, Bys.ActivityMainPage.PubDetailsTabPortalsTblBodyRow, 0,
                portalName, "td"));

            /// 6. Click the X icon in the catalog row of the selected table and verify that it gets removed from the selected table and added to
            /// the available table. Also verify that the portal gets removed from the portals table
            AMP.RemoveCatalogFromActivity(catalogName);
            Assert.True(ElemGet.Grid_ContainsRecord(browser, AMP.PubDetailsTabAvailCatTbl, Bys.ActivityMainPage.PubDetailsTabAvailCatTblBodyRow, 1,
                catalogName, "td", Bys.ActivityMainPage.PubDetailsTabAvailCatTblFirstBtn, Bys.ActivityMainPage.PubDetailsTabAvailCatTblNextBtn));
            Assert.False(ElemGet.Grid_ContainsRecord(browser, AMP.PubDetailsTabSelCatTbl, Bys.ActivityMainPage.PubDetailsTabSelCatTblBodyRow, 1,
                catalogName, "td"));
            Assert.False(ElemGet.Grid_ContainsRecord(browser, AMP.PubDetailsTabPortalsTbl, Bys.ActivityMainPage.PubDetailsTabPortalsTblBodyRow, 0,
                portalName, "td"));
        }

        [Test]
        [Description("")]
        [Property("Prerequisites", "See manual steps and IDs 4, 5 and 6 here: https://code.premierinc.com/docs/x/BYnbAw")]
        [Property("Status", "In Progress")]
        [Author("Mike Johnston")]
        public void CustomFee()
        {
            string activityName = "";
            if (BrowserName == BrowserNames.Chrome)
            { activityName = "TestAuto Activity 2 Chrome"; }
            if (BrowserName == BrowserNames.InternetExplorer)
            { activityName = "TestAuto Activity 2 IE"; }
            if (BrowserName == BrowserNames.Firefox)
            { activityName = "TestAuto Activity 2 FF"; }

            string portalName = "_Test Portal";

            /// 1. Login as TestAuto_TestPortal_User1
            LoginPage LP = Navigation.GoToLoginPage(browser);
            MyDashboardPage MDP = LP.Login("TestAuto_TestPortal_User1", "password");

            /// 2. Open the "TestAuto Activity 1" activity
            SearchResultsPage SP = MDP.Search(activityName);
            ActivityMainPage AMP = SP.GoToActivity(activityName);

            /// 3. Go to the Publishing Details tab, then modify the Custom fee for the portal that the activity is associated to         
            string newFee = string.Format("{0}.00", DataUtils.GetRandomInteger(200).ToString());
            AMP.ChangeCustomFee(portalName, newFee);

            /// 4. Get the AID of the activity, navigate to the test portal's Final environment, login, and assert that new fee is reflected on the
            /// Activity Details page
            AMP.ClickAndWait(AMP.DetailsTab);
            string AID = AMP.DetailsTabActivityNumberLbl.Text;
            TestPortalHelp.Login(browser, "testauto_user1", "test");
            Assert.AreEqual("$" + newFee, TestPortalHelp.ActivityFee(browser, AID));
        }

        [Test]
        [Description("")]
        [Property("Prerequisites", "See manual steps and IDs 7, 8 and 9 here: https://code.premierinc.com/docs/x/BYnbAw")]
        [Property("Status", "In Progress")]
        [Author("Mike Johnston")]
        public void Publish()
        {
            string activityName = "";
            if (BrowserName == BrowserNames.Chrome)
            { activityName = "TestAuto Activity 3 Chrome"; }
            if (BrowserName == BrowserNames.InternetExplorer)
            { activityName = "TestAuto Activity 3 IE"; }
            if (BrowserName == BrowserNames.Firefox)
            { activityName = "TestAuto Activity 3 FF"; }

            string portalName = "_Test Portal";

            /// 1. Login as TestAuto_TestPortal_User1
            LoginPage LP = Navigation.GoToLoginPage(browser);
            MyDashboardPage MDP = LP.Login("TestAuto_TestPortal_User1", "password");

            /// 2. Open the "TestAuto Activity 1" activity
            SearchResultsPage SP = MDP.Search(activityName);
            ActivityMainPage AMP = SP.GoToActivity(activityName);

            /// 3. Get the AID of the activity, navigate to the test portal's Final environment, login, and assert that the activity does not show up 
            /// since we did not publish it yet
            // First, we should check to make sure the activity is not published (if it is, then the test failed at some point, and didnt reach 
            // the part of the test where it cleans itself up and unpublished the activity). So if its not unpublished, then we will unpublish
            AMP.UnpublishActivity();
            AMP.ClickAndWait(AMP.DetailsTab);
            string AID = AMP.DetailsTabActivityNumberLbl.Text;
            // The following is commented until the create user API is developed
            //TestPortalHelp.Login(browser, "testauto_user1", "test");
            //Assert.True(TestPortalHelp.ActivityNotAppearingWithAID(browser, AID));

            /// 4. Go back to CME360, publish the activity, go to the test portal and assert that the activity is now appearing
            //Navigation.GoToMyDashboardPage(browser);
            //MDP.GoToRecentItem(CMEConstants.RecentItemCategory.Activity, activityName);
            AMP.PublishActivity();
            TestPortalHelp.Login(browser, "testauto_user1", "test");
            Assert.True(TestPortalHelp.ActivityAppearingWithAID(browser, AID));

            // Data cleanup
            Navigation.GoToMyDashboardPage(browser);
            MDP.Search(activityName);
            SP.GoToActivity(activityName);
            AMP.UnpublishActivity();
        }

        //[Test]
        [Description("First test for CME360. Clicking Distribution tab and verifying Catalog link")]
        [Property("Status", "In Progress")]
        [Author("Lakshmi")]
        public void DistributionTest()
        {

            /// 1. Navigate to the login page
            LoginPage LP = Navigation.GoToLoginPage(browser);
            //Assert.True(false);
            MyDashboardPage MDP = LP.Login("TestAuto_TestPortal_User1", "password");

            DistributionPage DP = MDP.ClickAndWaitBasePage(MDP.DistributionTab);
            CatalogsPage CP = DP.ClickAndWait(DP.CatalogsLnk);
          

            Assert.True(DP.FilterByLbl.Displayed);
            Assert.True(DP.CatalogLibraryLbl.Displayed);


            AddCatalogPage ACP = CP.ClickAndWait(CP.AddNewCatalogLnk);

            
            ACP.DetailsTabCatalogNameTxt.SendKeys("TestLK");
            ACP.DetailsTabShortLabelTxt.SendKeys("Short Label Test");
            ACP.DetailsTabDescriptionTxt.SendKeys("Description Test LK");

            ACP.ClickAndWait(ACP.DetailsTabCancelBtn);


        }

        //[Test]
        [Description("First test for CME360. Clicking Distribution tab and verifying portal link and associating the catalog to portal")]
        [Property("Status", "In Progress")]
        [Author("Bala")]
        public void DistributionTest2()
        {
            /// 1. Navigate to the login page
            LoginPage LP = Navigation.GoToLoginPage(browser);
            //Assert.True(false);
            MyDashboardPage MDP = LP.Login("cap_admin", "password");

            DistributionPage DP = MDP.ClickAndWaitBasePage(MDP.DistributionTab);

            DP.ClickAndWait(DP.PortalsLnk);

            PortalPage PP = DP.GoToPortalDetails("CAP Learning", "img", "Edit");

            PP.ClickAndWait(PP.CatAndActTab);

            PP.AddCatalog("test catalog", "input", "Add Catalog");

        }

        //[Test]
        [Description("First test for CME360. Clicking Distribution tab and verifying portal link and associating the catalog to portal")]
        [Property("Status", "In Progress")]
        [Author("Bala")]
        public void DistributionTest3()
        {
            /// 1. Navigate to the login page
            LoginPage LP = Navigation.GoToLoginPage(browser);
            //Assert.True(false);
            MyDashboardPage MDP = LP.Login("cap_admin", "password");

            DistributionPage DP = MDP.ClickAndWaitBasePage(MDP.DistributionTab);

            DP.ClickAndWait(DP.PortalsLnk);

            PortalPage PP = DP.GoToPortalDetails("CAP Learning", "img", "Edit");

            PP.ClickAndWait(PP.CatAndActTab);

            PP.RemoveCatalog("test catalog", "input", "Remove Catalog");

        }
        #endregion Tests
    }
}

