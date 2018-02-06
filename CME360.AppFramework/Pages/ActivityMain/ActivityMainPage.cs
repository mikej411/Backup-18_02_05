using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;
using CME.AppFramework.Constants;


namespace CME.AppFramework
{
    public class ActivityMainPage : Page, IDisposable
    {
        #region constructors
        public ActivityMainPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return ""; } }

        #endregion properties

        #region elements
        public IWebElement DetailsTabPublishbtn { get { return this.FindElement(Bys.ActivityMainPage.DetailsTabPublishbtn); } }
        public IWebElement DetailsTabPublishConfirmbtn { get { return this.FindElement(Bys.ActivityMainPage.DetailsTabPublishConfirmbtn); } }
        public IWebElement EditPortalFormSaveBtn { get { return this.FindElement(Bys.ActivityMainPage.EditPortalFormSaveBtn); } }
        public IWebElement EditPortalFormCustomFeeTxt { get { return this.FindElement(Bys.ActivityMainPage.EditPortalFormCustomFeeTxt); } }
        public IWebElement PubDetailsTabPortalsTblBodyRow { get { return this.FindElement(Bys.ActivityMainPage.PubDetailsTabPortalsTblBodyRow); } }
        public IWebElement PubDetailsTabPortalsTblBody { get { return this.FindElement(Bys.ActivityMainPage.PubDetailsTabPortalsTblBody); } }
        public IWebElement PubDetailsTabPortalsTbl { get { return this.FindElement(Bys.ActivityMainPage.PubDetailsTabPortalsTbl); } }
        public IWebElement PubDetailsTabSelCatTblBody { get { return this.FindElement(Bys.ActivityMainPage.PubDetailsTabSelCatTblBody); } }
        public IWebElement PubDetailsTabSelCatTblBodyRow { get { return this.FindElement(Bys.ActivityMainPage.PubDetailsTabSelCatTblBodyRow); } }
        public IWebElement PubDetailsTabAvailCatTblAddCatLoadElem { get { return this.FindElement(Bys.ActivityMainPage.PubDetailsTabAvailCatTblAddCatLoadElem); } }
        public IWebElement PubDetailsTabSelectedCatTblRemoveCatLoadElem { get { return this.FindElement(Bys.ActivityMainPage.PubDetailsTabSelectedCatTblRemoveCatLoadElem); } }
        public IWebElement PubDetailsTabAvailCatTblNextBtn { get { return this.FindElement(Bys.ActivityMainPage.PubDetailsTabAvailCatTblNextBtn); } }
        public IWebElement PubDetailsTabAvailCatTblFirstBtn { get { return this.FindElement(Bys.ActivityMainPage.PubDetailsTabAvailCatTblFirstBtn); } }
        public IWebElement PubDetailsTabAvailCatTblBodyRow { get { return this.FindElement(Bys.ActivityMainPage.PubDetailsTabAvailCatTblBodyRow); } }
        public IWebElement PubDetailsTabAvailCatTblBody { get { return this.FindElement(Bys.ActivityMainPage.PubDetailsTabAvailCatTblBody); } }
        public IWebElement DetailsTab { get { return this.FindElement(Bys.ActivityMainPage.DetailsTab); } }
        public IWebElement PubDetailsTabAvailCatTblSearchCatLoadElem { get { return this.FindElement(Bys.ActivityMainPage.PubDetailsTabAvailCatTblSearchCatLoadElem); } }
        public IWebElement PubDetailsTabSelCatTbl { get { return this.FindElement(Bys.ActivityMainPage.PubDetailsTabSelCatTbl); } }
        public IWebElement PubDetailsTabAvailCatTbl { get { return this.FindElement(Bys.ActivityMainPage.PubDetailsTabAvailCatTbl); } }
        public IWebElement PubDetailsTabAvailCatSearchTxt { get { return this.FindElement(Bys.ActivityMainPage.PubDetailsTabAvailCatSearchTxt); } }
        public IWebElement PubDetailsTabAvailCatSearchBtn { get { return this.FindElement(Bys.ActivityMainPage.PubDetailsTabAvailCatSearchBtn); } }
        public IWebElement PubDetailsTab { get { return this.FindElement(Bys.ActivityMainPage.PubDetailsTab); } }
        public IWebElement DetailsTabUnPublishBtn { get { return this.FindElement(Bys.ActivityMainPage.DetailsTabUnPublishBtn); } }
        public IWebElement DetailsTabUnPublishConfirmBtn { get { return this.FindElement(Bys.ActivityMainPage.DetailsTabUnPublishConfirmBtn); } }
        public IWebElement DetailsTabSavebtn { get { return this.FindElement(Bys.ActivityMainPage.DetailsTabSavebtn); } }
        public SelectElement DetailsTabStageSelElem { get { return new SelectElement(this.FindElement(Bys.ActivityMainPage.DetailsTabStageSelElem)); } }
        public IWebElement DetailsTabActivityNameTxt { get { return this.FindElement(Bys.ActivityMainPage.DetailsTabActivityNameTxt); } }
        public IWebElement DetailsTabActivityNumberLbl { get { return this.FindElement(Bys.ActivityMainPage.DetailsTabActivityNumberLbl); } }

        
        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            try
            {
                this.WaitUntil(TimeSpan.FromSeconds(180), Criteria.ActivityMainPage.PageReady);
            }
            catch
            {
                RefreshPage();
            }
            // Adding a little sleep here, because in Firefox, there is a weird little delay which makes clicking on the Publishing
            // Details tab fail without this sleep
            Thread.Sleep(0500);
        }

        /// Refreshes the page and then uses the wait criteria that is found within WaitForInitialize to wait for the page to load.
        /// This is used as a catch block inside WaitForInitialize, in case the page doesnt load initially. Can also be used to 
        /// randomly refresh the page
        /// </summary>
        public void RefreshPage()
        {
            Browser.Navigate().Refresh();
            this.WaitUntil(TimeSpan.FromSeconds(180), Criteria.ActivityMainPage.PageReady);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            try { activeRequests.Clear(); }
            catch (Exception ex) { _log.ErrorFormat("Failed to dispose LoginPage", activeRequests.Count, ex); }
        }

        #endregion methods: repeated per page

        #region methods: page specific

        /// <summary>
        /// Clicks the user-specified element, and then waits for a window to close or open, or a page to load, depending on the element that was clicked
        /// </summary>
        /// <param name="buttonOrLinkElem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement buttonOrLinkElem)
        {
            // Error handler to make sure that the button that the tester passed in the parameter is actually on the page
            if (Browser.Exists(Bys.ActivityMainPage.PubDetailsTab))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == PubDetailsTab.GetAttribute("outerHTML"))
                {
                    //   buttonOrLinkElem.Click();
                    JavascriptUtils.Click(Browser, buttonOrLinkElem);

                    this.WaitUntil(Criteria.ActivityMainPage.PubDetailsTabAvailCatTblVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActivityMainPage.DetailsTab))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == DetailsTab.GetAttribute("outerHTML"))
                {
                    JavascriptUtils.Click(Browser, buttonOrLinkElem);
                    this.WaitUntil(Criteria.ActivityMainPage.PageReady);
                    return null;
                }
            }


            if (Browser.Exists(Bys.ActivityMainPage.PubDetailsTabAvailCatSearchBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == PubDetailsTabAvailCatSearchBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntil(Criteria.ActivityMainPage.PubDetailsTabAvailCatTblSearchCatLoadElemVisible);
                    this.WaitUntil(Criteria.ActivityMainPage.PubDetailsTabAvailCatTblSearchCatLoadElemNotVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActivityMainPage.DetailsTabUnPublishBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == DetailsTabUnPublishBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    Browser.WaitForElement(Bys.ActivityMainPage.DetailsTabUnPublishConfirmBtn, ElementCriteria.IsVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActivityMainPage.DetailsTabUnPublishConfirmBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == DetailsTabUnPublishConfirmBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    Browser.WaitForElement(Bys.ActivityMainPage.DetailsTabSavebtn, ElementCriteria.IsVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActivityMainPage.DetailsTabPublishbtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == DetailsTabPublishbtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    Browser.WaitForElement(Bys.ActivityMainPage.DetailsTabPublishConfirmbtn, ElementCriteria.IsVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActivityMainPage.DetailsTabPublishConfirmbtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == DetailsTabPublishConfirmbtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    Browser.WaitForElement(Bys.ActivityMainPage.DetailsTabUnPublishBtn, ElementCriteria.IsVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActivityMainPage.DetailsTabSavebtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == DetailsTabSavebtn.GetAttribute("outerHTML"))
                {
                    DetailsTabSavebtn.Click();
                    // ToDo: Need to add some type of wait criteria here. A test failed in firefox once so far because a 1 second wait wasnt long enough
                    Thread.Sleep(2500);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActivityMainPage.EditPortalFormSaveBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == EditPortalFormSaveBtn.GetAttribute("outerHTML"))
                {
                    EditPortalFormSaveBtn.Click();
                    this.WaitUntil(Criteria.ActivityMainPage.EditPortalFormCustomFeeTxtNotVisible);
                    return null;
                }
            }

            else
            {
                throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, " +
                    "or if the button is already added, then the page you were on did not contain the button.");
            }

            return null;
        }

        /// <summary>
        /// Checks to see if the Unpublish button is appearing on the Details tab (which means the activity is already published yet).
        /// If so, then this method does nothing. If not, then it checks to see if the Publish button is appearing. If so, then it 
        /// clicks the Publish button, then clicks the Publish Confirm button. If not, then it sets the stage to Construction
        /// Complete, and then publishes the activity
        /// </summary>
        public void PublishActivity()
        {
            ClickAndWait(DetailsTab);

            if (!Browser.Exists(Bys.ActivityMainPage.DetailsTabUnPublishBtn, ElementCriteria.IsVisible))
            {
                if (!Browser.Exists(Bys.ActivityMainPage.DetailsTabPublishbtn, ElementCriteria.IsVisible))
                {
                    ChangeActivityStage(CMEConstants.ActivityStage.ConstructionComplete);
                }

                ClickAndWait(DetailsTabPublishbtn);
                ClickAndWait(DetailsTabPublishConfirmbtn);
            }
        }

        /// <summary>
        /// Clicks on the Publishing Details tab, enters text in the Available Catalogs search box, clicks on Search, then
        /// waits a static amount of time. 
        /// </summary>
        /// <param name="searchText"></param>
        public void SearchForAvailableCatalog(string searchText)
        {
            ClickAndWait(PubDetailsTab);

            PubDetailsTabAvailCatSearchTxt.SendKeys(searchText);

            ClickAndWait(PubDetailsTabAvailCatSearchBtn);
        }

        /// <summary>
        /// Clicks on the Details tab, then sets the activity to a stage of your choice. Note that this will unpublish an activity
        /// if it is published
        /// </summary>
        /// <param name="stage">The text from one of the items in the Stage select element</param>
        public void ChangeActivityStage(CMEConstants.ActivityStage activityStage)
        {
            ClickAndWait(DetailsTab);

            switch (activityStage)
            {
                case CMEConstants.ActivityStage.UnderConstruction:
                    if (Browser.Exists(Bys.ActivityMainPage.DetailsTabUnPublishBtn, ElementCriteria.IsVisible))
                    {
                        ClickAndWait(DetailsTabUnPublishBtn);
                        ClickAndWait(DetailsTabUnPublishConfirmBtn);
                    }

                    else if (DetailsTabStageSelElem.SelectedOption.Text == "Construction Complete")
                    {
                        DetailsTabStageSelElem.SelectByText("Under Construction");
                        ClickAndWait(DetailsTabSavebtn);
                    }
                    break;

                case CMEConstants.ActivityStage.UnderReview:

                    break;

                case CMEConstants.ActivityStage.ConstructionComplete:
                    DetailsTabStageSelElem.SelectByText("Construction Complete");
                    ClickAndWait(DetailsTabSavebtn);
                    break;
            }
        }

        /// <summary>
        /// Goes to the Publishing Details tab. If the catalog is not added to the activity, then this searches for the catalog in the available table,
        /// clicks on the plus icon of this catalog, and then waits for catalog to be added to the Selected Catalogs table
        /// </summary>
        /// <param name="catalogName">The name of the activity</param>
        /// <returns></returns>
        public void AddCatalogToActivity(string catalogName)
        {
            ClickAndWait(PubDetailsTab);

            // If the catalog is not in the selected table, then add it
            if (!ElemGet.Grid_ContainsRecord(Browser, PubDetailsTabSelCatTbl, Bys.ActivityMainPage.PubDetailsTabSelCatTblBodyRow, 1, catalogName, "td"))
            {
                // If the catalog is not showing on the 1st page of the available table, then search for it
                if (ElemGet.Grid_ContainsRecord(Browser, PubDetailsTabAvailCatTblBodyRow, Bys.ActivityMainPage.PubDetailsTabAvailCatTblBodyRow, 1,
                    catalogName, "td"))
                {
                    SearchForAvailableCatalog(catalogName);
                }

                IWebElement row = ElemGet_CME360.Grid_GetRowByRowName(PubDetailsTabAvailCatTbl, Bys.ActivityMainPage.PubDetailsTabAvailCatTblBodyRow,
                catalogName, "td");

                ElemSet_CME360.Grid_ClickElementWithoutTextInsideRow(row, "input", "Add");

                this.WaitUntil(Criteria.ActivityMainPage.PubDetailsTabAvailCatTblAddCatLoadElemVisible);
                this.WaitUntil(Criteria.ActivityMainPage.PubDetailsTabAvailCatTblAddCatLoadElemNotVisible);
            }
        }

        /// <summary>
        /// Goes to the Publishing Details tab, clicks on the X icon of a user-specified catalog in the Selected Catalogs table and then waits for 
        /// catalog to be added to the Available Catalogs table
        /// </summary>
        /// <param name="catalogName">The name of the activity</param>
        /// <returns></returns>
        public void RemoveCatalogFromActivity(string catalogName)
        {
            ClickAndWait(PubDetailsTab);

            IWebElement row = ElemGet_CME360.Grid_GetRowByRowName(PubDetailsTabSelCatTbl, Bys.ActivityMainPage.PubDetailsTabSelCatTblBodyRow,
                catalogName, "td");

            ElemSet_CME360.Grid_ClickElementWithoutTextInsideRow(row, "input", "Remove");

            this.WaitUntil(Criteria.ActivityMainPage.PubDetailsTabSelectedCatTblRemoveCatLoadElemVisible);
            this.WaitUntil(Criteria.ActivityMainPage.PubDetailsTabSelectedCatTblRemoveCatLoadElemNotVisible);
        }

        /// <summary>
        /// Goes to the Publishing Details tab, clicks on the pencil icon of a user-specified portal in the Portal table, enters user-specified text 
        /// into the Custom Fee field, then clicks
        /// on Save
        /// </summary>
        /// <param name="portalName">the portal name</param>
        /// <param name="customFee">The custom fee you want to add</param>
        public void ChangeCustomFee(string portalName, string customFee)
        {
            ClickAndWait(PubDetailsTab);

            IWebElement row = ElemGet_CME360.Grid_GetRowByRowName(PubDetailsTabPortalsTbl, Bys.ActivityMainPage.PubDetailsTabPortalsTblBodyRow,
                portalName, "td");

            ElemSet_CME360.Grid_ClickElementWithoutTextInsideRow(row, "input", "Edit");

            this.WaitUntil(Criteria.ActivityMainPage.EditPortalFormCustomFeeTxtVisible);

            EditPortalFormCustomFeeTxt.Clear();

            EditPortalFormCustomFeeTxt.SendKeys(customFee);

            ClickAndWait(EditPortalFormSaveBtn);
        }

        /// <summary>
        /// Checks to see if the Unpublish button is appearing on the Details tab (which means the activity is published). If so, this
        /// clicks the Unpublish button, then clicks the Unpublish Confirm button,
        /// </summary>
        public void UnpublishActivity()
        {
            ClickAndWait(DetailsTab);
            if(Browser.Exists(Bys.ActivityMainPage.DetailsTabUnPublishBtn, ElementCriteria.IsVisible))
            {
                ClickAndWait(DetailsTabUnPublishBtn);
                ClickAndWait(DetailsTabUnPublishConfirmBtn);
            }
        }

        #endregion methods: page specific



    }
}