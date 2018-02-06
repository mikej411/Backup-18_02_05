using Browser.Core.Framework;

namespace CME.AppFramework
{
    public class ProjectsPageCriteria
    {
        public readonly ICriteria<ProjectsPage> ManageActivitiesLnkVisible = new Criteria<ProjectsPage>(p =>
        {
            return p.Exists(Bys.ProjectsPage.ManageActivitiesLnk, ElementCriteria.IsVisible);

        }, "Manage Activities link visible");

        public readonly ICriteria<ProjectsPage> ActivitiesSearchTxtVisible = new Criteria<ProjectsPage>(p =>
        {
            return p.Exists(Bys.ProjectsPage.ActivitiesSearchTxt, ElementCriteria.IsVisible);

        }, "Activity search text box visible");

        public readonly ICriteria<ProjectsPage> ManageActivitiesTblBodyRowNotExists = new Criteria<ProjectsPage>(p =>
        {
            return !p.Exists(Bys.ProjectsPage.ManageActivitiesTblBodyRow);

        }, "Manage Activities Search Results table, first row not exists");

        public readonly ICriteria<ProjectsPage> ManageActivitiesTblBodyRowExistsAndVisible = new Criteria<ProjectsPage>(p =>
        {
            return p.Exists(Bys.ProjectsPage.ManageActivitiesTblBodyRow, ElementCriteria.IsVisible);

        }, "Manage Activities Search Results table, first row visible");
        


        public readonly ICriteria<ProjectsPage> PageReady;

        public ProjectsPageCriteria()
        {
            PageReady = ManageActivitiesLnkVisible;
        }
    }
}
