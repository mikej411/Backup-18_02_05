using System.Configuration;

namespace CME.AppFramework.Constants
{
    public static class CMEConstants
    {
        public enum ActivityStage
        {
            UnderConstruction,
            UnderReview,
            ConstructionComplete
        }

        public enum RecentItemCategory
        {
            Activity,
            Project
        }

    }
}