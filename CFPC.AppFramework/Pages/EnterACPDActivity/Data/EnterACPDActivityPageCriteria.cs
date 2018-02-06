using Browser.Core.Framework;


namespace CFPC.AppFramework
{
    public class EnterACPDActivityPageCriteria
    {
        public readonly ICriteria<EnterACPDActivityPage> CategorySelElemEnabled = new Criteria<EnterACPDActivityPage>(p =>
        {
            return p.Exists(Bys.EnterACPDActivityPage.CategoryDrpDn, ElementCriteria.IsEnabled , ElementCriteria.IsVisible);

        }, "Category Select Element enabled");

        public readonly ICriteria<EnterACPDActivityPage> ProgramActivityTitleTextEnabled = new Criteria<EnterACPDActivityPage>(p =>
        {
            return p.Exists(Bys.EnterACPDActivityPage.ProgramActivityTitleTxt, ElementCriteria.IsEnabled);

        }, "Program Activity Title Text Box enabled");

        public readonly ICriteria<EnterACPDActivityPage> ContinueBtnExists = new Criteria<EnterACPDActivityPage>(p =>
        {
            return p.Exists(Bys.EnterACPDActivityPage.ContinueBtn);

        }, "Continue button exists in HTML");

        public readonly ICriteria<EnterACPDActivityPage> PageReady;

        public EnterACPDActivityPageCriteria()
        {
            PageReady = CategorySelElemEnabled;
        }
    }
}
