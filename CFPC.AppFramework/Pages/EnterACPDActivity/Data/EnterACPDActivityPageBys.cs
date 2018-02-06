using OpenQA.Selenium;


namespace CFPC.AppFramework
{
    // MIKE: Awesome! Youre following the naming convention of elements. Not following it fully for forms, but the element naming (sans forms/popups) looks good

    // MIKE: You are grouping these elements by page? Using the page object model, all pages should have separate classes. When we meet, you can explain whats going on here

    // MIKE: If you are following proper element naming convention, you dont need to group elements by area/page as you are doing below, and should instead group by element type. 
    // Or if the page has so many areas/pages, then you can do this type of grouping, but you should also group by element type inside this grouping as well. One example/reason why this is the case, 
    // is because if you have someone looking at the code who is not familar with the CFPC page in question, they wont know what any of your groups are below. On the other hand, if you group by
    // element type instead, everyone know what each element type is (radio buttons, tex boxes), so they will have an easier time navigating the code to find elements. See the related JIRA doc.

    public class EnterACPDActivityPageBys
    {
        //buttons for page
        public readonly By CategoryDrpDn = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl01_rcCategory_Arrow");
       //Elements within the CategorySelElem
       //public readonly By AssessmentSelElem = By.XPath("//*[@id=\"ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl01_rcCategory_DropDown\"]/div/ul/li[1]/b");
        //public readonly By GroupLearningSelElem = By.XPath("/html/body/form/div[1]/div/div/ul/li[2]");
        //public readonly By SelfLearningSelElem = By.XPath("/html/body/form/div[1]/div/div/ul/li[3]");

        //other elements
        public readonly By ActivityTypeDrpDn = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl01_rcActivityType_title");
        public readonly By LiveInPersonBtn = By.XPath("ctl00$ContentPlaceHolder1$CFPCActivitiesWizard$ctl09$rdDeliveryFormat");
        public readonly By LiveInPersonRdoBtn = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl01_rdDeliveryFormat_0");
        public readonly By ContinueBtn = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl01_btnContinue");
        public readonly By ArticleDrpDn = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl01_rcArticle");
        public readonly By AntibioticArticle = By.XPath("//*[@id=\"ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl01_rcArticle\"]/option[2]");

        //elements on the Enter a Cpd Details page
        public readonly By ProgramTitleCertifiedAssessmentTxt = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl15_t3163238");
        public readonly By ProvinceSelectorDrpDn = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl17_CEComboBox3449685");
        public readonly By CityTxt = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl18_t3163191");
        public readonly By PlanningOrganizationTxt = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl19_t3369566");
        public readonly By ActivityStartDateTxt = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl20_dtbDatePicker3369568_dateInput");
        public readonly By ActivityCompletionDateTxt = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl21_dtbDatePicker3163194_dateInput");
        public readonly By CreditsClaimedTxt = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl22_t3583155");
        public readonly By ChangedImprovedRdo = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl24_3163197|1");
        public readonly By LearnedNewRdo = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl26_3369571|1");
        public readonly By LearnMoreRdo = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl27_3369572|1");
        public readonly By ConfirmedRdo = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl28_3163201|1");
        public readonly By BiasedRdo = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl30_3163203|1");
        public readonly By DissatisfiedRdo = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl32_3163205|1");

        //For other certified group learning activities
        public readonly By ProgramTitleCertifiedGroupLearningTxt= By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl15_t3409351");
        public readonly By ProvinceSelectorCertifiedGroupLearningDrpDn = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl17_CEComboBox3449688");
        public readonly By CityTxtCertifiedGroupLearningTxt = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl18_t3163006");
        public readonly By PlanningOrganizationCertifiedGroupLearningTxt = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl19_t3372357");
        public readonly By ActivityStartDateCertifiedGroupLearningTxt = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl20_dtbDatePicker3372363_dateInput");
        public readonly By ActivityCompletionDateCertifiedGroupLearningTxt = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl21_dtbDatePicker3163009_dateInput");
        public readonly By CreditsClaimedDateCertifiedGroupLearningTxt = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl22_t3582711");
        public readonly By ChangedImprovedCertifiedGroupLearningRdo = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl24_3163013|1");
        public readonly By LearnedNewCertifiedGroupLearningRdo = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl26_3372396|1");
        public readonly By LearnedMoreCertifiedGroupLearningRdo = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl27_3372410|1");
        public readonly By ConfirmedCertifiedGroupLearningRdo = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl28_3163017|1");
        public readonly By BiasedCertifiedGroupLearningRdo = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl30_3163019|1");
        public readonly By DissatisfiedCertifiedGroupLearningRdo = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl32_3163021|1");

        //For other self Learning
        public readonly By ProgramTitleCertifiedSelfLearningTxt = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl15_t3163772");                                                                 
        public readonly By ProvinceSelectorCertifiedSelfLearningDrpDn = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl17_CEComboBox3449988");
        public readonly By CityTxtCertifiedSelfLearningTxt = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl18_t3163732");
        public readonly By PlanningOrganizationCertifiedSelfLearningTxt = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl19_t3375329");
        public readonly By ActivityStartDateCertifiedSelfLearningTxt = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl20_dtbDatePicker3375338_dateInput");
        public readonly By ActivityCompletionDateCertifiedSelfLearningTxt = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl21_dtbDatePicker3163735_dateInput");
        public readonly By CreditsClaimedDateCertifiedSelfLearningTxt = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl22_t3595618");
        public readonly By ChangedImprovedCertifiedSelfLearningRdo = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl24_3163739|1");
        public readonly By LearnedNewCertifiedSelfLearningRdo = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl26_3375345|1");
        public readonly By LearnedMoreCertifiedSelfLearningRdo = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl27_3375347|1");
        public readonly By ConfirmedCertifiedSelfLearningRdo = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl28_3163743|1");
        public readonly By BiasedCertifiedSelfLearningRdo = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl30_3163745|1");
        public readonly By DissatisfiedCertifiedSelfLearningRdo = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl32_3163747|1");

        //elements on the AMA Self-Learning(SL) Page
        public readonly By ProgramTitleAMASLTxt = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl15_t3972753");
        public readonly By ProvinceSelectorAMASLDrpDn = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl17_CEComboBox3972793");
        public readonly By CityAMASLTxt = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl18_t3972752");
        public readonly By PlanningOrganizationAMASLTxt = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl19_t3972754");
        public readonly By ActivityStartDateAMASLTxt = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl20_dtbDatePicker3972755_dateInput");
        public readonly By ActivityCompletionDateAMASLTxt = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl21_dtbDatePicker3972756_dateInput");
        public readonly By CreditsClaimedAMASLTxt = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl22_t3972903");
        public readonly By LearnedNewAMASLRdo = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl26_3972759|1");
        public readonly By LearnedMoreAMASLRdo = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl27_3972760|1");
        public readonly By ConfirmedAMASLRdo = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl28_3972761|1");
        public readonly By BiasedAMASLRdo = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl30_3972762|1");
        public readonly By DissatisfiedAMASLRdo = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl32_3972763|1");
        public readonly By ChangedImprovedAMASLRdo = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl24_3972758|1");

        //elements on the AMA Self-Learning(SL) Page
        public readonly By ProgramTitleAMAGLTxt = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl15_t3423013");
        public readonly By ProvinceSelectorAMAGLDrpDn = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl17_CEComboBox3449689");
        public readonly By CityAMAGLTxt = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl18_t3423012");
        public readonly By PlanningOrganizationAMAGLTxt = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl19_t3423015");
        public readonly By ActivityStartDateAMAGLTxt = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl20_dtbDatePicker3423016_dateInput");
        public readonly By ActivityCompletionDateAMAGLTxt = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl21_dtbDatePicker3423017_dateInput");
        public readonly By CreditsClaimedAMAGLTxt = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl22_t3581765");
        public readonly By LearnedNewAMAGLRdo = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl26_3423022|1");
        public readonly By LearnedMoreAMAGLRdo = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl27_3423023|1");
        public readonly By ConfirmedAMAGLRdo = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl28_3423024|1");
        public readonly By BiasedAMAGLRdo = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl30_3423026|1");
        public readonly By DissatisfiedAMAGLRdo = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl32_3423028|1");
        public readonly By ChangedImprovedAMAGLRdo = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl24_3423020|1");

        //Elements on the Article Page
        public readonly By ArticleDescriptionRdo = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl15_3164227|1");
        public readonly By ActivityCompletionDateArticleTxt = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl20_dtbDatePicker3130345_dateInput");
        public readonly By ActivityStartDateArticleTxt = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl19_dtbDatePicker3130344_dateInput");
        public readonly By ArticleCreditsRequestedTxt = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_fb1_ctl06_ctl20_dtbDatePicker3130345_dateInput");

        public readonly By SubmitButton = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl03_btnSubmit");

        // MIKE: 2 things. 
        // 1: If this is a popup, use the proper element naming convention as explained in JIRA. Add the name of the popup at the begining of the variable, then the text "Form", then the name of the element. 

        //for the popup
        public readonly By PopupSubmitBtn = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl06_wndMessagePopup_C_btnGoToCPDActivities2");
        
        public readonly By ProgramActivityTitleTxt = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl09_txtProgramTitle");

        public readonly By AdvancedSearchBtn = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl09_btnSearch");
        public readonly By AdvancedSearchNoResultsTxt = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl09_grdSessions_ClientState");

        public readonly By TooManyResultsLbl = By.XPath("//i[text()='Too many results. Please, enter more search criteria to filter down.']");
        public readonly By NoResultsLbl = By.XPath("/html/body/form/div[5]/div/div/div[3]/div[1]/div[1]/div[1]/div[2]/div/div/div[1]/div/div/div/div[1]/div[2]/div[9]/div[2]/div/div[2]/table/tbody/tr/td/div/span/b");

        //ama popup click
        public readonly By AMAPopupSubmitBtn = By.Id("ctl00_ContentPlaceHolder1_CFPCActivitiesWizard_ctl07_wndMessagePopup_C_btnok");
    }
}
