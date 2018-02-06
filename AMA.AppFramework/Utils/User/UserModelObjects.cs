using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AMA.AppFramework.Utils.User
{
    public abstract class LegacyModel
    {
        public abstract byte[] ToByteArray();

        public virtual bool IsValid
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Makes sure tht the value is Html Encoded before it is sent to Legacy API. 
        /// The decoding is to make sure we prevent double encoding if the value is already encoded.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected string PreProcessString(string value)
        {
            return HttpUtility.HtmlEncode(HttpUtility.HtmlDecode(value));
        }
    }
    public class UserInfo 
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
