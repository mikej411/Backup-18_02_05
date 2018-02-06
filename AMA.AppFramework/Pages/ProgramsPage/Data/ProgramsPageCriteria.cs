using Browser.Core.Framework;

namespace AMA.AppFramework
{
    public class ProgramsPageCriteria
    {
        public readonly ICriteria<ProgramsPage> ProgramMngtTbl = new Criteria<ProgramsPage>(p =>
        {
            return p.Exists(Bys.ProgramsPage.ProgramMngTbl, ElementCriteria.IsVisible);

        }, "Program Managment Table Is visible");

        public readonly ICriteria<ProgramsPage> LoadIconAppear = new Criteria<ProgramsPage>(p =>
        {
            return p.Exists(Bys.AMAPage.LoadIcon, ElementCriteria.AttributeValue("class", "page-splash dissolve-animation ng-hide")
                .OR(ElementCriteria.AttributeValue("class", "page-splash dissolve-animation ng-animate ng-hide")));
        }, "Load icon disappeared");

        public readonly ICriteria<ProgramsPage> PageReady;

        public ProgramsPageCriteria()
        {
            PageReady = ProgramMngtTbl.AND(LoadIconAppear);
        }
    }
}
