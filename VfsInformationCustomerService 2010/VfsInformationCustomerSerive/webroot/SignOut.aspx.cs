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

public partial class SignOut : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        WebSession.Reset();
        FormsAuthentication.SignOut();
        Response.Redirect("Login.aspx");

    }
}
