using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_QLDonVi : TrangKho
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
        }
    }
    protected void dtpLV_PreRender(object sender, EventArgs e)
    {
        ILEN.View.Load2LV(kho.DanhSachDonVi, lvDSDV);
        spnTong.InnerText = kho.DanhSachDonVi.Count.ToString();
    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        Response.Redirect(@"~/SuperAdmin/EditDonVi.aspx");
    }
   
}
