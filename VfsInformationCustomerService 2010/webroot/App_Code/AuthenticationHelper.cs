using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Vfs.WebCrawler.Utility
{

    public class AuthenticationHelper
    {
        public AuthenticationHelper()
        {
        }

        /* public static Profile MakeUserProfile(string email, string encryptPass)
         {
             User user = UserService.GetUserByEmailPassword(email, encryptPass);
             Profile profile = new Profile();
             profile.UserId = user.UserID;
             profile.LastName = user.LastName;
             profile.Firstname = user.FirstName;
             profile.Login = user.Login;

             profile.Username = string.Format("{0} {1}", user.FirstName, user.LastName);

             return profile;
         }*/
        /*
          public static Profile MakeUserProfile(int userId)
          {
              AuthenticationService.AuthenticationWS service = new AuthenticationService.AuthenticationWS();
              AuthenticationService.Login login = service.GetLoginById(userId);

              UserService.UserWS userService = new UserService.UserWS();
              User user = userService.GetUser(userId);

              Profile profile = new Profile();
              profile.UserId = (int)user.UserId;
              profile.LastName = user.Lastname;
              profile.Firstname = user.Firstname;
              profile.Username = login.Login;

              return profile;
          }*/
    }
}