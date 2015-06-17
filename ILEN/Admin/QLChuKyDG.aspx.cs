using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_QLChuKyDG : TrangKho
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            ILEN.View.Load2LV(kho.LayDanhSachChuKyDanhGiaTheoDV(this.MaDV), lvDSCK);
            this.KhoiTao();
        }
    }
    void KhoiTao()
    {
        spnTongSo.InnerText = kho.LayDanhSachChuKyDanhGiaTheoDV(this.MaDV).Count.ToString();
    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        Response.Redirect(@"~/Admin/EditChuKyDG.aspx");
    }

}