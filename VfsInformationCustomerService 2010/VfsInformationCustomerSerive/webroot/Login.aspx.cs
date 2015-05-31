using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using VfsCustomerService.Business;
using VfsCustomerService.Data;
using VfsCustomerService.Entities;
using VfsCustomerService.Utility;
using Vfs.WebCrawler.Utility;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.SetInferace();
    }

    protected void SetInferace()
    {
        this.loginButton.Text = Resources.UIResource.Login;
    }

    protected void loginButton_Click(object sender, EventArgs e)
    {     
        if (IsValid)
        {
            string encryptPass;

            ActiveDirectoryResourceService activeDirectoryResourceService = new ActiveDirectoryResourceService();
            User user = null;
            user = activeDirectoryResourceService.AuthenticateUser(this.userNameTextBox.Text, this.passwordTextBox.Text);

            //if (FormsAuthentication.Authenticate(this.userNameTextBox.Text, this.passwordTextBox.Text) == false)
            //{
            //    loginMessage.Text = Resources.UIResource.ErrorLoginMessage;
            //    return;
            //}

            if (user != null)
            {
                
                WebSession.Profile = AuthenticationHelper.MakeUserProfile(this.userNameTextBox.Text);         
                
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                1, // Ticket version
                this.userNameTextBox.Text, // Username to be associated with this ticket
                DateTime.Now, // Date/time issued
                DateTime.Now.AddHours(7), // Date/time to expire
                false, // "true" for a persistent user cookie (could be a checkbox on form)
                "WebSurfer", // User-data (the roles from this user record in our database)
                FormsAuthentication.FormsCookiePath); // Path cookie is valid for

                // Hash the cookie for transport over the wire
                string hash = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(
                FormsAuthentication.FormsCookieName, // Name of auth cookie (it's the name specified in web.config)
                 hash); // Hashed ticket

                // Add the cookie to the list for outbound response
                Response.Cookies.Add(cookie);                

                String returnUrl = Request.QueryString["ReturnUrl"];
                if ((returnUrl == null)) returnUrl = (FormsAuthentication.DefaultUrl);
                if (returnUrl == null) returnUrl = ResolveClientUrl(Request.AppRelativeCurrentExecutionFilePath);
                //Response.Redirect(returnUrl);               
                
                FormsAuthentication.RedirectFromLoginPage(this.userNameTextBox.Text, false);
            }
            else
            {
                loginMessage.Text = Resources.UIResource.ErrorLoginMessage;
            }
        }
    }

    
}
