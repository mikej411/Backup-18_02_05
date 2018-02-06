﻿using OpenQA.Selenium;

namespace CME.AppFramework
{
    /// <summary>
    /// 
    /// </summary>
    public class ActivityMainPageBys
    {

        // Buttons
        public readonly By PubDetailsTabAvailCatSearchBtn = By.Id("ctl00_btnSearch");
        public readonly By DetailsTabUnPublishBtn = By.Id("ctl00_UnPublish");
        public readonly By DetailsTabUnPublishConfirmBtn = By.Id("ctl00_UnPublishConfirmBtn");
        public readonly By DetailsTabPublishbtn = By.Id("ctl00_Publish");
        public readonly By DetailsTabPublishConfirmbtn = By.Id("ctl00_PublishConfirmBtn");
        public readonly By DetailsTabSavebtn = By.Id("ctl00_AddNode");



        

        // Charts

        // Check boxes


        // General
        public readonly By PubDetailsTabAvailCatTblAddCatLoadElem = By.Id("ctl00_availableUpdateProgress");
        public readonly By PubDetailsTabSelectedCatTblRemoveCatLoadElem = By.Id("ctl00_selectedUpdateProgress");
        public readonly By PubDetailsTabAvailCatTblSearchCatLoadElem = By.Id("ctl00_SearchUpdateProgress");
        public readonly By EditPortalFormSaveBtn = By.Id("ctl00_btnSaveCustomFee");





        // Labels                                                   
        public readonly By DetailsTabActivityNumberLbl = By.Id("ctl00_CECityActivityNumber");


        // Links

        // Menu Items    

        // Radio buttons

        // select elements
        public readonly By DetailsTabStageSelElem = By.Id("ctl00_ActivityStatus");


        // Tables   
        public readonly By PubDetailsTabSelCatTbl = By.XPath("//div[@id='ctl00_SelectedCatalogsUpdatePanel']/table");
        public readonly By PubDetailsTabSelCatTblBody = By.XPath("//div[@id='ctl00_AvailableCatalogsUpdatePanel']/table/tbody");
        public readonly By PubDetailsTabSelCatTblBodyRow = By.XPath("//div[@id='ctl00_AvailableCatalogsUpdatePanel']/table/tbody/tr[2]");
        public readonly By PubDetailsTabAvailCatTbl = By.XPath("//div[@id='ctl00_AvailableCatalogsUpdatePanel']/table");
        public readonly By PubDetailsTabAvailCatTblBody = By.XPath("//div[@id='ctl00_AvailableCatalogsUpdatePanel']/table/tbody");
        public readonly By PubDetailsTabAvailCatTblBodyRow = By.XPath("//div[@id='ctl00_AvailableCatalogsUpdatePanel']/table/tbody/tr[2]");
        public readonly By PubDetailsTabAvailCatTblFirstBtn = By.XPath("//div[@id='ctl00_AvailableCatalogsUpdatePanel']/table/descendant::a[text()='1']");
        public readonly By PubDetailsTabAvailCatTblNextBtn = By.XPath("//div[@id='ctl00_AvailableCatalogsUpdatePanel']/table/descendant::a[@id='ctl00_AvailableCatalogsPager_dlPaging_ctl00_lbPage']");
        public readonly By PubDetailsTabPortalsTbl = By.XPath("//div[@id='ctl00_PortalsUpdatePanel']/table");
        public readonly By PubDetailsTabPortalsTblBody = By.XPath("//div[@id='ctl00_PortalsUpdatePanel']/table/tbody");
        public readonly By PubDetailsTabPortalsTblBodyRow = By.XPath("//div[@id='ctl00_PortalsUpdatePanel']/table/tbody/tr[2]");


        // Tabs
        public readonly By PubDetailsTab = By.XPath("//span[text()='Publishing Details']");
        public readonly By DetailsTab = By.XPath("//span[text()='Details']");

        // Text boxes
        public readonly By DetailsTabActivityNameTxt = By.XPath("ctl00_txtActivityName");
        public readonly By PubDetailsTabAvailCatSearchTxt = By.Id("ctl00_txtAvailableCatalogs");
        public readonly By EditPortalFormCustomFeeTxt = By.Id("ctl00_txtCustomFee");


    }
}