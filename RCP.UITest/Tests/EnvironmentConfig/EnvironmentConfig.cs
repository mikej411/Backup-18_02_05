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
    public class EnvironmentConfig : TestBase
    {
        #region Constructors
        public EnvironmentConfig(string browserName) : base(browserName) { }

        // Remote Selenium Grid Test
        public EnvironmentConfig(string browserName, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests

        [Description("This method should be run on a new environment to create all the necessary users that are needed for all of the tests. A couple of manual " +
            " steps need to be taken before this method is run. 1. There must be 2 institutions, 1 named 'Premier' and the other named 'RCP'. 'Premier' is for CBD " +
            " users, 'RCP' is for PER users. 2. For PER and Diploma users, you must manually create a PER program titled TA_Program1, and another program titled " +
            " TA_DiplomaProgram1 (or whatever the ProgramCodes are called inside the UserUtils class). You should do this on the Lifetime Support site, and finalize " +
            " the program. Note that you should create 2 sections, with 2 milestones each, and each milestone should have evidence. Naming does not matter except for " +
            " optional milestones. You should create 1 optional milestone per section, and then title the optional milestone with the word 'optional' in it. 3. After " +
            " the below method is complete and the users are created, you will have to first manually login and accept the User License Agreement. You can do this " +
            " manually, or you could run the second method inside this class to do it for you." +
            " Next you must login as a program admin user in CBD and go to the Competence Commitee tab and create a random new agenda. Note that we have to do this " +
            " because I didnt add a dynamic enough wait criteria, which should wait if there is a row in the table OR if there are no agendas, maybe I have to add" +
            " that a certain button is disabled or something. Revisit that Wait criteria, and if I can figure it out, then I wont need to manually add an agenda" +
            " IMPORTANT: Only run the below method once per new environment. After it is run, comment the below test attribute so it is not run again")]
        //[Test]
        public void CreateAllStaticUsers()
        {
            //UserInfo user = UserUtils.CreateAndRegisterUser(UserUtils.Application.CBD, UserUtils.UserRole.LR);


            //// CBD
            //UserUtils.CreateAndRegisterUser(UserUtils.Application.CBD, UserUtils.UserRole.LR, UserUtils.Learner1Login);
            //UserUtils.CreateAndRegisterUser(UserUtils.Application.CBD, UserUtils.UserRole.LR, UserUtils.LearnerCH1Login);
            //UserUtils.CreateAndRegisterUser(UserUtils.Application.CBD, UserUtils.UserRole.LR, UserUtils.LearnerFF1Login);
            //UserUtils.CreateAndRegisterUser(UserUtils.Application.CBD, UserUtils.UserRole.LR, UserUtils.LearnerIE1Login);
            //UserUtils.CreateAndRegisterUser(UserUtils.Application.CBD, UserUtils.UserRole.LR, UserUtils.LearnerCH2Login);
            //UserUtils.CreateAndRegisterUser(UserUtils.Application.CBD, UserUtils.UserRole.LR, UserUtils.LearnerFF2Login);
            //UserUtils.CreateAndRegisterUser(UserUtils.Application.CBD, UserUtils.UserRole.LR, UserUtils.LearnerIE2Login);
            //UserUtils.CreateAndRegisterUser(UserUtils.Application.CBD, UserUtils.UserRole.OB, UserUtils.Observer1Login);
            //UserUtils.CreateAndRegisterUser(UserUtils.Application.CBD, UserUtils.UserRole.OB, UserUtils.Observer2Login);
            //UserUtils.CreateAndRegisterUser(UserUtils.Application.CBD, UserUtils.UserRole.PA, UserUtils.ProgAdmin1Login);
            //UserUtils.CreateAndRegisterUser(UserUtils.Application.CBD, UserUtils.UserRole.PGD, UserUtils.ProgDean1Login);
            //UserUtils.CreateAndRegisterUser(UserUtils.Application.CBD, UserUtils.UserRole.PD, UserUtils.ProgDirector1Login);
            //UserUtils.CreateAndRegisterUser(UserUtils.Application.CBD, UserUtils.UserRole.CC, UserUtils.CC1Login);

            //// PER
            //UserUtils.CreateAndRegisterUser(UserUtils.Application.PER, UserUtils.UserRole.ASRPER, UserUtils.Assessor1PERLogin);
            //UserUtils.CreateAndRegisterUser(UserUtils.Application.PER, UserUtils.UserRole.ASRPER, UserUtils.Assessor2PERLogin);
            //UserUtils.CreateAndRegisterUser(UserUtils.Application.PER, UserUtils.UserRole.ASRPER, UserUtils.Assessor3PERLogin);
            //UserUtils.CreateAndRegisterUser(UserUtils.Application.PER, UserUtils.UserRole.REF, UserUtils.Referee1PERLogin);
            //UserUtils.CreateAndRegisterUser(UserUtils.Application.PER, UserUtils.UserRole.REF, UserUtils.Referee2PERLogin);

            //// Diploma
            //UserUtils.CreateAndRegisterUser(UserUtils.Application.Diploma, UserUtils.UserRole.CSDiploma, UserUtils.ClinicalSupervisor1DiplomaLogin);
            //UserUtils.CreateAndRegisterUser(UserUtils.Application.Diploma, UserUtils.UserRole.DDDiploma, UserUtils.DiplDirector1DiplomaLogin);
            //UserUtils.CreateAndRegisterUser(UserUtils.Application.Diploma, UserUtils.UserRole.FOMDiploma, UserUtils.FacultyOfMed1DiplomaLogin);
            //UserUtils.CreateAndRegisterUser(UserUtils.Application.Diploma, UserUtils.UserRole.ASRDiploma, UserUtils.Assessor1DiplomaLogin);
            //UserUtils.CreateAndRegisterUser(UserUtils.Application.Diploma, UserUtils.UserRole.ASRDiploma, UserUtils.Assessor1DiplomaLogin);
            //UserUtils.CreateAndRegisterUser(UserUtils.Application.Diploma, UserUtils.UserRole.ASRDiploma, UserUtils.Assessor2DiplomaLogin);
            //UserUtils.CreateAndRegisterUser(UserUtils.Application.Diploma, UserUtils.UserRole.TraineeDiploma, UserUtils.Trainee1DiplomaLogin);

            // Mainport
            //UserUtils.CreateAndRegisterUser(UserUtils.Application.Mainport, UserUtils.UserRole.MP, UserUtils.MainportUser1Login);
        }


        //[Test]
        public void LoginToNewlyCreatedUsers()
        {
            //// CBD
            //LoginPage LP = Navigation.GoToLoginPage(Browser);
            //LP.LoginAsNewUser(UserUtils.UserRole.LR, UserUtils.Learner1Login, ConfigurationManager.AppSettings["LoginPassword"]);
            //LP.Logout();

            //Navigation.GoToLoginPage(Browser);
            //LP.LoginAsNewUser(UserUtils.UserRole.LR, UserUtils.LearnerCH1Login, ConfigurationManager.AppSettings["LoginPassword"]);
            //LP.Logout();

            //Navigation.GoToLoginPage(Browser);
            //LP.LoginAsNewUser(UserUtils.UserRole.LR, UserUtils.LearnerFF1Login, ConfigurationManager.AppSettings["LoginPassword"]);
            //LP.Logout();

            //Navigation.GoToLoginPage(Browser);
            //LP.LoginAsNewUser(UserUtils.UserRole.LR, UserUtils.LearnerIE1Login, ConfigurationManager.AppSettings["LoginPassword"]);
            //LP.Logout();

            //Navigation.GoToLoginPage(Browser);
            //LP.LoginAsNewUser(UserUtils.UserRole.LR, UserUtils.LearnerCH2Login, ConfigurationManager.AppSettings["LoginPassword"]);
            //LP.Logout();

            //Navigation.GoToLoginPage(Browser);
            //LP.LoginAsNewUser(UserUtils.UserRole.LR, UserUtils.LearnerFF2Login, ConfigurationManager.AppSettings["LoginPassword"]);
            //LP.Logout();

            //Navigation.GoToLoginPage(Browser);
            //LP.LoginAsNewUser(UserUtils.UserRole.LR, UserUtils.LearnerIE2Login, ConfigurationManager.AppSettings["LoginPassword"]);
            //LP.Logout();

            //Navigation.GoToLoginPage(Browser);
            //LP.LoginAsExistingUser(UserUtils.UserRole.OB, UserUtils.Observer1Login, ConfigurationManager.AppSettings["LoginPassword"]);
            //LP.Logout();

            //Navigation.GoToLoginPage(Browser);
            //LP.LoginAsExistingUser(UserUtils.UserRole.OB, UserUtils.Observer2Login, ConfigurationManager.AppSettings["LoginPassword"]);
            //LP.Logout();

            //Navigation.GoToLoginPage(Browser);
            //LP.LoginAsExistingUser(UserUtils.UserRole.PA, UserUtils.ProgAdmin1Login, ConfigurationManager.AppSettings["LoginPassword"]);
            //LP.Logout();

            //Navigation.GoToLoginPage(Browser);
            //LP.LoginAsExistingUser(UserUtils.UserRole.PGD, UserUtils.ProgDean1Login, ConfigurationManager.AppSettings["LoginPassword"]);
            //LP.Logout();

            //Navigation.GoToLoginPage(Browser);
            //LP.LoginAsExistingUser(UserUtils.UserRole.PD, UserUtils.ProgDirector1Login, ConfigurationManager.AppSettings["LoginPassword"]);
            //LP.Logout();

            ////Navigation.GoToLoginPage(Browser);
            ////LP.LoginAsExistingUser(UserUtils.UserRole.CC, UserUtils.CC1Login, ConfigurationManager.AppSettings["LoginPassword"]);
            ////LP.Logout();

            //// Mainport
            //Navigation.GoToLoginPage(Browser);
            //LP.LoginAsNewUser(UserUtils.UserRole.MP, UserUtils.MainportUser1Login, ConfigurationManager.AppSettings["LoginPassword"]);
            //LP.Logout();

            //// PER
            //Navigation.GoToLoginPage(Browser);
            //LP.LoginAsExistingUser(UserUtils.UserRole.ASRPER, UserUtils.Assessor1PERLogin, ConfigurationManager.AppSettings["LoginPassword"]);
            //LP.Logout();

            //Navigation.GoToLoginPage(Browser);
            //LP.LoginAsExistingUser(UserUtils.UserRole.ASRPER, UserUtils.Assessor2PERLogin, ConfigurationManager.AppSettings["LoginPassword"]);
            //LP.Logout();

            //Navigation.GoToLoginPage(Browser);
            //LP.LoginAsExistingUser(UserUtils.UserRole.ASRPER, UserUtils.Assessor3PERLogin, ConfigurationManager.AppSettings["LoginPassword"]);
            //LP.Logout();

            //Navigation.GoToLoginPage(Browser);
            //LP.LoginAsExistingUser(UserUtils.UserRole.REF, UserUtils.Referee1PERLogin, ConfigurationManager.AppSettings["LoginPassword"]);
            //LP.Logout();

            //Navigation.GoToLoginPage(Browser);
            //LP.LoginAsExistingUser(UserUtils.UserRole.REF, UserUtils.Referee2PERLogin, ConfigurationManager.AppSettings["LoginPassword"]);
            //LP.Logout();

            //// Diploma
            //Navigation.GoToLoginPage(Browser);
            //LP.LoginAsExistingUser(UserUtils.UserRole.CSDiploma, UserUtils.ClinicalSupervisor1DiplomaLogin, ConfigurationManager.AppSettings["LoginPassword"]);
            //LP.Logout();

            //Navigation.GoToLoginPage(Browser);
            //LP.LoginAsExistingUser(UserUtils.UserRole.DDDiploma, UserUtils.DiplDirector1DiplomaLogin, ConfigurationManager.AppSettings["LoginPassword"]);
            //LP.Logout();

            //Navigation.GoToLoginPage(Browser);
            //LP.LoginAsExistingUser(UserUtils.UserRole.FOMDiploma, UserUtils.FacultyOfMed1DiplomaLogin, ConfigurationManager.AppSettings["LoginPassword"]);
            //LP.Logout();

            //Navigation.GoToLoginPage(Browser);
            //LP.LoginAsExistingUser(UserUtils.UserRole.ASRDiploma, UserUtils.Assessor1DiplomaLogin, ConfigurationManager.AppSettings["LoginPassword"]);
            //LP.Logout();

            //Navigation.GoToLoginPage(Browser);
            //LP.LoginAsExistingUser(UserUtils.UserRole.ASRDiploma, UserUtils.Assessor2DiplomaLogin, ConfigurationManager.AppSettings["LoginPassword"]);
            //LP.Logout();

            //Navigation.GoToLoginPage(Browser);
            //LP.LoginAsExistingUser(UserUtils.UserRole.ASRDiploma, UserUtils.Assessor3DiplomaLogin, ConfigurationManager.AppSettings["LoginPassword"]);
            //LP.Logout();

            //Navigation.GoToLoginPage(Browser);
            //LP.LoginAsExistingUser(UserUtils.UserRole.TraineeDiploma, UserUtils.Trainee1DiplomaLogin, ConfigurationManager.AppSettings["LoginPassword"]);
            //LP.Logout();
        }

      
        #endregion Tests
    }
}

