using OpenQA.Selenium;

namespace LS.AppFramework
{
    /// <summary>
    /// Elements that will exist on the login page
    /// </summary>
    public class ProgramPageBys
    {
        // Buttons
        public readonly By CreditValidationSubmitBtn = By.XPath("//input[@value='Submit Validation']");
        public readonly By ProgAdjustTabAddAdjustFormAddAdjustBtn = By.XPath("//input[@value='Add Adjustment']");

        public readonly By ProgAdjustTabAddAdjustFormIsIntnlYesRdo = By.Id("True_IsInternational");
        public readonly By ProgAdjustTabAddAdjustFormIsIntnlNoRdo = By.Id("False_IsInternational");
        public readonly By ProgAdjustTabAddAdjustFormLeaveStartDtTxt = By.Id("Attribute_LeaveStartDate");
        public readonly By ProgAdjustTabAddAdjustFormLeaveEndDtTxt = By.Id("Attribute_LeaveEndDate");
        public readonly By ProgAdjustTabAddAdjustFormLeaveCodeSelElem = By.Id("Attribute_LeaveCode");
        public readonly By ProgAdjustTabAddAdjustFormIsVoluntYesRdo = By.Id("True_IsVoluntary");
        public readonly By ProgAdjustTabAddAdjustFormIsVoluntNoRdo = By.Id("False_IsVoluntary");
        public readonly By ProgAdjustTabAddAdjustFormEffectiveDtRdo = By.Id("Attribute_EffectiveDate");


        // Charts

        // Check boxes


        // forms
        public readonly By ProgAdjustTabAddAdjustForm = By.Id("adjustmenteditor");


        // Labels         
        public readonly By DetailsTabNameValueLbl = By.XPath("//td[contains(text(),'Name:')]/following-sibling::td[1]");
        public readonly By DetailsTabStatusValueLbl = By.XPath("//td[contains(text(),'Status:')]/following-sibling::td[1]");
        public readonly By DetailsTabStartsValueLbl = By.XPath("//td[contains(text(),'Starts:')]/following-sibling::td[1]");
        public readonly By DetailsTabEndsValueLbl = By.XPath("//td[contains(text(),'Ends:')]/following-sibling::td[1]");
        public readonly By DetailsTabProgramValueLbl = By.XPath("//td[contains(text(),'Program:')]/following-sibling::td[1]");
        public readonly By DetailsTabCreditsValueLbl = By.XPath("//td[contains(text(),'Credits Applied:')]/following-sibling::td[1]");


        // Links

        public readonly By ProgAdjustTabAddAdjustLnk = By.Id("addAdjustment");

        // Menu Items    


        // Radio buttons
        public readonly By CreditValidationAcceptRdo = By.Id("Accept");
        public readonly By CreditValidationRejectRdo = By.Id("Reject");

        // select elements       
        public readonly By SelfReportActTabValidStatusSelElem = By.Id("validationFilter");
        public readonly By ProgAdjustTabAddAdjustFormAdjustCodeSelElem = By.Id("LeaveCode");

        
        // Tables   
        public readonly By SelfReportActTabActivityTbl = By.XPath("//table[@id='externalActivitiesRepeater']");
        public readonly By SelfReportActTabActivityTblBody = By.XPath("//table[@id='externalActivitiesRepeater']/tbody");
        public readonly By SelfReportActTabActivityTblBodyRow = By.XPath("//table[@id='externalActivitiesRepeater']/tbody/tr"); // If one row exists in this table, then this will be that row. This is used to wait for the table to load 
        public readonly By ProgramAdjustmentsActivityTbl = By.XPath("//table[@id='adjustmentRepeater']");
        public readonly By ProgramAdjustmentsActivityTblBody = By.XPath("//table[@id='adjustmentRepeater']/tbody");
        public readonly By ProgramAdjustmentsActivityTblBodyRow = By.XPath("//table[@id='adjustmentRepeater']/tbody/tr"); // If one row exists in this table, then this will be that row. This is used to wait for the table to load



        // Tabs
        public readonly By SelfReportActTab = By.XPath("//a[text()='Self-Reported Activities']");
        public readonly By ProgramAdjustmentsTab = By.XPath("//a[text()='Program Adjustments']");
        public readonly By DetailsTab = By.XPath("//a[text()='Details']");


        // Text boxes



    }
}