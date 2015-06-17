using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_QLBoPhan : TrangKho
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            ILEN.View.Load2LV(kho.DanhSachBoPhan, lvDSBP);
            this.KhoiTao();
        }
    }
    void KhoiTao()
    {
        spnSoBP.InnerText = this.DSBP.Count.ToString();
        btnCreate.Text = MyUtility.TiengViet ? "Thêm bộ phận mới" : "Create New Division";
    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        Response.Redirect(@"~/Admin/EditBoPhan.aspx");
    }
}