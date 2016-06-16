using ConSolHWeb.Data.Models;
using System.Collections;
using System.Web;
using System.Web.Security;

namespace ConSolHWeb.Data
{
    public class AuthenticationModule
    {
        private const int AUTHENTICATION_TIMEOUT = 20;
        //private static readonly ILog log = LogManager.GetLogger(typeof(AuthenticationModule));

        /// <summary>
        /// Try to authenticate the user.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int AuthenticateUser(string username, string password, bool persistLogin)
        {
            //Attempt to Validate User Credentials using UsersDB
            User userId = DataService.Provider.Login(username, PortalSecurity.Encrypt(password));

            if (userId != null)
            {
                if (userId.IsActive == 1)
                {
                    string FullName = userId.FirstName + " " + userId.LastName + " ( " + userId.Title + " )";
                    HttpContext.Current.Session["Name"] = FullName;
                    FormsAuthentication.SetAuthCookie(username, persistLogin);
                    Util.GetPermUserDetail(username);
                    int UserRecID = Util.GetUserID(username);
                    HttpContext.Current.Session["UserName"] = username;
                    string roleName = "";
                    roleName = DataService.Provider.GetUserRoleByUserId(username);
                    HttpContext.Current.Session["UserRole"] = roleName;
                    HttpContext.Current.Session["UserRecID"] = UserRecID;
                    Hashtable htable = Util.GetConfig();
                    HttpContext.Current.Session["CONFIG"] = htable;
                    FullName = null;
                    return userId.UserId;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return 0;
            }
        }
    }
}