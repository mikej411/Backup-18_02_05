using System.Configuration;

namespace LS.AppFramework.Constants
{
    public static class LSConstants
    {

        /// <summary>
        /// Represents what is searched for on any search page
        /// </summary>
        public enum SearchResults
        {
            Sites,
            Participants,
            Programs,
            Activities
        }

        /// <summary>
        /// The adjustment codes inside the Adjustment Code select element on the Add Adjustment form within the Program Adjustments tab on the Maintenance of Certification Program page
        /// </summary>
        public enum AdjustmentCodes
        {
            EXT1,
            EXT2,
            EXT2F,
            PRA,
            PER,
            INTNL,
            LEAVE,
            TEMP,
            VOLUNTARY,
            NOCYCLE,
            CUSTOM,
            REINSTATEDNonCompliance,
            REINSTATEDOther,
            PERProgram,
            PRAProgram,
            VoluntaryProgram,
            InternationalProgram,
            MainProgram,
            ResidentProgram


        }

    }
}