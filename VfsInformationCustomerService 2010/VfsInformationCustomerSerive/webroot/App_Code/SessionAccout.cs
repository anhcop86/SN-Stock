using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for SessionCreateAccount
/// </summary>
/// 

namespace Vfs.WebCrawler.Utility
{
    public class SessionCreateAccount
    {
        private long userId;
        private string email;
        private string login;
        private string password;

        public SessionCreateAccount() { }

        public long UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Login
        {
            get { return login; }
            set { login = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
    }
}