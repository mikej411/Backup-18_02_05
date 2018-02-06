using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFPC.AppFramework
{
    /// <summary>
    /// Provides access to all the known "Criteria" for the the application.
    /// Criteria are typically used when waiting for elements.  I often wait until some
    /// "Criteria" is met before continuing with a test.
    /// </summary>
    public static class Criteria
    {
        public static readonly LoginPageCriteria LoginPage = new LoginPageCriteria();

        //public static readonly ActivitiesListPageCriteria ActivitiesListPage = new ActivitiesListPageCriteria();

        public static readonly DashboardPageCriteria DashboardPage = new DashboardPageCriteria();

        public static readonly EnterACPDActivityPageCriteria EnterACPDActivityPage = new EnterACPDActivityPageCriteria();

        public static readonly CreditSummaryPageCriteria CreditSummaryPage = new CreditSummaryPageCriteria();

        public static readonly HoldingAreaPageCriteria HoldingAreaPage = new HoldingAreaPageCriteria();

        //public static readonly CBDLearnerPageCriteria CBDLearnerPage = new CBDLearnerPageCriteria();

        //public static readonly CBDObserverPageCriteria CBDObserverPage = new CBDObserverPageCriteria();


    }
}
