using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_QLBieuMau : TrangKho
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            ILEN.View.Load2GV(kho.TimBieuMauTheoDV(1), lvDSBM);
        }
       
    }
    protected void btnThem_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/EditBieuMau.aspx");
    }
    protected void lbtnEditTTBM_Click(object sender, EventArgs e)
    {
        var maBM = (sender as LinkButton).CommandArgument;
        var kq = kho.TimBieuMau(int.Parse(maBM));
        if (kq != default(BieuMauDanhGia))
        {
            kq.Chon = !kq.Chon;
            kho.Luu();
        }
        Response.Redirect(@"~/Admin/QLBieuMau.aspx");
    }
    protected void lbtnEditBM_Click(object sender, EventArgs e)
    {
        var maBM = (sender as LinkButton).CommandArgument;
        Response.Redirect(@"~/Admin/EditBieuMau.aspx?id=" + maBM);
    }
}