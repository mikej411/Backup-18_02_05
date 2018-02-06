using Browser.Core.Framework;

namespace AMA.AppFramework
{
    public class EditInstitutionCriteria
    {
        public readonly ICriteria<EditInstitutionPage> InstitutionSaveBtnEnabledANDVisible = new Criteria<EditInstitutionPage>(p =>
        {
            return p.Exists(Bys.EditInstitutionPage.InstitutionSaveBtn, ElementCriteria.IsEnabled,ElementCriteria.IsVisible);

        }, "Username text box  visible");

        public readonly ICriteria<EditInstitutionPage> LoadIconNotVisible = new Criteria<EditInstitutionPage>(p =>
        {
            return p.Exists(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);

        }, "Load Icon Not visible");

        public readonly ICriteria<EditInstitutionPage> PageReady;

        public EditInstitutionCriteria()
        {
            PageReady = InstitutionSaveBtnEnabledANDVisible.AND(LoadIconNotVisible);
        }
    }
}
