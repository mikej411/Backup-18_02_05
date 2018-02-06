using Browser.Core.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CFPC.AppFramework
{
    public static class UserUtils
    {
        #region properties

        static string baseAPIUrl = System.Configuration.ConfigurationManager.AppSettings["APIUrl"].ToString();
        static string token = System.Configuration.ConfigurationManager.AppSettings["Token"].ToString();

        /// <summary>
        /// Specifies which application the user will be registered to/>
        /// </summary>
        /// <see cref="UserUtils.CreateUser(null, string, string, string)"/>
        public enum Application
        {
            Mainpro,
            Cert
        }

      
    

        #endregion properties

        #region methods

        public static List<UserInfo> CreateUsers(string charactersBeforeRandomName)
        {
            List<UserInfo> users = new List<UserInfo>();
            users.Add(CreateUser(charactersBeforeRandomName));

            return users;
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <returns></returns>
        public static UserInfo CreateUser(String nameToken)
        {

            //generate the user string the date and time
            //Create Strings of User Data
            DateTime dt = DateTime.Now;
            int currentDay = dt.Day;
            int currentMonth = dt.Month;
            int currentYear = dt.Year;
            int currentHour = dt.Hour;
            int currentMinute = dt.Minute;
            int currentSecond = dt.Second;

            

            string resp = string.Empty;

            UserInfo newUserModel = BuildUserModel(nameToken);

            using (var wc = new WebClient())
            {

                String fullAPIUrl = String.Format("{0}api/user", baseAPIUrl);
            
                wc.Headers.Add("Content-Type", "application/json");
                wc.Headers.Add("Token", token);
              
                
                string body = JsonConvert.SerializeObject(newUserModel);

                //modify the url so that it works with CFPC
                fullAPIUrl = fullAPIUrl.Substring(0, 55) + "/user";
                //  Console.WriteLine(fullAPIUrl);
                int x = 0;
                resp = wc.UploadString(fullAPIUrl, body);
            }

            dynamic data = JObject.Parse(resp);

           RegisterUser(data.Id.ToString(), newUserModel.Username);

            return newUserModel;
        }

        /// <summary>
        /// Builds the user information that then gets plugged into the API request's body
        /// </summary>
        /// <returns></returns>
        private static UserInfo BuildUserModel(String nameToken)
        {
            //generate the user string the date and time
            //Create Strings of User Data
            DateTime dt = DateTime.Now;
            int currentDay = dt.Day;
            int currentMonth = dt.Month;
            int currentYear = dt.Year;
            int currentHour = dt.Hour;
            int currentMinute = dt.Minute;
            int currentSecond = dt.Second;

            //generate a random key so that when the tests run in parrallel
            //these new users will not be the same
            Random rnd = new Random();
            String uniqueKey =  rnd.Next(1, 10000000) + "";
            String userName = nameToken + "-" + currentMonth + "-" + currentDay + "-" + currentYear + "-" + currentHour + "-" + currentMinute + "-" + currentSecond + "-ID:" + uniqueKey;


            UserInfo newUserModel = new UserInfo();
            newUserModel.Address = "121 Lake Dr";
            newUserModel.Address2 = "Suite 100";
            newUserModel.City = "pittsburgh";
            newUserModel.CountryCode = "US";
            newUserModel.Degree = "MD";
            newUserModel.EmailAddress = "Daniel_Nestor@premierinc.com";
            newUserModel.FirstName = userName;
            newUserModel.LastName = userName;
            newUserModel.PostalCode = "12345";
            newUserModel.State = "AL";
            newUserModel.Username = userName;
     

            newUserModel.Fields = new Field[]
            {   new Field() { Name = "CFPC_Phone1", Value = "123-456-7890" },
                new Field() { Name = "CFPC_Category", Value = "ABC" },
                new Field() { Name = "CFPC_Enrollment_Date", Value = "1/30/2015" },
                new Field() { Name = "Phone_Number", Value = "123-456-7890" },
                new Field() { Name = "CFPC_Phone2", Value = "111-111-1111" },
                new Field() { Name = "CFPC_Salutation", Value = "Dr" },
                new Field() { Name = "CFPC_Sex", Value = "Male" },
                new Field() { Name = "CFPC_Mail_Language", Value = "E" },
                new Field() { Name = "CFPC_Province", Value = "Ontario" },
                new Field() { Name = "CFPC_Province_Description", Value = "Ontario" },
                new Field() { Name = "CFPC_Country_Code", Value = "USA" },
                new Field() { Name = "CFPC_Country_Dscription", Value = "USA" },
                new Field() { Name = "CFPC_Location_Code", Value = "ABC" },
                new Field() { Name = "CFPC_Name_Reference", Value = "XYZ" },
                new Field() { Name = "CFPC_Birthdate", Value = "1986-03-08" },
                new Field() { Name = "CFPC_Chapter", Value = "USA" },
                new Field() { Name = "CFPC_Chapter_Name", Value = "ABC" },
                new Field() { Name = "CFPC_Discontinued_Flag", Value = "0" },
                new Field() { Name = "CFPC_Discontinued_Reason", Value = "ABC" },
                new Field() { Name = "CFPC_Discontinued_Reason_Description", Value = "XYZ" },
                new Field() { Name = "CFPC_Discontinued_Date", Value = "1990-10-21" },
                new Field() { Name = "CFPC_Reinstatement_Date", Value = "1989-01-01" },
                new Field() { Name = "US_or_Foreign_Flag", Value = "1" },
                new Field() { Name = "CFPC_Licence_Number", Value = "1987110" },
                new Field() { Name = "CFPC_Subcategory", Value = "AB|CD|EF" },
                new Field() { Name = "Profession", Value = "PHY" },
                new Field() { Name = "PARTICIPANT_ID", Value = userName }};

            return newUserModel;
        }

        /// <summary>
        /// Assigns the tester-specfified role to a user
        /// </summary>
        /// <param name="userGUID">The GUID of the user you want to assign the role to</param>
        /// <param name="userName"></param>
        private static void RegisterUser(string userGUID, string userName)
        {
            string resp = string.Empty;


                    using (var wc = new WebClient())
                    {

                        String fullAPIUrl = String.Format("{0}/api/Activity/841ECFA4-4D5F-4A1F-9840-5E364E4B86AB/user", baseAPIUrl);
                        wc.Headers.Add("Content-Type", "application/json");
                        wc.Headers.Add("Token", token);

                         fullAPIUrl = fullAPIUrl.Substring(0, 55) + "";
                         fullAPIUrl = fullAPIUrl + "/activity/841ECFA4-4D5F-4A1F-9840-5E364E4B86AB/user";
                         int x = 0; 

                         resp = wc.UploadString(fullAPIUrl, JsonConvert.SerializeObject(new
                        {
                            UserIdentifier = userGUID,
                            MembershipType = "Individual",
                            MembershipRole = "User",
                            StartDate = "2017-02-01",
                            Category = "A"
                        }));
                    }
            }


        public static void DeleteUser(string userName)
        {
            string resp = string.Empty;
            DeleteUser user = new DeleteUser();
            user.UserName = userName;
            user.ActivityGUID = "AF78C6CF-8EA7-4B12-B399-B91D99649F5F";

            using (var wc = new WebClient())
            {
                String url = String.Format("{0}/api/apps/cbd/integration/delete", baseAPIUrl, userName);
                wc.Headers.Add("Content-Type", "application/json");
                wc.Headers.Add("Token", token);
                resp = wc.UploadString(url, JsonConvert.SerializeObject(user));
            }
        }


      

        #endregion methods
    }
}

