using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SuperAdmin_DangNhap : TrangKho
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        SuperAdmin sa = kho.TimSuperAdmin(txtUsername.Text);
        if (sa == default(SuperAdmin))
        {
            lblThongBao.Text = ThongBao.SaiTenDangNhap;
            return;
        }
        if (!sa.MatKhau.Equals(txtPassword.Text.ToMD5()))
        {
            lblThongBao.Text = ThongBao.SaiTenMatKhau;
            return;
        }
        Session["sa"] = sa;
        Response.Redirect(@"~/SuperAdmin/QLDonVi.aspx");
    }
}