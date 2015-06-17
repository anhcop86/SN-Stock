using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SACommon : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void lblDangXuat_Click(object sender, EventArgs e)
    {
        Session.RemoveAll();
        Response.Redirect(@"~/SuperAdmin/DangNhap.aspx");
    }
}
