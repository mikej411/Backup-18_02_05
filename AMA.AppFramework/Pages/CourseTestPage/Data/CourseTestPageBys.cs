using OpenQA.Selenium;

namespace AMA.AppFramework
{
    public class CourseTestPageBys
    {
        // Main page

        //Button
        public readonly By ContinueBtn = By.XPath("//a[@class='btn btn-default Button ng-scope']/span[.='Continue']");        
        public readonly By ExitActivitybtn = By.XPath("//a[@class='btn btn-default Button ng-scope']");
        public readonly By ContinuePostAssessmentBtn = By.XPath("//a[@class='btn btn-default Button ng-scope'][2]");
        public readonly By PreviousPostAssessmentBtn = By.XPath("//a[@class='btn btn-default Button ng-scope'][1]");
        public readonly By TestLaunchBtn = By.XPath("//span[.='Launch']/..");
        public readonly By TestSubmitBtn = By.XPath("//input[@value='Submit']");
        public readonly By TestSaveFinishLaterBtn = By.XPath("//input[@value='Save and Finish Later']");
        public readonly By TestCancelBtn = By.XPath("//input[@value='Cancel']");
        //public readonly By PostAssesmentContinueBtn = By.XPath("//a[@analytics-event='Activity Material']");
        public readonly By CertificateCloseBtn = By.XPath("//a[@class='btn btn-default Button ng-scope']/span[.='Close']");

        //Link
        public readonly By ActivityOverviewLnk = By.XPath("//a[.='Activity Overview']");//a[@analytics-event='Activity Overview']
        public readonly By PreAssessmentLnk = By.XPath("//a[.='Pre-Assessment']");

        //Radio Button
        public readonly By FrameFirstQuestionanswerCorrectRdo = By.XPath("//label[.='A. True']");
        public readonly By FrameFirstQuestionanswerFailRdo = By.XPath("//label[.='B. False']");
        public readonly By FrameSecondQuestionanswerCorrectRdo = By.XPath("//label[.='A. Harmful, abusive']");
        public readonly By FrameSecondQuestionanswerFailRdo = By.XPath("//label[.='A. Harmful, abusive']");
        public readonly By FrameThirdQuestionanswerFailRdo = By.XPath("//label[.='C. Intentional neglect']");
        public readonly By FrameThirdQuestionanswerCorrectRdo = By.XPath("//label[.='D. All of the above']");
        public readonly By FrameCloseBtn = By.XPath("//button[@id='btnOk']");
        //table[@id='assessmentList']//tr[1]//span[@class='ng-binding status-complete'][1]//to find texton postassesment page
        
        //tables
        public readonly By AssessmentTbl = By.XPath("//table[@id='assessmentList']");
        public readonly By PostTestFrame = By.XPath("//iframe[@class='frame']");

        //Elements To Wait
        public readonly By CourseWaitContainer = By.XPath("//div[@class='col-xs-12 col-md-9']");
        public readonly By CourseCreditInfoConatiner = By.XPath("//div[@class='col-xs-12 col-md-3 creditInfoContainer']");


        //Label
        public readonly By CourseTitleLbl = By.XPath("//label[@class='activityTitle ng-binding']");
        
    }
}