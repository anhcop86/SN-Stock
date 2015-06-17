using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_QLVTCV : TrangKho
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.KhoiTao();
            ILEN.View.Load2LV(this.DSVTCV, lvVTCV);
        }
    }
    void KhoiTao()
    {
        spnSoLuong.InnerText = this.DSVTCV.Count.ToString();
        
    }
  
    protected void btnThem_Click(object sender, EventArgs e)
    {
        Response.Redirect(@"~/Admin/EditVTCV.aspx");
    }
}