using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMA.AppFramework.Utils.User
{
   public class UserUtils
    {
       

        /// <summary>
        /// Specifies type of role to assign to the user"/>
        /// </summary>
        /// <see cref="UserUtils.CreateUser(null, string, string, string,string)"/>
        public enum UserRole
        {
            Ama_Staff,
            Admin,
            Manager,
            Resident
        }
        /// <summary>
        /// Builds the user information that then gets plugged into the API request's body
        /// </summary>
        /// <param name="charactersBeforeRandomName">If needed, you can include any character(s) to place before the username. If not needed, leave this null</param>
        /// <param name="role"><see cref="UserRole.UserRole"/></param>
        /// <returns></returns>
        public static UserInfo GetUser( UserRole role)
        {
            string userName = string.Empty;

            switch (role)
            {
                case UserRole.Ama_Staff:
                    userName = "10031315";
                    break;
                case UserRole.Admin:
                    userName = "10031314";
                    break;
                case UserRole.Manager:
                    userName = "10020462";//10031032
                    break;
                case UserRole.Resident:
                    userName = "10031059";
                    break;
            }
            UserInfo newUserModel = new UserInfo();
            newUserModel.Username = userName;
            newUserModel.Password = "password";
            return newUserModel;
        }
    }
}
