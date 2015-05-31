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
using Vfs.WebCrawler.Utility;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (WebSession.Profile.IsAuthenticated == true)
        {
            this.LoginMessageLiteral.Text = WebSession.Profile.Firstname;
        }
        else
        {
            this.LoginMessageLiteral.Text = string.Empty;
        }
    }
}
