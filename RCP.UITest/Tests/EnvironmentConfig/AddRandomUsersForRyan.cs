using Browser.Core.Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using RCP.AppFramework;
using System.Data;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using OpenQA.Selenium.Firefox;
using Browser.Core.Framework.Resources;
using OpenQA.Selenium.Remote;
using System.Reflection;
using System.Configuration;

namespace RCP.UITest
{
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [TestFixture]
    public class AddRandomUsersForRyan : TestBase
    {
        #region Constructors
        public AddRandomUsersForRyan(string browserName) : base(browserName) { }

        // Remote Selenium Grid Test
        public AddRandomUsersForRyan(string browserName, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests

        [Test]
        public void AddRandomUsersForRyanMethod()
        {
            // Users with specific details. fr_CA for french
            // Diploma
            //UserInfo blah = UserUtils.CreateAndRegisterUser(UserUtils.Application.Diploma, UserUtils.UserRole.CSDiploma,
            //    "DIPCSUser1_2DOM_CECITY", null, null, "DIPClinicalSupervisor", "1", "fr_CA", "2DOM");

            //UserInfo blah2 = UserUtils.CreateAndRegisterUser(UserUtils.Application.Diploma, UserUtils.UserRole.DDDiploma,
            //    "DIPDDUser1_2DOM_CECITY", null, null, "DIPDiplomaDirector", "1", "fr_CA", "2DOM");

            //UserInfo blah3 = UserUtils.CreateAndRegisterUser(UserUtils.Application.Diploma, UserUtils.UserRole.FOMDiploma,
            //    "DIPFOMUser1_2DOM_CECITY", null, null, "DIPFacultyOfMedicine", "1", "fr_CA", "2DOM");

            //UserInfo blah4 = UserUtils.CreateAndRegisterUser(UserUtils.Application.Diploma, UserUtils.UserRole.ASRDiploma,
            //    "DIPASRUser1_2DOM_CECITY", null, null, "DIPAssessor", "1", "fr_CA", "2DOM");

            //UserInfo blah5 = UserUtils.CreateAndRegisterUser(UserUtils.Application.Diploma, UserUtils.UserRole.ASRDiploma,
            //    "DIPASRUser2_2DOM_CECITY", null, null, "DIPAssessor", "2", "fr_CA", "2DOM");

            //UserInfo blah6 = UserUtils.CreateAndRegisterUser(UserUtils.Application.Diploma, UserUtils.UserRole.ASRDiploma,
            //    "DIPASRUser3_2DOM_CECITY", null, null, "DIPAssessor", "3", "fr_CA", "2DOM");

            //UserInfo blah7 = UserUtils.CreateAndRegisterUser(UserUtils.Application.Diploma, UserUtils.UserRole.TraineeDiploma,
            //    "DIPTrainUser1_2DOM_CECITY", null, null, "DIPTrainee", "1", "fr_CA", "2DOM");

            //UserInfo blah8 = UserUtils.CreateAndRegisterUser(UserUtils.Application.Diploma, UserUtils.UserRole.TraineeDiploma,
            //    "DIPTrainUser2_2DOM_CECITY", null, null, "DIPTrainee", "2", "fr_CA", "2DOM");

            //UserInfo blah9 = UserUtils.CreateAndRegisterUser(UserUtils.Application.Diploma, UserUtils.UserRole.TraineeDiploma,
            //    "DIPTrainUser3_2DOM_CECITY", null, null, "DIPTrainee", "3", "fr_CA", "2DOM");


            UserInfo blah17 = UserUtils.CreateAndRegisterUser(UserUtils.Application.PER, UserUtils.UserRole.TraineePER,
                "blah-F", null, null, "PERTrainee", "3", "fr_CA", "2POM-F");



            //// PER
            UserInfo blah10 = UserUtils.CreateAndRegisterUser(UserUtils.Application.PER, UserUtils.UserRole.REF,
               "PERREFUser1_2POM-F", null, null, "PERReferee", "1", "en_US", "2POM-F");

            UserInfo blah11 = UserUtils.CreateAndRegisterUser(UserUtils.Application.PER, UserUtils.UserRole.REF,
                "PERREFUser2_2POM-F", null, null, "PERReferee", "2", "en_US", "2POM-F");

            UserInfo blah12 = UserUtils.CreateAndRegisterUser(UserUtils.Application.PER, UserUtils.UserRole.ASRPER,
                "PERASRUser1_2POM-F", null, null, "PERAssessor", "1", "en_US", "2POM-F");

            UserInfo blah13 = UserUtils.CreateAndRegisterUser(UserUtils.Application.PER, UserUtils.UserRole.ASRPER,
                "PERASRUser2_2POM-F", null, null, "PERAssessor", "2", "en_US", "2POM-F");

            UserInfo blah14 = UserUtils.CreateAndRegisterUser(UserUtils.Application.PER, UserUtils.UserRole.TraineePER,
                "PERTrainUser1_2POM-F", null, null, "PERTrainee", "1", "en_US", "2POM-F");

            UserInfo blah15 = UserUtils.CreateAndRegisterUser(UserUtils.Application.PER, UserUtils.UserRole.TraineePER,
                "PERTrainUser2_2POM-F", null, null, "PERTrainee", "2", "en_US", "2POM-F");

            UserInfo blah16 = UserUtils.CreateAndRegisterUser(UserUtils.Application.PER, UserUtils.UserRole.TraineePER,
                "PERTrainUser3_2POM-F", null, null, "PERTrainee", "3", "en_US", "2POM-F");




            //***************************************************************************************************************




            //// Users without specific details
            //// Diploma
            //UserInfo bblah = UserUtils.CreateAndRegisterUser(UserUtils.Application.Diploma, UserUtils.UserRole.CSDiploma,
            //    "DIPCSUser1", null, null, "DIPClinicalSupervisor", "1", "en_US", "1 DR PER");

            //UserInfo bblah2 = UserUtils.CreateAndRegisterUser(UserUtils.Application.Diploma, UserUtils.UserRole.DDDiploma,
            //    "DIPDDUser1", null, null, "DIPDiplomaDirector", "1", "en_US", "1 DR PER");

            //UserInfo bblah3 = UserUtils.CreateAndRegisterUser(UserUtils.Application.Diploma, UserUtils.UserRole.FOMDiploma,
            //    "DIPFOMUser1", null, null, "DIPFacultyOfMedicine", "1", "en_US", "1 DR PER");

            //UserInfo bblah4 = UserUtils.CreateAndRegisterUser(UserUtils.Application.Diploma, UserUtils.UserRole.ASRDiploma,
            //    "DIPAssUser1", null, null, "DIPAssessor", "1", "en_US", "1 DR PER");

            //UserInfo bblah5 = UserUtils.CreateAndRegisterUser(UserUtils.Application.Diploma, UserUtils.UserRole.ASRDiploma,
            //    "DIPAssUser2", null, null, "DIPAssessor", "2", "en_US", "1 DR PER");

            //UserInfo bblah6 = UserUtils.CreateAndRegisterUser(UserUtils.Application.Diploma, UserUtils.UserRole.ASRDiploma,
            //    "DIPAssUser3", null, null, "DIPAssessor", "3", "en_US", "1 DR PER");

            //UserInfo bblah7 = UserUtils.CreateAndRegisterUser(UserUtils.Application.Diploma, UserUtils.UserRole.TraineeDiploma,
            //    "DIPTrainUser1", null, null, "DIPTrainee", "1", "en_US", "1 DR PER");

            //UserInfo bblah8 = UserUtils.CreateAndRegisterUser(UserUtils.Application.Diploma, UserUtils.UserRole.TraineeDiploma,
            //    "DIPTrainUser2", null, null, "DIPTrainee", "2", "en_US", "1 DR PER");

            //UserInfo bblah9 = UserUtils.CreateAndRegisterUser(UserUtils.Application.Diploma, UserUtils.UserRole.TraineeDiploma,
            //    "DIPTrainUser3", null, null, "DIPTrainee", "3", "en_US", "1 DR PER");



            //// PER
            //UserInfo bblah10 = UserUtils.CreateAndRegisterUser(UserUtils.Application.PER, UserUtils.UserRole.REF,
            //   "PERREFUser1", null, null, "PERReferee", "1", "en_US", "1 DR PER");

            //UserInfo bblah11 = UserUtils.CreateAndRegisterUser(UserUtils.Application.PER, UserUtils.UserRole.REF,
            //    "PERREFUser2", null, null, "PERReferee", "2", "en_US", "1 DR PER");

            //UserInfo bblah12 = UserUtils.CreateAndRegisterUser(UserUtils.Application.PER, UserUtils.UserRole.ASRPER,
            //    "PERASRUser1", null, null, "PERAssessor", "1", "en_US", "1 DR PER");

            //UserInfo bblah13 = UserUtils.CreateAndRegisterUser(UserUtils.Application.PER, UserUtils.UserRole.ASRPER,
            //    "PERASRUser2", null, null, "PERAssessor", "2", "en_US", "1 DR PER");

            //UserInfo bblah14 = UserUtils.CreateAndRegisterUser(UserUtils.Application.PER, UserUtils.UserRole.TraineePER,
            //    "PERTrainUser1", null, null, "PERTrainee", "1", "en_US", "1 DR PER");

            //UserInfo bblah15 = UserUtils.CreateAndRegisterUser(UserUtils.Application.PER, UserUtils.UserRole.TraineePER,
            //    "PERTrainUser2", null, null, "PERTrainee", "2", "en_US", "1 DR PER");

            //UserInfo bblah16 = UserUtils.CreateAndRegisterUser(UserUtils.Application.PER, UserUtils.UserRole.TraineePER,
            //    "PERTrainUser3", null, null, "PERTrainee", "3", "en_US", "1 DR PER");
        }
        #endregion Tests
    }
}

